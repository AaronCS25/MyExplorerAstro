using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonHandler : MonoBehaviour
{
    public SphereInteractable sphere;
    public void HandleButtonClick(string buttonName)
    {
        switch (buttonName)
        {
            case "infoButton":
                Debug.Log("Info button clicked");
                break;
            case "editButton":
                Debug.Log("Edit button clicked");
                break;
            case "dropButton":
                if (sphere != null)
                {
                    sphere.DestroySphereAndUI(); 
                }
                break;
            default:
                Debug.Log("Unknown button clicked");
                break;
        }
    }
}
