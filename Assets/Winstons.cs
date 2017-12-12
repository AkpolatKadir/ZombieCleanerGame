using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Winstons : MonoBehaviour {
	public Text winston;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		winston.text = "Winston's palace \n" + System.DateTime.Now.ToString ();
		
	}
}
