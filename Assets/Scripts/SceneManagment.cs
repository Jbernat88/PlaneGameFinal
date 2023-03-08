using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    public AudioSource buttonClick;
    public AudioClip click;

    //Load the levels scene
    public void Levels()
    {
       
        StartCoroutine(SceneFlow("Levels"));
    }

    //Load the menu scene
    public void Menu()
    {
        
        StartCoroutine(SceneFlow("Menu"));
    }

    //Load the options scene
    public void Options()
    {
        StartCoroutine(SceneFlow("Options"));
    }

    public void Uno()
    {
        StartCoroutine(SceneFlow("1r"));
    }

    public void Dos()
    {
        StartCoroutine(SceneFlow("2ndo"));
    }

    public void Tres()
    {
        StartCoroutine(SceneFlow("3ero"));
    }

    //Wait one second when change the scene 
    public IEnumerator SceneFlow(string name)
    {
        buttonClick.PlayOneShot(click); //Active the sound
        yield return new WaitForSeconds(1f);//Wait 1 second
        SceneManager.LoadScene(name);//Load the scenes
    }
}
