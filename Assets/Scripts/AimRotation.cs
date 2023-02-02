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
        Vector3 targetOrientation = target.position - transform.position;// Direeción de apuntado
        Debug.DrawRay(transform.position, targetOrientation, Color.green);//Linea para ver donde apunta la torreta

        //Slerp
        Quaternion targetOrientationQuaternion = Quaternion.LookRotation(targetOrientation);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetOrientationQuaternion, Time.deltaTime);
    }
}
