using System;
using System.Threading;
using UnityEngine;

public class PlayerModel: MonoBehaviour
{
    private PlayerView view;
    private PlayerController controller; 
    public Vector2 position;
    private bool isAlive = true;
    public int score = 0;
    private static PlayerModel instance;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        else Destroy(this);
    }

    public void Start()
    {
        view = GetComponent<PlayerView> ();
        controller = GetComponent<PlayerController>();
        position = GetComponent<Transform>().position;

    }

    // Métodos para manipular o estado do jogador
    public void Move(Vector2 direction)
    {
        if (!isAlive)
        {
            return;
        }

        Vector2 newPosition = position + direction;
        view.DisplayPlayer( newPosition );
    }

    public void AddScore(int points)
    {
        if (isAlive)
        {
            score += points;
        }
    }

    public void Die()
    {
        isAlive = false;
    }
}
