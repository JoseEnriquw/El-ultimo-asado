using UnityEngine;

namespace Assets.Scripts.Character
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private float walkSpeed = 2f;
        [SerializeField] private float runSpeed = 5f;
        [SerializeField] private float mouseSensitivity = 100f;

        private CharacterController characterController;
        private Animator animator;
        private Camera firstPersonCamera;

        private Vector3 moveDirection = Vector3.zero;
        private float gravity = -9.81f;
        private float verticalVelocity;

        private float xRotation = 0f;

        private void Awake()
        {
            // Obtener componentes necesarios
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            firstPersonCamera = GetComponentInChildren<Camera>();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            HandleMouseLook();
            HandleMovement();
            HandleAnimations();
        }

        private void HandleMouseLook()
        {
            // Obtener la entrada del mouse
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Rotar el jugador en el eje Y (horizontal)
            transform.Rotate(Vector3.up * mouseX);

            // Rotar la cámara en el eje X (vertical)
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            firstPersonCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        private void HandleMovement()
        {
            // Entrada del usuario
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            // Determinar velocidad actual
            float currentSpeed = isRunning ? runSpeed : walkSpeed;

            // Dirección de movimiento en relación con la orientación actual
            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                Vector3 moveDir = transform.right * direction.x + transform.forward * direction.z;
                moveDirection = moveDir.normalized * currentSpeed;
            }
            else
            {
                moveDirection = Vector3.zero;
            }

            // Aplicar gravedad mínima
            if (characterController.isGrounded)
            {
                verticalVelocity = -0.5f;
            }
            else
            {
                verticalVelocity += gravity * Time.deltaTime;
            }

            moveDirection.y = verticalVelocity;

            // Mover al personaje
            characterController.Move(moveDirection * Time.deltaTime);
        }

        private void HandleAnimations()
        {
            // Calcular la velocidad horizontal
            Vector3 horizontalVelocity = new Vector3(characterController.velocity.x, 0, characterController.velocity.z);
            float speedPercent = horizontalVelocity.magnitude / runSpeed;

            // Actualizar el parámetro 'Speed' en el Animator
            animator.SetFloat("Speed", speedPercent, 0.1f, Time.deltaTime);
        }
    }
}
