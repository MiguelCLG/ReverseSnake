using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class FunctionalTests
{
    [OneTimeSetUp]
    public void loadScene()
    {
        SceneManager.LoadScene("level");
    }

    [UnityTest, Order(0)]
    public IEnumerator PlayerScoreStartsAtZero()
    {
        var player = GameObject.Find("Player");
        var playerController = player.GetComponent<PlayerController>();

        //player.Move(new Vector2(1, 0));
        yield return null;

        Assert.AreEqual(0, playerController.GetScore());
    }

    [UnityTest, Order(1)]
    public IEnumerator snakeGrowsOnEatAndDoNotUpdateScore()
    {
        var player = GameObject.Find("Player");
        var playerController = player.GetComponent<PlayerController>();
        var snake = GameObject.Find("Snake");

        var food = GameObject.Find("Food");

        Assert.AreEqual(snake.GetComponent<SnakeModel>().snakeBodyPositions.Count, 1);

        var lastPosition = food.transform.position;
        EventRegistry.GetEventPublisher("OnFoodEaten").RaiseEvent(snake);

        yield return new WaitForSeconds(1);

        Assert.AreNotEqual(food.transform.position, lastPosition);


        //player.Move(new Vector2(1, 0));
        yield return null;

        Assert.AreEqual(0, playerController.GetScore());
    }


    [UnityTest, Order(2)]
    public IEnumerator playerEatsFoodAndScoreIsUpdated()
    {
        var player = GameObject.Find("Player");
        var playerController = player.GetComponent<PlayerController>();

        EventRegistry.GetEventPublisher("OnFoodEaten").RaiseEvent(player);

        yield return new WaitForSeconds(1);


        Assert.AreEqual(10, playerController.GetScore());
    }

    [UnityTest, Order(3)]
    public IEnumerator snakeDoesNotGrowWhenPlayerEatsFood()
    {
        var player = GameObject.Find("Player");
        var playerController = player.GetComponent<PlayerController>();
        var snake = GameObject.Find("Snake");

        var food = GameObject.Find("Food");

        var lastBodyCount = snake.GetComponent<SnakeModel>().snakeBodyPositions.Count;       

        EventRegistry.GetEventPublisher("OnFoodEaten").RaiseEvent(player);

        yield return new WaitForSeconds(1);

        Assert.AreEqual(snake.GetComponent<SnakeModel>().snakeBodyPositions.Count, lastBodyCount);
    }
}
