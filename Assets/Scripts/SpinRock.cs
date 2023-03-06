using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinRock : MonoBehaviour
{
    public float spinSpeed = 30f;
    public Vector3 direction;
   
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(direction, spinSpeed * Time.deltaTime);
    }
}
