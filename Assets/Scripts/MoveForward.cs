using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 20f;//Velocidad de la bala 
    public float timelife = 5f;//tiempo de duración de la bala

    public GameObject explosionEffect;
    public GameObject explosionRock;
    public GameObject explosionEnemy;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timelife); //En 5 segundos se destruye la bala
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento en forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
       

        if (other.tag == "Enemy") //Cuando colisione con el player se destruye la bala y se activa el effecto de particulas
        {
            Debug.Log("fer");
            Instantiate(explosionEnemy, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(other.gameObject);
            //soundManager.SelecionAudio(6, 0.3f
        }

        if (other.tag == "Wall") //Cuando colisione con la pared se destruye la bala y se activa el effecto de particulas
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.tag == "Asteroid") //Cuando colisione con la pared se destruye la bala y se activa el effecto de particulas
        {
            Instantiate(explosionRock, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(other.gameObject);           
        }
    }
}
