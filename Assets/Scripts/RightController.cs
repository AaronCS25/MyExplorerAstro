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

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color")) { color = Color.red };
    }

    void Update()
    {
        HandleInteraction();
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
                HandleObjectInteraction(hit);
            }
            else if (hit.collider.CompareTag(uiTag))
            {
                HandleUIInteraction(hit);
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

    void HandleObjectInteraction(RaycastHit hit)
    {
        lineRenderer.material.color = Color.green;

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null) 
            {
                interactable.OnInteract();
            }
        }
    }

    void HandleUIInteraction(RaycastHit hit)
    {
        lineRenderer.material.color = Color.blue;
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Button button = hit.collider.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.Invoke();
            }
        }
    }
}
