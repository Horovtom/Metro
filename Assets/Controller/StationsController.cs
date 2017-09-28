using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationsController : MonoBehaviour {
    public GameObject stationPrefab;
    Dictionary<int, GameObject> stations;

    public static StationsController Instance { get; private set; }

    public StationsController() {
        stations = new Dictionary<int, GameObject>();
        Instance = this;
    }

    // Use this for initialization
    void Start() {
        Game game = GameController.Instance.Game;
        GameObject[] stationsContainers = GameObject.FindGameObjectsWithTag("Stations");
        GameObject NStationsContainer = null, EStationsContainer = null, WStationsContainer = null, SStationsContainer = null;


        //Top stations
        foreach (GameObject o in stationsContainers) {
            switch (o.name) {
                case "NStations":
                    NStationsContainer = o;
                    break;
                case "EStations":
                    EStationsContainer = o;
                    break;
                case "WStations":
                    WStationsContainer = o;
                    break;
                case "SStations":
                    SStationsContainer = o;
                    break;
            }
        }

        //N
        int start = 1;
        int stop = 8;
        for (int i = start; i <= stop; i++) {
            GameObject stationGO = (GameObject)GameObject.Instantiate(stationPrefab, NStationsContainer.transform.position + (new Vector3(-(i - start), 0, 0)), Quaternion.identity);
            stationGO.name = "Station [" + i + "]";

            //Rotate by 90˚
            Vector3 rot = stationGO.transform.eulerAngles;
            rot = new Vector3(rot.x, rot.y, rot.z + 90);
            stationGO.transform.rotation = Quaternion.Euler(rot);

            stationGO.transform.SetParent(NStationsContainer.transform);
            SpriteRenderer sr = (SpriteRenderer)stationGO.GetComponent<SpriteRenderer>();
            sr.sortingLayerName = "Trains";
            stations[i] = stationGO;
            SetStationColor(i, PlayerColor.None);
        }
        //W
        start = 9; 
        stop = 16;
        for (int i = start; i <= stop; i++) {
            GameObject stationGO = (GameObject)GameObject.Instantiate(stationPrefab, WStationsContainer.transform.position + (new Vector3(0, -(i - start), 0)), Quaternion.identity);
            stationGO.name = "Station [" + i + "]";
            stationGO.transform.SetParent(WStationsContainer.transform);
            SpriteRenderer sr = (SpriteRenderer)stationGO.GetComponent<SpriteRenderer>();
            sr.sortingLayerName = "Trains";
            stations[i] = stationGO;
            SetStationColor(i, PlayerColor.None);
        }
        //S
        start = 17;
        stop = 24;
        for (int i = start; i <= stop; i++) {
            GameObject stationGO = (GameObject)GameObject.Instantiate(stationPrefab, SStationsContainer.transform.position + (new Vector3((i - start), 0, 0)), Quaternion.identity);
            stationGO.name = "Station [" + i + "]";

            //Rotate by 90˚
            Vector3 rot = stationGO.transform.eulerAngles;
            rot = new Vector3(rot.x, rot.y, rot.z + 90);
            stationGO.transform.rotation = Quaternion.Euler(rot);

            stationGO.transform.SetParent(SStationsContainer.transform);
            SpriteRenderer sr = (SpriteRenderer)stationGO.GetComponent<SpriteRenderer>();
            sr.sortingLayerName = "Trains";
            stations[i] = stationGO;
            SetStationColor(i, PlayerColor.None);
        }
        //E
        start = 25;
        stop = 32;
        for (int i = start; i <= stop; i++) {
            GameObject stationGO = (GameObject)GameObject.Instantiate(stationPrefab, EStationsContainer.transform.position + (new Vector3(0, (i - start), 0)), Quaternion.identity);
            stationGO.transform.SetParent(EStationsContainer.transform);
            stationGO.name = "Station [" + i + "]";
            SpriteRenderer sr = (SpriteRenderer)stationGO.GetComponent<SpriteRenderer>();
            sr.sortingLayerName = "Trains";
            stations[i] = stationGO;
            SetStationColor(i, PlayerColor.None);
        }

        game.DisplayStations();
    }
	
    // Update is called once per frame
    void Update() {
		
    }

    public void SetStationColor(int station, PlayerColor color) {
        if (!stations.ContainsKey(station)) {
            Debug.Log("Trying to set station " + station + " color to " + color + ", but this station number does not exist! Stations length is: " + stations.Count);
            return;
        }

        Debug.Log("Setting station: " + station + " to color: " + color);

        stations[station].SetActive(true);

        switch (color) {
            case PlayerColor.B:
                stations[station].GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case PlayerColor.C:
                stations[station].GetComponent<SpriteRenderer>().color = Color.cyan;
                break;
            case PlayerColor.G:
                stations[station].GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case PlayerColor.O:
                stations[station].GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 127 / 255f, 80 / 255f, 1);
                break;
            case PlayerColor.R:
                stations[station].GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case PlayerColor.Y:
                stations[station].GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            default:
                stations[station].SetActive(false);
                break;
        }

    }
}
