using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroCode {
    class TileRepository {
        private List<Dictionary<Direction, Direction>> tiles;
        private List<int> tilesCounts;

        public TileRepository(string configFile) {
            tiles = new List<Dictionary<Direction, Direction>>();
            tilesCounts = new List<int>();

            //Read line by line
            try {
                char[] delimiter = { ' ' };
                foreach (string line in File.ReadLines(@configFile)) {
                    string[] words = line.Split(delimiter);
                    if (words.Length != 9) {
                        Debug.LogError("Config file has wrong number of tokens on line: " + tileID);
                        //Console.WriteLine("Config file has wrong number of tokens: " + words.Length);
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
            } catch (UnauthorizedAccessException UAEx) {
                //Console.WriteLine(UAEx.Message);
                Debug.LogError(UAEx.Message);
            } catch (PathTooLongException PathEx) {
                //Console.WriteLine(PathEx.Message);
                Debug.LogError(PathEx.Message);
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
                Debug.LogError("Want to get tileCount of tile with index: " + index + "", which is out of range!");
                throw new ArgumentException();
            }
            return tilesCounts[index];
        }

        /// <summary>
        /// Gets connections on specified tile, format of dictionary is [From] = Where
        /// </summary>
        public Dictionary<Direction, Direction> GetTile(int index) {
            if (index < 0 || index >= tiles.Count) {
                Debug.LogError("Want to get tile with index: " + index + "", which is out of range!");
                throw new ArgumentException();
            }

            return tiles[index];
        }
    }

}
