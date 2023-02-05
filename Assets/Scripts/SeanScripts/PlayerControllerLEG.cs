using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerControllerLEG : MonoBehaviour
{

    public Inventory inventory;
    public Displayer displayer;
    public Health health;
    public InputToAnimator ita;
    public static PlayerControllerLEG playerInstance;

    public AudioClip rupeeSoundClip;

    public GameObject bulletPrefab;

    Rigidbody rb;

    public float movementSpeed = 4;
    private float swordDamage = 1.0f;
    public bool isJolted = false;
    public bool isInvinicible = false;

    private void Awake()
    {
        if (playerInstance == null)
        {
            playerInstance = this;
        }
        else if (playerInstance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inventory = GetComponent<Inventory>();
        displayer = GetComponent<Displayer>();
        health = GetComponent<Health>();
        ita = GetComponent<InputToAnimator>();
        if (inventory == null)
        {
            Debug.LogWarning("WARNING: PlayerController has no inventory!");
        }
        if (displayer == null)
        {
            Debug.LogWarning("WARNING: PlayerController has no displayer!");
        }
        displayer.displayHearts();
        //displayer.displayLeft("Sword");
        //displayer.displayRight("Bow");
        /*
        inventory = new Inventory();//This seems cleaner to me, do you agree?
        displayer = new Displayer();
        */
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentInput = GetInput();
        if (!(ita.isAttacking) && !(isJolted))//I know this is a mess of state, it's a wip
        {
            rb.velocity = currentInput * movementSpeed;
        } 
        else if(ita.isAttacking)
        {
            rb.velocity = new Vector3(0.0f,0.0f,0.0f);
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            swordAttack();
        }
    }


    //MARK

    Vector2 GetInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        
        //prevents diagonal movement
        if (Mathf.Abs(xInput) > 0.0f) {
            yInput = 0;
        }
        

        return new Vector2(xInput, yInput);
    }

    //MARK

    public void TakeDamage(float damageMultiplier)
    {
        ita.damaged();
        if(GameController.godMode || isInvinicible)
        {
            return;
        }
        health.hearts -= 0.5f * damageMultiplier;
        if (health.hearts <= 0)
        {
            GameController.gameInstance.gameOver();
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        GameObject other = coll.gameObject;

        if (other.tag.Equals("rupee"))
        {
            if (inventory != null)
            {
                inventory.AddRupees(1);
               // Debug.Log("Collected rupee!");
            }

            Destroy(other);

            AudioSource.PlayClipAtPoint(rupeeSoundClip, Camera.main.transform.position);
        }
        else if (other.CompareTag("Enemy"))
        {
            EnemyController ec = other.GetComponent<EnemyController>();
            if (!(isInvinicible))
            {
                TakeDamage(ec.contactDamage);
                jolt(transform.position - coll.ClosestPoint(transform.position));
            }
        }
    }








    //COUNTER MARK
    public void jolt(Vector3 direction)
    {
        isJolted = true;
        isInvinicible = true;
        rb.velocity = direction * 30.0f;
        StartCoroutine(stopjolt());
        StartCoroutine(stopInvincibllity());
        //Player is pushed away from enemy and becomes invincible temporarally
    }

    IEnumerator stopjolt()
    {
        yield return (new WaitForSeconds(0.1f));
        isJolted = false;
        rb.velocity = Vector3.zero;
    }

    IEnumerator stopInvincibllity()
    {
        yield return (new WaitForSeconds(1.0f));
        isInvinicible = false;
    }


    public void swordAttack()
    {
        if (!(ita.attack()))
        {
            return;
        }
        Vector3 castDir = new Vector3(0.0f, 0.0f, 0.0f);
        float len = 1.0f;
        switch (ita.lastDirection)
        {
            case 1:
                castDir.y = 1.0f;
                break;
            case 2:
                castDir.x = 1.0f;
                break;
            case 3:
                castDir.y = -1.0f;//BUGGED - DOES NOT HIT
                break;
            case 4:
                castDir.x = -1.0f;
                break;
        }
        RaycastHit hitData;

        if (Physics.Raycast(gameObject.transform.position, castDir, out hitData, len))
        {
            if (hitData.collider.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("HIT ENEMY!");
                if (hitData.collider.gameObject.GetComponent<EnemyController>())
                {
                    hitData.collider.gameObject.GetComponent<EnemyController>().takeDamage(swordDamage);
                }
            }
        }
        if (health.isAtMaxHearts())
        {
            fireBullet(ita.lastDirection);
        }
    }

    void fireBullet(int dir)
    {
        GameObject temp = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        temp.GetComponent<BulletController>().damage = swordDamage;
        temp.GetComponent<Animator>().SetInteger("dir", dir);
        float speed = 6.0f;
        switch (dir)
        {
            case 1:
                temp.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 1.0f, 0.0f) * speed;
                break;
            case 2:
                temp.GetComponent<Rigidbody>().velocity = new Vector3(1.0f, 0.0f, 0.0f) * speed;
                break;
            case 3:
                temp.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, -1.0f, 0.0f) * speed;
                break;
            case 4:
                temp.GetComponent<Rigidbody>().velocity = new Vector3(-1.0f, 0.0f, 0.0f) * speed;
                break;
        }

    }

}
