using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthRing : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Image  ringHealthBar;
   

    private float maxHealth; //Vida Max
    public float health;
    float lerpSpeed;

    private PlayerController playerControllerScript;

    private void Start()
    {
        playerControllerScript = FindObjectOfType<PlayerController>();
        maxHealth = health;
    }

    private void Update()
    {
        healthText.text = $"{health}%";
        if (health > maxHealth) health = maxHealth; //sI SUMO VIDA ME EL MAXIMO SERA SIENDO 100

        lerpSpeed = 3f * Time.deltaTime; //VELOCIDAD DE BAJAR LA VIDA

        if( health <= 0)
        {
             
        }

        HealthBarFiller();
        ColorChanger();
    }

    void HealthBarFiller()
    {
        //velocidad a la que bajara de un punto de vida hasat el proximo.
        ringHealthBar.fillAmount = Mathf.Lerp(ringHealthBar.fillAmount, (health / maxHealth), lerpSpeed);
    }
    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.white, (health / maxHealth));      
        ringHealthBar.color = healthColor;
    }
    public void Damage(float damagePoints)
    {
        if (health != 0)
            health -= damagePoints;
    }
    public void Heal(float healingPoints)
    {
        if (health < maxHealth)
            health += healingPoints;
    }
}
