using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Inicio : MonoBehaviour
{
    public Volume vl;
    float suma = 0.05f;

    private void Awake()
    {
        Time.timeScale = 0;
        vl.weight = 0f;
        StartCoroutine("Ebrio");
    }

    IEnumerator Ebrio()
    {
        for (float i = 0; i < 1; i+=suma)
        {
            vl.weight = i;
            yield return new  WaitForSeconds(0.1f);
        }
        StartCoroutine(MenosEbrio());
    }

    IEnumerator MenosEbrio()
    {
        for (float i = 1; i > 0; i -= suma)
        {
            vl.weight = i;
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(Ebrio());
    }

    public void Reanudar() 
    { 
        Time.timeScale = 1;
    }

    public void Ocultar(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void Mostrar (GameObject obj)
    {
        obj.SetActive(true);
    }

    public void Reintentar()
    {
        SceneManager.LoadScene(0);
    }
}
