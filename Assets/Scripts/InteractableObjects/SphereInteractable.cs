using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereInteractable : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        // Aquí va la lógica específica del cubo (ej: cambiar color, rotar, etc.)
        Debug.Log("Interactuando con la esfera!");
    }
}
