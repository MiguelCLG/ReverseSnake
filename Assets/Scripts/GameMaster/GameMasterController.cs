using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void ApresentaNovoEstado() { }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartGame()
    {
        view.CloseWindow();
        model.ConstroiJogo();
    }

    // Events

    private void RegisterEvents()
    {
        EventRegistry.RegisterEvent("OnFoodEaten");
        EventSubscriber.SubscribeToEvent("OnFoodEaten", OnFoodEaten);

        EventRegistry.RegisterEvent("OnPlayerMove");
        EventSubscriber.SubscribeToEvent("OnPlayerMove", OnPlayerMove);
    }
    private void OnFoodEaten(object sender, object obj)
    {
        if (obj is GameObject gO)
        {
            model.EscolhePosicao(model.food.gameObject);
            Debug.Log($"GameMaster: OnFoodEaten was called by {gO.tag}");
        }
    }

    private void OnPlayerMove(object sender, object obj)
    {
        if (obj is PlayerController playerController){
            model.SwitchOccupiedPosition(playerController.gameObject);
            Debug.Log($"GameMaster: OnPlayerMove was called by {playerController.tag}");
        }
    }

    public void OnQuit()
    {
        Debug.Log("Application quit!");
        Application.Quit();
    }

}