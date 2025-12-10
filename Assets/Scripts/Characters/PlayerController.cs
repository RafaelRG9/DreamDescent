using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public InputReader input;
    public Transform mainCamera;

    [Header("Settings")]
    public float moveSpeed = 8f;
    public float rotationSpeed = 15f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float gravityMultiplier = 2f;

    // Internal Variables
    private CharacterController _controller;
    private Vector3 _velocity;
    private Vector2 _inputVector;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        if (mainCamera == null) mainCamera = Camera.main.transform;
    }

    void Start()
    {
        input.MoveEvent += HandleMove;
        input.JumpEvent += HandleJump;
    }

    void OnDestroy()
    {
        input.MoveEvent -= HandleMove;
        input.JumpEvent -= HandleJump;
    }

    private void HandleMove(Vector2 dir) => _inputVector = dir;

    private void HandleJump()
    {
        if (_controller.isGrounded)
            _velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
    }

    void Update()
    {
        ApplyMovement();
        ApplyGravity();
    }

    private void ApplyMovement()
    {
        // 1. If not moving, don't calculate anything
        if (_inputVector.sqrMagnitude < 0.1f) return;

        // 2. Calculate the Direction relative to the Camera
        // Atan2(x, y) converts input (A/D, W/S) to an angle
        float targetAngle = Mathf.Atan2(_inputVector.x, _inputVector.y) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;

        // 3. Create the rotation (Where we WANT to face)
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

        // 4. Rotate the character smoothly towards that direction
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // 5. Move in the direction we are facing
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        _controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (_controller.isGrounded && _velocity.y < 0) _velocity.y = -2f;
        _velocity.y += gravity * gravityMultiplier * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}