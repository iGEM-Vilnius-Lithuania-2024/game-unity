using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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
    public Image reward;
    public GameObject defeatPopup;
    
    public GameObject humanBacteria;
    public GameObject animalBacteria;
    public GameObject soilBacteria;
    public GameObject waterBacteria;
    public GameObject plantBacteria;
    public GameObject foodBacteria;
    public GameObject humanBackgroundVertical;
    public GameObject humanBackgroundHorizontal;
    public GameObject animalBackgroundVertical;
    public GameObject animalBackgroundHorizontal;
    public GameObject soilBackgroundVertical;
    public GameObject soilBackgroundHorizontal;
    public GameObject waterBackgroundVertical;
    public GameObject waterBackgroundHorizontal;
    public GameObject plantBackgroundVertical;
    public GameObject plantBackgroundHorizontal;
    public GameObject foodBackgroundVertical;
    public GameObject foodBackgroundHorizontal;
    
    
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
            case Surface.Human:
                PlaceBacteria(humanBacteria);
                PlaceBackground(humanBackgroundVertical, humanBackgroundHorizontal);
                break;
            case Surface.Water:
                PlaceBacteria(waterBacteria);
                PlaceBackground(waterBackgroundVertical, waterBackgroundHorizontal);
                break;
            case Surface.Animal:
                PlaceBacteria(animalBacteria);
                PlaceBackground(animalBackgroundVertical, animalBackgroundHorizontal);
                break;
            case Surface.Soil:
                PlaceBacteria(soilBacteria);
                PlaceBackground(soilBackgroundVertical, soilBackgroundHorizontal);
                break;
            case Surface.Plant:
                PlaceBacteria(plantBacteria);
                PlaceBackground(plantBackgroundVertical, plantBackgroundHorizontal);
                break;
            case Surface.Food:
                PlaceBacteria(foodBacteria);
                PlaceBackground(foodBackgroundVertical, foodBackgroundHorizontal);
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
            itemType = ItemType.Promoter;
        } else if (random <= 2.0)
        {
            itemType = ItemType.Genes1;
        } else if (random <= 3.0)
        {
            itemType = ItemType.Genes2;
        } else
        {
            itemType = ItemType.Ori;
        }
        
        Item item = new Item(itemType, rarity);
        player.giveItem(item);
        reward.sprite = Resources.Load<Sprite>("items/" + item.type.Name + "_" + item.rarity);
    }
}