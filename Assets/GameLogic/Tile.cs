using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum Direction { N = 0, E = 1, S = 2, W = 3 }
public class Tile {
    public int X { get; }
    public int Y { get; }
    private readonly TileRepository repository;
    private int type;
    private Dictionary<Direction, Direction> connections;
    private Action<Tile> callbacksTypeChanged;

    public int Type
    { 
        get { return type; }
        set
        { 
            if (value == -1) 
                Debug.Log("Clearing type of tile: [" + X + ", " + Y + "]");
            else 
                Debug.Log("Tile - Changing type of [" + X + ", " + Y + "] to: " + value); 
            
            SetType(value);
        }
    }

    private void SetType(int value) {
        if (value == -1)
            connections = null;
        else {
            //TODO:IMPLEMENT
            //throw new System.NotImplementedException();
        }

        type = value;

        if (callbacksTypeChanged != null) {
            callbacksTypeChanged(this);
        }
    }

    public Tile(int x, int y, TileRepository repository) {
        X = x;
        Y = y;
        this.repository = repository;

        Type = 1;
    }

    public void RegisterTypeChangedCallback(Action<Tile> callback) {
        callbacksTypeChanged += callback;
        callback(this);
    }
}
