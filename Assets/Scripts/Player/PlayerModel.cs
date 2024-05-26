using System;
using System.Threading;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] public int scoreToAdd = 10;  
    private PlayerView view;                    
    private PlayerController controller;       
    private bool isAlive = true;               
    private int score = 0;                     
    private static PlayerModel instance;         

    // Inicialização do singleton para garantir uma única instância deste componente.
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

    // Inicializa referências.
    public void Start()
    {
        view = GetComponent<PlayerView>();  
        controller = GetComponent<PlayerController>();  
    }

   
    // Movimenta o jogador na direção especificada, se ele estiver vivo.
    public void Move(Vector2 direction)
    {
        if (!isAlive)
        {
            return;  
        }

        Vector2 newPosition = new Vector2(transform.position.x, transform.position.y) + direction;  
        view.DisplayPlayer(GameGrid.getInstance().ClampOnScreen(newPosition));  
    }

    // Adiciona pontos à pontuação do jogador, se ele estiver vivo.
    public void AddScore(int points)
    {
        if (isAlive)
        {
            score += points;  
        }
    }

    // Retorna a pontuação atual do jogador.
    public int GetScore()
    {
        return score;
    }

    // Define o estado do jogador para "morto".
    public void Die()
    {
        isAlive = false; 
    }
}
