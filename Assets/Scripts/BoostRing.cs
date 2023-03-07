using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoostRing : MonoBehaviour
{
    //Boost components and stats
    public Image ringBoostBar;
    public float boost = 5f;
    private float maxBoost; 
    private float maxRingBoost = 5f;
    float lerpSpeed;

    private PlayerMovement playerMovmentScript;

    // Start is called before the first frame update
    void Start()
    {
        playerMovmentScript = FindObjectOfType<PlayerMovement>();//Conect the scripts
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovmentScript.canBoost) //Rest evrey frame to the bar
        {
            BoostDown();
        }

        else
        {
            BoostUp();
        }

        ColorChanger();
    }

    //Change the color when the bar goes down
    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.blue, Color.white, boost / maxRingBoost);
        ringBoostBar.color = healthColor;
    }

    void BoostDown()
    {
        if (boost != 0) //Can't be negative
        {
             boost -= Time.deltaTime;
            ringBoostBar.fillAmount = boost / maxRingBoost;
        }       
    }

    void BoostUp()
    {
        if (boost < 5) //As long as it is less than one we keep adding.
        {
            boost += Time.deltaTime;
            ringBoostBar.fillAmount = boost / maxRingBoost;
        }

    }
}
