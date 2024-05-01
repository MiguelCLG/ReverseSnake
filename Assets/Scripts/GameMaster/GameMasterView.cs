using System;
using System.Threading;

public class GameMasterView
{
    private GameMasterModel model;
    private static GameMasterView instance;
    private static readonly object _lock = new object();
    public string Value { get; set; }

    private GameMasterView()
    {
        model = GameMasterModel.GetInstance("model");
    }

    public static GameMasterView GetInstance(string value)
    {
        /* 
         * criamos um singleton neste caso porque apenas existira apenas uma View de GameMaster e podemos criar ligacoes entre o Model, View e Controller mais facilmente 
         * usamos _lock para poder fazer o singleton "thread-safe"
         * https://refactoring.guru/pt-br/design-patterns/singleton/csharp/example#example-1
         */

        if (instance == null)
        {
            lock (_lock)
            {
                instance = new GameMasterView();
                instance.Value = value;
            }
        }
        return instance;
    }

    public void MostraJogo() { }
    public void MostraEstado() { }
}