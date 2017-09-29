using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player {
    public string Name { get; }

    public int Score { get; set; }

    public PlayerColor Color { get; }

    public virtual bool IsInteractive() {
        return false;
    }

    public Player(string name, PlayerColor color) {
        this.Name = name;
        Score = 0;
        this.Color = color;
    }

    /// <summary>
    /// Queries player to make a move, considering current boardState and a tile he has in hand. 
    /// If player returns vector of (-1, -1), he will get another tile. This is however allowed to do only once.
    /// If player returns vector of (-2, -2), he will get more time to think (Used for interactive players - waiting for mouse input)
    /// </summary>
    public abstract Vector2 Move(BoardState boardState, int tile);

    public virtual void InputClicked(Vector2 pos) {
        return;
    }
}