using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinRock : MonoBehaviour
{
    public float spinSpeed = 30f;
    public Vector3 direction; //Direction where rotate
   
    // Update is called once per frame
    void Update()
    {
        //Spin
        transform.Rotate(direction, spinSpeed * Time.deltaTime);
    }
}
