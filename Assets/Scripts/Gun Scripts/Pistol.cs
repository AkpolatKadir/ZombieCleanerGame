using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun {

    // Use this for initialization
  
    // Update is called once per frame
    void Start()
    {
        magazineCount = 5;
    }

    public override void onFire(Transform PlayerGunPosition, Vector3 mouseDirection)
    {
        Debug.Log("PISTOL SHOT.");
        base.onFire(PlayerGunPosition, mouseDirection);

        GameObject beam = Instantiate(bullet, PlayerGunPosition.position, PlayerGunPosition.rotation) as GameObject;        
        beam.GetComponent<Rigidbody2D>().AddForce(mouseDirection * bulletSpeed,ForceMode2D.Impulse);
        AudioSource.PlayClipAtPoint(fireSound, Camera.main.transform.position);//We always hear our player's sound effect on the max volume and in camera position.
        
       

    }
}
