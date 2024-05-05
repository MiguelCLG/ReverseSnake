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

    //função do Unity para quando o objecto entra em contacto com outro
    //caso a snake ou o player entrem em contacto chama esta funcao
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventRegistry.GetEventPublisher("OnFoodEaten").RaiseEvent(collision.gameObject);
    }
}
