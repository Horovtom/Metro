using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player {
    public HumanPlayer(string name, PlayerColor color)
        : base(name, color) {
    
    }

    private Vector2 clicked = new Vector2(-2, -2);

    public override bool IsInteractive() {
        return true;
    }

    public override Vector2 Move(BoardState boardState, int tile) {
        if (boardState.IsTilePlaced((int)clicked.x, (int)clicked.y)) {
            return new Vector2(-2, -2);
        }

        Vector2 returning = clicked;
        clicked = new Vector2(-2, -2);
        return returning;
    }

    public override void InputClicked(Vector2 pos) {
        clicked = pos;
    }
}
