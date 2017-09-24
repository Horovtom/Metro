using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Game {
    public int Width { get; }
    public int Height { get; }
    private Tile[,] tiles;
    private string configFile;

    public Game(string configFilePath) {
        Width = 8;
        Height = 8;

        tiles = new Tile[Width, Height];

        this.configFile = configFilePath;
        InitializeTiles();

        //TODO: Implement
        //throw new System.NotImplementedException();
    }

    public Tile GetTileAt(int x, int y) {
        if (!IsFieldWithTile(x, y)) {
            Debug.LogError("GetTileAt - out of bounds: " + x + ", " + y);
            return null;
        }
        return tiles[x, y];
    }

    public bool IsFieldWithTile(int x, int y) {
        return x >= 0 && x < Width && y>=0 && y < Height && ((x != 3 && x != 4) || (y != 3 && y != 4));
    }

    void InitializeTiles() {
        for (int x = 0; x < Width; x++) {
            for (int y = 0; y < Height; y++) {
                tiles[x, y] = new Tile(x, y, configFile);
            }
        }
    }
}
