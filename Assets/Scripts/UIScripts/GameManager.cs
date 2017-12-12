using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public int deadCount;//Players dead count in a level.
    public int playerScore;//Player's total score.
    public int numberOfDeadZombies;
    public int totalZombieKilled = 0;

    public int wave;
    public int zombieCount;//Current alive zombie count;
    public int zombieCreatedCount;//Total created zombie count.

    public int currentLevel;
    public int difficulty;// 1: Easy , 2: Medium, 3: Hard.

    public bool isPlaying;// Whether the game is on play now.

    public GameObject waveCanvas;
    public Text waveCanvasTxt;// Wave count in the wave canvas.

    FormationController formationController;


    public static GameManager instance;

    public GameObject winCanvas;// going to be shown in level 1 after killing all the zombies.


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "Level01")
        {
            Debug.Log("Onlvl vas load");
            currentLevel = 1;
            instance.winCanvas = GameObject.Find("WinCanvas");
            instance.winCanvas.SetActive(false);

        }
        else if (SceneManager.GetActiveScene().name == "Level02")
        {
            currentLevel = 2;
            instance.waveCanvas = GameObject.Find("WaveCanvas");
            instance.waveCanvasTxt = GameObject.Find("WaveCountTxt").GetComponent<Text>();
            instance.waveCanvas.SetActive(false);
            zombieCount = 0;

            formationController = GameObject.Find("EnemySpawner").GetComponent<FormationController>();
            instance.StartCoroutine("StartNewWave");
        }
    }

    void Start()
    {
        playerScore = 0;

        wave = 0;
      

        difficulty = 1; // default  easy .

    }





    public void AddScoreToPlayer(int addedScore)
    {
        playerScore += addedScore;// Each zombie gives 100 point to the players score.
    }


    public int GetPlayerScore()
    {
        return playerScore;
    }


    public int WaveZombieCountCalculator()
    {

        return wave * difficulty * 3;

    }

    public int PlayerZombieKilledCount()
    {
        return zombieCreatedCount - zombieCount;
    }

    public void DecreaseZombieCount()
    {
        zombieCount--;
        totalZombieKilled++;

        if (zombieCount <= 0)// All zombies have died.
        {
            if (currentLevel == 2)
            {
                StartCoroutine("StartNewWave");//Starts the each wave.
            }
            else if (currentLevel == 1)//Change the current level to the level 2.
            {
                StartCoroutine("EndOfLevel1");// Change the level after waiting 3 seconds.
            }
        }
    }

    IEnumerator StartNewWave()
    {
        wave++;
        waveCanvasTxt.text = "Wave " + wave;
        waveCanvas.SetActive(true);
        zombieCreatedCount = 0;


        yield return new WaitForSeconds(3);

        formationController.SpawnUntilFull();
        waveCanvas.SetActive(false);
    }

    IEnumerator EndOfLevel1()
    {
        winCanvas.SetActive(true);

        yield return new WaitForSeconds(3);

        Application.LoadLevel(2);
    }





}
