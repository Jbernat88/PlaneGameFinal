using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    public AudioSource button;
    //Load the levels scene
    public void Levels()
    {
        SceneManager.LoadScene("Levels");

        button.Play();
    }

    //Load the menu scene
    public void Menu()
    {
        SceneManager.LoadScene("Menu");

        button.Play();
    }

    //Load the options scene
    public void Options()
    {
        SceneManager.LoadScene("Options");

        button.Play();
    }
}
