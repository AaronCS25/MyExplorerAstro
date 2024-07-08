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
    private UIButtonHandler uiButtonHandler;

    // private InteractableHighlight currentTarget;
    // private ObjectInteractionHandler currentInteractionHandler;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color")) { color = Color.red };

        uiButtonHandler = FindObjectOfType<UIButtonHandler>();
        if (uiButtonHandler == null)
        {
            Debug.LogError("UIButtonHandler no encontrado en la escena.");
        }
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
        // int layerMask = interactableLayer.value & ~(1 << LayerMask.NameToLayer("UI"));

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

                if (OVRInput.GetDown(OVRInput.Button.One)) // A button
                {
                    IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                    if (interactable != null) 
                    {
                        interactable.OnInteract(); // Llama al método específico del objeto
                    }

                    // ObjectInteractionHandler interactionHandler = hit.collider.GetComponent<ObjectInteractionHandler>();
                    // if (interactionHandler != null)
                    // {
                    //     if (currentInteractionHandler != null && currentInteractionHandler != interactionHandler)
                    //     {
                    //         currentInteractionHandler.HideCanvas();
                    //     }
                    //     interactionHandler.OnInteract();
                    //     currentInteractionHandler = interactionHandler;
                    // }
                    // else
                    // {
                    //     // If clicked on an interactable object without ObjectInteractionHandler, handle interaction normally
                    //     if (currentInteractionHandler != null)
                    //     {
                    //         currentInteractionHandler.HideCanvas();
                    //         currentInteractionHandler = null;
                    //     }
                    // }
                    // Send interaction message to the object
                    // hit.collider.gameObject.SendMessage("OnInteract", SendMessageOptions.DontRequireReceiver);
                }
            }
            else if (hit.collider.CompareTag(uiTag))
            {
                lineRenderer.material.color = Color.blue;

                if (OVRInput.GetDown(OVRInput.Button.One)) // A button
                {
                    Button button = hit.collider.GetComponent<Button>();
                    if (button != null && uiButtonHandler != null)
                    {
                        uiButtonHandler.HandleButtonClick(button.name);
                    }
                }
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
