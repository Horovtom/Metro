using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TileController : MonoBehaviour {
    
    private Dictionary<Tile, GameObject> tileGOMap;
    private Dictionary<GameObject,Tile> GOTileMap;
    public GameObject tilePrefab;
    private Dictionary<string, Sprite> tileSprites;
    
    // Use this for initialization
    void Start() {
        tileGOMap = new Dictionary<Tile, GameObject>();
        GOTileMap = new Dictionary<GameObject,Tile>();
        Game game = GameController.Instance.Game;
        LoadSprites();

        //Fill the TileGOMap:
        for (int x = 0; x < game.Width; x++) {
            for (int y = 0; y < game.Height; y++) {
                if (!game.IsFieldWithTile(x, y))
                    continue;

                Tile tile = game.GetTileAt(x, y);
                GameObject tileGO = (GameObject) GameObject.Instantiate(tilePrefab, this.transform.position + (new Vector3(x, y, 0)), Quaternion.identity);
                tileGO.name = "Tile [" + x + ", " + y + "]";
                tileGO.transform.SetParent(this.transform, true);
                tileGO.GetComponent<SpriteRenderer>().sortingLayerName = "Cards";
                GOTileMap.Add(tileGO, tile);
                tileGOMap.Add(tile, tileGO);
                Debug.Log("I am here!");

                tile.RegisterTypeChangedCallback(TileSpriteChanged);
            }
        }
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

    void TileSpriteChanged(Tile tile) {
        //Debug.Log("I am here!");
        GameObject tileGO = tileGOMap[tile];
        int spriteGroup = (int) Math.Floor(tile.Type / 12.0) + 1;
        int spriteNumber = tile.Type % 12;

        string spriteName = "tiles" + spriteGroup + "_" + spriteNumber;

        if (!tileSprites.ContainsKey(spriteName)) {
            Debug.LogError("Sprite: " + spriteName + " not present in dictionary! Sprite missing!");
            return;
        }
        Sprite s = tileSprites[spriteName];
        tileGO.GetComponent<SpriteRenderer>().sprite = s;
    }
}
