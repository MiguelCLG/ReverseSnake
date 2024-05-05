using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeView : MonoBehaviour
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



    // Update is called once per frame
    void Update()
    {
        
    }
    //metodo pra mostrar a posicao da snake
    public void DisplaySnake(Vector2 newPosition)
    {
        transform.position = newPosition;
    }
}
