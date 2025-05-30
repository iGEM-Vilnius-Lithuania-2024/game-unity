﻿using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BattleManager : MonoBehaviour
{
    public Player player;
    public Canvas canvas;
    
    public GameObject target;
    public GameObject verticalLine;
    public GameObject horizontalLine;
    public GameObject targetArea;
    public GameObject aim;
    public GameObject battleBacteriaCanvas;
    
    public GameObject victoryPopup;
    public TMP_Text rewardTitle;
    public Image reward;
    public Image perkIcon;
    public TMP_Text perkBonus;
    public TMP_Text perkDescription;
    public GameObject defeatPopup;

    public AudioSource damageSound;
    public AudioSource victorySound;
    public AudioSource defeatSound;
    
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
        _spawnedObject = Instantiate(objectToSpawn);
        
        GameObject battleBacteriaCanvasObject = Instantiate(battleBacteriaCanvas, new Vector3(0, 100, 0), new Quaternion(0f, 0f, 0f, 1f));
        battleBacteriaCanvasObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = MainManager.Instance.selectedBacteria.name;
        
        _spawnedObject.tag = "bacteria";
        Bacteria bacteriaScript = _spawnedObject.AddComponent<Bacteria>();
        bacteriaScript.healthBar = battleBacteriaCanvasObject.transform.GetChild(0).GetComponent<HealthBar>();
    }
    
    private void PlaceBackground(GameObject vGameObject, GameObject hGameObject)
    {
        Instantiate(vGameObject);
        Instantiate(hGameObject);
    }
    
    private void SpawnObjects()
    {
        PlaceBacteria(Resources.Load<GameObject>("Bacterias/" + MainManager.Instance.selectedBacteria.name));
        switch (MainManager.Instance.detectedSurface)
        {
            case Surface.Human:
                PlaceBackground(humanBackgroundVertical, humanBackgroundHorizontal);
                break;
            case Surface.Water:
                PlaceBackground(waterBackgroundVertical, waterBackgroundHorizontal);
                break;
            case Surface.Animal:
                PlaceBackground(animalBackgroundVertical, animalBackgroundHorizontal);
                break;
            case Surface.Soil:
                PlaceBackground(soilBackgroundVertical, soilBackgroundHorizontal);
                break;
            case Surface.Plant:
                PlaceBackground(plantBackgroundVertical, plantBackgroundHorizontal);
                break;
            case Surface.Food:
                PlaceBackground(foodBackgroundVertical, foodBackgroundHorizontal);
                break;
        }
    }

    public IEnumerator PlayerDied()
    {
        player.currentHealth = 1;
        defeatSound.Play();
        SaveSystem.SaveScanInfo(DateTime.Now, ScanInfoStatic.scanPosition);
        
        target.SetActive(false);
        verticalLine.SetActive(false);
        horizontalLine.SetActive(false);
        targetArea.SetActive(false);
        target.SetActive(false);
        aim.SetActive(false);
        canvas.GetComponentInChildren<SlideTarget>().Destroy();

        defeatPopup.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        MainManager.Instance.SwitchMapScene();
    }
    
    public void BacteriaDied()
    {
        victorySound.Play();
        SaveSystem.SaveScanInfo(DateTime.Now, ScanInfoStatic.scanPosition);
        SaveSystem.SaveCollectionId(MainManager.Instance.selectedBacteria.id);
        target.SetActive(false);
        verticalLine.SetActive(false);
        horizontalLine.SetActive(false);
        targetArea.SetActive(false);
        target.SetActive(false);
        aim.SetActive(false);
        canvas.GetComponentInChildren<SlideTarget>().Destroy();

        GiveReward();
        victoryPopup.SetActive(true);
    }
    
    private void GiveReward()
    {
        float random = Random.Range(0.0f, 100.0f);
        int rarity;
        if (random <= 90.0)
        {
            rarity = 0;
        } else if (random <= 96.0)
        {
            rarity = 1;
        } else if (random <= 99.0)
        {
            rarity = 2;
        } else
        {
            rarity = 3;
        }
        
        random = Random.Range(0f, 5f);
        int itemType;
        if (random <= 1.0)
        {
            itemType = 0;
        } else if (random <= 2.0)
        {
            itemType = 1;
        } else if (random <= 3.0)
        {
            itemType = 2;
        } else if (random <= 4.0)
        {
            itemType = 3;
        } else
        {
            itemType = 4;
        }
        
        ItemKey newItem = new ItemKey(new Tuple<int, int>(itemType, rarity), false, -1);
        player.giveItem(newItem);
        reward.sprite = Resources.Load<Sprite>(Items.items[newItem.id].iconPath);
        Item item = Items.items[newItem.id];
        int index = item.name.IndexOf('(');
        if (index != -1)
        {
            rewardTitle.text = item.name.Substring(0, index);
        }
        else
        {
            rewardTitle.text = item.name;
        }
        if (item.type == ItemType.Promoter)
        {
            perkIcon.gameObject.SetActive(true);
            perkIcon.sprite = Resources.Load<Sprite>(((PromoterItem)item).boostIconPath);
            perkIcon.transform.localScale = new Vector3(1f, 1f, 1f);
            perkBonus.text = "+" + ((PromoterItem)item).slotsUnlocked;
            perkDescription.text = "Unlocks " + ((PromoterItem)item).slotsUnlocked + " extra gene slots";
        }
        else if (item.type == ItemType.Ori)
        {
            perkIcon.gameObject.SetActive(false);
            perkBonus.text = "X" + ((OriItem)item).multiplier;
            perkDescription.text = "Multiplies all genes bonus by " + ((OriItem)item).multiplier;
        } 
        else
        {
            perkIcon.gameObject.SetActive(true);
            perkIcon.sprite = Resources.Load<Sprite>(((GeneItem)item).boostIconPath);
            perkIcon.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
            perkBonus.text = "+" + ((GeneItem)item).boost;
            perkDescription.text = "Increases " + ((GeneItem)item).attribute + " by " + ((GeneItem)item).boost;
        }
    }
}