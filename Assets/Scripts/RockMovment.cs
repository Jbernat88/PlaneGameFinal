using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMovment : MonoBehaviour
{
    public float speed = 20f;//Velocidad de la bala 
    public Transform playerTarget;
    public float timelife = 5f;//tiempo de duración de la bala
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(playerTarget.transform);
        Destroy(gameObject, timelife); //En 5 segundos se destruye la bala
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento en forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
