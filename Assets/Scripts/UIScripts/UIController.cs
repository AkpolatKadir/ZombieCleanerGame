using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public GameObject Player;
    public GameObject Dog;
    Dog dogController;
    private PlayerController playerController;
    private EnemyAI enemyAI;
    public Text bulletCount;
    public Text zombieCount;
    public Text healthTxt;
    public Text scoreTxt;
    public Text magazineTxt;
    public Text levelTxt;
    public Text dogTxt;
    
  

    GameManager gameManager;//We get the score from game manager.

    // Use this for initialization
    void Start()
    {

        playerController = Player.GetComponent<PlayerController>();
        dogController = Dog.GetComponent<Dog>();
        gameManager = GameManager.instance;
        
    }


    // Update is called once per frame
    void LateUpdate()
    {

      
        zombieCount.text = gameManager.zombieCount.ToString();       
        healthTxt.text = playerController.health.ToString();
        bulletCount.text = playerController.guns[playerController.selectedGun].bulletCount.ToString();
        scoreTxt.text = gameManager.GetPlayerScore().ToString();
        magazineTxt.text = playerController.guns[playerController.selectedGun].magazineCount.ToString();
        levelTxt.text = gameManager.deadCount.ToString();
        dogTxt.text = dogController.health.ToString();
       
        


    }

    
  
}
