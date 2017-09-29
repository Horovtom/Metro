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
    /// Violet
    /// </summary>
    V = 5,
    None = -1
}

public class Game {
    public int Width { get; }

    public int Height { get; }

    private bool changedTile = false;

    //Move this to Stack
    private Tile[,] board;
    private ScheduleRepository scheduleRepository;

    private Players players;

    public int NumOfPlayers { get; }

    private TilesStack stack;

    public Tile TileInHand { get; protected set; }

    public Station[] Stations { get; }

    public void Start() {
         

        this.players = new Players(NumOfPlayers);

        TileInHand = stack.Pop();

        players.AddPlayer(new HumanPlayer("PrvniHrac", PlayerColor.Y));
        players.AddPlayer(new HumanPlayer("DruhyHrac", PlayerColor.B));
        players.AddPlayer(new HumanPlayer("TretiHrac", PlayerColor.G));
        players.AddPlayer(new HumanPlayer("CtvrtyHrac", PlayerColor.V));
        players.AddPlayer(new HumanPlayer("PatyHrac", PlayerColor.O));
        players.AddPlayer(new HumanPlayer("SestyHrac", PlayerColor.R));

        TileController.Instance.DisplayTileInHand(TileInHand.Type, players.GetPlayerOnMove().Color);
    }

    public Game(string tilesConfig, string scheduleConfig, int numOfPlayers) {
        Width = 8;
        Height = 8;
        this.NumOfPlayers = numOfPlayers;
        this.Stations = new Station[32];

        board = new Tile[Width, Height];
        TileRepository tileRepository = new TileRepository(tilesConfig);
        stack = new TilesStack(tileRepository);
        LoadScheduleConfig(scheduleConfig);
    }

    public Tile GetTileAt(int x, int y) {
        if (!IsFieldWithTile(x, y)) {
            //Debug.Log("GetTileAt - out of bounds: " + x + ", " + y);
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
        players.InputClicked(pos);
    }

    void SetUpStations() {
        for (int i = 0; i < NumOfPlayers; i++) {
            Schedule s = scheduleRepository.GetSchedule((PlayerColor)i, NumOfPlayers);
            foreach (int st in s.GetStations()) {
                Station station = new Station(st, (PlayerColor)i);
            }
        }
    }

    public PlayerColor GetPlayerAtStation(int stationNumber) {
        throw new NotImplementedException();
    }

    public int[] GetPlayersScores() {
        return players.GetScores();
    }

    public void DisplayStations() {
        SetUpStations();
    }

    bool IsValidMove(Vector2 move, int type) {
        if (board[(int)move.x, (int)move.y] != null) {
            return false;
        }

        //TODO: Corners!

        return true;
    }

    void DoMove(Vector2 move, Tile tileInHand) {
        Debug.Log("Putting " + tileInHand.Type + " on " + move.x + " " + move.y);
        board[(int)move.x, (int)move.y] = tileInHand;
        TileController.Instance.DisplayTile((int)move.x, (int)move.y, tileInHand.Type);
    }

    public PlayerColor PlayerOnTurn() {
        return players.GetPlayerOnMove().Color;
    }

    public void Update() {
        Vector2 move = players.Move(new BoardState(this), TileInHand.Type);
        if (move.Equals(new Vector2(-2, -2))) {
            //He needs more time! 
            return;
        } else if (move.Equals(new Vector2(-1, -1))) {
            //He wants another tile!
            if (changedTile) {
                Debug.Log("Player: " + players.GetPlayerOnMove().Name + " is trying to change tile he got, but changed already! Invalid!!");
            } else {
                changedTile = true;
                stack.ReturnToStack(TileInHand);
                TileInHand = stack.Pop();
            }
        } else if (IsValidMove(move, TileInHand.Type)) {
            //It is valid
            changedTile = false;
            DoMove(move, TileInHand);
            TileInHand = stack.Pop();
            players.IncrementPlayer();
            Debug.Log("Player to move: " + players.GetPlayerOnMove().Name + " to put tile " + TileInHand.Type);
        } else {
            Debug.Log("Player: " + players.GetPlayerOnMove().Name + " is trying to do an invalid move! " + move + " with tile: " + TileInHand.Type);
        }

        TileController.Instance.DisplayTileInHand(TileInHand.Type, players.GetPlayerOnMove().Color);

    }

    public PlayerColor[] GetPlayerOrder() {
        return players.GetPlayerOrder();
    }
}
