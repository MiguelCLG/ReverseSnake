using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodView : MonoBehaviour
{
    private static FoodView Instance { get; set; }

    private FoodModel model;

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
        model = GetComponent<FoodModel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
