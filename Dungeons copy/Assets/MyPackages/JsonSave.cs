using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonSave
{
    public static string gameConfigsFolderPath = Application.dataPath + @"\configs";

    public static void SaveToFile<T>(T dataToSave,string fileName)
    {
        string json = JsonUtility.ToJson(dataToSave);

        if (!Directory.Exists(gameConfigsFolderPath))
        {
            Directory.CreateDirectory(gameConfigsFolderPath);
        }
        File.WriteAllText(gameConfigsFolderPath + $"\\{fileName}.json", json);
    }

    public static T GetDataFromJson<T>(string filename)
    {
        T data = default;
        string json;
        string path = $"{gameConfigsFolderPath}\\{filename}.json";
        if (File.Exists(path))
        {
            json = File.ReadAllText(path);
            data = JsonUtility.FromJson<T>(json);
        }
        return data;
    }

}
