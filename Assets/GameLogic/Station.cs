using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station {
    public int Index { get; }

    public Vector2 TileCoordinates
    {
        get
        {
            Vector2 returning = new Vector2();
            if (Index <= 8) {
                returning.y = 8;
                returning.x = 8 - Index;
            } else if (Index <= 16) {
                returning.x = -1;
                returning.y = 16 - Index;
            } else if (Index <= 24) {
                returning.y = -1;
                returning.x = Index - 17;
            } else {
                returning.x = 8;
                returning.y = Index - 25;
            }
            return returning;
        }
    }

    public PlayerColor Color { get; }

    public bool TrackComplete
    {
        get { return TrackComplete; }
        set
        {
            if (TrackComplete)
                return;
            else if (value == true) {
                TrackComplete = true;
                StationsController.Instance.SetStationColor(Index, PlayerColor.None);
            } else
                TrackComplete = false;
        }
    }

    public Station(int index, PlayerColor color) {
        Color = color;
        Index = index;
        TrackComplete = false;
        StationsController.Instance.SetStationColor(Index, Color);
    }

    public Station clone() {
        Station other = new Station(Index, Color);
        other.TrackComplete = TrackComplete;
        return other;
    }
}
