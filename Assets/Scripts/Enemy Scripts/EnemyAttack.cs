using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    PlayerController playerController;
    Dog dog;
     GameManager gameManager;
    int level;
    public float attackSpeed;
    Animator zombieAnimator;
    int damage;
    // Use this for initialization


    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        dog = GameObject.FindGameObjectWithTag("Dog").GetComponent<Dog>();
        zombieAnimator = GetComponentInParent<Animator>();
         gameManager = GameManager.instance;//Sets the level coming from game manager.

        level = gameManager.currentLevel;

        damage = 5 * gameManager.difficulty;

    }



    private void OnCollisionExit2D(Collision2D collider)
    {

        if (level == 1)
            CancelInvoke("AttackToPlayer");
        else if (level == 2)
            CancelInvoke("AttackToDog");

        zombieAnimator.SetBool("moveZombie", true);
        zombieAnimator.SetBool("Idle", false);
        zombieAnimator.SetBool("attack", false);
        zombieAnimator.SetTrigger("ChangeState");
        GetComponent<EnemyAI>().chasePlayer = true;

    }


    void AttackToPlayer()
    {
        zombieAnimator.SetBool("moveZombie", false);
        zombieAnimator.SetBool("Idle", false);
        zombieAnimator.SetBool("attack", true);
        zombieAnimator.SetTrigger("ChangeState");


        playerController.DealDamage(damage);//Deal damage to the player.
    }

    void AttackToDog()
    {
        zombieAnimator.SetBool("moveZombie", false);
        zombieAnimator.SetBool("Idle", false);
        zombieAnimator.SetBool("attack", true);
        zombieAnimator.SetTrigger("ChangeState");


        dog.DealDamage(damage);//Deal damage to the dog.
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (level == 1)
        {
            if (collision.gameObject.tag == "Player")
            {
                InvokeRepeating("AttackToPlayer", 0.1f, attackSpeed);
                GetComponent<EnemyAI>().chasePlayer = false;
            }
        }
        else if (level == 2)
        {
            if (collision.gameObject.tag == "Dog")
            {
                InvokeRepeating("AttackToDog", 0.1f, attackSpeed);
                GetComponent<EnemyAI>().chasePlayer = false;
            }
        }

    }

}
