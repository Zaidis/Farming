using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private CharacterController controller;
    [Header("Player Body")]
    [SerializeField] private Vector3 velocity;
    private float gravity = -14f;

    [SerializeField] bool isGrounded;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private Transform groundCheck;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] private GameObject hoeParent;
    public float mouseSensitivity { get; set; }
    float xRotation = 0f;
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
        controller = this.gameObject.GetComponent<CharacterController>();
        Physics.IgnoreLayerCollision(9, 14);
    }
    private void Start() {
        mouseSensitivity = 300f;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update() {
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90f);
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        hoeParent.transform.localRotation = Camera.main.transform.localRotation;
        transform.Rotate(Vector3.up * mouseX);

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundLayer);

        if(isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        controller.Move((transform.right * x + transform.forward * z) * movementSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if(!isGrounded)
            velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

}
