using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    //Load the levels scene
    public void Levels()
    {
        SceneManager.LoadScene("Levels");
    }

    //Load the menu scene
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    //Load the options scene
    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
}
