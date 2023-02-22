using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoostRing : MonoBehaviour
{
    public Image ringBoostBar;
    public float boost = 5f;
    private float maxBoost; //Vida Max
    private float maxRingBoost = 5f;
    float lerpSpeed;
    private PlayerMovement playerMovmentScript;
    // Start is called before the first frame update
    void Start()
    {
        playerMovmentScript = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovmentScript.canBoost) //cada frame le restamos a la barra
        {
            BoostDown();
        }

        else
        {
            BoostUp();
        }

        ColorChanger();
    }

    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.blue, Color.white, boost / maxRingBoost);
        ringBoostBar.color = healthColor;
    }

    void BoostDown()
    {
        if (boost != 0) //no puede ser negativo
        {
             boost -= Time.deltaTime;
            ringBoostBar.fillAmount = boost / maxRingBoost;
        }       
    }

    void BoostUp()
    {
        if (boost < 5) //mientras sea menos q uno sieguimos sumando.
        {
            boost += Time.deltaTime;
            ringBoostBar.fillAmount = boost / maxRingBoost;
        }

    }
}
