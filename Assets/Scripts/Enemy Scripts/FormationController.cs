using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour
{

    public GameObject enemyPrefabs;
    GameManager gameManager;
    public float width = 10f;
    public float height = 5f;
    public float spawnDelay;


    // Use this for initialization
    void Start()
    {
        gameManager = GameManager.instance;
    }

    public void SpawnUntilFull()
    {
        if (gameManager.zombieCreatedCount <= gameManager.WaveZombieCountCalculator())//Creates correct amount of zombies.
        {
            Transform freePosition = NextFreePosition();

            if (freePosition)
            {
                GameObject enemy = Instantiate(enemyPrefabs, freePosition.position, Quaternion.identity) as GameObject;
                enemy.transform.parent = freePosition;
            }
            if (NextFreePosition())
            {
                Invoke("SpawnUntilFull", spawnDelay);
            }
        }
        
            //CancelInvoke("SpawnUntilFull");
    }

    Transform NextFreePosition()
    {
        Debug.Log(" Yeni yer bulcak");

        return transform.GetChild(Random.Range(0, transform.childCount));
        /* foreach (Transform childPositionGameObject in transform)
         {
             if (childPositionGameObject.childCount == 0)
             {
                 return childPositionGameObject;
             }
         }

         return null;*/
    }




    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }




}
