using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionEscena : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //funciones de los botones

    public void botonStart()
    {
        //cargar la escena de game llamando al gestor
        SceneManager.LoadScene("Dificultad");


    }

    public void botonExit()
    {
        //cargar la escena de game llamando al gestor
        Application.Quit();
    }
}
