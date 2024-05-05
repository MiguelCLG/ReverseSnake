using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
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
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    //metodo para atualizar a posicao da snake
    public void SetPosition(Vector3 newPosition)
    {
        view.DisplaySnake(newPosition);
    }
    //metodo para quando a sneke come a comida
    private void OnFoodEaten(object sender, object obj)
    {
        if(obj is GameObject gO)
            Debug.Log($"Snake: OnFoodEaten was called by {gO.tag}");
    }
}
