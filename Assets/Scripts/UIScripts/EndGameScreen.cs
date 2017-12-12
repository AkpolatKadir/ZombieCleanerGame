using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameScreen : MonoBehaviour
{
    public Text zombiesKilledTxt;
    public Text score;
    void Start()
    {
        zombiesKilledTxt.text = GameManager.instance.totalZombieKilled.ToString();
        score.text= "SCORE: " + GameManager.instance.playerScore;
    }



    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
