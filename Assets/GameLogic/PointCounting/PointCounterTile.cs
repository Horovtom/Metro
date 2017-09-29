using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public struct ReturnPath {
    public bool EndsWithStation;
    public int PathLength;
    public PlayerColor stationColor;
    public int stationNumber;
}

public class PointCounterTile {
    public int X { get; }

    public int Y { get; }

    private PointCounter parent;

    /// <summary>
    /// Map on exits from this tile
    /// </summary>
    private Dictionary<Direction, PointCounterTile> neighbors;

    private Tile tile;

    public PointCounterTile(Tile t, PointCounterTile[] neighbors, int x, int y, PointCounter parent) {
        X = x;
        Y = y;
        this.parent = parent;
        if (neighbors.Length != 4) {
            Debug.LogError("Wrong usage of PointCounterTile, neighbours argument has to have 4 fields!");
        }
        this.neighbors = new Dictionary<Direction, PointCounterTile>();

        foreach (Direction d in Enum.GetValues(typeof(Direction))) {
            if (neighbors[(int)d] != null) {
                this.neighbors[d] = neighbors[(int)d];
                this.neighbors[d].SetNeighbor(this, Utilities.oppositeDirection(d));
            }
        }

        this.tile = t;
    }

    public PointCounterTile(Tile t, Dictionary<Direction,PointCounterTile> d, int x, int y, PointCounter parent) {
        X = x; 
        Y = y;
        this.parent = parent;
        this.neighbors = new Dictionary<Direction, PointCounterTile>(d);    
        foreach (Direction dir in Enum.GetValues(typeof(Direction))) {
            if (neighbors[dir] != null) {
                neighbors[dir].SetNeighbor(this, Utilities.oppositeDirection(dir));
            }
        }
        this.tile = t;
    }

    /// <summary>
    /// Sets the neighbour on the direction specified by the argument
    /// </summary>
    void SetNeighbor(PointCounterTile pointCounterTile, Direction direction) {
        neighbors[direction] = pointCounterTile;
    }

    /// <summary>
    /// Calculates path length incoming to this tile from direction d.
    /// </summary>
    /// <param name="d">Incoming direction of a path</param>
    public int getPathLengthIncomingFrom(Direction d) {
        Direction outgoing = tile.GoThrough(d);
        if (neighbors.ContainsKey(outgoing) && neighbors[outgoing] != null) {
            return 1 + neighbors[outgoing].getPathLengthIncomingFrom(Utilities.oppositeDirection(outgoing));
        } else {
            return 1;
        }
    }

    /// <summary>
    /// Calcualtes path length outgoing from this tile to direction d.
    /// </summary>
    /// <param name="d">Outhoing direction of a path</param>
    public int getPathLengthOutgoingTo(Direction d) {
        if (neighbors.ContainsKey(d) && neighbors[d] != null)
            return 1 + neighbors[d].getPathLengthIncomingFrom(Utilities.oppositeDirection(d));
        else
            return 0;
    }

    /// <summary>
    /// Goes from this Tile in specified direction, measuring outgoing rail
    /// </summary>
    public ReturnPath getRailOutTo(Direction d) {
        ReturnPath p = new ReturnPath();
            
        if (neighbors.ContainsKey(d) && neighbors[d] != null) {
            ReturnPath r = neighbors[d].getRailOutFrom(Utilities.oppositeDirection(d));
            p.EndsWithStation = r.EndsWithStation;
            p.PathLength = r.PathLength + 1;
            p.stationColor = r.stationColor;
            p.stationNumber = r.stationNumber;

            return p;
        } else {
            //Ends with station?
            int ews = EndsWithStation(d);
            if (ews == -1) {
                //No station
                p.EndsWithStation = false;
                p.PathLength = 1;
            } else if (ews == 0) {
                //Middle station
                p.EndsWithStation = true;
                p.PathLength = 1;
                p.stationColor = PlayerColor.None;
                p.stationNumber = 0;
            } else {
                //Normal station
                p.EndsWithStation = true;
                p.PathLength = 1;
                p.stationColor = parent.GetStationColor(ews);
                p.stationNumber = ews;
            }
        }
        return p;
    }

    /// <summary>
    /// Is a station in specified direction on this tile?
    /// </summary>
    /// <returns>Number of the station, 0 if it is one of the middle stations, -1 if there is no station </returns>
    public int EndsWithStation(Direction d) {
        switch (d) {
            case Direction.N:
                if (Y == 7)
                    return 8 - X;
                if ((X == 3 || X == 4) && (Y == 2))
                    return 0;
                break;
            case Direction.E:
                if (X == 7)
                    return Y + 25;
                if ((X == 2) && (Y == 3 || Y == 4))
                    return 0;
                break;
            case Direction.S:
                if (Y == 0)
                    return 17 + X;
                if ((X == 3 || X == 4) && (Y == 5))
                    return 0;
                break;
            case Direction.W:
                if (X == 0)
                    return 16 - Y;
                if ((X == 5) && (Y == 3 || Y == 4))
                    return 0;
                break;
        }
        return -1;
    }

    /// <summary>
    /// Goes to this Tile from specified direction, measuring outgoing rail
    /// </summary>
    public ReturnPath getRailOutFrom(Direction d) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Goes from this Tile in specified direction, measuring incoming rail
    /// </summary>
    /// 
    public ReturnPath getRailInTo(Direction d) {
        throw new NotImplementedException();

    }

    /// <summary>
    /// Goes to this Tile from specified direction, measuring incoming rail
    /// </summary>
    public ReturnPath getRailInFrom(Direction d) {
        throw new NotImplementedException();

    }

    public void UnlinkNeighbor(Direction d) {
        PointCounterTile pct = neighbors[d];
        pct.UnlinkNeighbor(Utilities.oppositeDirection(d));
        neighbors.Remove(d);
    }

    public void UnlinkNeighbor(PointCounterTile t) {
        foreach (Direction d in Enum.GetValues(typeof(Direction))) {
            if (neighbors[d].Equals(t)) {
                neighbors.Remove(d);
                t.UnlinkNeighbor(this);
            }
        }
    }

}
