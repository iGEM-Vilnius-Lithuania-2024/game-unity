using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlideTarget : MonoBehaviour
{
    public Player player;
    public GameObject aim;
    public float duration = 2.0f;
    public GameObject verticalLine;
    public GameObject horizontalLine;
    public GameObject target;
    public GameObject damagePopup;
    public AudioSource targetSound;
    public AudioSource hitSound;
    public AudioSource missSound;
    public GameObject camera;

    private Vector3 topPosition;
    private Vector3 bottomPosition;
    private Vector3 leftPosition;
    private Vector3 rightPosition;
    private float elapsedTime = 0.0f;
    private bool movingDown = true;
    private bool isVerticalMovement = true;
    private float horizontalLineY;
    private float horizontalDuration;
    private RectTransform canvasRectTransform;
    private RectTransform targetCanvasReactTransorm;
    private GameObject spawnedObject;

    void Start()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            canvasRectTransform = canvas.GetComponent<RectTransform>();
            Canvas targetCanvas = canvas.transform.GetChild(0).GetComponent<Canvas>();
            targetCanvasReactTransorm = targetCanvas.GetComponent<RectTransform>();
            SpawnAtRandomPosition();
            if (canvasRectTransform != null)
            {
                horizontalDuration = duration / canvasRectTransform.rect.height * canvasRectTransform.rect.width;
                float canvasHeight = canvasRectTransform.rect.height;
                float canvasWidth = canvasRectTransform.rect.width;
                Vector3 targetPosition = aim.transform.localPosition;
                topPosition = new Vector3(targetPosition.x, canvasHeight / 2, targetPosition.z);
                bottomPosition = new Vector3(targetPosition.x, -canvasHeight / 2, targetPosition.z);
                leftPosition = new Vector3(-canvasWidth / 2, targetPosition.y, targetPosition.z);
                rightPosition = new Vector3(canvasWidth / 2, targetPosition.y, targetPosition.z);
            }
        }
    }

    void Update()
    {
        if (aim == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            isVerticalMovement = !isVerticalMovement;
            elapsedTime = (isVerticalMovement ? duration : horizontalDuration) * (1 - elapsedTime / (isVerticalMovement ? duration : horizontalDuration));
            if (!isVerticalMovement)
            {
                horizontalLineY = aim.transform.localPosition.y;
                leftPosition = new Vector3(leftPosition.x, horizontalLineY, leftPosition.z);
                rightPosition = new Vector3(rightPosition.x, horizontalLineY, rightPosition.z);
                targetSound.Play();
            }
            else
            {
                float damage = CalculateDamage();
                PopUpDamage((int)Math.Round(damage, 0), aim.transform.localPosition);
                GameObject bacteria = GameObject.FindGameObjectWithTag("bacteria");
                int roundedDamage = (int)Math.Round(damage, 0);
                bacteria.GetComponent<Bacteria>().TakeDamage(roundedDamage);
                SpawnAtRandomPosition();
                if (roundedDamage == 0)
                {
                    missSound.Play();
                }
                else
                {
                    camera.GetComponent<CameraShake>().StartShake();
                    targetSound.Play();
                    StartCoroutine(PlaySoundWithDelay(hitSound, 0.05f));
                }
            }
        }

        if (isVerticalMovement)
        {
            verticalLine.SetActive(true);
            horizontalLine.SetActive(false);
            MoveVertical();
        }
        else
        {
            verticalLine.SetActive(false);
            horizontalLine.SetActive(true);
            Vector3 horizontalLinePos = horizontalLine.transform.localPosition;
            horizontalLinePos.y = aim.transform.localPosition.y;
            horizontalLine.transform.localPosition = horizontalLinePos;
            MoveHorizontal();
        }
    }

    void MoveVertical()
    {
        if (movingDown)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > duration)
            {
                elapsedTime = duration;
                movingDown = false;
            }
        }
        else
        {
            elapsedTime -= Time.deltaTime;
            if (elapsedTime < 0.0f)
            {
                elapsedTime = 0.0f;
                movingDown = true;
            }
        }

        float t = elapsedTime / duration;
        aim.transform.localPosition = Vector3.Lerp(topPosition, bottomPosition, t);
    }

    void MoveHorizontal()
    {
        if (movingDown)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > horizontalDuration)
            {
                elapsedTime = horizontalDuration;
                movingDown = false;
            }
        }
        else
        {
            elapsedTime -= Time.deltaTime;
            if (elapsedTime < 0.0f)
            {
                elapsedTime = 0.0f;
                movingDown = true;
            }
        }

        float t = elapsedTime / horizontalDuration;
        aim.transform.localPosition = Vector3.Lerp(leftPosition, rightPosition, t);
    }

    public void SpawnAtRandomPosition()
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
        }
        float randomX = Random.Range(-targetCanvasReactTransorm.rect.width / 2, targetCanvasReactTransorm.rect.width / 2);
        float randomY = Random.Range(-targetCanvasReactTransorm.rect.height / 2, targetCanvasReactTransorm.rect.height / 2);
        Vector3 randomPosition = new Vector3(randomX, randomY, 0);
        
        spawnedObject = Instantiate(target, targetCanvasReactTransorm);
        spawnedObject.transform.localPosition = randomPosition;
        spawnedObject.SetActive(true);
    }
    
    private float CalculateDamage()
    {
        Vector3 aimPosition = aim.transform.localPosition;
        Vector3 targetPosition = spawnedObject.transform.localPosition;
        
        float distanceFromCenter = Vector3.Distance(aimPosition, Vector3.zero);
        
        if (distanceFromCenter > 100)
        {
            return 0;
        }

        float distance = Vector3.Distance(aimPosition, targetPosition);
        float maxDistance = Mathf.Sqrt(Mathf.Pow(targetCanvasReactTransorm.rect.width / 2, 2) + Mathf.Pow(targetCanvasReactTransorm.rect.height / 2, 2));
    
        float normalizedDistance = Mathf.Clamp01(distance / maxDistance);

        return Mathf.Lerp(player.attackDamage, (float)Math.Round(player.attackDamage/2, 0), normalizedDistance);
    }

    private void PopUpDamage(int damage, Vector3 aimPosition)
    {
        GameObject damagePopupInstance = Instantiate(damagePopup, canvasRectTransform);
        damagePopupInstance.transform.localPosition = aimPosition;
        damagePopupInstance.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = damage.ToString();
    }
    
    IEnumerator PlaySoundWithDelay(AudioSource audioSource, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.Play();
    }
}