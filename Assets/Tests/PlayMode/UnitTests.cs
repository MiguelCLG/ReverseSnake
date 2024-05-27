using System.Collections;
using System.Collections.Generic;
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


    [UnityTest]
    public IEnumerator GameStarts()
    {
        var gameMasterObject = GameObject.Find("GameMaster");

        var view = gameMasterObject.GetComponent<GameMasterView>();

        yield return null;     
        
        Assert.IsNotNull( view );
    
    }



    [UnityTest]
    public IEnumerator PlayerScoreStartsAtZero()
    {

        var player = GameObject.Find("Player");
        var playerController = player.GetComponent<PlayerController>();
        
        //player.Move(new Vector2(1, 0));
        yield return null;

        Assert.AreEqual(0, playerController.GetScore());
    }



    [UnityTest]
    public IEnumerator FoodMoves()
    {
        var snake = GameObject.Find("Snake");
        
        var food = GameObject.Find("Food");

        Assert.AreEqual(snake.GetComponent<SnakeModel>().snakeBodyPositions.Count, 1);

        var lastPosition = food.transform.position;
        EventRegistry.GetEventPublisher("OnFoodEaten").RaiseEvent(snake);

        yield return new WaitForSeconds(1);

        Assert.AreNotEqual(food.transform.position, lastPosition);

        Assert.Greater(snake.GetComponent<SnakeModel>().snakeBodyPositions.Count, 1);

    }

}
