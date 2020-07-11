using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    public float gravity = -20f;

    [Header("Movement")]
    public float walkSpeed = 12f;
    [Range(1f, 5f)] public float sprintMultiply = 2.1f;

    [Header("Jump")]
    public float jumpForce = 1.9f;

    [Header("Look")]
    public Camera camera;
    public float cameraSense = 10f;

    private float cameraVerticalAngle;

    Vector3 moveInput = Vector3.zero;
    Vector3 rotaInput = Vector3.zero;

    CharacterController character;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        playerMove();
        playerLook();
    }

    private void playerMove()
    {
        if (character.isGrounded)
        {
            float speed;
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0) speed = walkSpeed * sprintMultiply;
            else speed = walkSpeed;

            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);
            moveInput = transform.TransformDirection(moveInput) * speed;

            if (Input.GetKey(KeyCode.Space)) moveInput.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
        moveInput.y += gravity * Time.deltaTime;
        character.Move(moveInput * Time.deltaTime);
    }

    private void playerLook()
    {
        rotaInput.x = Input.GetAxis("Mouse X") * cameraSense * Time.deltaTime;
        rotaInput.y = Input.GetAxis("Mouse Y") * cameraSense * Time.deltaTime;

        cameraVerticalAngle += rotaInput.y;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -70, 70);

        transform.Rotate(Vector3.up * rotaInput.x);

        camera.transform.localRotation = Quaternion.Euler(-cameraVerticalAngle, 0f,0f);
    }

}
