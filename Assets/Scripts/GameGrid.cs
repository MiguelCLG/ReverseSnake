using System;
using System.Collections.Generic;
using UnityEngine;



// Esta classe é um helper que cria uma grid do ecrão do jogo
public class GameGrid
{
    public Vector2 gridSize { get; set; } = new Vector2(50, 50);
    public Vector2 cellSize { get; set; } = new Vector2(32, 32);

    // A origem d agrid
    private Vector2 originalPosition;

    private Vector2 halfCellSize;

    private static GameGrid Instance;

    public Dictionary<string, Vector2> occupiedCells = new Dictionary<string, Vector2>();


    public GameGrid() {
        Instance = this;        
        halfCellSize = cellSize / 2;
    }


    public GameGrid(Vector2 gridSize, Vector2 cellSize, Vector2 originalPosition)
    {
        Instance = this;
        this.gridSize = gridSize;
        this.cellSize = cellSize;
        this.originalPosition = originalPosition;
        halfCellSize = cellSize / 2;
    }

    // Metodo para termos sempre disponivel um Singleton da classe
    public static GameGrid getInstance()
    {
        return Instance;
    }

    // Esta função recebe uma posição Vector 2 fora da grid e transforma-a numa posição da grid
    public Vector2 CalculateMapPosition(Vector2 position)
    {
        return (position + originalPosition) * cellSize + halfCellSize;
    }

    // Obtem uma Obtem uma posição ao calhas na grid
    public Vector2 GetRandomGridPosition()
    {
        System.Random rng = new System.Random();
        int newX = rng.Next(0, Mathf.RoundToInt(gridSize.x));
        int newY = rng.Next(0, Mathf.RoundToInt(gridSize.y));

        Vector3 newPos = new Vector3(newX, newY, 0);
        Vector2 newGridPosition = CalculateMapPosition(newPos);

        return newPos;
    }

    // Dada um posição na grid retorna essa posição no sistema de coordenadas reais
    public Vector2 CalculateGridCoordinates(Vector2 mapPosition) {
        Vector2 gridPosition = new Vector2(
                Mathf.Floor(((mapPosition - originalPosition) / cellSize).x), Mathf.Floor(((mapPosition - originalPosition) / cellSize).y)
            );
        return gridPosition;
    }

    // Mantém uma posição dentro do ecrã
    // Esta montada de forma que se possa teleportar nas pontas do ecrã
    // Ou seja um objecto que saia pelo direita do ecra voltará a aparecer na esquerda
    public Vector2 ClampOnScreen(Vector2 position)
    {
        var cellPosition = CalculateGridCoordinates(position);

        if(cellPosition.x< 0 )
            cellPosition.x = gridSize.x - 1;
        else if(cellPosition.y < 0)
            cellPosition.y = gridSize.y - 1;
        else if(cellPosition.x > gridSize.x -1 )
            cellPosition.x = 0;
        else if (cellPosition.y > gridSize.y -1 )
            cellPosition.y = 0;

        return CalculateMapPosition(cellPosition);
    }

}
