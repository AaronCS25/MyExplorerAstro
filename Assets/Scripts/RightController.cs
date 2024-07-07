using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightController : MonoBehaviour
{
    public float maxDistance = 10.0f;
    public LayerMask interactableLayer;
    public string interactableTag = "Interactable";

    private LineRenderer lineRenderer;
    // private InteractableHighlight currentTarget;
    // private ObjectInteractionHandler currentInteractionHandler;

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

        // Create a layer mask that ignores UI elements
        int layerMask = interactableLayer.value & ~(1 << LayerMask.NameToLayer("UI"));

        if (Physics.Raycast(ray, out hit, maxDistance, interactableLayer))
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);

            if (hit.collider.CompareTag(interactableTag))
            {
                lineRenderer.material.color = Color.green;

                // InteractableHighlight highlight = hit.collider.GetComponent<InteractableHighlight>();
                // if (highlight != null)
                // {
                //     if (currentTarget != null && currentTarget != highlight)
                //     {
                //         currentTarget.RemoveHighlight();
                //     }
                //     highlight.Highlight();
                //     currentTarget = highlight;
                // }

                // if (OVRInput.GetDown(OVRInput.Button.One)) // A button
                // {
                //     ObjectInteractionHandler interactionHandler = hit.collider.GetComponent<ObjectInteractionHandler>();
                //     if (interactionHandler != null)
                //     {
                //         if (currentInteractionHandler != null && currentInteractionHandler != interactionHandler)
                //         {
                //             currentInteractionHandler.HideCanvas();
                //         }
                //         interactionHandler.OnInteract();
                //         currentInteractionHandler = interactionHandler;
                //     }
                //     else
                //     {
                //         // If clicked on an interactable object without ObjectInteractionHandler, handle interaction normally
                //         if (currentInteractionHandler != null)
                //         {
                //             currentInteractionHandler.HideCanvas();
                //             currentInteractionHandler = null;
                //         }
                //     }
                //     // Send interaction message to the object
                //     hit.collider.gameObject.SendMessage("OnInteract", SendMessageOptions.DontRequireReceiver);
                // }
            }
            else
            {
                lineRenderer.material.color = Color.red;
                // if (currentTarget != null)
                // {
                //     currentTarget.RemoveHighlight();
                //     currentTarget = null;
                // }
            }
        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * maxDistance);
            lineRenderer.material.color = Color.red;

            // if (currentTarget != null)
            // {
            //     currentTarget.RemoveHighlight();
            //     currentTarget = null;
            // }

            // If clicked on void space, hide current canvas
            // if (OVRInput.GetDown(OVRInput.Button.One)) // A button
            // {
            //     if (currentInteractionHandler != null)
            //     {
            //         currentInteractionHandler.HideCanvas();
            //         currentInteractionHandler = null;
            //     }
            // }
        }
    }
}
