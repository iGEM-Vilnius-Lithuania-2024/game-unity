using bacteria;
using UnityEngine;
using UnityEngine.UI;

namespace collection
{
    public class CollectionManager : MonoBehaviour
    {
        public GameObject templateUIPrefab;
        public Camera renderCamera; 
        public GameObject panel;
        public Material blackUnlitMaterial;

        void Start()
        {
            InstantiateBacteriaUI();
        }

        void InstantiateBacteriaUI()
        {
            CollectionSave collectionSave = SaveSystem.LoadCollection();
            
            foreach (BacteriaInfo bacteria in BacteriaList.bacterias)
            {
                GameObject bacteriaPrefab = Resources.Load<GameObject>("Bacterias/" + bacteria.name);

                GameObject uiPrefabInstance = Instantiate(templateUIPrefab, panel.transform);

                RenderTexture renderTexture = new RenderTexture(256, 256, 16);
                renderCamera.targetTexture = renderTexture;
                
                GameObject bacteriaInstance = Instantiate(bacteriaPrefab, renderCamera.transform.position + renderCamera.transform.forward * 5f, Quaternion.identity);
                
                if (!collectionSave.ids.Contains(bacteria.id)) 
                {
                    foreach (Renderer bacteriaRenderer in bacteriaInstance.GetComponentsInChildren<Renderer>())
                    {
                        bacteriaRenderer.material = blackUnlitMaterial;
                    }
                }
                else
                {
                    Button button = uiPrefabInstance.GetComponent<Button>();
                    if (button == null)
                    {
                        button = uiPrefabInstance.AddComponent<Button>();
                    }
                    button.onClick.AddListener(() => OnBacteriaClick(bacteria.id));
                }
                
                renderCamera.Render();

                RawImage rawImage = uiPrefabInstance.GetComponentInChildren<RawImage>();
                rawImage.texture = renderTexture;

                Destroy(bacteriaInstance);
                renderCamera.targetTexture = null;
                renderCamera.transform.position += new Vector3(10, 10, 10);
            }
        }
        void OnBacteriaClick(int bacteriaId)
        {
            BacteriaDescription descriptionManager = GetComponent<BacteriaDescription>();
            if (descriptionManager != null)
            {
                descriptionManager.OpenItemDescription(bacteriaId);
            }
        }
    }
}