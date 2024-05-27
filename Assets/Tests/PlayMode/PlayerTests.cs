using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTests
{

    [UnityTest]
    public IEnumerator playerScore()
    {
        var gameObject = new GameObject();

        var gameMasterController = gameObject.AddComponent<GameMasterController>();
        //        player.Start();
        //        player.Move(new Vector2(1, 0));
        yield return new WaitForSeconds(1);

        Assert.AreEqual(0, player.GetScore());
    }


    [UnityTest]
    public IEnumerator playerScore()
    {
        var gameObject = new GameObject();

        var player = gameObject.AddComponent<PlayerModel>();
//        player.Start();
//        player.Move(new Vector2(1, 0));
        yield return new WaitForSeconds(1);

        Assert.AreEqual(0, player.GetScore());
    }
}
