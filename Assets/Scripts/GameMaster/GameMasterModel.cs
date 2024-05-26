using System;
using System.Collections.Generic;
using System.Linq;
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


    // Singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);
        

        // Cria��o da grid
        Vector2 originalGridPosition = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

        grid = new GameGrid(gridSize, cellSize, originalGridPosition);

    /* 
        Para j� estamos a criar uma dependencia entre o Game Master e os objectos Food, Player e Snake.
        Como o jogo � pequeno e n�o tem muitos elementos, podemos deixar como est�. 
        Mas se quisermos realmente desacolpolar tudo, teremos de registar um evento para cada um deles e subscrever nos respectivos controllers
     */
        food = FindObjectOfType<FoodController>();
        player = FindObjectOfType<PlayerController>();
        snake = FindObjectOfType<SnakeController>();
    }

    // Chamado antes do primeiro frame
    private void Start()
    {
        controller = GetComponent<GameMasterController>();
        view = GetComponent<GameMasterView>();
    }

    // Metodo que constroi o jogo
    public void ConstroiJogo() {

        // Escolhe as primeiras posicoes para o inicio do jogo
        EscolhePosicao(player.gameObject);
        EscolhePosicao(snake.gameObject);
        EscolhePosicao(food.gameObject);

        // diz � snake para come�ar o jogo (neste caso, calcula o caminho)

    }

    // Metodo que escolhe uma posição para um objecto
    public void EscolhePosicao(GameObject objecto)
    {
        Vector2 newGridPosition = grid.GetRandomGridPosition();

        while (grid.occupiedCells.ContainsKey(newGridPosition))
        {
            newGridPosition = grid.GetRandomGridPosition();
        }

        // try catch here please
        var objectInGrid = grid.occupiedCells.FirstOrDefault((oc) => oc.Value == objecto.tag);
        grid.occupiedCells.Remove(objectInGrid.Key);
        grid.occupiedCells.Add(newGridPosition, objecto.tag);
        
        var newWorldPosition = grid.CalculateMapPosition(newGridPosition);

        if (objecto.CompareTag("Player"))
            player.SetPosition(newWorldPosition);
        else if(objecto.CompareTag("Snake"))
            snake.SetPosition(newWorldPosition);
        else if (objecto.CompareTag("Food"))
            food.SetPosition(newWorldPosition);
    }

    // Metodo que troca a posição de um objecto
    public void SwitchOccupiedPosition(GameObject objecto)
    {   
        if(objecto.tag == "Snake")
        {
            var aux = new Dictionary<Vector2, string>();
            foreach(var oc in grid.occupiedCells)
            {
                if(oc.Value != "Snake")
                {
                    aux.Add(oc.Key, oc.Value);
                }
            }
            grid.occupiedCells = aux;
            foreach(Transform body in objecto.transform)
            {
                var newGridPosition = grid.CalculateGridCoordinates(body.position);
                if(grid.occupiedCells.ContainsKey(newGridPosition) && grid.occupiedCells[newGridPosition] == "Food" )
                {
                    grid.occupiedCells.Remove(newGridPosition);
                }
                grid.occupiedCells.Add(newGridPosition, "Snake");
            }
            return;
        }

        var tag = grid.occupiedCells.Where((e) => e.Value.Contains(objecto.tag)).Select(pair => pair.Key);

        grid.occupiedCells.Remove(tag.Single());

        var newPosition = grid.CalculateGridCoordinates(objecto.transform.position);
        if (grid.occupiedCells.ContainsKey(newPosition) && grid.occupiedCells[newPosition] == "Food")
        {
            grid.occupiedCells.Remove(newPosition);
        }
        grid.occupiedCells.Add(grid.CalculateGridCoordinates(objecto.transform.position), objecto.tag);

        /*
            occupiedCells = {
                (1,1): "Snake",
                (2,2): "Player",
                (3,3): "Food", 
                (0,1): "Snake",
                (0,0): "Snake",

            }

         */
    }

    public void AumentaPontuacao() { }

}