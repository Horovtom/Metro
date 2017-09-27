using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player {
    public string Name { get; }

    public int Score { get; set; }

    public PlayerColor Color { get; }

    public Player(string name, PlayerColor color) {
        this.Name = name;
        Score = 0;
        this.Color = color;
    }

    /// <summary>
    /// Queries player to make a move, considering current boardState and a tile he has in hand. 
    /// If player returns null of vector of (-1, -1), he will get another tile. This is however allowed to do only once.
    /// </summary>
    public abstract Vector2 Move(BoardState boardState, int tile);
}