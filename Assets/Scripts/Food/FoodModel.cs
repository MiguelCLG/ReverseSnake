using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodModel : MonoBehaviour
{
    private static FoodModel Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);

    }
}
