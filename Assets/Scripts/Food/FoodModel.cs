using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodModel : MonoBehaviour
{
    private static FoodModel Instance { get; set; }
    private FoodView view;
    private FoodController controller;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);

    }
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<FoodView>();
        controller = GetComponent<FoodController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
