using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schedule {
    public int PlayerCount { get; }

    public PlayerColor Color { get; }

    private readonly List<int> stations;


    public Schedule(PlayerColor color, int numOfPlayers) {
        this.Color = color;
        this.PlayerCount = numOfPlayers;
        stations = new List<int>();
    }

    public void AddStation(int i) {
        if (stations.Contains(i)) {
            Debug.Log("Station " + i + " was already in this schedule!");
            return;
        }
        stations.Add(i);
    }

    public void RemoveStation(int station) {
        stations.Remove(station);
    }

    public bool ContainsStation(int station) {
        return stations.Contains(station);
    }

    public int[] GetStations() {
        int[] returning = new int[stations.Count];
        stations.CopyTo(returning);
        return returning;
    }

    public int GetStation(int index) {
        if (index < 0 || index >= stations.Count) {
            Debug.Log("Cannot get station at index: " + index + ", because there is no such station in this schedule...");
            return -1;
        }
        return stations[index];
    }

    public int GetScheduleLength() {
        return stations.Count;
    }
}
