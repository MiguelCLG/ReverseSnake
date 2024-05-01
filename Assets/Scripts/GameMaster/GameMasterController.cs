using System;
using System.Threading;

public class GameMasterController
{
    private GameMasterModel model;
    private GameMasterView view;
    private static GameMasterController instance;
    private static readonly object _lock = new object();
    public string Value { get; set; }

    private GameMasterController()
    {
        model = GameMasterModel.GetInstance("model");
        view = GameMasterView.GetInstance("view");
    }

    public static GameMasterController GetInstance(string value)
    {
        /* 
         * criamos um singleton neste caso porque apenas existira apenas um Controller de GameMaster e podemos criar ligacoes entre o Model, View e Controller mais facilmente 
         * usamos _lock para poder fazer o singleton "thread-safe"
         * https://refactoring.guru/pt-br/design-patterns/singleton/csharp/example#example-1
         */

        if (instance == null)
        {
            lock (_lock)
            {
                instance = new GameMasterController();
                instance.Value = value;
            }
        }
        return instance;
    }

    public void ApresentaNovoEstado() { }
}