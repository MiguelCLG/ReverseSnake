using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerView : MonoBehaviour, IView
{
    private PlayerModel model;              
    private static PlayerView instance;     

    // Garante que apenas uma instância dessa classe exista.
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

    // Inicializa a referência ao modelo.
    public void Start()
    {
        model = GetComponent<PlayerModel>();  
    }

    // Atualiza a posição do jogador na interface do usuário.
    public void DisplayView(Vector2 newPosition)
    {
        transform.position = newPosition;  
    }

    // Exibe visualmente o estado de "morte" do jogador e notifica outros componentes.
    public void DisplayDeath()
    {
        EventRegistry.GetEventPublisher("OnPlayerDeath").RaiseEvent(this);
    }
}
