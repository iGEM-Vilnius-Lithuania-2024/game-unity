using System;
using System.Collections;
using UnityEngine;

public class Bacteria : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 100;
    public float damage = 5;
    
    public Player player;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        player = GameObject.Find("Player").GetComponent<Player>();
        StartCoroutine(DealDamageOverTime());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    IEnumerator DealDamageOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            player.TakeDamage(damage);
        }
    }

    void Die()
    {
        // TODO: Die animation
        
        // TODO: Victory screen
        
        gameObject.transform.parent.gameObject.SetActive(false);
        SaveSystem.SaveScanInfo(DateTime.Now, ScanInfoStatic.scanPosition);
        MainManager.Instance.SwitchMapScene();
    }
}
