using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public GameObject iceBallPrefab;


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
            ita.useItem();
            GetComponent<AltWeaponScroller>().useItem();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GetComponent<AltWeaponScroller>().scrollAltWeapon();
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

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("collision: " + collision.gameObject.name);
    }
    private void OnTriggerEnter(Collider coll)
    {
        //Debug.Log("Entered: " + coll.name);
        GameObject other = coll.gameObject;

        if (other == null)
        {
            //Debug.Log("Null trigger");

            return;
        }
        if (GameController.isTransition)
        {
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            if (!(isInvinicible))
            {
                Debug.Log("HIT");
                AudioController.audioInstance.playEffect(AudioController.audioInstance.enemyHit);
                EnemyController ec = other.GetComponent<EnemyController>();
                health.takeDamage(ec.contactDamage);
                jolt(transform.position - coll.ClosestPoint(transform.position));
                
            }
        }
        else if (other.tag.Equals("enemyProg"))
        {
            if (!(isInvinicible))
            {
                AudioController.audioInstance.playEffect(AudioController.audioInstance.enemyHit);
                EnemyProjectile ep = other.GetComponent<EnemyProjectile>();
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
            GetComponent<Bow>().equipped = true;
            Destroy(other);
        }
        else if (other.CompareTag("Boomerang"))
        {
            GetComponent<Boomarang>().equipped = true;
            //Debug.Log("post Acquired boomerang");
            Destroy(other);
        }
        else if (other.CompareTag("Snowball"))
        {
            GetComponent<SnowballThrower>().equipped = true;
            Destroy(other);
        }
        else if(other.CompareTag("Ladder"))
        {
            mog.setAllowY(true);
        }
        else if(other.CompareTag("KeeseRoomLock"))
        {
            other.GetComponent<KeeseRoomDoorLock>().lockDoor();
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

    public void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Ladder"))
        {
            mog.setAllowY(true);
        }

        else if (other.CompareTag("Enemy"))
        {
            
            if (!(isInvinicible))
            {
                Debug.Log("HIT");
                AudioController.audioInstance.playEffect(AudioController.audioInstance.enemyHit);
                EnemyController ec = other.GetComponent<EnemyController>();
                health.takeDamage(ec.contactDamage);
                jolt(transform.position - other.ClosestPoint(transform.position));

            }
        }
        else if (other.tag.Equals("enemyProg"))
        {
            
            if (!(isInvinicible))
            {
                AudioController.audioInstance.playEffect(AudioController.audioInstance.enemyHit);
                EnemyProjectile ep = other.GetComponent<EnemyProjectile>();
                TakeDamage(ep.damage);
                jolt(transform.position - other.ClosestPoint(transform.position));

            }
        }
        else if (other.CompareTag("BowRoom"))
        {
            GameController.gameInstance.enterBowRoom();
        }
        else if (other.CompareTag("ExitBowRoom"))
        {
            GameController.gameInstance.exitBowRoom();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            mog.setAllowY(false);
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
        Collider c = collision.collider;
        GameObject other = collision.gameObject;
        Vector2 dir = Vector2.zero;
        if (other.CompareTag("movable") && acceptInput)
        {
            if (mog.getxInput() < 0)
            {
                dir = Vector2.left;
            }
            else if (mog.getxInput() > 0)
            {
                dir = Vector2.right;
            }
            else if (mog.getyInput() > 0)
            {
                dir = Vector2.up;
            }
            else if (mog.getyInput() < 0)
            {
                dir = Vector2.down;
            }
        }
        else if (other.CompareTag("movable2"))
        {
            if(mog.getxInput() > 0 || (mog.getxInput() == 0 && mog.getyInput() == 0))
            {
                return;
            }
            
            if (mog.getyInput() > 0)
            {
                dir = Vector2.up;
            }
            else if(mog.getyInput() < 0)
            {
                dir = Vector2.down;
            }
            else if(mog.getxInput() < 0)
            {
                dir = Vector2.left;
            }
            
        }

        if(!GameController.gameInstance.moving)
        {
            GameController.gameInstance.moving = true;
            StartCoroutine(GameController.gameInstance.StartMoveBlock(c, dir));
        }

    }

    private int vecToDir(Vector3 v)
    {
        if(Mathf.Abs(v.x) > Mathf.Abs(v.y))
        {
            if(v.x > 0)
            {
                return (2);
            }
            else
            {
                return (4);

            }
        }
        else
        {
            if(v.y >0)
            {
                return (1);

            }
            else
            {
                return (3);

            }
        }
    }

    public void jolt(Vector3 direction)
    {
        isJolted = true;
        isInvinicible = true;

        mog.moveDir(vecToDir(direction),movementSpeed*5.0f,0.2f);
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
                    if(hitData.collider.gameObject.GetComponent<MoveOnGrid>() && !(hitData.collider.gameObject.GetComponent<EnemyController>().chonker)) {
                        hitData.collider.gameObject.GetComponent<MoveOnGrid>().enemyJolt(castDir);
                    }
                }
            }
            else if(hitData.collider.gameObject.CompareTag("Frozen"))
            {
                Debug.Log("HIT FROZEN!");
                if (hitData.collider.gameObject.GetComponent<EnemyController>())
                {
                    IceBallController hold = Instantiate(iceBallPrefab).GetComponent<IceBallController>();
                    hold.launch(castDir);
                    hold.gameObject.transform.position = hitData.collider.gameObject.transform.position;
                    hitData.collider.gameObject.GetComponent<EnemyController>().die();

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