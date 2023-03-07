using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthRing : MonoBehaviour
{
    //Variables to save UI components
    public TextMeshProUGUI healthText;
    public Image  ringHealthBar;

    //Variables to save Particles components
    public ParticleSystem explosion;
    public Transform spawnParticles;

    //Variables to set Health Stats 
    private float maxHealth; 
    public float health;
    float lerpSpeed;

    //GameOver
    public bool gameOver;

    //Conection with the PlayerController Script
    private PlayerController playerControllerScript;

    private void Start()
    {
        gameOver = false; //Game over start false
        playerControllerScript = FindObjectOfType<PlayerController>();
        maxHealth = health; //we set the health to be the same as max health
    }

    private void Update()
    {
        healthText.text = $"{health}%"; // The health text displays our current health
        if (health > maxHealth) health = maxHealth; // If i add live, my live still be 100  

        lerpSpeed = 3f * Time.deltaTime; //Deacrese life bar at this velocity

        //If life is 0, activate the game over panel
        if( health <= 0)
        {
            gameOver = true;
            gameObject.SetActive(false);
            Instantiate(explosion, spawnParticles.position, spawnParticles.rotation);
        }

        HealthBarFiller();
        ColorChanger();
    }
    //I display the current health in the health bar
    void HealthBarFiller()
    {
        ringHealthBar.fillAmount = Mathf.Lerp(ringHealthBar.fillAmount, (health / maxHealth), lerpSpeed);
    }

    //Change the color of my health bar
    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.white, (health / maxHealth));      
        ringHealthBar.color = healthColor;
    }

    //Funcion to take damage
    public void Damage(float damagePoints)
    {
        if (health != 0)
            health -= damagePoints;
    }
}
