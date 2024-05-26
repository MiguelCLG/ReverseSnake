using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        foreach (Transform child in transform)
        {
            var aux = grid.CalculateGridCoordinates(child.position);
            child.position = grid.CalculateMapPosition(previousPosition);
            snakeBodyPositions.AddLast(previousPosition);
            previousPosition = aux;
        }
        model.snakeBodyPositions = snakeBodyPositions;
    }
}
