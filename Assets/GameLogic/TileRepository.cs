using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TileRepository {
    private List<Dictionary<Direction, Direction>> tiles;
    private List<int> tilesCounts;

    public TileRepository(string configFile) {
        tiles = new List<Dictionary<Direction, Direction>>();
        tilesCounts = new List<int>();

        //Read line by line

        char[] delimiter = { ' ' };
        foreach (string line in configFile.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)) {
            string[] words = line.Split(delimiter);
            if (words.Length != 9) {
                Debug.LogError("Config file has wrong number of tokens!");
                throw new FormatException();
            }

            tilesCounts.Add(int.Parse(words[0]));
            Dictionary<Direction, Direction> directions = new Dictionary<Direction, Direction>();
            for (int i = 0; i < 4; i++) {
                Direction from = (Direction)Enum.Parse(typeof(Direction), words[i * 2 + 1]);
                Direction where = (Direction)Enum.Parse(typeof(Direction), words[i * 2 + 2]);

                directions[from] = where;
            }
            tiles.Add(directions);
        }

    }

    /// <summary>
    /// Gets total number of tile types, loaded in config
    /// </summary>
    public int GetTileTypeCount() {
        return tilesCounts.Count;
    }

    /// <summary>
    /// Gets number of cards of specified tile, loaded in config
    /// </summary>
    public int GetTileCount(int index) {
        if (index < 0 || index >= tiles.Count) {
            Debug.LogError("Want to get tileCount of tile with index: " + index + ", which is out of range!");
            throw new ArgumentException();
        }
        return tilesCounts[index];
    }

    /// <summary>
    /// Gets Tile with ready connections
    /// </summary>
    public Tile GetTile(int type) {
        if (type < 0 || type >= tiles.Count) {
            Debug.LogError("Want to get tile with index: " + type + ", which is out of range!");
            throw new ArgumentException();
        }
        return new Tile(type, tiles[type]);
    }
}

