using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpinObject : MonoBehaviour
{
    public float spinSpeed = 30f;
    public Vector3 direction;

    public string sceneName;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(direction, spinSpeed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene(sceneName);
    }
}
