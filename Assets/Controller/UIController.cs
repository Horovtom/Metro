using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour {
    
    public Text yellowScore, blueScore, orangeScore, redScore, violetScore, greenScore;
    public int offset;
    private Dictionary<PlayerColor, Text> uiMapping;

    private bool playerOrderSet = false;
    // Use this for initialization
    void Start() {
        yellowScore.text = blueScore.text = orangeScore.text = redScore.text = violetScore.text = greenScore.text = "0";
        uiMapping = new Dictionary<PlayerColor, Text>();
        uiMapping[PlayerColor.Y] = yellowScore;
        uiMapping[PlayerColor.B] = blueScore;
        uiMapping[PlayerColor.O] = orangeScore;
        uiMapping[PlayerColor.R] = redScore;
        uiMapping[PlayerColor.V] = violetScore;
        uiMapping[PlayerColor.G] = greenScore;


    }
	
    // Update is called once per frame
    void Update() {
        if (!GameController.Instance.GameStarted)
            return;

        if (!playerOrderSet) {
            playerOrderSet = true;
            PlayerColor[] po = GameController.Instance.Game.GetPlayerOrder();

            for (int i = 0; i < po.Length; i++) {
                Vector3 pos = new Vector3(0, -i * offset, 0);
                pos += this.transform.position;
                uiMapping[po[i]].transform.position = pos;
            }
        }


        int[] scores = GameController.Instance.Game.GetPlayersScores();
        PlayerColor onTurn = GameController.Instance.Game.PlayerOnTurn();
        for (int i = 0; i < scores.Length; i++) {
            //PlayerColor color = (PlayerColor)Enum.Parse(typeof(PlayerColor), i.ToString());
            PlayerColor color = (PlayerColor)i;
            if (onTurn.Equals(color)) {
                uiMapping[color].text = color + ":" + scores[i] + "<";
            } else {
                uiMapping[color].text = color + ":" + scores[i];
            }

        }
    }
}
