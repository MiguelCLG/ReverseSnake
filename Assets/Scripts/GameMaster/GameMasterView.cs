using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class GameMasterView: MonoBehaviour
{
    private GameMasterModel model;
    private static GameMasterView Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);
    }

    private void Start()
    {
        model = GetComponent<GameMasterModel>();
        DrawGrid();
    }

    private void DrawGrid()
    {
        int rowStart = Mathf.RoundToInt(transform.position.x);
        int columnStart = Mathf.RoundToInt(transform.position.y);

        for (int row = rowStart; row < rowStart + (model.gridSize.x * model.cellSize.x); row += (int)model.cellSize.x)
        {
            for (int column = columnStart; column < columnStart + (model.gridSize.y * model.cellSize.y); column += (int)model.cellSize.y)
            {
                var color = Color.white;

                // Direita
                var start = new Vector2(row, column);
                var end = new Vector2(row, column + model.cellSize.x);
                Debug.DrawLine(start, end, color, 100f);

                // Esquerda
                start = new Vector2(row + model.cellSize.y, column);
                end = new Vector2(row + model.cellSize.y, column + model.cellSize.x);
                Debug.DrawLine(start, end, color, 100f);

                // Baixo
                start = new Vector2(row, column);
                end = new Vector2(row + model.cellSize.y, column);
                Debug.DrawLine(start, end, color, 100f);

                // Cima
                start = new Vector2(row, column + model.cellSize.x);
                end = new Vector2(row + model.cellSize.y, column + model.cellSize.x);
                Debug.DrawLine(start, end, color, 100f);

            }

        }
    }
    public void MostraJogo() { }
    public void MostraEstado() { }
}