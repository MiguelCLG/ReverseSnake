using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour, IController
{
    private static PlayerController instance;
    private PlayerModel model;
    private PlayerView view;

    // Garante que existe apenas uma instância deste controller
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

    // Inicialização
    public void Start()
    {
        model = GetComponent<PlayerModel>();
        view = GetComponent<PlayerView>();
        SubscribeToEvents();
    }

    // Subscreve em eventos relevantes usando um sistema de eventos
    private void SubscribeToEvents()
    {
        EventSubscriber.SubscribeToEvent("OnFoodEaten", OnFoodEaten);
    }
    public void UnsubscribeEvents()
    {
        EventSubscriber.UnsubscribeFromEvent("OnFoodEaten", OnFoodEaten);
    }
    // Atualiza a cada frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            MovePlayer(Vector2.left);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            MovePlayer(Vector2.down);
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            MovePlayer(Vector2.up);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            MovePlayer(Vector2.right);
    }

    // Movimenta o jogador na direção especificada
    public void MovePlayer(Vector2 direction)
    {
        model.Move(direction);
        EventRegistry.GetEventPublisher("OnPlayerMove").RaiseEvent(this);
    }

    // Processa a ação de comer comida e adiciona pontos
    public void PlayerEatsFood(int scoreValue)
    {
        model.AddScore(scoreValue);
        EventRegistry.GetEventPublisher("OnScoreIncrease").RaiseEvent(this);
    }

    // Processa a morte do jogador
    public void PlayerDies()
    {
        model.Die();
        view.DisplayDeath();
    }

    // Define a posição visual do jogador
    public void SetPosition(Vector2 newPosition)
    {
        view.DisplayView(newPosition);
    }

    // Retorna a pontuação atual do jogador
    public int GetScore() { return model.GetScore(); }
    public int GetHighScore() { return model.GetHighScore(); }

    // Manipulador de evento para colisões do Unity
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Snake"))
        {
            PlayerDies();
        }
    }

    // Manipulador de evento para quando comida é comida
    private void OnFoodEaten(object sender, object obj)
    {
        if (obj is GameObject gameObj && gameObj.tag == "Player")
        {
            PlayerEatsFood(model.scoreToAdd);
        }
    }
}
