using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;

public class TilesStackTest {
    private readonly string config = "2 N N E E S S W W" + Environment.NewLine +
                                     "3 N E E S S W W N";

    [Test]
    public void ReturnToStackTest() {
        TilesStack ts = new TilesStack(new TileRepository(config));

        int i = 1000;
        while (i > 0) {
            Tile t = ts.Pop();
            Assert.NotNull(t);
            ts.ReturnToStack(t);
            Tile s = ts.Pop();
            Assert.NotNull(s);
            if (t != s)
                return;
        }

        Assert.Fail();
    }


}
