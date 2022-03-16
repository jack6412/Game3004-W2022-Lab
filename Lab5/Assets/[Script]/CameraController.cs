using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Properties")]
    public float mouseSensitivity = 10.0f;
    public Transform playerBody;

    private float XRptation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity,
              mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        XRptation -= mouseY;
        XRptation = Mathf.Clamp(XRptation, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(XRptation, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
