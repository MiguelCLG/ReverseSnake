using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SnakeView : MonoBehaviour, IView
{
    private SnakeModel model;
    private static SnakeView instance;

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

    // Start is called before the first frame update
    void Start()
    {
        model = GetComponent<SnakeModel>();        
    }

    //metodo pra mostrar a posicao da snake
    public void DisplayView(Vector2 newPosition)
    {
        var previousPosition = newPosition;
        var snakeBodyPositions = new LinkedList<Vector2>();
        var grid = GameGrid.getInstance();

        if(transform.childCount > 0) // caso ja haja snake head
        {
            // calcular e aplicar a nova rotação da snake head
            float angleInDegrees = model.CalculateHeadRotation(grid.CalculateGridCoordinates(transform.GetChild(0).position), newPosition);
            transform.GetChild(0).rotation = Quaternion.Euler(0, 0, angleInDegrees);
        }

        foreach (Transform child in transform) // Mover todas as partes da snake para a posicao da anterior parte
        {
            var aux = grid.CalculateGridCoordinates(child.position);
            child.position = grid.CalculateMapPosition(previousPosition);
            snakeBodyPositions.AddLast(previousPosition);
            previousPosition = aux;
        }
        model.snakeBodyPositions = snakeBodyPositions;
    }
}
