using UnityEngine;

public class BacteriaClickHandler : MonoBehaviour
{
    public int bacteriaId;
    public BacteriaDescription descriptionManager;

    private void OnMouseDown()
    {
        descriptionManager.OpenItemDescription(bacteriaId);
    }
}