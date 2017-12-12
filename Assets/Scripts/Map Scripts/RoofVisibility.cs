using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofVisibility : MonoBehaviour {

    
	// Use this for initialization

	
	// Update is called once per frame


     void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        if (collision.tag == "Player")
        {
            Debug.Log("Roof Enter");
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

        }
    }
  
     void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            Debug.Log("Exit");
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
