using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    public PlayerMaster Master;
    //Variables
    public float sens;
    private float cameraVerticalRotation = 0f;
    private float cameraHorizontalRotation = 0f;

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
        cameraVerticalRotation -= lookAng.y*sens* Time.fixedDeltaTime;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90, 90);
        cameraHorizontalRotation += lookAng.x * sens * Time.fixedDeltaTime;

        transform.eulerAngles = Vector3.right * cameraVerticalRotation + Vector3.up * cameraHorizontalRotation;

        Master.movementMaster.rb.rotation = Quaternion.Euler( Vector3.up * cameraHorizontalRotation);
    }
}
