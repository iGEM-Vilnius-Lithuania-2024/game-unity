using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneManager : MonoBehaviour
{

    public GameObject battleButton;
    public GameObject runButton;
    public GameObject guidanceDialog;
    public GameObject infoDialog;
    
    public void Battle()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            infoDialog.SetActive(false);
            guidanceDialog.SetActive(true);
            SaveSystem.SaveScanInfo(DateTime.Now, ScanInfoStatic.scanPosition);
            MainManager.Instance.SwitchMapScene();
            battleButton.SetActive(false);
            runButton.SetActive(false);
        }
    }

    public void Run()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}