using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ScheduleRepository {
    /// <summary>
    /// List indexed by colors, dictionary indexed by playerCounts
    /// </summary>
    private List<Dictionary<int, Schedule>> schedules;

    public ScheduleRepository(string config) {
        schedules = new List<Dictionary<int, Schedule>>();

        if (!PlayerPrefs.HasKey("MaxPlayers")) {
            PlayerPrefs.SetInt("MaxPlayers", 6);
        }

        int maxPlayers = PlayerPrefs.GetInt("MaxPlayers");
        for (int i = 0; i < maxPlayers; i++) {
            schedules.Add(new Dictionary<int, Schedule>());
        }

        char[] delimiter = { ' ' };
        foreach (string line in config.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)) {
            string[] words = line.Split(delimiter);
            if (words.Length < 2) {
                Debug.LogError("Config file has wrong number of tokens!");
                throw new FormatException();
            }

            int numOfPlayers = int.Parse(words[0]);
            PlayerColor color = (PlayerColor)Enum.Parse(typeof(PlayerColor), words[1]);

            Schedule s = new Schedule(color, numOfPlayers);

            for (int i = 2; i < words.Length; i++) {
                s.AddStation(int.Parse(words[i]));
            }

            schedules[(int)color].Add(numOfPlayers, s);
        }
    }

    public Schedule GetSchedule(PlayerColor color, int numOfPlayers) {
        if (schedules.Count <= (int)color) {
            Debug.Log("This SchedulesRepository does not support color: " + color + " It only has: " + schedules.Count + " colors");
            return null;
        }

        if (!schedules[(int)color].ContainsKey(numOfPlayers)) {
            Debug.Log("This ScheduleRepository does not contain " + numOfPlayers + " version for color " + color);
            Debug.Log("Color " + color + " has only " + schedules[(int)color].Count + " versions for players!");
            return null;
        }

        return schedules[(int)color][numOfPlayers];
    }
}
