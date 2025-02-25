using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agente;
    Animator anim;

    public float distanceEnemy;
    bool detected;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    /*IEnumerator DetectaMovimiento()
    {
        yield return new WaitForSeconds(3f);
    }*/

    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= distanceEnemy) 
        { 
            detected = true; 
            //StartCoroutine(DetectaMovimiento());
        }
        if (detected) 
        {
            agente.destination = target.position;
        }
        if (distance >= distanceEnemy)
        {
            detected = false;
        }
        anim.SetFloat("Speed", agente.velocity.magnitude);
    }
}
