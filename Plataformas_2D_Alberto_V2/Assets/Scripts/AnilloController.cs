using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnilloController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //que rote el anillo
        //Otras formas para rotar que hacen lo mismo
            transform.Rotate(0,1,0);
        //transform.Rotete(new Vector3(0,1,0));
       // transform.Rotate(Vector3.up);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag== "Player")
        {
            //audioSourcePlayer.PlayOneShot(anilloConseguido);
            //encontrar un objeto en la gerarquia que tenga este controlador
            FindAnyObjectByType<CofreController>().tieneAnillo = true;
            Destroy(gameObject);
        }
    }
}
