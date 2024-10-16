﻿
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Mapbox.Utils;
using UnityEngine;
using Mapbox.Json;
using Unity.VisualScripting;

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
    
    public static void SaveCollectionId(int id)
    {
        CollectionSave collectionSave = LoadCollection();
        if (!collectionSave.ids.Contains(id))
        {
            collectionSave.ids.Add(id);
            SaveCollection(collectionSave);
        }
    }
    
    public static void SaveCollection(CollectionSave collection)
    {
        string path = Application.persistentDataPath + "/collection.json";
        
        string jsonString = JsonConvert.SerializeObject(collection);
        
        File.WriteAllText(path, jsonString);
    }
    
    public static CollectionSave LoadCollection()
    {
        string path = Application.persistentDataPath + "/collection.json";

        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);

            CollectionSave collection = JsonConvert.DeserializeObject<CollectionSave>(jsonString);
            return collection;
        }
        
        return new CollectionSave();
    }
    
    public static void CloseTip(int id)
    {
        TipsInfo tipsInfo = LoadTips();
        if (!tipsInfo.ids.Contains(id))
        {
            tipsInfo.ids.Add(id);
            SaveTips(tipsInfo);
        }
    }
    
    public static Boolean IsTipOpen(int id)
    {
        TipsInfo tipsInfo = LoadTips();
        return !tipsInfo.ids.Contains(id);
    }
    
    public static void SaveTips(TipsInfo tips)
    {
        string path = Application.persistentDataPath + "/tipsInfo.json";
        
        string jsonString = JsonConvert.SerializeObject(tips);
        
        File.WriteAllText(path, jsonString);
    }
    
    public static TipsInfo LoadTips()
    {
        string path = Application.persistentDataPath + "/tipsInfo.json";

        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);

            TipsInfo tipsInfo = JsonConvert.DeserializeObject<TipsInfo>(jsonString);
            return tipsInfo;
        }
        TipsInfo newTipsInfo = new TipsInfo();
        newTipsInfo.ids = new List<int>();
        SaveTips(newTipsInfo);
        
        return newTipsInfo;
    }
}