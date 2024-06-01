using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using UnityEngine;

public class SnakeModel : MonoBehaviour
{
    // public Vector2 gridSize; // Tamanho do grid
    private GameGrid grid;

    public float moveSpeed = 1.0f; // Velocidade de movimento da snake
    public LinkedList<Vector2> snakeBodyPositions = new LinkedList<Vector2>(); // Lista das posições do corpo
    private Queue<Vector2> pathToFollow = new Queue<Vector2>(); // Caminho para seguir
    private Vector2 currentTarget;

    // TODO: Devem todos ir para a view
    [SerializeField] private Sprite snakeImage; // Imagem do corpo da snake
    [SerializeField] private Sprite snakeHead; // Imagem da cabeça da snake
    [SerializeField] private GameObject snakePartPrefab;

    // TODO: snakeBodySprites deve ir para a view
    private LinkedList<GameObject> snakeBodySprites = new LinkedList<GameObject>(); // Sprites para o corpo

    private static SnakeModel instance;
    private SnakeView view;
    private SnakeController controller;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        view = GetComponent<SnakeView>();
        controller = GetComponent<SnakeController>();
    }

    public void InitializeSnake(object sender, object obj)
    {
        grid = GameGrid.getInstance();

        // Vamos buscar a posicao inicial da snake na grid
        Vector2 initialGridPosition = grid.occupiedCells.FirstOrDefault((oc) => oc.Value == "Snake").Key;

        //TODO: Referencias ao que for visual (Sprites, CreateSnakeSprite, etc..) devem ir para a view
        snakeBodyPositions.AddFirst(initialGridPosition);

        CreateSnakeSprite(grid.CalculateMapPosition(initialGridPosition), true);
        UpdatePathToFood();
    }

    public void MoveTowardsTarget(object sender, object obj)
    {
        if (pathToFollow.Count > 0)
        {
            currentTarget = pathToFollow.Dequeue();
            MoveSnake(currentTarget);
        }
    }

    public void MoveSnake(Vector2 newPosition)
    {
        UpdateSnakeBodyGraphics(newPosition);
    }


    //TODO: Possivelmente vai para a view
    private void CreateSnakeSprite(Vector2 position, bool isHead)
    {
        GameObject spriteObj = Instantiate(snakePartPrefab);
        spriteObj.transform.position = position;
        spriteObj.transform.SetParent(transform);
        
        SpriteRenderer renderer = spriteObj.GetComponent<SpriteRenderer>();

        renderer.sprite = isHead ? snakeHead : snakeImage;
        renderer.tag = "Snake";
        
        snakeBodySprites.AddLast(spriteObj);
    }

    private void UpdateSnakeBodyGraphics(Vector2 newPosition)
    {
        view.DisplayView(newPosition);
        EventRegistry.GetEventPublisher("OnSnakeMove").RaiseEvent(controller);
    }

    public void GrowSnake()
    {
        Vector2 tailPosition = snakeBodyPositions.Last.Value;

        foreach (Vector2 neighbor in GetNeighbors(tailPosition))
        {
            if(!grid.occupiedCells.ContainsKey(neighbor))
            {
                grid.occupiedCells.Add(neighbor, "Snake");
                snakeBodyPositions.AddLast(neighbor);
                break;
            }
        }

        CreateSnakeSprite(grid.CalculateMapPosition(tailPosition), false);
    }

    public void UpdatePathToFood()
    {
            LinkedList<Vector2> path = FindPath(snakeBodyPositions.First(), grid.occupiedCells.FirstOrDefault((oc) => oc.Value == "Food").Key);
        
            pathToFollow = new Queue<Vector2>(path);
            if (pathToFollow.Count > 0)
                currentTarget = pathToFollow.Dequeue();
    }

    private LinkedList<Vector2> FindPath(Vector2 start, Vector2 goal)
    {
        PriorityQueue<Vector2> openSet = new PriorityQueue<Vector2>();

        Dictionary<Vector2, Vector2> cameFrom = new Dictionary<Vector2, Vector2>();
      
        Dictionary<Vector2, float> gScore = new Dictionary<Vector2, float> { [start] = 0 };
        
        Dictionary<Vector2, float> fScore = new Dictionary<Vector2, float> { [start] = Heuristic(start, goal) };

        List<Vector2> visited = new List<Vector2>();

        openSet.Enqueue(start, fScore[start]);

        while (openSet.Any())
        {
            Vector2 current = openSet.Dequeue();

            if (current.Equals(goal))
                return ReconstructPath(cameFrom, current);

            foreach (Vector2 neighbor in GetNeighbors(current))
            {
                if (grid.occupiedCells.ContainsKey(neighbor) && grid.occupiedCells[neighbor].Equals("Snake"))
                    continue;
                float tentativeGScore = gScore[current] + Vector2.Distance(current, neighbor);
                if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = tentativeGScore + Heuristic(neighbor, goal);
                    if (!openSet.Contains(neighbor) && !visited.Contains(neighbor))
                        openSet.Enqueue(neighbor, fScore[neighbor]);
                }
            }
            visited.Add(current);
        }
        return new LinkedList<Vector2>();
    }

    // Metodo que recebe a nova e a posicao anterior
    // E retorna o novo angulo para que a snake esteja a olhar para onde vai!
    public float CalculateHeadRotation(Vector2 oldPosition, Vector2 newPosition)
    {
        // Calculate the differences in coordinates
        float deltaX = newPosition.x - oldPosition.x;
        float deltaY = newPosition.y - oldPosition.y;

        // Calculate the angle in radians
        float angleInRadians = Mathf.Atan2(deltaY, deltaX);

        // Convert the angle to degrees
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

        return angleInDegrees;
    }

    private float Heuristic(Vector2 a, Vector2 b)
    {
        return Vector2.Distance(a, b); 
    }

    private IEnumerable<Vector2> GetNeighbors(Vector2 node)
    {
        List<Vector2> neighbors = new List<Vector2>();
        Vector2[] possibleMoves = { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };

        foreach (Vector2 move in possibleMoves)
        {
            Vector2 neighbor = node + move;

            // Wrap around logic
            if (neighbor.x < 0) neighbor.x = grid.gridSize.x - 1;
            else if (neighbor.x >= grid.gridSize.x) neighbor.x = 0;
            if (neighbor.y < 0) neighbor.y = grid.gridSize.y - 1;
            else if (neighbor.y >= grid.gridSize.y) neighbor.y = 0;

            neighbors.Add(neighbor);
        }

        return neighbors;
    }


    private LinkedList<Vector2> ReconstructPath(Dictionary<Vector2, Vector2> cameFrom, Vector2 current)
    {
        LinkedList<Vector2> path = new LinkedList<Vector2>();
        path.AddFirst(current);
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.AddFirst(current);
        }
        return path;
    }

    public Queue<Vector2> PathToFollow
    {
        get { return pathToFollow; }
    }
}
