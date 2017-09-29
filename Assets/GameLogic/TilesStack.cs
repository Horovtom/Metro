using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class TilesStack {
    Stack<Tile> stack;


    public TilesStack(TileRepository tr) {
        stack = new Stack<Tile>();

        for (int type = 0; type < tr.GetTileTypeCount(); type++) {
            for (int i = 0; i < tr.GetTileCount(type); i++) {
                stack.Push(tr.GetTile(type));
            }
        }
    }

    public void ReturnToStack(Tile tile) {
        stack.Push(tile);
        ShuffleStack();
    }

    public Tile Pop() {
        ShuffleStack();
        return stack.Pop();
    }

    private void ShuffleStack() {
        List<Tile> shuffle = new List<Tile>(stack);

        System.Random _random = new System.Random();

        Tile myTile;

        int n = shuffle.Count;
        for (int i = 0; i < n; i++) {
            // NextDouble returns a random number between 0 and 1.
            // ... It is equivalent to Math.random() in Java.
            int r = i + (int)(_random.NextDouble() * (n - i));
            myTile = shuffle[r];
            shuffle[r] = shuffle[i];
            shuffle[i] = myTile;
        }

        stack = new Stack<Tile>(shuffle);
    }
}
