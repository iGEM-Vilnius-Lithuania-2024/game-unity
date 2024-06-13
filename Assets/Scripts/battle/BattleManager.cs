using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject skinBacteria;
    public GameObject waterBacteria;
    public GameObject woodBacteria;
    public GameObject skinBackground;
    public GameObject waterBackground;
    public GameObject woodBackground;
    
    private GameObject _spawnedObject;

    private void Start()
    {
        SpawnObjects();
    }
    
    private void PlaceBacteria(GameObject objectToSpawn)
    {
        _spawnedObject = Instantiate(objectToSpawn, new Vector3(0, 50, 100), new Quaternion(0f, 0f, 0f, 1f));
        _spawnedObject.transform.localScale = new Vector3(2000, 2000, 2000);
    }
    
    private void PlaceBackground(GameObject objectToSpawn)
    {
        _spawnedObject = Instantiate(objectToSpawn, new Vector3(25, 0, 130), new Quaternion(0f, 0f, 0f, 1f));
        _spawnedObject.transform.rotation = Quaternion.Euler(90, 0, 0);
        Instantiate(objectToSpawn, new Vector3(25, 0, 390), new Quaternion(0f, 0f, 0f, 1f));
    }

    
    private void SpawnObjects()
    {
        switch (MainManager.Instance.detectedSurface)
        {
            case Surface.Skin:
                PlaceBacteria(skinBacteria);
                PlaceBackground(skinBackground);
                break;
            case Surface.Water:
                PlaceBacteria(waterBacteria);
                PlaceBackground(waterBackground);
                break;
            case Surface.Wood:
                PlaceBacteria(woodBacteria);
                PlaceBackground(woodBackground);
                break;
        }
    }
}