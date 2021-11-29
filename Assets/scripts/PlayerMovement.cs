using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the script used to control the player
/// in the scene. This will move the object and
/// rotate the camera as well.
/// script by: Camilo Zambrano
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// This controls how fast or how slow the
    /// camera moves when the player moves it's
    /// mouse.
    /// </summary>
    [Range(0f, 200f)]
    [SerializeField]
    [Tooltip("This controls how fast or how slow the camera moves when the player moves it's mouse.")]
    private float LookSensitivity = 50f;

    /// <summary>
    /// This is the speed that the player will have when moving."
    /// </summary>
    [SerializeField]
    [Tooltip("This is the speed that the player will have when moving.")]
    private float speed = 10f;

    /// <summary>
    /// This is the gravity that will be used to
    /// calculate the vertical speed when the
    /// player is airbone and not touching the
    /// ground.
    /// </summary>
    [SerializeField]
    [Tooltip("This is the gravity that will be used to calculate the vertical speed when the  player is airbone.")]
    private float gravity = -9.806f;

    /// <summary>
    /// This is the object used to detect if the
    /// player is touching the ground.
    /// </summary>
    [SerializeField]
    [Tooltip("This is the object used to detect if the player is touching the ground.")]
    private Transform groundCheck;

    /// <summary>
    /// This is how far the floor can be from the
    /// ground check in order to mark the player as
    /// grounded or airbone.
    /// </summary>
    [SerializeField]
    [Tooltip("This is how far the floor can be from the ground check in order to mark the player as grounded or airbone.")]
    private float distanceToGround = 0.5f;

    /// <summary>
    /// This is the layer used to detect platforms
    /// on the level.
    /// </summary>
    [SerializeField]
    [Tooltip("This is the layer used to detect platforms on the level.")]
    private LayerMask platformsMask;

    /// <summary>
    /// This is the character controller used for
    /// the player.
    /// </summary>
    private CharacterController _controller;

    /// <summary>
    /// This is the transform component for the
    /// camera object used in the scene.
    /// </summary>
    private Transform _cameraTransform;

    /// <summary>
    /// This is the ammount of rotation we apply to
    /// the camera's transform in the scene.
    /// </summary>
    private float _cameraRotation = 0f;

    /// <summary>
    /// This is the speed vector for the player,
    /// used when the player is airbone.
    /// </summary>
    private Vector3 _currentSpeed;

    /// <summary>
    /// This is used to tell if the player is
    /// touching the ground or not.
    /// </summary>
    private bool _isGrounded;

    /// <summary>
    /// This method is called on the first frame
    /// and we use it to set up the reference to
    /// the camera in the scene and hide the cursor.
    /// </summary>
    void Start()
    {
        if (transform.childCount > 0)
        {
            _cameraTransform = transform.GetChild(0);
        }

        Cursor.lockState = CursorLockMode.Locked;
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCamera();
        UpdateMovement();
    }

    /// <summary>
    /// This method updates the camera angle using
    /// the mouse (when the player moves the mouse,
    /// the camera moves too).
    /// </summary>
    private void UpdateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        mouseX *= LookSensitivity * Time.deltaTime;

        float mouseY = Input.GetAxis("Mouse Y");
        mouseY *= LookSensitivity * Time.deltaTime;

        _cameraRotation -= mouseY;
        _cameraRotation = Mathf.Clamp(_cameraRotation, -90f, 90f);

        _cameraTransform.localRotation = Quaternion.Euler(_cameraRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    /// <summary>
    /// This method moves the player based on the
    /// current inputs of the player. W, A, S, and
    /// D are the preferred Keys for this, but the
    /// input system also suports arrow keys and
    /// some controllers too.
    /// </summary>
    private void UpdateMovement()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, distanceToGround, platformsMask);

        if (_isGrounded && _currentSpeed.y != 0)
        {
            _currentSpeed.y = -1f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 playerMovement = transform.right * horizontal;
        playerMovement += transform.forward * vertical;

        _ = _controller.Move(playerMovement * speed * Time.deltaTime);

        _currentSpeed.y += gravity * Time.deltaTime;

        _ = _controller.Move(_currentSpeed * Time.deltaTime);
    }
}
