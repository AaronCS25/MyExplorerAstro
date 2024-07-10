using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightController : MonoBehaviour
{
    public float maxDistance = 10.0f;
    public LayerMask interactableLayer;
    public string interactableTag = "Interactable";
    public string uiTag = "UI";

    private LineRenderer lineRenderer;
    private GameObject objectBeingMoved;
    private Vector3 fixedRayEnd;
    private bool isMovingObject = false;
    private float objectDistance;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color")) { color = Color.red };
    }

    void Update()
    {
        HandleButtonPress();

        if (isMovingObject)
        {
            MoveObject();
        }
        else
        {
            HandleInteraction();
        }
    }

    void HandleButtonPress()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (!isMovingObject)
            {
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, maxDistance, interactableLayer) && hit.collider.CompareTag(interactableTag))
                {
                    StartMovingObject(hit);
                }
            }
            else
            {
                StopMovingObject();
            }
        }

        // Manejo del botón A (OVRInput.Button.One)
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxDistance, interactableLayer))
            {
                if (hit.collider.CompareTag(interactableTag))
                {
                    InteractWithObject(hit);
                }
                else if (hit.collider.CompareTag(uiTag))
                {
                    InteractWithUI(hit);
                }
            }
        }
    }

    void HandleInteraction()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        bool hitSuccess = Physics.Raycast(ray, out hit, maxDistance, interactableLayer);

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, hitSuccess ? hit.point : transform.position + transform.forward * maxDistance);

        if (hitSuccess)
        {
            if (hit.collider.CompareTag(interactableTag))
            {
                lineRenderer.material.color = Color.green;
            }
            else if (hit.collider.CompareTag(uiTag))
            {
                lineRenderer.material.color = Color.blue;
            }
            else
            {
                lineRenderer.material.color = Color.red;
            }
        }
        else
        {
            lineRenderer.material.color = Color.red;
        }
    }

    void InteractWithObject(RaycastHit hit)
    {
        IInteractable interactable = hit.collider.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.OnInteract();
            Debug.Log("Interacted with object: " + hit.collider.gameObject.name);
        }
    }

    void InteractWithUI(RaycastHit hit)
    {
        Button button = hit.collider.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.Invoke();
            Debug.Log("Clicked UI button: " + button.name);
        }
    }

    void StartMovingObject(RaycastHit hit)
    {
        objectBeingMoved = hit.collider.gameObject;
        fixedRayEnd = hit.point;
        objectDistance = hit.distance;
        isMovingObject = true;
        lineRenderer.material.color = Color.yellow;
        Debug.Log("Started moving object: " + objectBeingMoved.name);
    }

    void StopMovingObject()
    {
        objectBeingMoved = null;
        isMovingObject = false;
        lineRenderer.material.color = Color.red;
        Debug.Log("Stopped moving object");
    }

    void MoveObject()
    {
        if (objectBeingMoved != null)
        {
            Vector3 newPosition = transform.position + transform.forward * objectDistance;
            objectBeingMoved.transform.position = newPosition;

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, newPosition);

            Debug.Log("Moving object to: " + newPosition);
        }
    }
}