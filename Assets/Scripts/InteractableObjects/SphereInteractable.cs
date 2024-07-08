using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereInteractable : MonoBehaviour, IInteractable
{
    public GameObject canvasPrefab;
    private GameObject currentCanvas;
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    public void OnInteract()
    {
        if (currentCanvas != null)
        {
            Destroy(currentCanvas);
        }

        currentCanvas = Instantiate(canvasPrefab, transform.position, Quaternion.identity);
        currentCanvas.transform.SetParent(transform, false);
        PositionCanvas();

        UIButtonHandler uiButtonHandler = currentCanvas.GetComponentInChildren<UIButtonHandler>();
        if (uiButtonHandler != null)
        {
            uiButtonHandler.sphere = this;
        }
    }

    void PositionCanvas()
    {
        currentCanvas.transform.localPosition = new Vector3(0, 0, 0);
        UpdateCanvasRotation();
    }

    void Update()
    {
        if (currentCanvas != null)
        {
            UpdateCanvasRotation();
        }
    }

    void UpdateCanvasRotation()
    {
        Vector3 directionToCamera = currentCanvas.transform.position - cameraTransform.position;
        directionToCamera.y = 0;
        currentCanvas.transform.rotation = Quaternion.LookRotation(directionToCamera);
    }

    public void HideCanvas()
    {
        if (currentCanvas != null)
        {
            Destroy(currentCanvas);
        }
    }

    public void DestroySphereAndUI()
    {
        if (currentCanvas != null)
        {
            Destroy(currentCanvas);
        }
        Destroy(gameObject);
    }
}
