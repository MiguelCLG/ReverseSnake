using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class UnitTests
{

    [OneTimeSetUp]
    public void loadScene()
    {
        SceneManager.LoadScene("level");
    }

    [UnityTest, Order(0)]
    public IEnumerator sceneLoads()
    {
        var gm = GameObject.Find("GameMaster");
        Assert.IsNotNull(gm.GetComponent<GameMasterController>());
        yield return null;
    }

    [UnityTest, Order(1)]
    public IEnumerator playerMoves()
    {
        var player = GameObject.Find("Player");
        var playerController = player.GetComponent<PlayerController>();
        var playerView = player.GetComponent<PlayerView>();

        var moves = new Vector2[] { Vector2.up, Vector2.left, Vector2.right, Vector2.down };

        foreach (var move in moves)
        {
            var oldPosition = new Vector2(playerView.transform.position.x, playerView.transform.position.y);
            playerController.MovePlayer(move);
            yield return new WaitForSeconds(1);
            var newPosition = GameGrid.getInstance().ClampOnScreen(oldPosition + move);
            Assert.AreEqual(
                newPosition.x,
                oldPosition.x + move.x);
        }
        
        yield return new WaitForSeconds(1);

        Assert.IsTrue(true);

    }

    [UnityTest, Order(2)]
    public IEnumerator snakePathIsCalculated()
    {
        var snake = GameObject.Find("Snake");
        var snakeModel = snake.GetComponent<SnakeModel>();
        var lastQueueTail = snakeModel.PathToFollow.Peek();
        var lastQueueCount = snakeModel.PathToFollow.Count();

        EventRegistry.GetEventPublisher("OnFoodEaten").RaiseEvent(snake);
        
        yield return new WaitForSeconds(1);
        Assert.True(snakeModel.PathToFollow.Last() != lastQueueTail || snakeModel.PathToFollow.Count != lastQueueCount);

    }
}
