using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private GameManagerSO gameManager;
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Light spotLight;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private AudioClip landingSound;
    [SerializeField] private float footstepInterval = 0.5f;
    private float nextFootstepTime;
    private bool wasInAir = false;
    private CharacterController characterController;
    private float verticalRotation = 0f;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
        HandleJump();
        HandleInteraction();
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(move.normalized * moveSpeed * Time.deltaTime);
        if ((horizontal != 0 || vertical != 0) && isGrounded && Time.time >= nextFootstepTime)
        {
            PlayRandomFootstep();
            nextFootstepTime = Time.time + footstepInterval;
        }
    }

    private void HandleJump()
    {
        bool wasGroundedBefore = isGrounded;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, characterController.height / 2 + 0.1f);

        if (!wasGroundedBefore && isGrounded && wasInAir)
        {
            audioSource.PlayOneShot(landingSound);
            wasInAir = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
            wasInAir = true;
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        velocity.y = Mathf.Clamp(velocity.y, -20f, 20f);

        characterController.Move(velocity * Time.deltaTime);
    }

    private void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(playerCamera.transform.position, transform.forward, out RaycastHit hit, interactDistance))
            {
                if (hit.transform.TryGetComponent(out Button button))
                {
                    button.ButtonPressed();
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            spotLight.enabled = !spotLight.enabled;
        }
    }

    private void PlayRandomFootstep()
    {
        if (footstepSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, footstepSounds.Length);
            audioSource.PlayOneShot(footstepSounds[randomIndex]);
        }
    }

    public void Die()
    {
        gameManager.PlayerDied();
    }
}
