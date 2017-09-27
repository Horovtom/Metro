using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardState {
    private readonly Game game;


    public BoardState(Game game) {
        this.game = game;
    }

    public int GetTileTypeAt(int x, int y) {
        return game.GetTileAt(x, y).Type;
    }

    public PlayerColor GetPlayerAtStation(int stationNumber) {
        return game.GetPlayerAtStation(stationNumber);
    }

    /// <summary>
    /// Gets array of scores, indexed by PlayerColors   
    /// </summary>
    public int[] GetPlayersScores() {
        return game.GetPlayersScores();
    }
}
