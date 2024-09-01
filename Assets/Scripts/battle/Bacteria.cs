using System.Collections;
using UnityEngine;

public class Bacteria : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 100;
    public float damage = 5;
    public float attackDuration = 0.8f; 
    public float attackScaleFactor = 2.0f; 
    
    public Player player;
    public HealthBar healthBar;
    public BattleManager battleManager;
    
    public float moveSpeed = 3f;
    public float xRange = 15f;
    public float yRange = 15f;

    private Vector3 targetPosition;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private Quaternion originalRotation;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        player = GameObject.Find("Player").GetComponent<Player>();
        battleManager = GameObject.Find("Battle Manager").GetComponent<BattleManager>();
        StartCoroutine(DealDamageOverTime());
        originalPosition = transform.position;
        originalScale = transform.localScale;
        originalRotation = transform.rotation;
        SetRandomTargetPosition();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
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
            
            StartCoroutine(Attack());
        }
    }

    void Die()
    {
        StartCoroutine(ShrinkAndDie());
    }
    
    private IEnumerator Attack()
    {
        float elapsed1 = 0.0f;

        Vector3 targetRotationEuler = originalRotation.eulerAngles + new Vector3(-60, 0, 0);

        while (elapsed1 < attackDuration)
        {
            float progress = elapsed1 / attackDuration;

            transform.localScale = Vector3.Lerp(originalScale, originalScale * attackScaleFactor, progress);

            transform.rotation = Quaternion.Euler(Vector3.Lerp(originalRotation.eulerAngles, targetRotationEuler, progress));

            elapsed1 += Time.deltaTime;
            yield return null;
        }
        
        battleManager.damageSound.Play();
        player.TakeDamage(damage);
        float elapsed2 = 0.0f;

        while (elapsed2 < attackDuration)
        {
            float progress = elapsed2 / attackDuration;
        
            transform.localScale = Vector3.Lerp(originalScale * attackScaleFactor, originalScale, progress);
            
            transform.rotation = Quaternion.Euler(Vector3.Lerp(targetRotationEuler, originalRotation.eulerAngles, progress));
        
            elapsed2 += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
        transform.rotation = originalRotation;
    }
    
    private IEnumerator ShrinkAndDie()
    {
        float duration = 0.5f;
        float elapsed = 0.0f;

        Vector3 originalScale = transform.localScale;

        while (elapsed < duration)
        {
            float progress = elapsed / duration;

            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, progress);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.zero;
        
        gameObject.transform.gameObject.SetActive(false);
        battleManager.BacteriaDied();
    }
    
    private void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-xRange, xRange);
        float randomY = Random.Range(-yRange, yRange);
        targetPosition = originalPosition + new Vector3(randomX, randomY, 0);
    }
}
