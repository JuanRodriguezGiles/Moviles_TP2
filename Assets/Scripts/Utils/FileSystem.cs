using System.IO;
using UnityEngine;

public class FileSystem : MonoBehaviour
{
    private static void SafeCreateDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            return;
        }

        Directory.CreateDirectory(path);
    }

    public void SaveData(string data)
    {
        SafeCreateDirectory(Application.persistentDataPath + "/" + "Data");
        string json = JsonUtility.ToJson(data);
        StreamWriter write = new StreamWriter(Application.persistentDataPath + "/" + "Data" + "/data.json");
        write.Write(json);
        write.Flush();
        write.Close();
    }

    public string LoadData() // "/data.json"
    {
        //Data acquisition
        var reader = new StreamReader(Application.persistentDataPath + "/" + "Data" + "/data.json");
        string json = reader.ReadToEnd();
        reader.Close();
        return json;
    }
}