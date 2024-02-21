using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CofreController : MonoBehaviour
{
    private AudioSource audioSourcePlayer;
    public AudioClip exito;
    public bool tieneAnillo = false;
    public GameObject panelWin;
    private Animator animator;
    private GameObject globos;

    // Start is called before the first frame update
    void Start()
    {
        audioSourcePlayer = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        globos = GameObject.FindGameObjectWithTag("Globos");
        globos.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag== "Player" && tieneAnillo)
        {
            GetComponent<Animator>().SetTrigger("open");
            audioSourcePlayer.PlayOneShot(exito);
            globos.SetActive(true);
            //activar fin del juego
            StartCoroutine("finalJuego");
        }

        
    }
    //corrutina para que cambie la escena 4 segundos despues de abrir el cofre
    IEnumerator finalJuego()
    {

        //segundos a esperar
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("FinalGanar");

    }


}
