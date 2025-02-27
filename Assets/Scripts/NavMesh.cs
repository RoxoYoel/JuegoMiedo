using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
    public Transform objetivo;
    private NavMeshAgent agent;
    private Animator animator;
    private bool detected = false;
    public float detectionRange;

    private Coroutine detectionCoroutine;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, objetivo.position);

        if (!detected && distance <= detectionRange)
        {
            detected = true;
            agent.SetDestination(objetivo.position);

            // Iniciar temporizador para verificar si el objetivo sigue cerca
            if (detectionCoroutine != null)
                StopCoroutine(detectionCoroutine);
            detectionCoroutine = StartCoroutine(CheckIfStillDetected());
        }

        if (detected)
        {
            if (agent.destination != objetivo.position)
            {
                agent.SetDestination(objetivo.position);
            }
        }
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
        }
    }

    IEnumerator CheckIfStillDetected()
    {
        yield return new WaitForSeconds(3f); // Espera 3 segundos

        float distance = Vector3.Distance(transform.position, objetivo.position);
        if (distance > detectionRange)
        {
            detected = false;
            agent.ResetPath(); // Detiene el movimiento del agente
        }
    }
}
