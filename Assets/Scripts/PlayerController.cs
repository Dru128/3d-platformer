using System;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lookSpeed = 3f;
    [SerializeField] private float jumpForce = 30f;

    private bool isPressedRightMouse;
    private PlayerMotor motor;

    private bool isGrounded;
    private void Start()
    {
        isPressedRightMouse = Input.GetMouseButton(1);
        motor = GetComponent<PlayerMotor>();
    }

    void OnCollisionEnter()
    {
        isGrounded = true;
    }

    private void Update()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 moveHor = transform.right * xMov;
        Vector3 moveVer = transform.forward * zMov;

        Vector3 velocity = (moveHor + moveVer).normalized * speed;

        motor.Move(velocity);

        float yRot = Input.GetAxisRaw ("Mouse X");
        Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSpeed;
        motor.Rotate(rotation);

        float xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 camRotation = new Vector3(xRot, 0f, 0f) * lookSpeed;
        motor.RotateCam(camRotation);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            motor.doJump(jumpForce);
        }

        if (isPressedRightMouse != Input.GetMouseButton(1))
        {
            isPressedRightMouse = Input.GetMouseButton(1);
            motor.AroundPlayer(isPressedRightMouse);
        }
    }
}

