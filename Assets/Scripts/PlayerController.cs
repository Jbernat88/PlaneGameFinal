using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Score stats
    public TextMeshProUGUI pointText;
    private int score = 0;
    private int maxScore = 3;
    private int points;
    public string key;//Key to unlock the next level

    //Health stats
    private int currentHealth = 100;
    private HealthRing healthRingScript;

    //Shooting mechanic variables
    public GameObject proyectil;
    public GameObject lCanon;
    public GameObject rCanon;
    private bool canShoot = true;
    private float coolDown = 1f;

    //Particles effect
    public ParticleSystem explosion;

    //Game Over Panel
    public GameObject gameOverPanel;

    //Audios
    private AudioSource playerAudioSource;
    public AudioClip[] list;
    



    private void Awake()
    {
        healthRingScript = FindObjectOfType<HealthRing>(); //Conect the scripts
        healthRingScript.health = currentHealth; //We put life in the Health variable.
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();

        gameOverPanel.SetActive(false);

        if (!PlayerPrefs.HasKey("Score1"))//If I have not saved anything the score is 0
        {
            pointText.text = $"Coins: {score}/{maxScore}";
        }
        else if (PlayerPrefs.GetInt("Score1")!= 3)//if I haven't saved the player prefs but the score stays at 0
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
        if (healthRingScript.gameOver)//If life is 0 active the Game Over Panel
        {
            gameOverPanel.SetActive(true);
            gameObject.SetActive(false);
        }

        //Apear the misil.
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (canShoot)
            {
                playerAudioSource.PlayOneShot(list[3]);
                canShoot = false;
                StartCoroutine(ShootCooldown());

                Instantiate(proyectil, lCanon.transform.position, transform.rotation);
                Instantiate(proyectil, rCanon.transform.position, transform.rotation);
            }
        }
    }

    //Player Collides
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Money")) //Coins
        {
            playerAudioSource.PlayOneShot(list[0]);
            Destroy(otherCollider.gameObject);
                           
            UpdateScore(); //Add points in all the object
        }

        if (otherCollider.gameObject.CompareTag("Bullet"))//Enemy Bullet
        {
            playerAudioSource.PlayOneShot(list[1]);
            UpdateLive(-10);//Rest 10 live
        }

        if (otherCollider.gameObject.CompareTag("Wall")) //Wall
        {
            playerAudioSource.PlayOneShot(list[2]);
            healthRingScript.gameOver = true;
            
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (otherCollider.gameObject.CompareTag("Win")) //Win
        {
            SceneManager.LoadScene("Win");//Change to the win scene
        }

        if (otherCollider.gameObject.CompareTag("Asteroid")) //Asteroid
        {
            playerAudioSource.PlayOneShot(list[2]);
            healthRingScript.gameOver = true;

            Instantiate(explosion, transform.position, transform.rotation);
        }

    }

    public void UpdateScore()
    {
        if(score < maxScore)//If the level starts and I have already collected the 3, they do not add up
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

    //CoolDown of shoot
    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(coolDown);
        canShoot = true;
    }

}
