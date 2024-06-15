using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float maxHealth = 100;
    public float currentHealth;
    public float pointIncreasePerMinute = 1;

    public HealthBar healthBar;
    
    private float saveInterval = 60f;
    private float saveTimer;
    private GameObject lose;
    void Start()
    {
        PlayerInfo playerInfo = SaveSystem.LoadPlayerInfo();
        currentHealth = playerInfo.currentHealth + (float)(DateTime.Now - playerInfo.saveTime).TotalMinutes * pointIncreasePerMinute;
        
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        
        saveTimer = saveInterval;
    }
    
    void Update()
    {
        currentHealth += pointIncreasePerMinute * (Time.deltaTime / 60);
        
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0f)
        {
            currentHealth = 0;
            
            GameObject.FindGameObjectWithTag("lose").transform.position = new Vector3(2, 0, 0);
            
            SaveSystem.SavePlayerInfo(currentHealth + 1);
            SaveSystem.SaveScanInfo(DateTime.Now, ScanInfoStatic.scanPosition);
            MainManager.Instance.SwitchMapScene();
        }
        
        healthBar.SetHealth(currentHealth);
        
        saveTimer -= Time.deltaTime;
        if (saveTimer <= 0f)
        {
            SaveSystem.SavePlayerInfo(currentHealth);
            saveTimer = saveInterval;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        SaveSystem.SavePlayerInfo(currentHealth);
    }
}