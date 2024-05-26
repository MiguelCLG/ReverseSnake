using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class SnakeModel : MonoBehaviour
{
    // public Vector2 gridSize; // Tamanho do grid
    private GameGrid grid;



    public float moveSpeed = 1.0f; // Velocidade de movimento da snake
    private LinkedList<Vector2> snakeBodyPositions = new LinkedList<Vector2>(); // Lista das posições do corpo
    private Queue<Vector2> pathToFollow = new Queue<Vector2>(); // Caminho para seguir
    private Vector2 currentTarget;

    [SerializeField] private Sprite snakeImage; // Imagem do corpo da snake
    [SerializeField] private Sprite snakeHead; // Imagem da cabeça da snake
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
        Vector2 initialGridPosition = grid.occupiedCells["Snake"];
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

    private void CreateSnakeSprite(Vector2 position, bool isHead)
    {
        GameObject spriteObj = new GameObject("SnakePart");
        spriteObj.transform.position = position;
        spriteObj.transform.SetParent(transform);
        SpriteRenderer renderer = spriteObj.AddComponent<SpriteRenderer>();
        renderer.sprite = isHead ? snakeHead : snakeImage;
        renderer.tag = isHead ? "Snake" : $"Snake_Body_{transform.childCount + 1}";
        
        snakeBodySprites.AddLast(spriteObj);
    }

    private void UpdateSnakeBodyGraphics(Vector2 newPosition)
    {
        //snakeBodyPositions.AddFirst(newPosition);
        //GameObject oldTail = snakeBodySprites.Last.Value;
        //snakeBodySprites.RemoveLast();
        //oldTail.transform.position = newPosition;
        //snakeBodySprites.AddFirst(oldTail);

        view.DisplaySnake(GameGrid.getInstance().ClampOnScreen(grid.CalculateMapPosition(newPosition)));
        EventRegistry.GetEventPublisher("OnSnakeMove").RaiseEvent(controller);
    }

    public void GrowSnake()
    {
        Vector2 tailPosition = snakeBodyPositions.Last.Value;
        snakeBodyPositions.AddLast(tailPosition);
        CreateSnakeSprite(tailPosition, false);
    }

    public void UpdatePathToFood()
    {
            LinkedList<Vector2> path = FindPath(grid.occupiedCells["Snake"], grid.occupiedCells["Food"]);
        
            pathToFollow = new Queue<Vector2>(path);
            if (pathToFollow.Count > 0)
                currentTarget = pathToFollow.Dequeue();
    }

    public class PriorityQueue<T>
    {
        private SortedDictionary<float, Queue<T>> dictionary = new SortedDictionary<float, Queue<T>>();

        public void Enqueue(T item, float priority)
        {
            if (!dictionary.ContainsKey(priority))
            {
                dictionary[priority] = new Queue<T>();
            }
            dictionary[priority].Enqueue(item);
        }

        public T Dequeue()
        {
            var firstPair = dictionary.First();
            var items = firstPair.Value;
            T item = items.Dequeue();
            if (items.Count == 0)
            {
                dictionary.Remove(firstPair.Key);
            }
            return item;
        }

        public int Count
        {
            get
            {
                return dictionary.Sum(x => x.Value.Count);
            }
        }

        public bool Any() => Count > 0;

        // Novo método para verificar se um elemento já está na fila com qualquer prioridade
        public bool Contains(T item)
        {
            return dictionary.Any(pair => pair.Value.Contains(item));
        }
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


    private float Heuristic(Vector2 a, Vector2 b)
    {
        return Vector2.Distance(a, b); 
    }

    private IEnumerable<Vector2> GetNeighbors(Vector2 node)
    {
        List<Vector2> neighbors = new List<Vector2>();
        // Definição correta da variável:
        Vector2[] possibleMoves = { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };

        foreach (Vector2 move in possibleMoves)  // Agora 'possibleMoves' está correto
        {
            Vector2 neighbor = node + move;
            // Checa se a posição é válida e não está fora dos limites do grid
            if (
                    neighbor.x >= 0 && 
                    neighbor.x < grid.gridSize.x && 
                    neighbor.y >= 0 && 
                    neighbor.y < grid.gridSize.y
                )
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
}
