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
        model = new GameMasterModel();
    }

    public static GameMasterView GetInstance(string value)
    {
        /* 
         * criamos um singleton neste caso porque apenas existirá apenas um Model de GameMaster e podemos criar ligações entre o Model, View e Controller mais fácilmente 
         * usamos _lock para poder fazer o singleton "thread-safe"
         * https://refactoring.guru/pt-br/design-patterns/singleton/csharp/example#example-1
         */

        if (instance == null)
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance = new GameMasterView();
                    instance.Value = value;
                }
            }
        }
    }
}