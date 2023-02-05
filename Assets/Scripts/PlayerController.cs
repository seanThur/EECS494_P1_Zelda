using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController playerInstance;
    
    public Inventory inventory;
    public Displayer displayer;
    public Rigidbody rb;
    public MoveOnGrid mog;
    public float movementSpeed = 4.0f;

    public Health health;
    public InputToAnimator ita;

    public GameObject bulletPrefab;

    private float swordDamage = 1.0f;
    public bool isJolted = false;
    public bool isInvinicible = false;
    public static bool isTransition = false;
    public static bool acceptInput = true;
    private int secondary = 0;


    public Weapon altWeapon;

    //singleton pattern 
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
        void Start()
    {
        rb = GetComponent<Rigidbody>();
        inventory = GetComponent<Inventory>();
        health = GetComponent<Health>();
        ita = GetComponent<InputToAnimator>();
        mog = GetComponent<MoveOnGrid>();
        mog.movementSpeed = movementSpeed;
        altWeapon = GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            swordAttack();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //GetComponent<Bow>().Use(ita.lastDirection);//Hardcoded for milestone
            GetComponent<Bow>().Use(ita.lastDirection);//Hardcoded for milestone
                                                       // GetComponent<Boomarang>().Use(ita.lastDirection);//Hardcoded for milestone
                                                       //GetComponent<BombDropper>().Use(ita.lastDirection);//Hardcoded for milestone
            ita.useItem();
            
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            GetComponent<BombDropper>().Use(ita.lastDirection);//Hardcoded for milestone
        }
    }

    void FixedUpdate()
    {
        if (!acceptInput || ita.isAttacking || GameController.isTransition)
        {
            rb.velocity = Vector3.zero;
        }
        else if (!(ita.isAttacking) && !(isJolted) && acceptInput)//I know this is a mess of state, it's a wip
        {
            mog.manualSet(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        Debug.Log("Entered: " + coll.name);
        GameObject other = coll.gameObject;

        if (other == null)
        {
            Debug.Log("Null trigger");

            return;
        }
        if (GameController.isTransition)
        {
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("HIT");
            AudioController.audioInstance.playEffect(AudioController.audioInstance.enemyHit);
            EnemyController ec = other.GetComponent<EnemyController>();
            if (!(isInvinicible))
            {
                health.takeDamage(ec.contactDamage);
                jolt(transform.position - coll.ClosestPoint(transform.position));
                
            }
        }
        else if (other.tag.Equals("enemyProg"))
        {
            AudioController.audioInstance.playEffect(AudioController.audioInstance.enemyHit);
            EnemyProjectile ep = other.GetComponent<EnemyProjectile>();
            if (!(isInvinicible))
            {
                TakeDamage(ep.damage);
                jolt(transform.position - coll.ClosestPoint(transform.position));
                
            }
        }
        else if (other.tag.Equals("rupee"))
        {
            Debug.Log("Collected rupee!");
            inventory.AddRupees(1);
            AudioController.audioInstance.playEffect(AudioController.audioInstance.rupee);

            Destroy(other);

        }
        else if (other.tag.Equals("heart"))
        {
            Debug.Log("Collected heart");
            AudioController.audioInstance.playEffect(AudioController.audioInstance.heartKey);
            Destroy(other);

            if (!health.isAtMaxHearts())
            {
                health.heal(1);
            }

        }
        else if (other.tag.Equals("bomb"))
        {
            Debug.Log("Collected bomb");
            AudioController.audioInstance.playEffect(AudioController.audioInstance.bombDrop);
            Destroy(other);

            inventory.AddBombs(1);
            
        }
        else if (other.tag.Equals("key"))
        {
            Debug.Log("Collected key");
            AudioController.audioInstance.playEffect(AudioController.audioInstance.heartKey);
            Destroy(other);

            inventory.addKeys(1);
            
        }
        else if(other.CompareTag("Bow"))
        {
            Debug.Log("Acquired bow");
            inventory.acquireBow();
            Destroy(other);
        }
        else if (other.CompareTag("Boomerang"))
        {
            Debug.Log("pre Acquired boomerang");
            inventory.acquireBoomerang();
            Debug.Log("post Acquired boomerang");
            Destroy(other);
        }
        //doorcheck
        else
        {
            int dir = 0;

            if (other.tag.Equals("LnorthDoor") || other.tag.Equals("LeastDoor") || other.tag.Equals("LsouthDoor") || other.tag.Equals("LwestDoor") 
                && inventory.keyCount > 0)
            {
                GameController.gameInstance.unlockDoor(other);
            }
            else if (other.tag.Equals("northDoor"))
            {
                dir = 1;
            }
            else if (other.tag.Equals("eastDoor"))
            {
                dir = 2;
            }
            else if (other.tag.Equals("southDoor"))
            {
                dir = 3;

            }
            else if (other.tag.Equals("westDoor"))
            {
                dir = 4;
            }

            if(dir != 0)
            {
                GameController.gameInstance.transition(dir);
            }

        }
    }

    public void TakeDamage(float damageMultiplier)
    {
        if (GameController.godMode || isInvinicible)
        {
            return;
        }

        ita.damaged();
        health.takeDamage(damageMultiplier);
        AudioController.audioInstance.playEffect(AudioController.audioInstance.linkHurt);
        Debug.Log("TOOK DAMAGE: " + health.hearts);
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject other = collision.gameObject;
        Vector3 dir = Vector3.zero;
        if (other.CompareTag("movable") && acceptInput)
        {
            if (mog.getxInput() < 0)
            {
                dir = Vector3.left;
            }
            else if (mog.getxInput() > 0)
            {
                dir = Vector3.right;
            }
            else if (mog.getyInput() > 0)
            {
                dir = Vector3.up;
            }
            else if (mog.getyInput() < 0)
            {
                dir = Vector3.down;
            }
        }
        else if (other.CompareTag("movable2"))
        {
            if (!(mog.getyInput() < 0))
            {
                return;
            }

            dir = Vector3.down;
        }

        acceptInput = false;
        StartCoroutine(GameController.gameInstance.MoveBlock(other.transform, dir));
        acceptInput = true;
    }




    public void jolt(Vector3 direction)
    {
        isJolted = true;
        isInvinicible = true;
        mog.reverse(movementSpeed * 5.0f, 0.2f);
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
                len = 1.25f;
                break;
            case 2:
                castDir.x = 1.0f;
                len = 1.15f;
                break;
            case 3:
                castDir.y = -1.0f;//Downward
                len = 1.25f;
                break;
            case 4:
                castDir.x = -1.0f;
                len = 1.15f;
                break;
        }
        RaycastHit hitData;

        if (Physics.Raycast(gameObject.transform.position, castDir, out hitData, len, 3))
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
            AudioController.audioInstance.playEffect(AudioController.audioInstance.swordCombined);
        }
        else
        {
            AudioController.audioInstance.playEffect(AudioController.audioInstance.sword);
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