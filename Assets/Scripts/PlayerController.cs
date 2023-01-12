using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    
    public Inventory inventory;
    public Displayer displayer;
    
    public AudioClip rupeeSoundClip;

    Rigidbody rb;

    public float movementSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inventory = GetComponent<Inventory>();
        if (inventory == null)
        {
            Debug.LogWarning("WARNING: PlayerController has no inventory!");
        }

        if (displayer == null)
        {
            Debug.LogWarning("WARNING: PlayerController has no displayer!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentInput = GetInput();
        rb.velocity = currentInput * movementSpeed;
    }

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

    public void TakeDamage(float damageMultiplier)
    {
        if(GameController.godMode)
        {
            return;
        }
        inventory.hearts -= 0.5f * damageMultiplier;
        if (inventory.hearts <= 0)
        {
            GameController.gameInstance.GameOver();
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
                Debug.Log("Collected rupee!");
            }

            Destroy(other);

            AudioSource.PlayClipAtPoint(rupeeSoundClip, Camera.main.transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag.Equals("enemy"))
        {
            TakeDamage(1);
            Debug.Log("Took damage!");
         

            //add audio/visual effects of damage here

        }
    }


}
