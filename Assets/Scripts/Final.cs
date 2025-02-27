using UnityEngine;

public class Final : MonoBehaviour
{
    public GameObject[] final;
    Collider colFinal;
    int aleatorio;
    public GameObject noCasa;
    public GameObject casa;
    public GameObject iglesia;
    public GameObject reintentar;

    Animator anim;
    Rio_Move movimiento;
    CharacterController cc;

    void Start()
    {
        anim = GetComponent<Animator>();
        movimiento = GetComponent<Rio_Move>();
        cc = GetComponent<CharacterController>();
        aleatorio = Random.Range(0, final.Length);
        print(aleatorio);
        colFinal = final[aleatorio].GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == colFinal)
        {
            casa.SetActive(true);
            Time.timeScale = 0;
        }
        else if(!other.gameObject.CompareTag("Iglesia") && !other.gameObject.CompareTag("Enemy"))
        {
            noCasa.SetActive(true);
        }

        if (other.gameObject.CompareTag("Iglesia"))
        {
            iglesia.SetActive(true);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            anim.SetTrigger("Dead");
            movimiento.enabled = false;
            cc.enabled = false;
            Invoke("Reintentar", 3f);
        }
    }

    public void Reintentar()
    {
        reintentar.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        noCasa.SetActive(false);
        iglesia.SetActive(false);
    }
}
