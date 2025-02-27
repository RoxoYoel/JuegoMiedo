using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Rio_Move : MonoBehaviour
{
    // Character Controller
    CharacterController cc;
    Animator animator;

    public float moveSpeed; // Velocidad de movimiento
    public float runSpeed;
    public float rotateSpeed; // Velocidad de rotación

    float lateral;
    float avanzar;
    float curSpeed;

    void Start()
    {
        // Obtener referencias al Animator y CharacterController
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Obtener el input del jugador
        lateral = Input.GetAxis("Horizontal"); // Movimiento lateral
        avanzar = Input.GetAxis("Vertical");   // Movimiento hacia adelante/atrás

        animator.SetFloat("Speed", curSpeed);
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, lateral * rotateSpeed, 0);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        curSpeed = moveSpeed * avanzar;
        cc.SimpleMove(forward * curSpeed);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            curSpeed = runSpeed * avanzar;
            cc.SimpleMove(forward * curSpeed);
        }
    }
}



