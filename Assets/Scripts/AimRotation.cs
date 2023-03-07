using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRotation : MonoBehaviour
{
    [SerializeField]
    private Transform target;//Target that enemy always look
  
    // Update is called once per frame
    void Update()
    {
        Vector3 targetOrientation = target.position - transform.position;//Aim Direction
        Debug.DrawRay(transform.position, targetOrientation, Color.green);//Linea para ver donde apunta la torreta
        transform.LookAt(target.transform); //The enemys always look the player
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Proyectil") //if the player bullet collides with enemy, the enemy and the bullet has been destroyed
        {
            //Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
