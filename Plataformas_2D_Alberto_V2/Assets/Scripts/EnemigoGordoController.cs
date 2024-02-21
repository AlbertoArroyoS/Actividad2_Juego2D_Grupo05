using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NewBehaviourScript : MonoBehaviour
{


    //saber la position del player
    public Transform player;
    //velocidad del enemigo
    float speed = 2f;
    //para voltear al enemigo
    SpriteRenderer spriteRenderer;
    bool endGame = false;
    // public PlayerController playerController;
    // private int damage = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        
        // Buscar el objeto del jugador solo si no es nulo
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            //playerController = playerObject.GetComponent<PlayerController>();
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            //movernos hacia el player, moveTowards del punto A a B
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            if (transform.position.x > player.position.x)
            {
                //voltear
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }

    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        

    }*/
}
