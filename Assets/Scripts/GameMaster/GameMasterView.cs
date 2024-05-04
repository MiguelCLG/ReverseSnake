using System;
using System.Threading;
using UnityEngine;

public class GameMasterView: MonoBehaviour
{
    private GameMasterModel model;
    private static GameMasterView Instance;

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
    }


    public void MostraJogo() { }
    public void MostraEstado() { }
}