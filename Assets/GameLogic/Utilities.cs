using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {
    public static Direction oppositeDirection(Direction d) {
        switch (d) {
            case Direction.E:
                return Direction.W;
            case Direction.N:
                return Direction.S;
            case Direction.S:
                return Direction.N;
            case Direction.W:
                return Direction.E;
        }

        return Direction.N;
    }
}
