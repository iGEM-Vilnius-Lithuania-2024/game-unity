using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float maxHealth = 100;
    public float currentHealth;
    public float pointIncreasePerMinute = 1;
    public float attackDamage = 20;
    public Item equippedItem1;
    public Item equippedItem2;
    public Item equippedItem3;
    public Item equippedItem4;
    public List<Item> items;
    
    public GameObject damagePopup;
    public HealthBar healthBar;
    public GameObject HP;
    public GameObject ATK;
    public BattleManager battleManager;
    
    private float saveInterval = 60f;
    private float saveTimer;
    private GameObject lose;
    void Start()
    {
        PlayerInfo playerInfo = SaveSystem.LoadPlayerInfo();
        PlayerInfoToPlayer(playerInfo);
        
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(currentHealth);
        }
        
        if (HP != null && ATK != null)
        {
            HP.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = ((int) Math.Round(maxHealth, 0)).ToString();
            ATK.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = ((int) Math.Round(attackDamage, 0)).ToString();
        }
        
        saveTimer = saveInterval;
    }
    
    void Update()
    {
        if (HP != null && ATK != null)
        {
            HP.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = ((int) Math.Round(maxHealth, 0)).ToString();
            ATK.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = ((int) Math.Round(attackDamage, 0)).ToString();
        }
        
        currentHealth += pointIncreasePerMinute * (Time.deltaTime / 60);
        
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0f)
        {
            currentHealth = 1;
            SaveSystem.SavePlayerInfo(this);
            
            battleManager.PlayerDied();
        }

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        saveTimer -= Time.deltaTime;
        if (saveTimer <= 0f)
        {
            SaveSystem.SavePlayerInfo(this);
            saveTimer = saveInterval;
        }
    }

    private void PlayerInfoToPlayer(PlayerInfo playerInfo)
    {
        this.currentHealth = playerInfo.currentHealth + (float)(DateTime.Now - playerInfo.saveTime).TotalMinutes * pointIncreasePerMinute;
        this.equippedItem1 = playerInfo.equippedItem1;
        this.equippedItem2 = playerInfo.equippedItem2;
        this.equippedItem3 = playerInfo.equippedItem3;
        this.equippedItem4 = playerInfo.equippedItem4;
        this.items = new List<Item>(playerInfo.items);
    }

    public void TakeDamage(float damage)
    {
        PopUpDamage((int)Math.Round(damage, 0));
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        SaveSystem.SavePlayerInfo(this);
    }
    
    private void PopUpDamage(int damage)
    {
        RectTransform canvasRectTransform = GameObject.FindWithTag("canvas").GetComponent<RectTransform>();
        GameObject damagePopupInstance = Instantiate(damagePopup, canvasRectTransform);
        damagePopupInstance.transform.localPosition = new Vector3(90, 260, 0);
        var textMeshPro = damagePopupInstance.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        textMeshPro.text = damage.ToString();
        textMeshPro.color = Color.red;
    }
    
    public void giveItem(Item item)
    {
        items.Add(item);
        SaveSystem.SavePlayerInfo(this);
    }
}