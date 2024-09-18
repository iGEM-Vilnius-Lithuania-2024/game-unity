using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    public GameObject cameraDisabledDialog;
    
    IEnumerator Start()
    {
        if (WebCamTexture.devices.Length == 0)
        {
            cameraDisabledDialog.SetActive(true);
            yield return new WaitForSeconds(3f);
            
            cameraDisabledDialog.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}