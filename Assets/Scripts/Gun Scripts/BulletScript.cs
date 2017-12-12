using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public int LifeTime;
    // Use this for initialization


    // Update is called once per frame
 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, LifeTime);

    }



}
