using UnityEngine;

public class NavbarManager : MonoBehaviour
{
    public GameObject noInternetDialog;

    private void Update()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            noInternetDialog.SetActive(true);
        }
        else if(noInternetDialog.activeSelf)
        {
            noInternetDialog.SetActive(false);
        }
    }
}