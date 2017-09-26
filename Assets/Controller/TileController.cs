﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TileController : MonoBehaviour {
    public static TileController Instance { get; private set; }

    private Dictionary<Tile, GameObject> tileGOMap;
    private Dictionary<GameObject,Tile> GOTileMap;
    public GameObject tilePrefab;
    private Dictionary<string, Sprite> tileSprites;

    private GameObject[,] boardGO;
    private Dictionary<GameObject,Vector2> boardCoordsMap;

    public TileController() {
        Instance = this;
    }
    
    // Use this for initialization
    void Start() {
        tileGOMap = new Dictionary<Tile, GameObject>();
        GOTileMap = new Dictionary<GameObject,Tile>();
        Game game = GameController.Instance.Game;
        LoadSprites();
        boardGO = new GameObject[game.Width, game.Height];
        boardCoordsMap = new Dictionary<GameObject, Vector2>();

        for (int x = 0; x < game.Width; x++) {
            for (int y = 0; y < game.Height; y++) {
                if (!game.IsFieldWithTile(x, y))
                    continue;

                GameObject tileGO = (GameObject)GameObject.Instantiate(tilePrefab, this.transform.position + (new Vector3(x, y, 0)), Quaternion.identity);
                tileGO.name = "Tile [" + x + ", " + y + "]";
                tileGO.transform.SetParent(this.transform, true);
                tileGO.GetComponent<SpriteRenderer>().sortingLayerName = "Cards";
                boardGO[x, y] = tileGO;
                boardCoordsMap[tileGO] = new Vector2(x, y);
            }
        }


//        //Fill the TileGOMap:
//        for (int x = 0; x < game.Width; x++) {
//            for (int y = 0; y < game.Height; y++) {
//                if (!game.IsFieldWithTile(x, y))
//                    continue;
//
//                Tile tile = game.GetTileAt(x, y);
//                GameObject tileGO = (GameObject)GameObject.Instantiate(tilePrefab, this.transform.position + (new Vector3(x, y, 0)), Quaternion.identity);
//                tileGO.name = "Tile [" + x + ", " + y + "]";
//                tileGO.transform.SetParent(this.transform, true);
//                tileGO.GetComponent<SpriteRenderer>().sortingLayerName = "Cards";
//                GOTileMap.Add(tileGO, tile);
//                tileGOMap.Add(tile, tileGO);
//                //Debug.Log("I am here!");
//
//                tile.RegisterTypeChangedCallback(TileSpriteChanged);
//            }
//        }
    }

    void LoadSprites() {
        tileSprites = new Dictionary<string, Sprite>();
        Sprite[] sprites1 = Resources.LoadAll<Sprite>("Images/tiles1");
        Sprite[] sprites2 = Resources.LoadAll<Sprite>("Images/tiles2");
        Sprite[] sprites3 = Resources.LoadAll<Sprite>("Images/tiles3");

        foreach (Sprite s in sprites1) {
            tileSprites[s.name] = s;
        }
        foreach (Sprite s in sprites2) {
            tileSprites[s.name] = s;
        }
        foreach (Sprite s in sprites3) {
            tileSprites[s.name] = s;
        }
    }
	
    // Update is called once per frame
    void Update() {
		
    }

    //    void TileSpriteChanged(Tile tile) {
    //        //Debug.Log("I am here!");
    //        GameObject tileGO = tileGOMap[tile];
    //
    //
    //        string spriteName = "tiles" + tile.Type;
    //
    //        if (!tileSprites.ContainsKey(spriteName)) {
    //            Debug.LogError("Sprite: " + spriteName + " not present in dictionary! Sprite missing!");
    //            return;
    //        }
    //        Sprite s = tileSprites[spriteName];
    //        tileGO.GetComponent<SpriteRenderer>().sprite = s;
    //    }

    void DisplayTile(int x, int y, int Type) {
        string spriteName = "tiles" + Type;
        if (!tileSprites.ContainsKey(spriteName)) {
            Debug.LogError("Sprite: " + spriteName + " not present in dictionary! Sprite missing!");
            return;
        }
        Sprite s = tileSprites[spriteName];
        boardGO[x, y].GetComponent<SpriteRenderer>().sprite = s;
    }

    public Vector2 GetCoordsOfTile(GameObject gameObject) {
        return boardCoordsMap[gameObject];
    }
}
