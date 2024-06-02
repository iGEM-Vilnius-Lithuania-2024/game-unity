using UnityEngine;

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
}