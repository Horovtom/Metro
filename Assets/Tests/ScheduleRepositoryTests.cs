using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;

public class ScheduleRepositoryTests {
    private readonly string config = 
        "2 Y 1 3 5 7 9" + Environment.NewLine +
        "2 B 2 4 6 8 10" + Environment.NewLine +
        "3 Y 1 4 7 8 9";

    [Test]
    public void TestGetStation() {
        ScheduleRepository sr = new ScheduleRepository(config);
        Schedule s = sr.GetSchedule(PlayerColor.Y, 2);
        Assert.AreEqual(1, s.GetStation(0));
        Assert.AreEqual(9, s.GetStation(4));
        Assert.AreEqual(-1, s.GetStation(5));
    }

    [Test]
    public void TestGetStations() {
        ScheduleRepository sr = new ScheduleRepository(config);
        Schedule s = sr.GetSchedule(PlayerColor.Y, 2);
        int[] stations = s.GetStations();
        int[] shouldBe = { 1, 3, 5, 7, 9 };
        for (int i = 0; i < shouldBe.Length; i++) {
            Assert.AreEqual(shouldBe[i], stations[i]);
        }
    }

    [Test]
    public void TestAddStation() {
        ScheduleRepository sr = new ScheduleRepository(config);
        Schedule s = sr.GetSchedule(PlayerColor.Y, 2);
        Assert.AreEqual(-1, s.GetStation(5));
        Assert.AreEqual(5, s.GetScheduleLength());
        s.AddStation(2);
        Assert.AreEqual(2, s.GetStation(5));
        Assert.AreEqual(6, s.GetScheduleLength());
    }

    [Test]
    public void TestMultipleSchedules() {
        ScheduleRepository sr = new ScheduleRepository(config);
        Schedule r = sr.GetSchedule(PlayerColor.Y, 2);
        Schedule g = sr.GetSchedule(PlayerColor.Y, 3);
        Schedule b = sr.GetSchedule(PlayerColor.B, 2);
        Assert.Null(sr.GetSchedule(PlayerColor.B, 3));

        Assert.AreEqual(4, b.GetStation(1));
        Assert.AreEqual(10, b.GetStation(4));
    }
}
