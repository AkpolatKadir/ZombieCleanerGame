using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public int bulletCount;
    public float bulletSpeed;
    public float firingRate;


    public GameObject bullet;
    public int magazineCount;


    public AudioClip fireSound;
    public AudioClip emptyMagSound;//When there is no bullet left.
    
  
	

    public virtual void onFire(Transform PlayerGunPosition,Vector3 mouseDirection)
    {
        Debug.Log("Bullet count decrase");
        //Bullet count decreased.
        --bulletCount;
    }
}
