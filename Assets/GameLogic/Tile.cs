using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum Direction {
    N = 0,
    E = 1,
    S = 2,
    W = 3

}

public class Tile {
    private Vector2 Position
    {
        get ;
        set ;
    }

    public int Type { get; }

    private Dictionary<Direction, Direction> connections;

    public bool Placed { get { return Position.x >= 0 && Position.y >= 0; } }

    public Tile(int type, Dictionary<Direction, Direction> connections) {
        this.Type = type;
        this.connections = connections;
    }

    public Direction GoThrough(Direction d) {
        return connections[d];
    }
}
