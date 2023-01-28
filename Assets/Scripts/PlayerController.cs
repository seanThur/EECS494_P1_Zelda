using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //public static PlayerController instance;
    public Inventory inventory;
    public AudioClip rupeeSoundClip;
    public Rigidbody rb;
    public MoveOnGrid mog;
    public float movementSpeed = 4.0f;
    public Text coords;

    public float xCameraDist = 16;
    public float yCameraDist = 8;
    public float xPlayerDist = 4.5f;
    public float yPlayerDist = 4.5f;

    public Displayer displayer;
    public Health health;
    public InputToAnimator ita;
    public static PlayerController playerInstance;
    public GameObject bulletPrefab;
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
        //inventory = new Inventory();//Review
        inventory = GetComponent<Inventory>();
        displayer = GetComponent<Displayer>();
        health = GetComponent<Health>();
        ita = GetComponent<InputToAnimator>();
        mog = GetComponent<MoveOnGrid>();
        mog.movementSpeed = movementSpeed;
        if (inventory == null)
        {
            Debug.LogWarning("WARNING: PlayerController has no inventory!");
        }
        if (displayer == null)
        {
            Debug.LogWarning("WARNING: PlayerController has no displayer!");
        }
        displayer.displayHearts(3);
        displayer.displayLeft("Sword");
        displayer.displayRight("Bow");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            swordAttack();
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            GetComponent<Bow>().Use(ita.lastDirection);//Hardcoded for milestone
        }
    }

    void FixedUpdate()
    {
        if (!(ita.isAttacking) && !(isJolted))//I know this is a mess of state, it's a wip
        {
            mog.manualSet(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else if (ita.isAttacking)
        {
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
  
    private void OnTriggerEnter(Collider coll)
    {
        Debug.Log("Entered: " + coll.name);
        GameObject other = coll.gameObject;

        if(other == null)
        {
            Debug.Log("Null trigger");

            return;
        }

        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 cameraDest = Camera.main.transform.position;
        Vector3 playerPos = transform.position;
        Vector3 playerDest = transform.position;

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("HIT");
            EnemyController ec = other.GetComponent<EnemyController>();
            if (!(isInvinicible))
            {
                TakeDamage(ec.contactDamage);
                jolt(transform.position - coll.ClosestPoint(transform.position));
            }
        }
        else if (other.tag.Equals("rupee"))
        {
            
            inventory.AddRupees(1);
            
            Debug.Log("Collected rupee!");
            

            Destroy(other);

            AudioSource.PlayClipAtPoint(rupeeSoundClip, cameraPos);

        }
        else if (other.tag.Equals("heart"))
        {
            Debug.Log("Collected heart");
            Destroy(other);

            health.heal(1);
            displayer.displayHearts(health.hearts);

            //heart sound effect
        }
        else if(other.tag.Equals("bomb"))
        {
            Debug.Log("Collected bomb");
            Destroy(other);

            inventory.AddBombs(1);
            //anything else
        }
        else if(other.tag.Equals("key"))
        {
            Debug.Log("Collected key");
            Destroy(other);

            inventory.AddKeys(1);
            //anything else
        }
        //doorcheck
        else
        {
            
            if (other.tag.Equals("LnorthDoor") && inventory.GetKeys() > 0)
            {
                other.tag = "northDoor";
                inventory.AddKeys(-1);
                Debug.Log("Unlocked north door");
            }
            else if (other.tag.Equals("LeastDoor") && inventory.GetKeys() > 0)
            {
                other.tag = "eastDoor";
                inventory.AddKeys(-1);
                Debug.Log("Unlocked east door");
            }
            else if (other.tag.Equals("LsouthDoor") && inventory.GetKeys() > 0)
            {
                other.tag = "southDoor";
                inventory.AddKeys(-1);
                Debug.Log("Unlocked south door");
            }
            else if(other.tag.Equals("LwestDoor") && inventory.GetKeys() > 0)
            {
                other.tag = "westDoor";
                inventory.AddKeys(-1);
                Debug.Log("Unlocked west door");
            }

            if (other.tag.Equals("northDoor"))
            {
                cameraDest.y += yCameraDist;
                playerDest.y += yPlayerDist;
                
            }
            else if(other.tag.Equals("eastDoor"))
            {
                   cameraDest.x += xCameraDist;
                   playerDest.x += xPlayerDist;

            }
            else if(other.tag.Equals("southDoor"))
            {
                   cameraDest.y -= yCameraDist;
                   playerDest.y -= yPlayerDist;
                
            }
            else if(other.tag.Equals("westDoor"))
            {
                   cameraDest.x -= xCameraDist;
                   playerDest.x -= xPlayerDist;
            
            }
            else
            {
                return;
            }

            Debug.Log("hit " + other.name);

            StartCoroutine(MoveObjectOverTime(Camera.main.transform, cameraPos, cameraDest, 2));
            
            StartCoroutine(MoveObjectOverTime(transform, playerPos, playerDest, 2));

            
        }
    }


    public void TakeDamage(float damageMultiplier)
    {
        ita.damaged();
        if (GameController.godMode || isInvinicible)
        {
            return;
        }
        health.hearts -= 0.5f * damageMultiplier;
        if (health.hearts <= 0)
        {
            GameController.instance.GameOver();
        }
        displayer.displayHearts(health.hearts);
    }

    //from https://github.com/ayarger/494_demos/blob/master/WorkshopCoroutines/Assets/Scripts/CoroutineUtilities.cs example
    public static IEnumerator MoveObjectOverTime(Transform target, Vector3 initial_pos, Vector3 dest_pos, float duration_sec)
    {

        //dont hit any triggers
        if(target.GetComponent<Collider>() != null)
        {
            target.GetComponent<Collider>().enabled = false;
        }
        
        //player disappear
        if(target.tag.Equals("Player"))
        {
            target.localScale = new Vector3(0, 0, 0);
        }

        float initial_time = Time.time;
        // The "progress" variable will go from 0.0f -> 1.0f over the course of "duration_sec" seconds.
        float progress = (Time.time - initial_time) / duration_sec;

        while (progress < 1.0f)
        {
            // Recalculate the progress variable every frame. Use it to determine
            // new position on line from "initial_pos" to "dest_pos"
            progress = (Time.time - initial_time) / duration_sec;
            Vector3 new_position = Vector3.Lerp(initial_pos, dest_pos, progress);
            target.position = new_position;
            
            // yield until the end of the frame, allowing other code / coroutines to run
            // and allowing time to pass.
            yield return null;
        }

        target.position = dest_pos;

        //hit triggers again
        if(target.GetComponent<Collider>() != null)
        {
            target.GetComponent<Collider>().enabled = true;
        }

        //player reappear
        if (target.tag.Equals("Player"))
        {
            target.localScale = new Vector3(1, 1, 1);
        }
    }

    public void jolt(Vector3 direction)
    {
        isJolted = true;
        isInvinicible = true;
        mog.reverse(movementSpeed*5.0f,0.2f);
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
        float len=1.0f;
        switch (ita.lastDirection)
        {
            case 1:
                castDir.y = 1.0f;
                len = 1.25f;
                break;
            case 2:
                castDir.x = 1.0f;
                len = 1.15f;
                break;
            case 3:
                castDir.y = -1.0f;
                len = 1.25f;
                break;
            case 4:
                castDir.x = -1.0f;
                len = 1.15f;
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
