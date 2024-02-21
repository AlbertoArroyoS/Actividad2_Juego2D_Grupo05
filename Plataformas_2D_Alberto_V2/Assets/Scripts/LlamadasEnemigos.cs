using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class LlamadasEnemigos : MonoBehaviour
{

    //variables
    public GameObject enemigoGordoPrefab;
    public GameObject enemigoGedeonPrefab;
    // Start is called before the first frame update
    //saber la position del player
    private Transform player;

    void Start()
    {
        // Buscar el objeto del jugador solo si no es nulo
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        StartCoroutine("CrearEnemigos");    
    }

    // Update is called once per frame
    void Update()
    {
        //Cuando nuestro personaje muera, que desaparezcan los enemigos de pantalla y no se sigan creando
        if (player == null)
        {
            pararEnemigos();
        }


    }

    //corrutina, funcion especial que se pueden poner pausas y continua desde el punto que se quedo antes de la pausa
    IEnumerator CrearEnemigos()
    {

        //segundos a esperar
        yield return new WaitForSeconds(5);
        while (true)
            {
            //instanciar enemigos, que enemigo , posicion del objeto que tiene el script, quaternion que no se rote
             Instantiate(enemigoGordoPrefab,transform.position, Quaternion.identity);
            //hacer pausa con tiempo Random
             yield return new WaitForSeconds(Random.Range(4f,7f));
             //si se quiere llamar en cualquier lugar, CREAR UN VECTOR3 CON X, Y ALEATORIAS
             //Instantiate(enemigoGordoPrefab, ****CAMBIAR ESTO ****, Quaternion.identity);
        }
       
    }

    public void pararEnemigos()
    {
        StopCoroutine("CrearEnemigos");
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Gordo");
        for (int i = 0; i < enemigos.Length; i++)
        {
            Destroy(enemigos[i]);
           
        }
    }
}
