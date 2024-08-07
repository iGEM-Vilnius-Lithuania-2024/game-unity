using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleManager : MonoBehaviour
{
    public Player player;
    
    public GameObject target;
    public GameObject verticalLine;
    public GameObject horizontalLine;
    public GameObject targetArea;
    public GameObject aim;
    
    public GameObject victoryPopup;
    public GameObject defeatPopup;
    
    public GameObject skinBacteria;
    public GameObject waterBacteria;
    public GameObject woodBacteria;
    public GameObject leafBacteria;
    public GameObject mossBacteria;
    public GameObject rockBacteria;
    public GameObject sandBacteria;
    public GameObject pavementBacteria;
    public GameObject skinBackgroundVertical;
    public GameObject skinBackgroundHorizontal;
    public GameObject waterBackgroundVertical;
    public GameObject waterBackgroundHorizontal;
    public GameObject woodBackgroundVertical;
    public GameObject woodBackgroundHorizontal;
    public GameObject leafBackgroundVertical;
    public GameObject leafBackgroundHorizontal;
    public GameObject mossBackgroundVertical;
    public GameObject mossBackgroundHorizontal;
    public GameObject rockBackgroundVertical;
    public GameObject rockBackgroundHorizontal;
    public GameObject sandBackgroundVertical;
    public GameObject sandBackgroundHorizontal;
    public GameObject pavementBackgroundVertical;
    public GameObject pavementBackgroundHorizontal;
    
    
    private GameObject _spawnedObject;

    private void Start()
    {
        victoryPopup.SetActive(false);
        defeatPopup.SetActive(false);
        SpawnObjects();
    }
    
    private void PlaceBacteria(GameObject objectToSpawn)
    {
        _spawnedObject = Instantiate(objectToSpawn, new Vector3(0, 50, 100), new Quaternion(0f, 0f, 0f, 1f));
        _spawnedObject.transform.localScale = new Vector3(2000, 2000, 2000);
    }
    
    private void PlaceBackground(GameObject vGameObject, GameObject hGameObject)
    {
        Instantiate(vGameObject);
        Instantiate(hGameObject);
    }
    
    private void SpawnObjects()
    {
        switch (MainManager.Instance.detectedSurface)
        {
            case Surface.Skin:
                PlaceBacteria(skinBacteria);
                PlaceBackground(skinBackgroundVertical, skinBackgroundHorizontal);
                break;
            case Surface.Water:
                PlaceBacteria(waterBacteria);
                PlaceBackground(waterBackgroundVertical, waterBackgroundHorizontal);
                break;
            case Surface.Wood:
                PlaceBacteria(woodBacteria);
                PlaceBackground(woodBackgroundVertical, woodBackgroundHorizontal);
                break;
            case Surface.Leaf:
                PlaceBacteria(leafBacteria);
                PlaceBackground(leafBackgroundVertical, leafBackgroundHorizontal);
                break;
            case Surface.Moss:
                PlaceBacteria(mossBacteria);
                PlaceBackground(mossBackgroundVertical, mossBackgroundHorizontal);
                break;
            case Surface.Rock:
                PlaceBacteria(rockBacteria);
                PlaceBackground(rockBackgroundVertical, rockBackgroundHorizontal);
                break;
            case Surface.Sand:
                PlaceBacteria(sandBacteria);
                PlaceBackground(sandBackgroundVertical, sandBackgroundHorizontal);
                break;
            case Surface.Pavement:
                PlaceBacteria(pavementBacteria);
                PlaceBackground(pavementBackgroundVertical, pavementBackgroundHorizontal);
                break;
        }
    }

    public void PlayerDied()
    {
        SaveSystem.SaveScanInfo(DateTime.Now, ScanInfoStatic.scanPosition);
        
        defeatPopup.SetActive(true);
        MainManager.Instance.SwitchMapScene();
    }
    
    public void BacteriaDied()
    {
        SaveSystem.SaveScanInfo(DateTime.Now, ScanInfoStatic.scanPosition);
        target.SetActive(false);
        verticalLine.SetActive(false);
        horizontalLine.SetActive(false);
        targetArea.SetActive(false);
        target.SetActive(false);
        aim.SetActive(false);

        GiveReward();
        victoryPopup.SetActive(true);
    }
    
    private void GiveReward()
    {
        float random = Random.Range(0.0f, 100.0f);
        ItemRarity rarity;
        if (random <= 90.0)
        {
            rarity = ItemRarity.Common;
        } else if (random <= 96.0)
        {
            rarity = ItemRarity.Uncommon;
        } else if (random <= 99.0)
        {
            rarity = ItemRarity.Rare;
        } else if (random <= 99.9)
        {
            rarity = ItemRarity.Epic;
        } else
        {
            rarity = ItemRarity.Legendary;
        }
        
        random = Random.Range(0f, 4f);
        ItemType itemType;
        if (random <= 1.0)
        {
            itemType = ItemType.Type1;
        } else if (random <= 2.0)
        {
            itemType = ItemType.Type2;
        } else if (random <= 3.0)
        {
            itemType = ItemType.Type3;
        } else
        {
            itemType = ItemType.Type4;
        }
        
        Item item = new Item(itemType, rarity);
        player.giveItem(item);
        //TODO: Set reward item icon
    }
}