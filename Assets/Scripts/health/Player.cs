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
    public Item[] items;
    
    public GameObject damagePopup;
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
            
            GameObject.FindGameObjectWithTag("lose").transform.position = new Vector3(0, 0, 0);
            
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
        PopUpDamage((int)Math.Round(damage, 0));
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        SaveSystem.SavePlayerInfo(currentHealth);
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
}