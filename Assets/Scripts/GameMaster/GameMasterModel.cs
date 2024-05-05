using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class GameMasterModel: MonoBehaviour
{
    private GameMasterController controller;
    private GameMasterView view;
    private static GameMasterModel Instance;
    private GameGrid grid;

    [SerializeField] FoodController food;
    [SerializeField] PlayerController player;
    [SerializeField] SnakeController snake;

    [SerializeField] public Vector2 gridSize = Vector2.zero;
    [SerializeField] public Vector2 cellSize = Vector2.zero;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);
        
        Vector2 originalGridPosition = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

        grid = new GameGrid(gridSize, cellSize, originalGridPosition);
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
        /*
            - Ir buscar uma posicao para a snake
            - Ir buscar uma posicao para o player
            - Ir buscar uma posicao para a comida
            - atribuir posicoes por eventos
         */
        EscolhePosicaoComida();
    }
    public void EscolhePosicaoComida() {
        // FAzer random de uma posicao da grid
        // chamar o controller da comida e atribuir a nova posicao

        System.Random rng = new System.Random();
        int newX = rng.Next(0, Mathf.RoundToInt(grid.gridSize.x)); 
        int newY = rng.Next(0, Mathf.RoundToInt(grid.gridSize.y)); 

        Vector3 newPos = new Vector3(newX, newY, 0);
        Vector2 newGridPos = grid.CalculateMapPosition(newPos);

        // TODO: Falta verificar se a posição já tem alguém

        food.SetPosition(newGridPos);
    }
    public void AumentaPontuacao() { }

}