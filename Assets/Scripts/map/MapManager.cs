using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public GameObject locationDisabledDialog;
    
    IEnumerator Start()
    {
        if (!Input.location.isEnabledByUser && !Application.isEditor)
        {
            locationDisabledDialog.SetActive(true);
            yield return new WaitForSeconds(3f);
            
            locationDisabledDialog.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}