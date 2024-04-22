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
        model = new GameMasterModel();
        view = new GameMasterView();
    }

    public static GameMasterController GetInstance(string value)
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
                    instance = new GameMasterController();
                    instance.Value = value;
                }
            }
        }
    }


}