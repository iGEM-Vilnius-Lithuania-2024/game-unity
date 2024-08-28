using System.Collections;
using bacteria;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Surface detectedSurface;
    public BacteriaInfo selectedBacteria;
    public AudioSource mainMusic;
    public AudioSource preBattleMusic;
    public AudioSource battleMusic;

    private AudioSource audioSource;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1 || scene.buildIndex == 2) {
            PlayMusic(preBattleMusic);
        } else if (scene.buildIndex == 3) {
            PlayMusic(battleMusic);
        } else {
            PlayMusic(mainMusic);
        }
    }
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(mainMusic);
        DontDestroyOnLoad(preBattleMusic);
        DontDestroyOnLoad(battleMusic);
    }

    private void OnStart()
    {
        PlayMusic(mainMusic);
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
    
    private void PlayMusic(AudioSource newAudioSource)
    {
        if (!newAudioSource.isPlaying)
        {
            if(audioSource != null)
                audioSource.Stop();
            audioSource = newAudioSource;
            audioSource.Play();
        }
    }
}