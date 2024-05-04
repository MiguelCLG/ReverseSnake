using System;
using System.Threading;
using UnityEngine;

public class GameMasterModel: MonoBehaviour
{
    private GameMasterController controller;
    private GameMasterView view;
    private static GameMasterModel Instance;

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
        controller = GetComponent<GameMasterController>();
        view = GetComponent<GameMasterView>();
    }
    public void ConstroiJogo() {
        /*
            - Ir buscar uma posicao para a snake
            - Ir buscar uma posicao para o player
            - Ir buscar uma posicao para a comida
            - atribuir posicoes por eventos
         */
    }
    public void EscolhePosicaoComida() { }
    public void AumentaPontuacao() { }

}