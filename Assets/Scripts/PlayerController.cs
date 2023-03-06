using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI pointText;
    private int score = 0;
    private int maxScore = 3;
    public int points;
    public string key;

    private int currentHealth = 100;
    public HealthRing healthRingScript;

    public GameObject proyectil;
    public GameObject lCanon;
    public GameObject rCanon;

    private bool canShoot = true;
    public float coolDown = 1f;

    public ParticleSystem explosion;

    public GameObject gameOverPanel;

    

    private void Awake()
    {
        healthRingScript = FindObjectOfType<HealthRing>();
        healthRingScript.health = currentHealth; //Metemos la vida en la variable Health.
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);

        if (!PlayerPrefs.HasKey("Score1"))//si no he guardat rs secore es 0
        {
            pointText.text = $"Coins: {score}/{maxScore}";
        }
        else if (PlayerPrefs.GetInt("Score1")!= 3)//si no he guardat pero score no es 3 se manten score a 0
        {
            pointText.text = $"Coins: {score}/{maxScore}";
        }
        else
        {
            pointText.text = $"Coins: {maxScore}/{maxScore}";
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        if (healthRingScript.gameOver)
        {
            gameOverPanel.SetActive(true);
            gameObject.SetActive(false);
        }

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

                Instantiate(proyectil, lCanon.transform.position, transform.rotation);
                Instantiate(proyectil, rCanon.transform.position, transform.rotation);
            }
        }
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Money")) //Moneda
        {
            Destroy(otherCollider.gameObject);
                           
            UpdateScore(); //Permite sumar los puntos de cada objeto
        }

        if (otherCollider.gameObject.CompareTag("Bullet"))
        {
            UpdateLive(-10);
        }

        if (otherCollider.gameObject.CompareTag("Wall")) //Moneda
        {
            healthRingScript.gameOver = true;
            
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (otherCollider.gameObject.CompareTag("Win")) //Moneda
        {
            SceneManager.LoadScene("Win");
        }

    }

    public void UpdateScore()
    {
        if(score < maxScore)//si començ es nivell y ya he pillat ses 3 no les pill
        {
            score++;//Linea per actualitzar el score
            pointText.text = $"Coins: {score}/{maxScore}";

            PlayerPrefs.SetInt(key, score);
        }
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
