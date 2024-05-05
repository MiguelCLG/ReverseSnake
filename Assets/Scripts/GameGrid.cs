using System;
using UnityEngine;
public class GameGrid
{
    public Vector2 gridSize { get; set; } = new Vector2(50, 50);
    public Vector2 cellSize { get; set; } = new Vector2(32, 32);

    private Vector2 originalPosition;

    private Vector2 halfCellSize;

    public GameGrid() {
        halfCellSize = cellSize / 2;
    }
    public GameGrid(Vector2 gridSize, Vector2 cellSize, Vector2 originalPosition)
    {
        this.gridSize = gridSize;
        this.cellSize = cellSize;
        this.originalPosition = originalPosition;
        halfCellSize = cellSize / 2;

    }

    public Vector2 CalculateMapPosition(Vector2 position)
    {
        return (position + originalPosition) * cellSize + halfCellSize;
    }

    public Vector2 GetRandomGridPosition()
    {
        System.Random rng = new System.Random();
        int newX = rng.Next(0, Mathf.RoundToInt(gridSize.x));
        int newY = rng.Next(0, Mathf.RoundToInt(gridSize.y));

        Vector3 newPos = new Vector3(newX, newY, 0);
        Vector2 newGridPosition = CalculateMapPosition(newPos);

        return newGridPosition;
    }
}
