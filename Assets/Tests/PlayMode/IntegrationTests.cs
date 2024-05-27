using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class IntegrationTests
{
    [OneTimeSetUp]
    public void loadScene()
    {
        SceneManager.LoadScene("level");
    }


    [UnityTest, Order(11)]
    public IEnumerator FoodChangesPositionWhenEaten()
    {
        var snake = GameObject.Find("Snake");
        var food = GameObject.Find("Food");

        var lastPosition = food.transform.position;
        
        EventRegistry.GetEventPublisher("OnFoodEaten").RaiseEvent(snake);

        yield return new WaitForSeconds(1);

        Assert.AreNotEqual(food.transform.position, lastPosition);

    }


    [UnityTest, Order(12)]
    public IEnumerator PlayerDiesCausesGameOver()
    {
        var player = GameObject.Find("Player");
        var snake = GameObject.Find("Snake");
        var gameMaster = GameObject.Find("GameMaster");
        
        EventRegistry.GetEventPublisher("OnPlayerDeath").RaiseEvent(snake);

        yield return new WaitForSeconds(1);

        //Assert.False(player.GetComponent<PlayerModel>().IsAlive());
        Assert.True(gameMaster.GetComponent<GameMasterView>().gameOverPanel.activeSelf);

    }

}
