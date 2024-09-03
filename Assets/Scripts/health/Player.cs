using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float maxHealth = 100;
    public float currentHealth;
    public float pointIncreasePerMinute = 1;
    public float attackDamage = 20;
    public float cooldown = 300;
    public List<ItemKey> items;
    
    public GameObject damagePopup;
    public HealthBar healthBar;
    public TextMeshProUGUI HP;
    public TextMeshProUGUI ATK;
    public TextMeshProUGUI REG;
    public BattleManager battleManager;
    private GameObject lose;
    void Start()
    {
        PlayerInfo playerInfo = SaveSystem.LoadPlayerInfo();
        PlayerInfoToPlayer(playerInfo);
        applyPerks();
        
        SaveSystem.SavePlayerInfo(this);
        
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(currentHealth);
        }
        
        if (HP != null && ATK != null)
        {
            HP.text = ((int) Math.Round(maxHealth, 0)).ToString();
            ATK.text = ((int) Math.Round(attackDamage, 0)).ToString();
        }
    }
    
    void Update()
    {
        if (HP != null && ATK != null)
        {
            HP.text = ((int) Math.Round(maxHealth, 0)).ToString();
            ATK.text = ((int) Math.Round(attackDamage, 0)).ToString();
            REG.text = ((int) Math.Round(pointIncreasePerMinute, 0)).ToString();
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
    }

    public void applyPerks()
    {
        float multiplier = 1;
        float hpBonus = 0;
        float atkBonus = 0;
        float regenBonus = 0;
        
        foreach (var item in items)
        {
            if (item != null && item.id != null && Items.items[item.id] != null)
            {
                if (Items.items[item.id].type == ItemType.Ori && item.isEquipped)
                {
                    multiplier = ((OriItem)Items.items[item.id]).multiplier;
                }

                if (Items.items[item.id].type == ItemType.Gene && item.isEquipped)
                {
                    if (((GeneItem)Items.items[item.id]).attribute == ItemAttribute.Attack)
                        atkBonus += ((GeneItem)Items.items[item.id]).boost;
                    if (((GeneItem)Items.items[item.id]).attribute == ItemAttribute.HP)
                        hpBonus += ((GeneItem)Items.items[item.id]).boost;
                    if (((GeneItem)Items.items[item.id]).attribute == ItemAttribute.Regen)
                        regenBonus += ((GeneItem)Items.items[item.id]).boost;
                }
            }
        }
        
        maxHealth = 100 + hpBonus * multiplier;
        attackDamage = 20 + atkBonus * multiplier;
        pointIncreasePerMinute = 1 + regenBonus * multiplier;
    }

    private void PlayerInfoToPlayer(PlayerInfo playerInfo)
    {
        currentHealth = playerInfo.currentHealth + (float)(DateTime.Now - playerInfo.saveTime).TotalMinutes * pointIncreasePerMinute;
        items = playerInfo.items;
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
    
    public void giveItem(ItemKey item)
    {
        items.Add(item);
        SaveSystem.SavePlayerInfo(this);
    }
}