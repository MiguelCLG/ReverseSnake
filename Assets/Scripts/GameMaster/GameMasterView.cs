using System;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using UnityEngine;

public class GameMasterView : MonoBehaviour
{
    private GameMasterModel model;
    private static GameMasterView Instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] public GameObject gameOverPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);
    }

    private void Start()
    {
        model = GetComponent<GameMasterModel>();
        DrawGrid();
    }

    // Metodo que desenha a grid, apenas para efeitos de debug
    private void DrawGrid()
    {
        int rowStart = Mathf.RoundToInt(transform.position.x);
        int columnStart = Mathf.RoundToInt(transform.position.y);

        for (int row = rowStart; row < rowStart + (model.gridSize.x * model.cellSize.x); row += (int)model.cellSize.x)
        {
            for (int column = columnStart; column < columnStart + (model.gridSize.y * model.cellSize.y); column += (int)model.cellSize.y)
            {
                var color = Color.white;

                // Direita
                var start = new Vector2(row, column);
                var end = new Vector2(row, column + model.cellSize.x);
                Debug.DrawLine(start, end, color, 100f);

                // Esquerda
                start = new Vector2(row + model.cellSize.y, column);
                end = new Vector2(row + model.cellSize.y, column + model.cellSize.x);
                Debug.DrawLine(start, end, color, 100f);

                // Baixo
                start = new Vector2(row, column);
                end = new Vector2(row + model.cellSize.y, column);
                Debug.DrawLine(start, end, color, 100f);

                // Cima
                start = new Vector2(row, column + model.cellSize.x);
                end = new Vector2(row + model.cellSize.y, column + model.cellSize.x);
                Debug.DrawLine(start, end, color, 100f);

            }

        }
    }

    public void CloseWindow()
    {
        gameOverPanel.SetActive(false);
    }

    // Metodo chamado quando o score aumenta
    public void DisplayNewScore(int newScore, int newHighScore)
    {
        scoreText.text = $"Score: {newScore}";
        highScoreText.text = $"HighScore: {newHighScore}";
    }

    // Metodo chamado quando o player morre
    public void PlayerDied()
    {
        gameOverPanel.SetActive(true);
    }
}
