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

    private void IncrementPlayer() {
        currentPlayerToMove++;
        if (currentPlayerToMove >= players.Length) {
            currentPlayerToMove = 0;
        }
    }

    /// <summary>
    /// Asks current player to move to Return his move. 
    /// This then increments player to move to next player.
    /// </summary>
    public Vector2 Move(BoardState bs, int tile) {
        int toMove = currentPlayerToMove;
        IncrementPlayer();
        return players[toMove].Move(bs, tile);
    }

    public int GetScore(int player) {
        if (player < 0 || player >= players.Length) {
            Debug.Log("Trying to get info about a player, that is nonexistent:" + player);
            return 0;
        }
        return playerScores[player];
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
}
