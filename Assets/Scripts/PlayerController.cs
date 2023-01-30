using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //public static PlayerController instance;
    public Inventory inventory;
    
    public Rigidbody rb;
    public MoveOnGrid mog;
    public float movementSpeed = 4.0f;
    public Text coords;

    private float xCameraDist = 16f;
    private float yCameraDist = 11f;
    private float xPlayerDist = 5f;
    private float yPlayerDist = 5f;

    public Displayer displayer;
    public Health health;
    public InputToAnimator ita;
    public static PlayerController playerInstance;
    public GameObject bulletPrefab;
    //public AudioController audioController;

    private float swordDamage = 1.0f;
    public bool isJolted = false;
    public bool isInvinicible = false;
    public static bool isTransition = false;

    public AudioClip music, rupee, heart, damage, bombBlow, bombDrop, enemyDie, enemyHit, fanfare, die, shield, swordFull, sword;

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
        mog = GetComponent<MoveOnGrid>();
        //audioController = GetComponent<AudioController>();
        mog.movementSpeed = movementSpeed;
        
        displayer.displayHearts(3);

        //swapped these since sword is x
        displayer.displayLeft("Bow");
        displayer.displayRight("Sword");

        AudioSource.PlayClipAtPoint(music, Camera.main.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            swordAttack();
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            GetComponent<Bow>().Use(ita.lastDirection);//Hardcoded for milestone
        }
    }

    void FixedUpdate()
    {
        if (!(ita.isAttacking) && !(isJolted) && !isTransition)//I know this is a mess of state, it's a wip
        {
            mog.manualSet(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else if (ita.isAttacking || isTransition)
        {
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
//<<<<<<< HEAD

  
//=======
    

//>>>>>>> SeanGivesUp
    private void OnTriggerEnter(Collider coll)
    {
        Debug.Log("Entered: " + coll.name);
        GameObject other = coll.gameObject;
        //Debug.Log("Num Keys: " + inventory.GetKeys());
        if(other == null)
        {
            Debug.Log("Null trigger");

            return;
        }
        if(isTransition)
        {
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
                AudioSource.PlayClipAtPoint(enemyHit, transform.position);
            }
        }
        else if(other.tag.Equals("enemyProg"))
        {
            EnemyProjectile ep = other.GetComponent<EnemyProjectile>();
            if (!(isInvinicible))
            {
                TakeDamage(ep.damage);
                jolt(transform.position - coll.ClosestPoint(transform.position));
            }
        }
        else if (other.tag.Equals("rupee"))
        {
            
            inventory.AddRupees(1);
            
            Debug.Log("Collected rupee!");
            AudioSource.PlayClipAtPoint(rupee, other.transform.position);

            Destroy(other);

        }
        else if (other.tag.Equals("heart"))
        {
            Debug.Log("Collected heart");
            AudioSource.PlayClipAtPoint(heart, other.transform.position);
            Destroy(other);

            if(!health.isAtMaxHearts())
            {
                health.heal(1);
            }
            
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
            AudioSource.PlayClipAtPoint(heart, other.transform.position);
            Destroy(other);

            inventory.AddKeys(1);
            //anything else
        }
        

        //doorcheck
        else
        {
            
            if (other.tag.Equals("LnorthDoor") && inventory.GetKeys() > 0)
            {
                other.transform.parent.transform.Find("Unlocked").gameObject.SetActive(true);
                other.SetActive(false);
                inventory.AddKeys(-1);
                Debug.Log("Unlocked north door");
            }
            else if (other.tag.Equals("LeastDoor") && inventory.GetKeys() > 0)
            {
                other.transform.parent.transform.Find("Unlocked").gameObject.SetActive(true);
                other.SetActive(false);
                inventory.AddKeys(-1);
                Debug.Log("Unlocked east door");
            }
            else if (other.tag.Equals("LsouthDoor") && inventory.GetKeys() > 0)
            {
                other.transform.parent.transform.Find("Unlocked").gameObject.SetActive(true);
                other.SetActive(false);
                inventory.AddKeys(-1);
                Debug.Log("Unlocked south door");
            }
            else if(other.tag.Equals("LwestDoor") && inventory.GetKeys() > 0)
            {
                other.transform.parent.transform.Find("Unlocked").gameObject.SetActive(true);
                other.SetActive(false);
                inventory.AddKeys(-1);
                Debug.Log("Unlocked west door");
            }

            if (other.tag.Equals("northDoor") && mog.getyInput() > 0)
            {
                cameraDest.y += yCameraDist;
                playerDest.y += yPlayerDist;
                
            }
            else if(other.tag.Equals("eastDoor") && mog.getxInput() > 0)
            {
                   cameraDest.x += xCameraDist;
                   playerDest.x += xPlayerDist;

            }
            else if(other.tag.Equals("southDoor") && mog.getyInput() < 0)
            {
                   cameraDest.y -= yCameraDist;
                   playerDest.y -= yPlayerDist;
                
            }
            else if(other.tag.Equals("westDoor") && mog.getxInput() < 0)
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
        AudioSource.PlayClipAtPoint(damage, transform.position);
        if (health.hearts <= 0)
        {
            GameController.instance.GameOver();
        }
        displayer.displayHearts(health.hearts);
        
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (other.CompareTag("movable"))
        {
            int dir = 0;
            if (mog.getxInput() > 0)
            {
                dir = 1;
            }
            else if (mog.getyInput() < 0)
                
            {
                dir = 2;
            }
            else if (mog.getxInput() < 0)
            {
                dir = 3;
            }


            isTransition = true;

            StartCoroutine(MoveBlock(other.transform, dir));

            isTransition = false;
        }
    }

    //dir: 0 = N, 1 = E, 2 = S, 3 = W
    public IEnumerator MoveBlock(Transform block, int dir)
    {
        

        //isTransition = true;
        float x = block.position.x;
        float y = block.position.y;
        float z = block.position.z;
        
        if(dir == 0)
        {
            y++;
        }
        else if(dir == 1)
        {
            x++;
        }
        else if(dir == 2)
        {
            y--;
        }
        else
        {
            x--;
        }

        Vector3 blockDest = new Vector3(x, y, z);
        Debug.Log("Moving block " + dir);
        StartCoroutine(MoveObjectOverTime(block, block.position, blockDest, 1));
        mog.manualSet(0, 0);
        yield return new WaitForSeconds(3f);
        
        //isTransition = false;
    }

    //from https://github.com/ayarger/494_demos/blob/master/WorkshopCoroutines/Assets/Scripts/CoroutineUtilities.cs example
    public static IEnumerator MoveObjectOverTime(Transform target, Vector3 initial_pos, Vector3 dest_pos, float duration_sec)
    {
        isTransition = true;
        float initial_time = Time.time;
        // The "progress" variable will go from 0.0f -> 1.0f over the course of "duration_sec" seconds.
        float progress = (Time.time - initial_time) / duration_sec;

        while (progress < 1.0f)
        {
            // Recalculate the progress variable every frame. Use it to determine
            // new position on line from "initial_pos" to "dest_pos"
            progress = (Time.time - initial_time) / duration_sec;

            //make player disappear in between rooms
            if(target.CompareTag("Player"))
            {
                if (progress > 0.1f)
                {
                    target.localScale = new Vector3(0, 0, 0);
                }

                //bring player back
                if(progress > 0.8f)
                {
                    target.localScale = new Vector3(1, 1, 1);
                }
            }
            Vector3 new_position = Vector3.Lerp(initial_pos, dest_pos, progress);
            target.position = new_position;
            
            // yield until the end of the frame, allowing other code / coroutines to run
            // and allowing time to pass.
            yield return null;
        }

        target.position = dest_pos;
        isTransition = false;
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
            AudioSource.PlayClipAtPoint(swordFull, transform.position);
        }
        else
        {
            AudioSource.PlayClipAtPoint(sword, transform.position);
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
