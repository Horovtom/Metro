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
    C = 5,
    None = -1
}

public class Game {
    public int Width { get; }

    public int Height { get; }

    //Move this to Stack
    private Tile[,] board;
    private ScheduleRepository scheduleRepository;

    private Players players;

    public int NumOfPlayers { get; }

    private TilesStack stack;

    public Tile TileInHand { get; protected set; }

    public Game(string tilesConfig, string scheduleConfig, int numOfPlayers) {
        Width = 8;
        Height = 8;

        board = new Tile[Width, Height];
        TileRepository tileRepository = new TileRepository(tilesConfig);
        stack = new TilesStack(tileRepository);
        LoadScheduleConfig(scheduleConfig);
        this.NumOfPlayers = numOfPlayers;

        this.players = new Players(numOfPlayers);


        //TODO: COMPLETE GAME PREPARATION
    }

    public Tile GetTileAt(int x, int y) {
        if (!IsFieldWithTile(x, y)) {
            Debug.LogError("GetTileAt - out of bounds: " + x + ", " + y);
            return null;
        }
        return board[x, y];
    }

    private void LoadScheduleConfig(string scheduleConfig) {
        scheduleRepository = new ScheduleRepository(scheduleConfig);
    }

    /// <summary>
    /// Determines, whether a specified position is regular spot for a tile on the game board
    /// </summary>
    public bool IsFieldWithTile(int x, int y) {
        return x >= 0 && x < Width && y >= 0 && y < Height && ((x != 3 && x != 4) || (y != 3 && y != 4));
    }

    public void ClickedOnTile(Vector2 pos) {
        
        Debug.Log("Tile had coords: " + pos.x + " " + pos.y);
    }

    void SetUpStations() {
        for (int i = 0; i < NumOfPlayers; i++) {
            Schedule s = scheduleRepository.GetSchedule((PlayerColor)i, NumOfPlayers);
            foreach (int st in s.GetStations()) {
                StationsController.Instance.SetStationColor(st, (PlayerColor)i);
            }
        }
    }

    public PlayerColor GetPlayerAtStation(int stationNumber) {
        throw new NotImplementedException();
    }

    public int[] GetPlayersScores() {
        throw new NotImplementedException();
    }

    public void DisplayStations() {
        SetUpStations();
    }
}
