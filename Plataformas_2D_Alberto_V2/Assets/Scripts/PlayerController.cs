using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    //Atributos
    private float moveHorizontal;
    private float moveVertical;
    private Rigidbody2D rigidPlayer;
    //velocidad del jugador, si es floar poner f al final
    private float speed = 4.5f;
    private SpriteRenderer render;
    private float jumpForce = 7.50f;
    private bool isOnGround = true;
    private bool esGolpeado = false;
    //puntos que valen los objetos
    private int puntosTotales;
    private int puntosManzana = 1;
    private int puntosMoneda = 2;
    private int puntosGema = 3;
    public TextMeshProUGUI puntosTexto, maxPuntosTexto;

    private AudioSource audioSourcePlayer;
    public AudioClip saltoClip, manzanaClip, gemaClip, monedaClip,atacaClip, muerteExplosionClip, anilloConseguido, vidaConseguida, damage, failCrofre;
    public Camera cameraPlayer;
    public GameObject explosionPrefab;

    private LlamadasEnemigos enemigosLlamada;
    public GameObject panelPerder;

    [Header("Animacion")]
    private Animator animator;

    //vidas
    public GameObject[] vidas;
    private int vidaCorazon = 3;
    //daño
    private int damageGordo = 1;
    private int damageGedeon = 2;

    private CofreController cofreController;
    private int puntosMax;
    private int puntosPartida;

   // public bool tieneAnillo = false;




    // Start is called before the first frame update
    void Start()
    {
        //objeto.componente.propiedad
        //objeto.componente.metodo()
        rigidPlayer = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        audioSourcePlayer = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        animator.SetBool("enSuelo", true);

        cofreController = GetComponent<CofreController>();

        //leer del registro de windows con PlayerPrefs la puntuacion maxima, entre sesiones de juego , puedes guardar datos y nombres
        puntosMax = PlayerPrefs.GetInt("PuntuacionMaxima",0);
        maxPuntosTexto.text = ("MAX: " + puntosMax.ToString());
        //reiniciar la puntuacion de la partida
        PlayerPrefs.SetInt("PuntuacionPartida", 0);


    }

    // Update is called once per frame
    void Update()
    {
        //patada
        if(Input.GetKeyDown(KeyCode.Space))
        {
            audioSourcePlayer.PlayOneShot(atacaClip);
            animator.SetTrigger("Ataque");
            
        }

        //salto
        if(Input.GetKeyDown(KeyCode.UpArrow) && isOnGround) 
        {

            animator.SetBool("enSuelo", false);
            audioSourcePlayer.PlayOneShot(saltoClip);
            rigidPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }

        //si no esta en tierra que coja la animacion de salto
        
        if (isOnGround == false )
        {
            animator.SetBool("enSuelo", false);
        }
        if(isOnGround == true )
        {
            animator.SetBool("enSuelo", true);
        }
        


        //movimiento
        moveHorizontal = Input.GetAxis("Horizontal");
        //para la animacion
        animator.SetFloat("Horizontal",Mathf.Abs (moveHorizontal));

        if (moveHorizontal < 0)
        {
            render.flipX = true;
        }
        if (moveHorizontal > 0)
        {
            render.flipX = false;
        }
        //la posicion mas actual + lo que sea la tecla que se pulse y velocidad a la que se esta moviendo con la variable speed
        transform.position += new Vector3(moveHorizontal,0,0) * Time.deltaTime*speed;



    }
    //cuando se tocan
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            animator.SetBool("enSuelo", true);
            isOnGround = true;
            esGolpeado = false;
        }

        if (collision.gameObject.tag == "Gordo")
        {
            audioSourcePlayer.PlayOneShot(damage);
            isOnGround = true;
            esGolpeado = true;
            //quitar la vida dependiendo del daño y que se descuenten los corazones de 1 en 1
            for (int i = 0; i < damageGordo; i++)
            {
                perderVida(1);
            }

        }

        if (collision.gameObject.tag == "Gedeon")
        {
            audioSourcePlayer.PlayOneShot(damage);
            isOnGround = true;
            esGolpeado = true;
            //quitar la vida dependiendo del daño y que se descuenten los corazones de 1 en 1
            for (int i = 0; i < damageGedeon; i++)
            {
                perderVida(1);
            }

        }

        if (collision.gameObject.tag == "PlataformaMovil")
        {
            animator.SetBool("enSuelo", true);      
        }
        if (collision.gameObject.tag == "Cofre")
        {
            isOnGround = true;
        }

        if (collision.gameObject.tag == "Cofre" && FindAnyObjectByType<CofreController>().tieneAnillo == false)
        {
            // ******* SONIDO  failCrofre
            audioSourcePlayer.PlayOneShot(failCrofre);
            animator.SetTrigger("TieneAnillo");
            // audioSourcePlayer.PlayOneShot(atacaClip);
           
        }



    }
    //cuando no se esta tocando el player con el suelo
    private void OnCollisionExit2D(Collision2D collision)
    {
        isOnGround = false;

        if (collision.gameObject.tag == "Cofre")
        {
            isOnGround = true;
            //animator.ResetTrigger("SinLlave");

        }
        if (collision.gameObject.tag == "Gordo" || collision.gameObject.tag == "Gedeon")
        {
            isOnGround = true;
        }
    }

    //Para sumar puntos con los objetos
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Manzana")
        {
            audioSourcePlayer.PlayOneShot(manzanaClip);
            puntosTotales += puntosManzana;
            //sacar mensaje por consola para depurar
            //Debug.Log("Puntos " + puntosTotales);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Moneda")
        {
            audioSourcePlayer.PlayOneShot(monedaClip);
            puntosTotales += puntosMoneda;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Gema")
        {
            audioSourcePlayer.PlayOneShot(gemaClip);
            puntosTotales += puntosGema;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Anillo")
        {
            audioSourcePlayer.PlayOneShot(anilloConseguido);
            //puntosTotales += puntosGema;
            //Destroy(collision.gameObject);
        }
        if (collision.tag == "Corazon")
        {
            bool vidaRecuperada = recuperarVida();
            if (vidaRecuperada)
            {
                audioSourcePlayer.PlayOneShot(vidaConseguida);
                Destroy(collision.gameObject);
            }

        }

        puntosTexto.text = ("PUNTOS: " + puntosTotales.ToString());
        PlayerPrefs.SetInt("PuntuacionPartida", puntosTotales);
        //Cuando se supere la puntuacion maxima que se ponga la maxima

        if (puntosTotales> puntosMax)
        {
            puntosMax=puntosTotales;
            maxPuntosTexto.text = ("MAX: " + puntosMax.ToString());
            PlayerPrefs.SetInt("PuntuacionMaxima", puntosMax);
        }

        
    }
    public void desactivarVida(int indice)
    {
        vidas[indice].SetActive(false);
    }

    public void activarVida(int indice)
    {
        vidas[indice].SetActive(true);
    }

    //restara las vidas por daño y devuelve cuantas le quedan al personaje
    //como tendremos varios personajes con distinto daño, se le pasa por parametro
    public void perderVida(int damage)
    {
        vidaCorazon -= damage;

        // Verificar si el índice está dentro del rango del array vidas
        if (vidaCorazon >= 0 && vidaCorazon < vidas.Length)
        {
            desactivarVida(vidaCorazon);
        }

        if (vidaCorazon <= 0)
        {
            panelPerder.SetActive(true);
            Instantiate(explosionPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            cameraPlayer.transform.parent = null;
            Destroy(gameObject);
        }
    }

    public bool recuperarVida()
    {
        if (vidaCorazon == 3 )
        {
            return false;
        }
        activarVida(vidaCorazon);
        vidaCorazon += 1;
        return true;
    }

}

