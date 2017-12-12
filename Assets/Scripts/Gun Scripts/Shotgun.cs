using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun {

    // Use this for initialization



     void Start()
    {
        magazineCount = 3;
    }
    // Update is called once per frame
    void Update () {
		
	}

    public override void onFire(Transform PlayerGunPosition,Vector3 mouseDirection)
    {
        Debug.Log("SHOTGUN SHOT.");
        base.onFire(PlayerGunPosition, mouseDirection);

        GameObject beam = Instantiate(bullet, PlayerGunPosition.position, PlayerGunPosition.rotation) as GameObject;        
        beam.GetComponent<Rigidbody2D>().AddForce(mouseDirection * bulletSpeed, ForceMode2D.Impulse);

        AudioSource.PlayClipAtPoint(fireSound, Camera.main.transform.position);//to make the gun sounds better we play it on the camera position.
    }
}
