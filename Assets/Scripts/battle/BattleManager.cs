using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject skinBacteria;
    public GameObject waterBacteria;
    public GameObject woodBacteria;
    public GameObject skinBackgroundVertical;
    public GameObject skinBackgroundHorizontal;
    public GameObject waterBackgroundVertical;
    public GameObject waterBackgroundHorizontal;
    public GameObject woodBackgroundVertical;
    public GameObject woodBackgroundHorizontal;
    
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
    
    private void PlaceBackground(GameObject vGameObject, GameObject hGameObject)
    {
        Instantiate(vGameObject);
        Instantiate(hGameObject);
    }
    
    private void SpawnObjects()
    {
        switch (MainManager.Instance.detectedSurface)
        {
            case Surface.Skin:
                PlaceBacteria(skinBacteria);
                PlaceBackground(skinBackgroundVertical, skinBackgroundHorizontal);
                break;
            case Surface.Water:
                PlaceBacteria(waterBacteria);
                PlaceBackground(waterBackgroundVertical, waterBackgroundHorizontal);
                break;
            case Surface.Wood:
                PlaceBacteria(woodBacteria);
                PlaceBackground(woodBackgroundVertical, woodBackgroundHorizontal);
                break;
        }
    }
}