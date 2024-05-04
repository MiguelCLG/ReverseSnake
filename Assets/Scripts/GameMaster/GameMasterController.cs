using System;
using UnityEditor;
using UnityEngine;

public class GameMasterController: MonoBehaviour
{
    [SerializeField] private Vector2 gridSize = Vector2.zero;
    [SerializeField] private Vector2 cellSize = Vector2.zero;

    Transform gameMasterTransform;
    private GameMasterModel model;
    private GameMasterView view;
    private static GameMasterController Instance { get; set; }

    private GameGrid grid;
  
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
        gameMasterTransform = GetComponent<Transform>();
        grid = new GameGrid(gridSize, cellSize);

        DrawGrid();
        StartGame();
    }

    private void RegisterEvents()
    {
        EventRegistry.RegisterEvent("OnFoodEaten");
        EventSubscriber.SubscribeToEvent("OnFoodEaten", OnFoodEaten);
    }
    private void DrawGrid()
    {
        int rowStart = Mathf.RoundToInt(gameMasterTransform.position.x);
        int columnStart = Mathf.RoundToInt(gameMasterTransform.position.y);

        for (int row = rowStart; row < rowStart + (gridSize.y * cellSize.y); row += (int)cellSize.y)
        {
            for (int column = columnStart; column < columnStart + (gridSize.x * cellSize.x); column+=(int)cellSize.x)
            {
                var color = Color.white;

                // Direita
                var start = new Vector2(row, column);
                var end = new Vector2(row, column + cellSize.x);
                Debug.DrawLine(start, end, color, 100f);

                // Esquerda
                start = new Vector2(row + cellSize.y, column);
                end = new Vector2(row + cellSize.y, column + cellSize.x);
                Debug.DrawLine(start, end, color, 100f);

                // Baixo
                start = new Vector2(row, column);
                end = new Vector2(row + cellSize.y, column);
                Debug.DrawLine(start, end, color, 100f);

                // Cima
                start = new Vector2(row, column + cellSize.x);
                end = new Vector2(row + cellSize.y , column + cellSize.x);
                Debug.DrawLine(start, end, color, 100f);

            }

        }
    }

    private void OnFoodEaten(object sender, object obj)
    {
        Debug.Log($"OnFoodEaten was called by {obj.GetType()}");
    }

    public void ApresentaNovoEstado() { }

    public void StartGame()
    {
        model.ConstroiJogo();
    }
}