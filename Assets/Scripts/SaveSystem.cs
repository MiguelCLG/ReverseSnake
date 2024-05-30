using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save(int highScore)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "highScore.dat";
        FileStream fs = new FileStream(path, FileMode.Create);
        
        binaryFormatter.Serialize(fs, highScore);

        fs.Close();
    }

    public static int Load() { 
        string path = Application.persistentDataPath + "highScore.dat";

        if(File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            int highScore = (int)binaryFormatter.Deserialize(fs);
            fs.Close();
            return highScore;
        }
        return 0;
    }
}

