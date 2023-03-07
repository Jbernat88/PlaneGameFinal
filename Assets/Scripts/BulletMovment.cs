using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovment : MonoBehaviour
{
    public float speed = 20f;//Velocity of the bullet  
    public float timelife = 5f;//Time Life of the bullet

    public GameObject explosionEffect;

    //private SoundManager soundManager;

    void Start()
    {
        Destroy(gameObject, timelife); //after 5 seconds the bullet has been destroyed
    }

    // Update is called once per frame
    void Update()
    {
        //Forward Movment
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    //Bullet colliders
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") //When it collides with the wplayer, the bullet is destroyed and the particle effect is activated
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject); //Destroy the bullet
        }

        if (other.tag == "Wall") //When it collides with the wall, the bullet is destroyed and the particle effect is activated
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);//Destroy the bullet
        }
    }
}
