using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // public OVRCameraRig cameraRig;  // Referencia al OVRCameraRig
    public CharacterController playerController; // Referencia al CharacterController del Player

    public float moveSpeed = 5f;    // Velocidad de movimiento
    public float rotationSpeed = 45f; // Velocidad de rotación

    private void Start()
    {
        // Asegúrate de que el Player tenga un componente CharacterController
        if (playerController == null)
        {
            playerController = GetComponentInParent<CharacterController>();
            if (playerController == null)
            {
                Debug.LogError("No se encontró CharacterController en el Player o sus padres.");
            }
        }
    }

    private void Update()
    {
        // Movimiento con el joystick izquierdo (aplicado al Player)
        Vector2 leftJoystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
        playerController.Move(transform.TransformDirection(new Vector3(leftJoystick.x, 0, leftJoystick.y)) * moveSpeed * Time.deltaTime);

        // Rotación con el joystick derecho (aplicado al CameraRig)
        Vector2 rightJoystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
        playerController.transform.Rotate(0, rightJoystick.x * rotationSpeed * Time.deltaTime, 0);
    }
}
