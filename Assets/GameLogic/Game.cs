using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum PlayerColor {
    /// <summary>
    /// Yellow
    /// </summary>
    Y = 0,
    /// <summary>
    /// Blue
    /// </summary>
    B = 1,
    /// <summary>
    /// Orange
    /// </summary>
    O = 2,
    /// <summary>
    /// Green
    /// </summary>
    G = 3,
    /// <summary>
    /// Red
    /// </summary>
    R = 4,
    /// <summary>
    /// Cyan
    /// </summary>
    C = 5
}

public class Game {
    public int Width { get; }

    public int Height { get; }

    private Tile[,] tiles;
    private ScheduleRepository repository;

    public Game(string tilesConfig, string scheduleConfig) {
        Width = 8;
        Height = 8;

        tiles = new Tile[Width, Height];
        InitializeTiles(tilesConfig);
        LoadScheduleConfig(scheduleConfig);

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

    private void LoadScheduleConfig(string scheduleConfig) {
        repository = new ScheduleRepository(scheduleConfig);
    }

    public bool IsFieldWithTile(int x, int y) {
        return x >= 0 && x < Width && y >= 0 && y < Height && ((x != 3 && x != 4) || (y != 3 && y != 4));
    }

    void InitializeTiles(string tilesConfig) {
        TileRepository tileRepository = new TileRepository(tilesConfig);
        for (int x = 0; x < Width; x++) {
            for (int y = 0; y < Height; y++) {
                tiles[x, y] = new Tile(x, y, tileRepository);
            }
        }
    }

    private void GetScheduleOfPlayer(PlayerColor color) {
        
    }
}
