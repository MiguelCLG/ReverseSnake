using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour, IController
{
    private SnakeModel model;
    private SnakeView view;
    private static SnakeController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<SnakeView>();
        model = GetComponent<SnakeModel>();
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        EventSubscriber.SubscribeToEvent("OnFoodEaten", OnFoodEaten);
        EventSubscriber.SubscribeToEvent("OnPlayerMove", model.MoveTowardsTarget);
        EventSubscriber.SubscribeToEvent("OnGameMasterLoaded", model.InitializeSnake);
    }
    public void UnsubscribeEvents()
    {
        EventSubscriber.UnsubscribeFromEvent("OnFoodEaten", OnFoodEaten);
        EventSubscriber.UnsubscribeFromEvent("OnPlayerMove", model.MoveTowardsTarget);
        EventSubscriber.UnsubscribeFromEvent("OnGameMasterLoaded", model.InitializeSnake);
    }
    //metodo para atualizar a posicao da snake
    public void SetPosition(Vector2 newPosition)
    {
        view.DisplayView(newPosition);
    }

    public void Grow()
    {
        model.GrowSnake();
    }
    //metodo para quando a sneke come a comida
    private void OnFoodEaten(object sender, object obj)
    {
        model.UpdatePathToFood();
    }
}
