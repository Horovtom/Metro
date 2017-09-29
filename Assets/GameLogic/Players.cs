using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Players {
    private readonly Player[] players;
    private readonly int[] playerScores;
    private int currentPlayerToMove;

    public Players(int playerCount) {
        players = new Player[playerCount];
        currentPlayerToMove = 0;
        playerScores = new int[playerCount];
    }

    public void IncrementPlayer() {
        currentPlayerToMove++;
        if (currentPlayerToMove >= players.Length) {
            currentPlayerToMove = 0;
        }
    }

    public void AddPlayer(Player player) {
        for (int i = 0; i < players.Length; i++) {
            if (players[i] == null) {
                players[i] = player;
                return;
            }
        }
    }

    /// <summary>
    /// Asks current player to move to Return his move. 
    /// </summary>
    public Vector2 Move(BoardState bs, int tile) {
        int toMove = currentPlayerToMove;
        return players[toMove].Move(bs, tile);
    }

    public int GetScore(int player) {
        if (player < 0 || player >= players.Length) {
            Debug.Log("Trying to get info about a player, that is nonexistent:" + player);
            return 0;
        }
        return playerScores[player];
    }

    public void InputClicked(Vector2 pos) {

        if (players[currentPlayerToMove].IsInteractive()) {
            players[currentPlayerToMove].InputClicked(pos);
        }
    }

    public void IncrementScore(int player, int increment) {
        if (player < 0 || player >= players.Length) {
            Debug.Log("Trying to get info about a player, that is nonexistent:" + player);
            return;
        }

        playerScores[player] += increment;
    }

    public int[] GetScores() {
        int[] returning = new int[playerScores.Length];
        Array.Copy(playerScores, returning, playerScores.Length);
        return returning;
    }

    public Player GetPlayerOnMove() {
        return players[currentPlayerToMove];
    }

    public PlayerColor[] GetPlayerOrder() {
        PlayerColor[] returning = new PlayerColor[players.Length];

        for (int i = 0; i < players.Length; i++) {
            returning[i] = players[i].Color;
        }
        return returning;
    }
}
