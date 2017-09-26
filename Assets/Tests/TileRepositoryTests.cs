using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

[TestFixture]
public class TileRepositoryTests {
    private readonly string config = "2 N N E E S S W W" + Environment.NewLine +
                                     "3 N E E S S W W N";

    [Test]
    public void TestGetTileTypeCount() {
        // TODO: Add your test code here
        TileRepository repository = new TileRepository(config);
        Assert.That(repository.GetTileTypeCount(), Is.EqualTo(2));
    }

    [Test]
    public void TestGetTileCount() {
        // TODO: Add your test code here
        TileRepository repository = new TileRepository(config);
        Assert.That(repository.GetTileCount(0), Is.EqualTo(2));
        Assert.That(repository.GetTileCount(1), Is.EqualTo(3));
    }

    [Test]
    public void TestGetTile() {
        // TODO: Add your test code here
        TileRepository repository = new TileRepository(config);
        Dictionary<Direction, Direction> directions = repository.GetTile(0);
        foreach (Direction d in Enum.GetValues(typeof(Direction))) {
            Assert.That(directions[d], Is.EqualTo(d));
        }

        directions = repository.GetTile(1);
        Assert.That(directions[Direction.N], Is.EqualTo(Direction.E));
        Assert.That(directions[Direction.E], Is.EqualTo(Direction.S));
        Assert.That(directions[Direction.S], Is.EqualTo(Direction.W));
        Assert.That(directions[Direction.W], Is.EqualTo(Direction.N));
    }
}
