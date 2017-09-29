using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PointCounter {
    private PointCounterTile[,] tiles;
    private Station[] stations;

    public PointCounter(Tile[,] tiles, Station[] stations) {
        this.tiles = new PointCounterTile[tiles.Length, tiles.Length];

        for (int y = 0; y < tiles.Length; y++) {
            for (int x = 0; x < tiles.Length; x++) {
                PointCounterTile[] neighbours = new PointCounterTile[4];
                if (x > 0) {
                    neighbours[(int)Direction.N] = this.tiles[x - 1, y];
                }
                if (y > 0) {
                    neighbours[(int)Direction.W] = this.tiles[x, y - 1];
                }
                if (x < tiles.Length - 1) {
                    neighbours[(int)Direction.E] = this.tiles[x + 1, y];
                }
                if (y < tiles.Length - 1) {
                    neighbours[(int)Direction.S] = this.tiles[x, y + 1];
                }
                this.tiles[x, y] = new PointCounterTile(tiles[x, y], neighbours, x, y, this);
            }
        }

        this.stations = new Station[stations.Length];
        for (int i = 0; i < stations.Length; i++) {
            this.stations[i] = stations[i].clone();
        }
    }

    public bool isValidToPlace(int x, int y, Tile t) {
        throw new NotImplementedException();
        if (tiles[x, y] != null)
            return false;
        Place(x, y, t);

    }

    public void Place(int x, int y, Tile t) {
        Dictionary<Direction, PointCounterTile> neighbors = new Dictionary<Direction, PointCounterTile>(4);
        if (x > 0) {
            neighbors[Direction.N] = tiles[x - 1, y];
        }
        if (y > 0) {
            neighbors[Direction.W] = tiles[x, y - 1];
        }
        if (x < tiles.Length - 1) {
            neighbors[Direction.E] = tiles[x + 1, y];
        }
        if (y < tiles.Length - 1) {
            neighbors[Direction.W] = tiles[x, y + 1];
        }
        tiles[x, y] = new PointCounterTile(t, neighbors, x, y, this);
    }

    public PlayerColor GetStationColor(int ews) {
        return stations[ews].Color;
    }
}
