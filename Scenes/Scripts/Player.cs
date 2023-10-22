using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 movementDirection;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private Transform cameraTransform;
    private float cameraRotX;
    [SerializeField] private float lookSpeed;
    [SerializeField] private float lookXLimit = 45;
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;
    private UIManager uiManager;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        uiManager = FindAnyObjectByType<UIManager>();
        currentHealth = maxHealth; 
        
    }

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float currentSpeedX = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical");
        float currentSpeedY = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal");
        Vector3 tempDirection = (transform.forward * currentSpeedX) + (transform.right * currentSpeedY);
        movementDirection.x = tempDirection.x;
        movementDirection.z = tempDirection.z;

        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            movementDirection.y = jumpPower;
        }

        if (!characterController.isGrounded)
        {
            movementDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(movementDirection * Time.deltaTime);

        cameraRotX += -Input.GetAxis("Mouse Y") * lookSpeed;
        cameraRotX = Mathf.Clamp(cameraRotX, -lookXLimit, lookXLimit);
        cameraTransform.localRotation = Quaternion.Euler(cameraRotX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            Debug.Log("Hit!");
            currentHealth--;
            uiManager.OnHealthReduced(currentHealth);
        }
    }
}