using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Rio_Move : MonoBehaviour
{
    Animator animator;

    // Character Controller
    CharacterController cc;
    public float moveSpeed = 2f; // Velocidad de movimiento
    public float rotateSpeed = 5f; // Velocidad de rotaci�n
    private Vector3 moveDirection; // Direcci�n del movimiento

    // Salto
    public float jumpForce = 5f; // Fuerza del salto
    private float gravityValue = -9.81f; // Valor de la gravedad
    private Vector3 velocity; // Vector de velocidad
    private bool isGrounded; // �Est� en el suelo?

    void Start()
    {
        // Obtener referencias al Animator y CharacterController
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Comprobar si el personaje est� en el suelo
        isGrounded = cc.isGrounded;

        // Reiniciar la velocidad vertical si est� en el suelo
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;

            // Desactivar la animaci�n de salto al aterrizar
            animator.SetBool("Jump", false);
        }

        // Obtener el input del jugador
        float lateral = Input.GetAxis("Horizontal"); // Movimiento lateral
        float avanzar = Input.GetAxis("Vertical");   // Movimiento hacia adelante/atr�s

        // Configurar par�metros de animaci�n
        animator.SetFloat("X", lateral); // Movimiento lateral
        animator.SetFloat("Z", avanzar); // Movimiento hacia adelante/atr�s

        // Movimiento horizontal
        /*moveDirection = transform.right * lateral + transform.forward * avanzar;
        Vector3 rot = new Vector3(0, transform.rotation.y * lateral, 0);
        cc.Move(moveDirection * rot * moveSpeed * Time.deltaTime);*/

        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = moveSpeed * Input.GetAxis("Vertical");
        cc.SimpleMove(forward * curSpeed);


        // Saltar si el jugador presiona el bot�n y est� en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump(); // Llamar a la funci�n de salto
        }

        // Aplicar gravedad
        velocity.y += gravityValue * Time.deltaTime;

        // Mover el personaje en base a la gravedad
        cc.Move(velocity * Time.deltaTime);

        // Actualizar el par�metro de animaci�n "isGrounded"
        animator.SetBool("isGrounded", true);
    }

    void Jump()
    {
        // Calcular la velocidad inicial hacia arriba para el salto
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravityValue);

        // Activar la animaci�n de salto
        animator.SetBool("Jump", true);
    }
}



