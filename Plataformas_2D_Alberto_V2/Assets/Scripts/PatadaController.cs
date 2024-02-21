using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatadaController : MonoBehaviour
{
    private AudioSource audioSourcePlayer;
    public AudioClip damage;
    // Start is called before the first frame update
    void Start()
    {
        audioSourcePlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Gordo")
        {
            audioSourcePlayer.PlayOneShot(damage);
            Destroy(collision.gameObject);
        }

        if (collision.tag == "Gedeon")
        {
            audioSourcePlayer.PlayOneShot(damage);
            Destroy(collision.gameObject);
        }

    }
}
