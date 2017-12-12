using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{

    public GameObject easyButton;
    public GameObject mediumButton;
    public GameObject hardButton;

    GameManager gameManager;



    void Start()
    {
        easyButton.SetActive(false);
        mediumButton.SetActive(false);
        hardButton.SetActive(false);

        gameManager = GameManager.instance;
    }


    public void LoadSceneFunction(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void OptionsOnClick()
    {

        easyButton.SetActive(true);
        mediumButton.SetActive(true);
        hardButton.SetActive(true);

    }
 

    public void ChangeDifficulty(int _difficulty)
    {
        gameManager.difficulty = _difficulty;
    }

	public void QuitGame()
	{
		Application.Quit ();
	}

}
