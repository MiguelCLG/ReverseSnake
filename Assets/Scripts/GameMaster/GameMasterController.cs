using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMasterController: MonoBehaviour
{
    private GameMasterModel model;
    private GameMasterView view;
    private static GameMasterController Instance { get; set; }

    // Primeira iteração do lifecycle do objecto unity
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

    // Chamado no inicio
    private void Start()
    {
        model = GetComponent<GameMasterModel>();
        view = GetComponent<GameMasterView>();
        int savedHighScore = SaveSystem.Load();
        view.DisplayNewScore(0, savedHighScore);
        StartGame();
    }

    public void ApresentaNovoEstado() { }

    // Chamado Quando o jogo recomeça
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Chamado quando o jogo Começa
    public void StartGame()
    {
        view.CloseWindow();
        model.ConstroiJogo();
        EventRegistry.GetEventPublisher("OnGameMasterLoaded").RaiseEvent(this);
    }

    // Events

    private void RegisterEvents()
    {
        EventRegistry.RegisterEvent("OnPlayerDeath");
        EventSubscriber.SubscribeToEvent("OnPlayerDeath", OnPlayerDeath);

        EventRegistry.RegisterEvent("OnFoodEaten");
        EventSubscriber.SubscribeToEvent("OnFoodEaten", OnFoodEaten);

        EventRegistry.RegisterEvent("OnPlayerMove");
        EventSubscriber.SubscribeToEvent("OnPlayerMove", OnPlayerMove);
        
        EventRegistry.RegisterEvent("OnScoreIncrease");
        EventSubscriber.SubscribeToEvent("OnScoreIncrease", OnScoreIncrease);

        EventRegistry.RegisterEvent("OnSnakeMove");
        EventSubscriber.SubscribeToEvent("OnSnakeMove", OnSnakeMove);

        EventRegistry.RegisterEvent("OnGameMasterLoaded");
    }
    private void OnFoodEaten(object sender, object obj)
    {
        if (obj is GameObject gO)
        {
            model.EscolhePosicao(model.food.gameObject);
            if(gO.tag == "Snake")
            {
                model.snake.Grow();
            }
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
    
    private void OnSnakeMove(object sender, object obj)
    {
        if (obj is SnakeController snakeController){
            model.SwitchOccupiedPosition(snakeController.gameObject);
        }
    }

    private void OnScoreIncrease(object sender, object obj)
    {
        if (obj is PlayerController player)
        {
            view.DisplayNewScore(player.GetScore(), player.GetHighScore());
        }
    }

    private void OnPlayerDeath(object sender, object obj)
    {
        view.PlayerDied();
    }

    public void OnQuit()
    {
        Debug.Log("Application quit!");
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        SaveSystem.Save(FindObjectOfType<PlayerModel>().GetHighScore());
    }

    // Quando o sistema operativo detecta que há pouca memoria disponivel o unity chama este metodo
    // Aqui podemos avisar o utilizador que pode perder o seu progresso
    public void OnLowMemory()
    {
        Debug.Log("OnLowMemory was Called!");
    }
}