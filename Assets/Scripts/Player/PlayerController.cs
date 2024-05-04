using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController: MonoBehaviour
{
    private static PlayerController instance;
    private PlayerModel model;
    private PlayerView view;

    private void Awake() {
        if (instance == null) {instance = this; }
        else Destroy(this);
    }

    public void Start()
    {
        model = GetComponent<PlayerModel>();
        view = GetComponent<PlayerView>();

    }

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

    public void MovePlayer(Vector2 direction)
    {
        // Movimenta o jogador e atualiza a visualização
        model.Move(direction);
    }

    public void PlayerEatsFood(int scoreValue)
    {
        // Adiciona pontos e atualiza a visualização
        model.AddScore(scoreValue);
        // view.DisplayPlayer();
    }

    public void PlayerDies()
    {
        // Atualiza o estado para morto e atualiza a visualização
        model.Die();
        view.DisplayDeath();
    }
}
