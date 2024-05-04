using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerView: MonoBehaviour
{
    private Transform tranform;
    private PlayerModel model;
    private static PlayerView instance;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else Destroy(this);
    }

    public void Start()
    {
        model = GetComponent<PlayerModel>();
        tranform = GetComponent<Transform>();
  
    }

    // Método para atualizar a visualização do jogador na interface do usuário.
    public void DisplayPlayer(Vector2 newPosition)
    {
        Vector3 pos = new Vector3(newPosition.x, newPosition.y, 0);
        // Placeholder para código de renderização real. Poderia atualizar a posição do jogador na UI.
        Debug.Log($"Player at position {model.position} with score {model.score}");
        tranform.position += pos;
    }

    // Método para exibir o status do jogador como morto na interface do usuário.
    public void DisplayDeath()
    {
        // Placeholder para código de renderização real.
        Debug.Log("Player is dead");
    }
}
