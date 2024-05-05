using System;
using UnityEditor;
using UnityEngine;

public class GameMasterController: MonoBehaviour
{

    private GameMasterModel model;
    private GameMasterView view;
    private static GameMasterController Instance { get; set; }

  
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);

        RegisterEvents();

    }

    private void Start()
    {
        model = GetComponent<GameMasterModel>();
        view = GetComponent<GameMasterView>();

        StartGame();
    }

    private void RegisterEvents()
    {
        EventRegistry.RegisterEvent("OnFoodEaten");
        EventSubscriber.SubscribeToEvent("OnFoodEaten", OnFoodEaten);
    }
    

    private void OnFoodEaten(object sender, object obj)
    {
        if (obj is GameObject gO){
            model.EscolhePosicaoComida();
            Debug.Log($"GameMaster: OnFoodEaten was called by {gO.tag}");
        }
    }

    public void ApresentaNovoEstado() { }

    public void StartGame()
    {
        model.ConstroiJogo();
    }
}