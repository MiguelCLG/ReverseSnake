using System;
using System.Threading;
using UnityEngine;

public class PlayerModel: MonoBehaviour
{
    [SerializeField] public int scoreToAdd = 10;


    private PlayerView view;
    private PlayerController controller; 
    private bool isAlive = true;
    private int score = 0;
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

    }

    // Métodos para manipular o estado do jogador
    public void Move(Vector2 direction)
    {
        if (!isAlive)
        {
            return;
        }
        
        Vector2 newPosition = new Vector2(transform.position.x, transform.position.y) + direction;

        view.DisplayPlayer(GameGrid.getInstance().ClampOnScreen(newPosition));
    }

    public void AddScore(int points)
    {
        if (isAlive)
        {
            score += points;
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void Die()
    {
        isAlive = false;
    }
}
