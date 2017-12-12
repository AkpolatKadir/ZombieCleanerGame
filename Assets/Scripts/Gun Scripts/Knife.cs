using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Gun {

    // Use this for initialization
  

    // Update is called once per frame
    void Update()
    {

    }

    public override void onFire(Transform PlayerGunPosition,Vector3 mouseDirection)
    {
        Debug.Log("KNIFE SWING.");


        AudioSource.PlayClipAtPoint(fireSound, Camera.main.transform.position);
    }
}
