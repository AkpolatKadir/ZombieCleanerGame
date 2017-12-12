using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum GunTypes : int { Knife, Pistol, Shotgun };//To make it clear to understand.

    public Gun[] guns;
    public int selectedGun = (int)GunTypes.Knife;//knife.
    private float waitingTime;
    public GameObject muzzleEffect;//Gun muzzle effect.


    public float speed = 15.0f;
    public float restartDelay = 3f;
    public int health = 100;

    public bool damageActive = true;

    GameManager gameManager;



    public AudioClip flashlightOnSound;//Turning on flashlight sound.
    public AudioClip reloadSound;

    public AudioClip damageSound;
    public AudioClip deathSound;

    Animator playerAnimator;
    public GameObject gameover;
    public GameObject tryagain;
    Dog dog;

    // Use this for initialization
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        waitingTime = Time.time;
        gameover.SetActive(false);
        tryagain.SetActive(false);
        damageActive = true;
        gameManager = GameManager.instance;
        dog = GameObject.FindGameObjectWithTag("Dog").GetComponent<Dog>();

    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log("Difficulty" + gameManager.difficulty);
        if (Input.GetButtonDown("Fire1"))
        {
            //InvokeRepeating("Fire", 0.001f, guns[selectedGun].firingRate);
            Fire();

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerAnimator.SetBool("AttackState", false);
            //CancelInvoke("Fire");
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            StartCoroutine("FlashlightTurnON");
        }
        Debug.Log("player health" + health);


    }

    void FixedUpdate()
    {


        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }



        if (Input.GetKeyUp(KeyCode.R))
        {
            if (selectedGun == (int)GunTypes.Pistol && guns[selectedGun].magazineCount > 0)
            {

                if (guns[selectedGun].bulletCount < 12)
                {

                    StartCoroutine("Reload", 12);
                }

            }
            if (selectedGun == (int)GunTypes.Shotgun && guns[selectedGun].magazineCount > 0)
            {
                if (guns[selectedGun].bulletCount < 5)
                {
                    StartCoroutine("Reload", 5);
                }

            }

        }


        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 lookPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        lookPosition = lookPosition - transform.position;
        float angle = Mathf.Atan2(lookPosition.y, lookPosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);




        if (Input.GetKey(KeyCode.Alpha1))//Sets variables for knife.
        {
            playerAnimator.SetInteger("GunChoice", (int)GunTypes.Knife);//knife
            playerAnimator.SetTrigger("ChangeGun");
            selectedGun = (int)GunTypes.Knife;

        }


        if (Input.GetKey(KeyCode.Alpha2))//Sets variables for pistol.
        {

            playerAnimator.SetInteger("GunChoice", (int)GunTypes.Pistol);//pistol
            playerAnimator.SetTrigger("ChangeGun");//To trigger Any State animation in control.
            selectedGun = (int)GunTypes.Pistol;

        }

        if (Input.GetKey(KeyCode.Alpha3))//Sets variables for shotgun.
        {
            playerAnimator.SetInteger("GunChoice", (int)GunTypes.Shotgun);//shotgun.
            playerAnimator.SetTrigger("ChangeGun");
            selectedGun = (int)GunTypes.Shotgun;
        }
    }

    void Fire()
    {
        if (guns[selectedGun].bulletCount > 0 || selectedGun == (int)GunTypes.Knife)// If player has bullet or has knife.
        {
            if (Time.time >= waitingTime)
            {
                playerAnimator.SetBool("AttackState", true);
                Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                guns[selectedGun].onFire(this.gameObject.transform.GetChild(1), direction);//Gets GunPosition Game Object's position.
                if (selectedGun != (int)GunTypes.Knife)
                    StartCoroutine("MuzzleEffect");
                waitingTime = Time.time + guns[selectedGun].firingRate;
            }
            else
            {
                playerAnimator.SetBool("AttackState", false);
            }
        }
        else
        {
            AudioSource.PlayClipAtPoint(guns[selectedGun].emptyMagSound, Camera.main.transform.position);
        }
    }





    public void DealDamage(int damage)
    {
        if (health > 0 && damageActive == true)
        {
            health -= damage;
            Debug.Log("Player health" + health);
            AudioSource.PlayClipAtPoint(damageSound, transform.position);

        }
        if (health <= 0 && damageActive == true)
        {
            damageActive = false;

            gameManager.deadCount++;


            StartCoroutine("RestartLevel", 1.5f);
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag=="HealthPack")
        {
			if (gameManager.currentLevel == 1)
			{
				if (health != 100) 
				{

				if(health+20<=100)
				{
					health += 20;
					Destroy(collider.gameObject);
				}
				else
				{
					health = 100;
					Destroy(collider.gameObject);
				}
				}
			} 
			else if (gameManager.currentLevel == 2) 
			{
				if (dog.health != 100) 
				{

				if(dog.health+20<=100)
				{
					dog.health += 20;
					Destroy(collider.gameObject);
				}
				else
				{
					dog.health = 100;
					Destroy(collider.gameObject);
				}
				}
			}
           
            
        }
        else if ( collider.gameObject.tag=="AmmoPack")
        {
            guns[selectedGun].magazineCount += 2;
            Destroy(collider.gameObject);
        }
    }



    IEnumerator RestartLevel(float waitTime)
    {
        gameManager.zombieCreatedCount = 0;
        gameManager.wave = 0;
        gameManager.totalZombieKilled = 0;
        gameManager.zombieCount = 0;
        
        if (gameManager.deadCount < 3)
        {

            tryagain.SetActive(true);

        }
        else
        {
            gameover.SetActive(true);

        }

        yield return new WaitForSeconds(waitTime);

        if (gameManager.deadCount < waitTime)
        {
            Application.LoadLevel(1);
        }
        else
        {
            Application.LoadLevel(3);
        }
    }

    IEnumerator Reload(int fullMagazineCount)
    {
        playerAnimator.SetBool("Reload", true);
        AudioSource.PlayClipAtPoint(reloadSound, Camera.main.transform.position);
        yield return new WaitForSeconds(0.55f);//Time to complete the animation.

        guns[selectedGun].bulletCount = fullMagazineCount;
        --guns[selectedGun].magazineCount;
        playerAnimator.SetBool("Reload", false);


    }

    IEnumerator FlashlightTurnON()
    {
        yield return new WaitForSeconds(0.5f);

        AudioSource.PlayClipAtPoint(flashlightOnSound, Camera.main.transform.position);
        if (transform.GetChild(3).gameObject.active == true)
        {
            transform.GetChild(3).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(3).gameObject.SetActive(true);
        }

    }

    IEnumerator MuzzleEffect()
    {
        muzzleEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        muzzleEffect.SetActive(false);
    }





}

