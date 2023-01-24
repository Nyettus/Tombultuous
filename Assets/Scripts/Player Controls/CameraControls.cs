using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    public PlayerMaster Master;
    //Variables
    public Transform player;
    public float sens;
    private float cameraVerticalRotation = 0f;

    //Input gathering
    public Vector2 lookAng;
    public void Start()
    {
        Master = GetComponentInParent<PlayerMaster>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookAng = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.paused)
            CameraController();

    }

    private void CameraController()
    {
        cameraVerticalRotation -= lookAng.y*sens* Time.deltaTime;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90, 90);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        player.Rotate(Vector3.up * lookAng.x*sens*Time.deltaTime);
    }
}
