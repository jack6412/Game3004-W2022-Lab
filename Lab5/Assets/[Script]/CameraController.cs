using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Properties")]
    public float controlSensitivity = 10.0f;
    public Transform playerBody;
    public Joystick R_Joystick;

    private float XRptation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //float mouseX = Input.GetAxis("Mouse X") * controlSensitivity,
        //      mouseY = Input.GetAxis("Mouse Y") * controlSensitivity;
        
        float Horizontal = R_Joystick.Horizontal * controlSensitivity,
              Vertical = R_Joystick.Vertical * controlSensitivity;

        XRptation -= Vertical;
        XRptation = Mathf.Clamp(XRptation, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(XRptation, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up * Horizontal);
    }
}
