using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionDificultad : MonoBehaviour
{

    GameObject panelFacil;
    GameObject panelDificil;

    private AudioSource audioSourcePlayer;
    public AudioClip empezarCancion;

    // Start is called before the first frame update
    void Start()
    {
        panelFacil = GameObject.Find("PanelFacil");
        panelDificil = GameObject.Find("PanelDificil");
        panelDificil.SetActive(false);
        panelFacil.SetActive(false);
        audioSourcePlayer = GetComponent<AudioSource>();
        audioSourcePlayer.PlayOneShot(empezarCancion);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void botonAventura()
    {
        panelFacil.SetActive(true);



    }

    public void botonNormal()
    {
        panelDificil.SetActive(true);

    }

    public void botonComenzarFacil()
    {

        //cargar la escena de game llamando al gestor
        SceneManager.LoadScene("GameFacil");
        

    }
    public void botonComenzarDificil()
    {
        
        //cargar la escena de game llamando al gestor
        SceneManager.LoadScene("Game");

    }

    public void botonVolver()
    {
        panelFacil.SetActive(false);
        panelDificil.SetActive(false);
    }

    

    
}
