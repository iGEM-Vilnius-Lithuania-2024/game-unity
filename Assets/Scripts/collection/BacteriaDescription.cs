using System;
using System.Collections;
using bacteria;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BacteriaDescription : MonoBehaviour
{
    public GameObject descriptionPopup;
    
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    
    public Transform bacteriaModelParent;
    public Camera bacteriaCamera;
    public GameObject templateUIPrefab;
    
    private GameObject currentBacteriaModel;
    private GameObject uiPrefabInstance;
    private Boolean stopCoroutine;

    public void OpenItemDescription(int id)
    {
        if (id > 0)
        {
            stopCoroutine = false;
            BacteriaInfo bacteria = BacteriaList.bacterias.Find(b => b.id == id); 
            nameText.text = bacteria.name;
            descriptionText.text = bacteria.description;
            descriptionPopup.SetActive(true);

            SpawnAndAnimateBacteriaModel(bacteria.name);
        }
    }

    public void CloseDescription()
    {
        descriptionPopup.SetActive(false);
        stopCoroutine = true;
        if (currentBacteriaModel != null)
        {
            Destroy(currentBacteriaModel);
        }
        if (uiPrefabInstance != null)
        {
            Destroy(uiPrefabInstance);
        }
    }
    
    private void SpawnAndAnimateBacteriaModel(string bacteriaName)
    {
        GameObject bacteriaPrefab = Resources.Load<GameObject>("Bacterias/" + bacteriaName);

        if (currentBacteriaModel != null)
        {
            Destroy(currentBacteriaModel);
        }

        if (uiPrefabInstance != null)
        {
            Destroy(uiPrefabInstance);
        }
        
        uiPrefabInstance = Instantiate(templateUIPrefab, bacteriaModelParent.transform);

        RectTransform rectTransform = uiPrefabInstance.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(200, 200);
        
        RenderTexture renderTexture = new RenderTexture(256, 256, 16);
        bacteriaCamera.targetTexture = renderTexture;

        currentBacteriaModel = Instantiate(bacteriaPrefab, bacteriaCamera.transform.position + bacteriaCamera.transform.forward * 5f, Quaternion.identity);
        currentBacteriaModel.transform.localScale /= 14;
        bacteriaCamera.Render();
        
        RawImage rawImage = uiPrefabInstance.GetComponentInChildren<RawImage>();
        rawImage.texture = renderTexture;
        
        StartCoroutine(SpinModel());
    }

    private IEnumerator SpinModel()
    {
        while (!stopCoroutine)
        {
            if (currentBacteriaModel != null)
            {
                currentBacteriaModel.transform.Rotate(Vector3.back, 15f * Time.deltaTime);
            }
            yield return null;
        }
    }
}