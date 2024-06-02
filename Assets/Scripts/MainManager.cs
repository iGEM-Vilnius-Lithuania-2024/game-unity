using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Surface detectedSurface;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void SwitchMapScene()
    {
        StartCoroutine(SwitchMapSceneAfterDelay());
    }
    
    private IEnumerator SwitchMapSceneAfterDelay()
    {
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(0);
    }
}