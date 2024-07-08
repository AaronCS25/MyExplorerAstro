using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereInteractable : MonoBehaviour, IInteractable
{
    public GameObject canvasPrefab;  // Asigna el prefab del canvas en el Inspector
    private GameObject currentCanvas;
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform; // Suponiendo que la cámara principal es el OVRCameraRig center eye anchor
    }

    public void OnInteract()
    {
        if (currentCanvas != null)
        {
            Destroy(currentCanvas);
        }

        // Instanciar el prefab del canvas en la posición del objeto
        currentCanvas = Instantiate(canvasPrefab, transform.position, Quaternion.identity);
        currentCanvas.transform.SetParent(transform, false);  // Establecer el objeto como padre
        PositionCanvas();

        // Asignar eventos de clic de los botones
        // AssignButtonEvents();
    }

    void PositionCanvas()
    {
        // Posicionar el canvas ligeramente encima del objeto
        currentCanvas.transform.localPosition = new Vector3(0, 0, 0); // Ajusta según sea necesario
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
        // Hacer que el canvas mire hacia la cámara
        Vector3 directionToCamera = currentCanvas.transform.position - cameraTransform.position;
        directionToCamera.y = 0; // Mantener el canvas vertical
        currentCanvas.transform.rotation = Quaternion.LookRotation(directionToCamera);
    }

    public void HideCanvas()
    {
        if (currentCanvas != null)
        {
            Destroy(currentCanvas);
        }
    }
}
