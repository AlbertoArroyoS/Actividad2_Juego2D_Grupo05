using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    Transform cam; //camara principal
    Vector3 camStartPos;
    float distance;


    GameObject[] backgrounds;
    Material[] mat;
    float[] backSpeed;

    float farthestBack;
    [Range(0.0001f, 0.02f)]
    public float parallaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        for(int i = 0; i < backCount; i++)
        {
            backgrounds[i]= transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
        }
        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        for(int i = 0;i < backCount; i++) // encontrar el plano que esta mas lejano
        {
            if (backgrounds[i].transform.position.z - cam.position.z > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - cam.position.z;
            }
        }

        for(int i = 0; i< backCount ; i++)
        {
            backSpeed[i]= 1 - (backgrounds[i].transform.position.z - cam.position .z); // el mas lejano atras
        }
    }

    private void LateUpdate()
    {
        if (cam == null) // Comprueba si la cámara se ha destruido
        {
            return;
        }
        distance = cam.position.x -camStartPos.x;
        transform.position = new Vector3(cam.position.x, transform.position.y, 0);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i]*parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0)*speed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
