using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI pointText;
    private int score = 0;
    private int maxScore = 3;
    public int points;

    private int currentHealth = 100;
    public HealthRing healthRingScript;

    public GameObject proyectil;
    public GameObject lCanon;
    public GameObject rCanon;

    private void Awake()
    {
        healthRingScript = FindObjectOfType<HealthRing>();
        healthRingScript.health = currentHealth; //Metemos la vida en la variable Health.
    }

    // Start is called before the first frame update
    void Start()
    {
        pointText.text = $"Coins: {score}/{maxScore}";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            healthRingScript.Damage(10);
        }

        //Aparición del proyectil.
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Instantiate(proyectil, lCanon.transform.position, gameObject.transform.rotation);
            Instantiate(proyectil, rCanon.transform.position, gameObject.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider otherCollider)
    {

        if (otherCollider.gameObject.CompareTag("Money")) //Moneda
        {
            Destroy(otherCollider.gameObject);
                           
            UpdateScore(points); //Permite sumar los puntos de cada objeto          
        }

        if (otherCollider.gameObject.CompareTag("Bullet"))
        {
            UpdateLive(-10);
        }
    }

    public void UpdateScore(int pointsToAdd)
    {
        score++;//Linea per actualitzar el score
        pointText.text = $"Coins: {score}/{maxScore}";

       // Debug.Log($"Tienes");
    }

    private void UpdateLive(int Change)
    {
        currentHealth += Change;
        healthRingScript.health = currentHealth;
    }

}
