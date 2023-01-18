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

    public float movementSpeed = 4.0f;

    public Text coords;

    public static float gridDist = 0.5f;

    public float xCameraDist = 16;
    public float yCameraDist = 8;
    public float xPlayerDist = 4.5f;
    public float yPlayerDist = 4.5f;
    private float x;
    private float y;
    private float xRem;
    private float yRem;
    private bool onGridx;
    private bool onGridy;

    private void Awake()
    {
        if (GameController.player == null)
        {
            GameController.player = this;
        }
        else if (GameController.player != this)
        {
            Destroy(gameObject);
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inventory = new Inventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //set gridMovement values
        x = rb.transform.position.x;
        y = rb.transform.position.y;

        //with only 1 decimal point
        xRem = (float) System.Math.Floor((double)(x % gridDist) * 10) / 10;
        yRem = (float)System.Math.Floor((double)(y % gridDist) * 10) / 10;

        if (xRem == 0.0f)
        {
            onGridy = true;
        }
        else
        {
            onGridy = false;
        }

        if (yRem == 0.0f)
        {
            onGridx = true;
        }
        else
        {
            onGridx = false;
        }

        //calls grid movement, checks movement
        Vector2 currentInput = GetInput();

        //coords.text = x.ToString() + " " + y.ToString() + "\n" + onGridx.ToString() + " " + onGridy.ToString() + "\n" + currentInput.x.ToString() + " " + currentInput.y.ToString();

        rb.velocity = currentInput * movementSpeed;
    }

    Vector2 GetInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        if(xInput == 0.0f && yInput == 0.0f)
        {
            return Vector2.zero;
        }


        //prevents diagonal movement, implements grid movement
        if (Mathf.Abs(xInput) > 0.0f)
        {
            yInput = 0.0f;
        }

        //by this point, only have an x input or a y input
        Debug.Assert((xInput != 0.0f && yInput == 0.0f) || (xInput == 0.0f && yInput != 0.0f), x.ToString() + " " + y.ToString() + " invalid");

        //should also be on one of the grids
        Debug.Assert(onGridx || onGridy, "Not on either gridline!");

        return gridMovement(xInput, yInput);
    }

    private Vector2 gridMovement(float xInput, float yInput)
    {
        Debug.Assert(xInput == 0.0f || yInput == 0.0f, "One input must be 0 by this point!");
        float xOutput = xInput; 
        float yOutput = yInput;

        //if xInput but not on grid x
        if (xInput != 0.0f && !onGridx)
        {
            //should be on grid y
            if (!onGridy)
                Debug.Assert(onGridy, "Not on either gridline! (should be on grid y)");

            xOutput = 0.0f;

            if (y % gridDist < (gridDist / 2.0f))
            {
                yOutput = -0.5f;
            }
            else
            {
                yOutput = 0.5f;
            }
        }
        else if (yInput != 0.0f && !onGridy)
        {
            if (!onGridx)
                Debug.Assert(onGridx, "Not on either gridline! (should be on grid x)");

            yOutput = 0.0f;

            if (x % gridDist < (gridDist / 2.0f))
            {
                xOutput = -0.5f;
            }
            else
            {
                xOutput = 0.5f;
            }
        }


        //Debug.Log("x: " + x + " " + "y: " + y + " xOutput: " + xOutput + " yOutput: " + yOutput + " onGridx: " + onGridx + " onGridy: " + onGridy
        //    + " xRem: " + y % xGridDist + " yRem: " + x % yGridDist);

        return new Vector2(xOutput, yOutput);
    }

    private void OnTriggerEnter(Collider coll)
    {
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

        if (other.tag.Equals("rupee"))
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
        else if (other.tag.Equals("enemy"))
        {

            //enemy trigger logic, if any
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

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag.Equals("enemy"))
        {
            //TakeDamage(1);
            Debug.Log("Took damage!");
         

            //add audio/visual effects of damage here
        }
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

}
