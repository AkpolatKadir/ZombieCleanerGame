using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelVisibility : MonoBehaviour {
    TastManager tm;

	// Use this for initialization
	void Start () {
        //gameObject.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {
        if(tm.Display ==true)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        if(tm.Display==false)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
		
	}
}
