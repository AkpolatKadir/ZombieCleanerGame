using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	private void OnTriggerExit2D(Collider2D collider)
	{
        if (collider.gameObject.tag == "bullet")
        {
            Destroy(collider.gameObject);            

        }
        if (collider.gameObject.tag == "shotgunBullet")
        {
            Destroy(collider.gameObject);           
        }
    }

}
