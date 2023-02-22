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

    private bool canShoot = true;
    public float coolDown = 1f;

    public ParticleSystem explosion;

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
            if (canShoot)
            {
                canShoot = false;
                StartCoroutine(ShootCooldown());

                Instantiate(proyectil, lCanon.transform.position, proyectil.transform.rotation);
                Instantiate(proyectil, rCanon.transform.position, proyectil.transform.rotation);
            }
        }
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        Debug.Log(otherCollider.gameObject.name);

        if (otherCollider.gameObject.CompareTag("Money")) //Moneda
        {
            Destroy(otherCollider.gameObject);
                           
            UpdateScore(points); //Permite sumar los puntos de cada objeto          
        }

        if (otherCollider.gameObject.CompareTag("Bullet"))
        {
            UpdateLive(-10);
        }

        if (otherCollider.gameObject.CompareTag("Wall")) //Moneda
        {
            gameObject.SetActive(false);
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }

    public void UpdateScore(int pointsToAdd)
    {
        score++;//Linea per actualitzar el score
        pointText.text = $"Coins: {score}/{maxScore}";
    }

    private void UpdateLive(int Change)
    {
        currentHealth += Change;
        healthRingScript.health = currentHealth;
    }

    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(coolDown);
        canShoot = true;
    }

}
