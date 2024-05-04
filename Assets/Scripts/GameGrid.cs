using System;
using UnityEngine;
public class GameGrid
{
    public Vector2 gridSize { get; set; } = new Vector2(50, 50);
    public Vector2 cellSize { get; set; } = new Vector2(32, 32);

    private Vector2 halfCellSize;

    public GameGrid() {
        halfCellSize = cellSize / 2;
    }
    public GameGrid(Vector2 size, Vector2 cellSize)
    {
        this.gridSize = size;
        this.cellSize = cellSize;
        halfCellSize = cellSize / 2;
    }

    public Vector2 CalculateMapPosition(Vector2 position)
    {
        return position * cellSize + halfCellSize;
    }
}
