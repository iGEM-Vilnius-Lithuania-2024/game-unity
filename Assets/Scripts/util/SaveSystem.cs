
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Mapbox.Utils;
using UnityEngine;
using Mapbox.Json;

public static class SaveSystem
{
    public static void SaveScanInfo(DateTime dateTime, Vector2d position)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/scanInfo.json";
        List<ScanInfo> scanInfos;
        
        if (System.IO.File.Exists(path))
        {
            using (System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
            {
                scanInfos = formatter.Deserialize(stream) as List<ScanInfo>;
            }
        }
        else
        {
            scanInfos = new List<ScanInfo>();
        }
        scanInfos.Add(new ScanInfo(dateTime, position));
        
        using (System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Create))
        {
            formatter.Serialize(stream, scanInfos);
        }
    }

    public static List<ScanInfo> LoadScanInfo()
    {
        string path = Application.persistentDataPath + "/scanInfo.json";
        if (System.IO.File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
            {
                List<ScanInfo> scanInfos = formatter.Deserialize(stream) as List<ScanInfo>;
                return scanInfos;
            }
        }
        
        Debug.LogError("Save file not found in " + path);
        return new List<ScanInfo>();
    }
    
    public static void SavePlayerInfo(Player player)
    {
        string path = Application.persistentDataPath + "/playerInfo.json";
        PlayerInfo playerInfo = new PlayerInfo(player);
        
        string jsonString = JsonConvert.SerializeObject(playerInfo);
        
        File.WriteAllText(path, jsonString);
    }
    
    public static PlayerInfo LoadPlayerInfo()
    {
        string path = Application.persistentDataPath + "/playerInfo.json";

        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);

            PlayerInfo playerInfo = JsonConvert.DeserializeObject<PlayerInfo>(jsonString);
            return playerInfo;
        }
        
        return new PlayerInfo().Initialize();
    }
}