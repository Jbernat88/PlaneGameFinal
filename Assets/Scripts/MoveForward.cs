using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    //Bullet stats
    public float speed = 20f;//Velocity
    public float timelife = 5f;//Time life

    //Particles effects
    public GameObject explosionEffect;
    public GameObject explosionRock;
    public GameObject explosionEnemy;

    //Audio
    private AudioSource playerBulAudioSource;
    public AudioClip[] pBullet;

    // Start is called before the first frame update
    void Start()
    {
        playerBulAudioSource = GetComponent<AudioSource>();
        Destroy(gameObject, timelife); //The bullet has been destroyed after 5 seconds
    }

    // Update is called once per frame
    void Update()
    {
        //Forward movment
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
       

        if (other.tag == "Enemy") //When it collides with the Enemy, the bullet and the enemy, is destroyed and the particle effect is activated
        {
            playerBulAudioSource.PlayOneShot(pBullet[0]);
            Instantiate(explosionEnemy, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }

        if (other.tag == "Wall") //When it collides with the  the bullet, is destroyed and the particle effect is activated
        {
            playerBulAudioSource.PlayOneShot(pBullet[1]);
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.tag == "Asteroid") //When it collides with the Asteroids, the bullet and the asteroid, is destroyed and the particle effect is activated
        {
            playerBulAudioSource.PlayOneShot(pBullet[1]);
            Instantiate(explosionRock, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(other.gameObject);           
        }
    }
}
