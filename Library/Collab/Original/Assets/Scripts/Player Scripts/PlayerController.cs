using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum GunTypes : int { Knife, Pistol, Shotgun };//To make it clear to understand.

    public Gun[] guns;
    public int selectedGun = (int)GunTypes.Knife;//knife.
    private float waitingTime;
    float restartTimer;
    public float speed = 15.0f;
    public float restartDelay = 5f;
    public int health = 100;

    public AudioClip flashlightOnSound;//Turning on flashlight sound.

    public AudioClip damageSound;
    public AudioClip deathSound;


    Animator animator;
    public GameObject gameover;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        waitingTime = Time.time;
        gameover.SetActive(false);

    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //InvokeRepeating("Fire", 0.001f, guns[selectedGun].firingRate);
            Fire();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("AttackState", false);
            //CancelInvoke("Fire");
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            StartCoroutine("FlashlightTurnON");
        }

        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);

            gameover.SetActive(true);
            restartTimer += Time.deltaTime;
            if (restartTimer >= restartDelay)
            {

                Application.LoadLevel(1);
            }
        }
    }
    // Update is called once per frame
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
            animator.SetInteger("GunChoice", (int)GunTypes.Knife);//knife
            animator.SetTrigger("ChangeGun");
            selectedGun = (int)GunTypes.Knife;

        }


        if (Input.GetKey(KeyCode.Alpha2))//Sets variables for pistol.
        {

            animator.SetInteger("GunChoice", (int)GunTypes.Pistol);//pistol
            animator.SetTrigger("ChangeGun");//To trigger Any State animation in control.
            selectedGun = (int)GunTypes.Pistol;

        }

        if (Input.GetKey(KeyCode.Alpha3))//Sets variables for shotgun.
        {
            animator.SetInteger("GunChoice", (int)GunTypes.Shotgun);//shotgun.
            animator.SetTrigger("ChangeGun");
            selectedGun = (int)GunTypes.Shotgun;
        }
    }

    void Fire()
    {
        if (guns[selectedGun].bulletCount > 0 || selectedGun == (int)GunTypes.Knife)// If player has bullet or has knife.
        {
            if (Time.time >= waitingTime)
            {
                animator.SetBool("AttackState", true);
                Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                guns[selectedGun].onFire(this.gameObject.transform.GetChild(1), direction);//Gets GunPosition Game Object's position.      
                waitingTime = Time.time + guns[selectedGun].firingRate;
            }
            else
            {
                animator.SetBool("AttackState", false);
            }
        }
        else
        {
            AudioSource.PlayClipAtPoint(guns[selectedGun].emptyMagSound, Camera.main.transform.position);
        }
    }


    private IEnumerator Reload(int fullMagazineCount)
    {
        animator.SetBool("Reload", true);
        yield return new WaitForSeconds(0.55f);//Time to complete the animation.

        guns[selectedGun].bulletCount = fullMagazineCount;
        --guns[selectedGun].magazineCount;
        animator.SetBool("Reload", false);


    }

    private IEnumerator FlashlightTurnON()
    {
        yield return new WaitForSeconds(0.5f);

        AudioSource.PlayClipAtPoint(flashlightOnSound, Camera.main.transform.position);
        if (transform.GetChild(4).gameObject.active == true)
        {
            transform.GetChild(4).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(4).gameObject.SetActive(true);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            if (health > 0)
            {
                health -= 10;
                AudioSource.PlayClipAtPoint(damageSound, transform.position);
            }

        }
    }
}

