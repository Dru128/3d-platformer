using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    public Camera cam;
    private Rigidbody rb;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 rotationCamera = Vector3.zero;

    private Quaternion StartCamRotation; 
    private Vector3 StartCamPosition; 
    // начальный угол наклона и позиция камеры относительно игрока
    private bool isAroundPlayer = false;
    
    private void Start()
    {
        StartCamRotation = cam.transform.rotation;
        StartCamPosition = cam.transform.position;
        Debug.Log(StartCamRotation);
        Debug.Log(StartCamPosition);
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }
    public void RotateCam(Vector3 _rotationCam)
    {
        rotationCamera = _rotationCam;
    }

    void FixedUpdate()
    {
        PerformMove();
        PerformRotation();
    }
    public void doJump(float jumpForce)
    {
        rb.AddForce(0f, jumpForce, 0f, ForceMode.Acceleration);
        Debug.Log("player jump");
    }
    void PerformMove()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    public void AroundPlayer(bool _isAroundPlayer)
    {
        isAroundPlayer = _isAroundPlayer;
        if (!isAroundPlayer)
        { 
            cam.transform.localRotation = StartCamRotation;
            cam.transform.localPosition = StartCamPosition;
        }
    }
    
    void PerformRotation()
    {
        if (isAroundPlayer)
        {
            cam.transform.RotateAround(rb.transform.position, Vector3.up, 
                Input.GetAxisRaw("Mouse X") * 360 * Time.deltaTime );
        }
        else
        {
            rb.MoveRotation( rb.rotation * Quaternion.Euler(rotation) );
        }
        if (cam != null) cam.transform.Rotate(-rotationCamera);
    }

}