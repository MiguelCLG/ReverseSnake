using System;
using UnityEngine;

public class TestEvents {


    public TestEvents()
    {
        this.registerToAllEvents();
    }

    // Subscrever todos os Eventos conhecidos
    public void registerToAllEvents()
    {
        EventSubscriber.SubscribeToEvent("OnFoodEaten", testOnFoodEaten);
        EventSubscriber.SubscribeToEvent("OnPlayerMove", testOnPlayerMove);
        EventSubscriber.SubscribeToEvent("OnPlayerDeath", testOnPlayerDeath);
        EventSubscriber.SubscribeToEvent("OnScoreIncrease", testOnScoreIncrease);
    }
    
    // Testar se a comida muda de posição
    public void testOnFoodEaten(object sender, object obj)
    {

    }

    // Testar se o player troca de posição
    public void testOnPlayerMove(object sender, object obj)
    {

    }

    // Testar se o score Aumenta
    public void testOnScoreIncrease(object sender, object obj)
    {

    }

    // Testar se o player não é imortal
    public void testOnPlayerDeath(object sender, object obj) {
        
    }


}