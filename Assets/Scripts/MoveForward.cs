using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 20f;//Velocidad de la bala 
    public float timelife = 5f;//tiempo de duraci�n de la bala

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
}
