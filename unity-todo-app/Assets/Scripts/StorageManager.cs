using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class StorageManager
{

    private static string masterKey = "e82d33663b9995d23cd93a7a68b782";

    public static Dictionary<string, string> GetAll()
    {
        if (!PlayerPrefs.HasKey(masterKey)) {
            return null;
        }
        string value = PlayerPrefs.GetString(masterKey);
        return Deserialize<Dictionary<string, string>>(value);
    }

    public static void Save(Dictionary<string, string> taskList) {
        PlayerPrefs.SetString(masterKey, Serialize<Dictionary<string, string>>(taskList));
    }

    private static T Deserialize<T>(string value) where T : class
    {
        byte[] bytes = Convert.FromBase64String(value);
        MemoryStream stream = new MemoryStream(bytes);
        BinaryFormatter formatter = new BinaryFormatter();
        return formatter.Deserialize(stream) as T;
    }

    private static string Serialize<T>(T data) where T : class
    {
        var memoryStream = new MemoryStream();
        var formatter = new BinaryFormatter();
        using (memoryStream)
        {
            formatter.Serialize(memoryStream, data);
        }
        return Convert.ToBase64String(memoryStream.ToArray());
    }
}
