using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue : MonoBehaviour {

    public string name;
    public string[] sentences;


    /*
    public GameObject canvas;
    public Text txt;
    public Button bt;
    private float time;
    public string[] npcSentences;
    public bool Display = false;
 
   

	// Use this for initialization
	void Start () {
        bt.gameObject.SetActive(false);
        
        time = 0;
    }
	
	// Update is called once per frame
	void Update () {

        
       
        
       

    }

    
    public void OnGuI()
    {
        time += Time.deltaTime;

        if (Display == true)
        {
           
            txt.text = npcSentences[0];
            
           
                txt.text = npcSentences[2];
            
           
                txt.text = npcSentences[3];
            
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
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Display = false;
        }
    }*/
}
