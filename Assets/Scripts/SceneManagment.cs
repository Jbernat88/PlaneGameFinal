using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    public void Levels()
    {
        SceneManager.LoadScene("Levels");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void Map1()
    {
        SceneManager.LoadScene("1r");
    }
}
