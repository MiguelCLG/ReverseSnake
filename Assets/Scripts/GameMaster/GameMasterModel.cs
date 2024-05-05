using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Linq;
using UnityEngine;

public class GameMasterModel: MonoBehaviour
{
    private GameMasterController controller;
    private GameMasterView view;
    private static GameMasterModel Instance;
    private GameGrid grid;

    [SerializeField] public FoodController food;
    [SerializeField] public PlayerController player;
    [SerializeField] public SnakeController snake;

    [SerializeField] public Vector2 gridSize = Vector2.zero;
    [SerializeField] public Vector2 cellSize = Vector2.zero;

    private Dictionary<string, Vector2> occupiedCells = new Dictionary<string, Vector2>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);
        

        // Criação da grid
        Vector2 originalGridPosition = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

        grid = new GameGrid(gridSize, cellSize, originalGridPosition);

    /* 
        Para já estamos a criar uma dependencia entre o Game Master e os objectos Food, Player e Snake.
        Como o jogo é pequeno e não tem muitos elementos, podemos deixar como está. 
        Mas se quisermos realmente desacolpolar tudo, teremos de registar um evento para cada um deles e subscrever nos respectivos controllers
     */
        food = FindObjectOfType<FoodController>();
        player = FindObjectOfType<PlayerController>();
        snake = FindObjectOfType<SnakeController>();
    }
    private void Start()
    {
        controller = GetComponent<GameMasterController>();
        view = GetComponent<GameMasterView>();
    }

    public void ConstroiJogo() {

        // Escolhe as primeiras posicoes para o inicio do jogo
        EscolhePosicao(player.gameObject);
        EscolhePosicao(snake.gameObject);
        EscolhePosicao(food.gameObject);

        // diz à snake para começar o jogo (neste caso, calcula o caminho)

    }
    public void EscolhePosicao(GameObject objecto)
    {
        Vector2 newGridPosition = grid.GetRandomGridPosition();

        while (occupiedCells.ContainsValue(newGridPosition))
        {
            newGridPosition = grid.GetRandomGridPosition();
        }

        occupiedCells.Remove(objecto.tag);
        occupiedCells.Add(objecto.tag, newGridPosition);

        if(objecto.CompareTag("Player"))
            player.SetPosition(newGridPosition);
        else if(objecto.CompareTag("Snake"))
            snake.SetPosition(newGridPosition);
        else if (objecto.CompareTag("Food"))
            food.SetPosition(newGridPosition);
    }

    public void SwitchOccupiedPosition(GameObject objecto)
    {
        occupiedCells.Remove(objecto.tag);
        occupiedCells.Add(objecto.tag, objecto.transform.position);
    }

    public void AumentaPontuacao() { }

}