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
        transform.position = newPosition;
    }

    // Método para exibir o status do jogador como morto na interface do usuário.
    public void DisplayDeath()
    {
        // Placeholder para código de renderização real.
        Debug.Log("Player is dead");
        EventRegistry.GetEventPublisher("OnPlayerDeath").RaiseEvent(this);
    }
}
