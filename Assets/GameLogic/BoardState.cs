using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardState {
    private readonly Game game;


    public BoardState(Game game) {
        this.game = game;
    }

    /// <summary>
    /// Returns -1 if the tile is not yet placed
    /// </summary>
    public int GetTileTypeAt(int x, int y) {
        Tile t = game.GetTileAt(x, y);
        if (t == null) {
            return -1;
        }
        else {
            return t.Type;
        }
    }

    public bool IsTilePlaced(int x, int y) {
        return GetTileTypeAt(x, y) != -1;
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
