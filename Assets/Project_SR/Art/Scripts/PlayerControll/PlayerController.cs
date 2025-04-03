using UnityEngine;

namespace SR
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed = 5f;
        public float jumpHeight = 1.5f;
        public float gravity = -9.81f;

        [Header("Mouse Look")]
        public Transform cameraTransform;
        public float mouseSensitivity = 2f;
        public float verticalLookLimit = 80f;

        private float verticalRotation = 0f;
        private Vector3 velocity;

        private CharacterController characterController;
        private InputManager input;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            input = InputManager.Instance;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            HandleJump();        
            ApplyGravity();      
            HandleMovement();    
            HandleLook();        
        }

        private void HandleMovement()
        {
            Vector2 moveInput = input.MoveInput;
            Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
            characterController.Move(move * moveSpeed * Time.deltaTime);
        }

        private void HandleJump()
        {
            if (input.JumpPressed && characterController.isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                // TODO: 점프 애니메이션 트리거나 이펙트 추가 가능
            }
        }

        private void ApplyGravity()
        {
            if (characterController.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }

        private void HandleLook()
        {
            Vector2 lookInput = input.LookInput;

            // 좌우 회전
            transform.Rotate(Vector3.up * lookInput.x * mouseSensitivity);

            // 상하 회전 (카메라만)
            verticalRotation -= lookInput.y * mouseSensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit, verticalLookLimit);
            cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }
}
