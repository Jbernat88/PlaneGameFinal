using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRotation : MonoBehaviour
{
    [SerializeField]
    private Transform target;
  
    // Update is called once per frame
    void Update()
    {
        Vector3 targetOrientation = target.position - transform.position;// Direeci�n de apuntado
        Debug.DrawRay(transform.position, targetOrientation, Color.green);//Linea para ver donde apunta la torreta
        transform.LookAt(target.transform);
        /*
        //Slerp
        Quaternion targetOrientationQuaternion = Quaternion.LookRotation(targetOrientation);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetOrientationQuaternion, Time.deltaTime);
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Proyectil") //Cuando colisione con el player se destruye la bala y se activa el effecto de particulas
        {
            //Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(other.gameObject);
            //soundManager.SelecionAudio(6, 0.3f
        }
    }
}
