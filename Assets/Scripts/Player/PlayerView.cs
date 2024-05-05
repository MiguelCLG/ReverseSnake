using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerView: MonoBehaviour
{
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
    }

    // Método para atualizar a visualização do jogador na interface do usuário.
    public void DisplayPlayer(Vector2 newPosition)
    {
        Vector3 pos = new Vector3(newPosition.x, newPosition.y, 0);
        // Placeholder para código de renderização real. Poderia atualizar a posição do jogador na UI.
        Debug.Log($"Player at position {transform.position} with score {model.GetScore()}");

        transform.position = pos;
    }

    // Método para exibir o status do jogador como morto na interface do usuário.
    public void DisplayDeath()
    {
        // Placeholder para código de renderização real.
        Debug.Log("Player is dead");
    }
}
