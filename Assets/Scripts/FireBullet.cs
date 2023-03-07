using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public GameObject bullet;
    [SerializeField]
    private float timer = 2f;//timer shoot of turret
                             
    [SerializeField]
    private int counter;

    public Transform target;
    private float attackRange = 70;//attack range of turret
    private bool canShoot;


    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;//The torret can shoot 
    }

    private void Update()
    {
        //The turret starts firing if the player enters the enemy's range
        float dist = Vector3.Distance(target.transform.position, transform.position);

        if (dist <= attackRange)
        {
            Attack();            
        }
    }

    void Attack()
    {
        if (canShoot)
        {
            canShoot = false;//The enemy can't shoot
            StartCoroutine(FireBullets_CR()); //The Courutine Start
        }
    }

    IEnumerator FireBullets_CR()
    { 
         Instantiate(bullet, transform.position, transform.rotation);//Instance the bullet
         yield return new WaitForSeconds(timer);//It is re-instantiated depending on the timer variable
         canShoot = true;
    }
}
