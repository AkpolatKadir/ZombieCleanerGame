using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public int health=40;
    public AudioClip barkSound;
    public AudioClip damageSound;
    Animator dogAnimator;
    bool damageActive;

    GameManager gameManager;

    public GameObject gameover;
    public GameObject tryagain;


    // Use this for initialization
    void Start()
    {
        dogAnimator = GetComponent<Animator>();
        InvokeRepeating("Bark", 0.0f, 2.0f);
        damageActive = true;
        gameManager = GameManager.instance;
        
    }

    // Update is called once per frame

    IEnumerator MoveLeftAndRight()
    {
        while (true)
        {
            //dogAnimator.SetBool("Move");
        }
    }

    void Bark()
    {
        AudioSource.PlayClipAtPoint(barkSound, transform.position);

    }

    public void DealDamage(int damage)
    {
        if (health > 0 && damageActive == true)
        {
            health -= damage;
            AudioSource.PlayClipAtPoint(damageSound, transform.position);

        }
        if (health <= 0 && damageActive == true)
        {
            damageActive = false;
            if (gameManager.deadCount == 0)
                gameManager.deadCount = 1;
            else
                gameManager.deadCount++;

            StartCoroutine("RestartLevel", 3f);
            
        }
    }


    IEnumerator RestartLevel(float waitTime)
    {
        gameManager.zombieCreatedCount = 0;
        gameManager.wave = 0;
        gameManager.totalZombieKilled = 0;
        gameManager.zombieCount = 0;

        if (gameManager.deadCount < 3)
        {

            tryagain.SetActive(true);

        }
        else
        {
            gameover.SetActive(true);

        }

        yield return new WaitForSeconds(waitTime);

        if (gameManager.deadCount < 3)
        {

            Application.LoadLevel(2);
        }
        else
        {
            Application.LoadLevel(3);
        }
    }

}
