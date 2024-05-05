using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    private static FoodController Instance { get; set; }
    private FoodView view;
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
        view = GetComponent<FoodView>();
        model = GetComponent<FoodModel>();
    }

    public void SetPosition(Vector3 newPosition)
    {
        view.DisplayFood(newPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventRegistry.GetEventPublisher("OnFoodEaten").RaiseEvent(collision.gameObject);
    }
}
