using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodView : MonoBehaviour, IView
{
    private static FoodView Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);

    }

    public void DisplayView(Vector2 newPosition)
    {
        transform.position = newPosition;
    }
}
