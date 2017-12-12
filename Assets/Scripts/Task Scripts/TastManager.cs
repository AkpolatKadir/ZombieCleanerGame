using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TastManager : MonoBehaviour {
	public GameObject canvas;
    public Text txt;
    public Button bt;
    private float time;
    public string[] npcSentences;
    public bool Display = false;
	int count = 0;
   

	// Use this for initialization
	void Start () {
        //bt.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update () {
        //StartCoroutine(TypeSentences(npcSentences[0]));
   
    }

    
    public void OnGuI()
    {
        //StartCoroutine(TypeSentences(npcSentences[0]));
		if (Display) {
			Debug.Log ("OnGuide");
			canvas.gameObject.SetActive (true);

		}       
    }

    IEnumerator TypeSentences (string sentences)
    {
		
		yield return new WaitForSeconds (2);
        txt.text = "";
        foreach(char letter in sentences)
        {
            txt.text += letter;
            yield return null;

        }
		count++;
		if(count < npcSentences.Length){
			StartCoroutine(TypeSentences(npcSentences[count]));
		}
    }

    public void GoStartMenu()
    {
        Application.LoadLevel(1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            Display = true;
			canvas.gameObject.SetActive (true);
			StartCoroutine (TypeSentences (npcSentences [count]));
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Display = false;
			canvas.gameObject.SetActive (false);
        }
    }
}
