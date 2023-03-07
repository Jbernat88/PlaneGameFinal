using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpinObject : MonoBehaviour
{
    public float spinSpeed = 30f;//Spin speed
    public Vector3 direction;
    public bool activePlanet; 


    public string sceneName;

    // Update is called once per frame
    void Update()
    {
        //Rotete machanic
        transform.Rotate(direction, spinSpeed * Time.deltaTime);
    }

    //when we click on a planet it loads the scene depending on the name entered
    private void OnMouseDown()
    {
        if (activePlanet)
        {
            SceneManager.LoadScene(sceneName);
        }   
    }
}
