using UnityEngine;
using UnityEngine.UI;

public class CameraUIHandler : MonoBehaviour
{
    // public Image playPauseButtonImage; // Referencia a la imagen del botón Play/Pause
    public GameObject spherePrefab;
    private bool play = true;

    public void OnMenuButtonClick()
    {
        Debug.Log("ABRIO MENÚ");
    }

    public void OnAddPlanetButtonClick()
    {
        Debug.Log("Agregar planeta");
        // Obtener la posición y rotación de la cámara
        Transform cameraTransform = Camera.main.transform;
        Vector3 cameraPosition = cameraTransform.position;
        Quaternion cameraRotation = cameraTransform.rotation;

        // Calcular la posición de instanciación (2 metros frente a la cámara)
        Vector3 spawnPosition = cameraPosition + cameraTransform.forward * 2f;

        // Instanciar la esfera
        GameObject newSphere = Instantiate(spherePrefab, spawnPosition, cameraRotation);
    }

    public void OnPlayPauseButtonClick()
    {
        // Cambiar el estado y la imagen del botón Play/Pause
        // ...
        Debug.Log(play ? "Pause":"Play" );
        play = !play;
    }
}
