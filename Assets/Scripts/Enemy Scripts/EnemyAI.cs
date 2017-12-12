using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 5f;
    public int givePoints;
    public int health;
    int level;//To determine zombies behaviour depending on level.
    public bool chasePlayer = false;

    public GameObject healthPack;
    public GameObject ammoPack;

    Transform targetTransform;

    GameManager gameManager;

    Animator zombieAnimator;

    // Use this for initialization
    void Start()
    {
        zombieAnimator = GetComponent<Animator>();

        gameManager = GameManager.instance;

        gameManager.zombieCount++;
        gameManager.zombieCreatedCount++;
        level = gameManager.currentLevel;//Sets the level coming from game manager.

        if (level == 1)// For level 1 , zombie will chase and attack to only player.
        {

            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;

        }
        else if (level == 2)//For level 2 , zombies will directly attack to the dog.
        {
            targetTransform = GameObject.FindGameObjectWithTag("Dog").transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //float distance = Vector3.Distance(transform.position, targetTransform.position);

        if (level == 1)
        {
            if (chasePlayer ) // If chase player trigger is triggered, zombie starts to chase.
            {
                Vector3 targetDir = targetTransform.position - transform.position;
                float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0, 0, angle);
                transform.Translate(Vector3.right * Time.deltaTime * speed);

            }
        }
        else if (level == 2)
        {
            Vector3 targetDir = targetTransform.position - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "bullet")
        {

            health -= 10;
            if (health <= 0)
            {

                Die();

            }
            Destroy(collision.gameObject);

        }

        if (collision.gameObject.tag == "shotgunBullet")
        {
            health -= 30;
            if (health <= 0)
            {
                Die();

            }
            Destroy(collision.gameObject);

        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (level == 1)
        {
            if (collider.gameObject.tag == "Player")
            {
                zombieAnimator.SetBool("moveZombie", true);
                zombieAnimator.SetBool("Idle", false);
                zombieAnimator.SetBool("attack", false);
                zombieAnimator.SetTrigger("ChangeState");


                chasePlayer = true;
            }
        }
        else if (level == 2)
        {

            zombieAnimator.SetBool("moveZombie", true);
            zombieAnimator.SetBool("Idle", false);
            zombieAnimator.SetBool("attack", false);
            zombieAnimator.SetTrigger("ChangeState");
        }

    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (level == 1)
        {
            if (collider.gameObject.tag == "Player")
            {
                zombieAnimator.SetBool("moveZombie", false);
                zombieAnimator.SetBool("Idle", true);
                zombieAnimator.SetBool("attack", false);
                zombieAnimator.SetTrigger("ChangeState");


                chasePlayer = false;
            }
        }


    }


    void Die()
    {
        
        int drop = Random.Range(1, 100);

        if (drop < 20 && drop > 10)// Health Pack drop.
        {
            Instantiate(healthPack, transform.position, Quaternion.identity);
        }
        else if (drop > 0 && drop < 10)// Ammo Pack drop.
        {
            Instantiate(ammoPack, transform.position, Quaternion.identity);
        }

        gameManager.DecreaseZombieCount();

        gameManager.AddScoreToPlayer(givePoints);
        Destroy(gameObject);


    }


}
