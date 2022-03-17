using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public UIController UIC;

    [Header("Movement Properties")]
    public float moveSpeed = 10.0f;
    public float gravity = 10.0f;
    public float jumpHight = 10.0f;
    public Vector3 velocity;

    [Header("Ground Properties")]
    public Transform groundCheck;
    public float Radius;
    public LayerMask groundMask;
    public bool isGround;

    [Header("Joystick Controller")]
    public GameObject miniMap;
    public GameObject ScreamControls;
    public Joystick L_Joystick;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (Application.platform != RuntimePlatform.Android)
        {
            ScreamControls.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics.CheckSphere(groundCheck.position, Radius, groundMask);

        if (isGround && velocity.y < 0.0f)
        {
            velocity.y = -2.0f;
        }

        float x = Input.GetAxis("Horizontal") + L_Joystick.Horizontal,
              z = Input.GetAxis("Vertical") + L_Joystick.Vertical;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButton("Jump") && isGround)
        {
            velocity.y = Mathf.Sqrt(jumpHight * -2.0f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.M))
            miniMap.SetActive(!miniMap.activeInHierarchy);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheck.position, Radius);
    }

    //Take damage on hazard
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            UIC.takeDamage(5);
        }
    }

    //Button controler
    public void OnButJump()
    {
        if (isGround)
        {
            velocity.y = Mathf.Sqrt(jumpHight * -2.0f * gravity);
        }
    }
    public void OnButMap()
    {
        miniMap.SetActive(!miniMap.activeInHierarchy);
    }
   
}
