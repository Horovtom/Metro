using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameController : MonoBehaviour {
    public string configFile;
    public static GameController Instance {get; private set; }

    public Game Game { get; private set;}

    public GameController() {
        Instance = this;
        Game = new Game(configFile);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
