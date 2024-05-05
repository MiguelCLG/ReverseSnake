using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeModel : MonoBehaviour
{
    private LinkedList<Sprite> snakeBody;
    [SerializeField]private Sprite snakeImage;
    [SerializeField]private Sprite snakeHead;
    private SnakeView view;
    private SnakeController controller;
    private static SnakeModel instance;


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
        view = GetComponent<SnakeView>();
        controller = GetComponent<SnakeController>();
        snakeBody = new LinkedList<Sprite>();
        snakeBody.AddFirst(snakeHead);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CalculaNovoCaminho()
    {
        
    }
    //metodo pra calcular o tamanho da snake
    public void CalculaTamanhoSnake()
    {
        snakeBody.AddAfter(snakeBody.First, snakeImage);
    }
}
