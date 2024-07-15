using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject skinBacteria;
    public GameObject waterBacteria;
    public GameObject woodBacteria;
    public GameObject leafBacteria;
    public GameObject mossBacteria;
    public GameObject rockBacteria;
    public GameObject sandBacteria;
    public GameObject pavementBacteria;
    public GameObject skinBackgroundVertical;
    public GameObject skinBackgroundHorizontal;
    public GameObject waterBackgroundVertical;
    public GameObject waterBackgroundHorizontal;
    public GameObject woodBackgroundVertical;
    public GameObject woodBackgroundHorizontal;
    public GameObject leafBackgroundVertical;
    public GameObject leafBackgroundHorizontal;
    public GameObject mossBackgroundVertical;
    public GameObject mossBackgroundHorizontal;
    public GameObject rockBackgroundVertical;
    public GameObject rockBackgroundHorizontal;
    public GameObject sandBackgroundVertical;
    public GameObject sandBackgroundHorizontal;
    public GameObject pavementBackgroundVertical;
    public GameObject pavementBackgroundHorizontal;
    
    
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
            case Surface.Leaf:
                PlaceBacteria(leafBacteria);
                PlaceBackground(leafBackgroundVertical, leafBackgroundHorizontal);
                break;
            case Surface.Moss:
                PlaceBacteria(mossBacteria);
                PlaceBackground(mossBackgroundVertical, mossBackgroundHorizontal);
                break;
            case Surface.Rock:
                PlaceBacteria(rockBacteria);
                PlaceBackground(rockBackgroundVertical, rockBackgroundHorizontal);
                break;
            case Surface.Sand:
                PlaceBacteria(sandBacteria);
                PlaceBackground(sandBackgroundVertical, sandBackgroundHorizontal);
                break;
            case Surface.Pavement:
                PlaceBacteria(pavementBacteria);
                PlaceBackground(pavementBackgroundVertical, pavementBackgroundHorizontal);
                break;
        }
    }
}