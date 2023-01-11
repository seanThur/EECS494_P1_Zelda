using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public AudioClip rupeeSoundClip;
    Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
        if(inventory == null)
        {
            Debug.LogWarning("WARNING: GameObject with a collector has no inventory!");
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        GameObject other = coll.gameObject;

        if(other.tag == "rupee")
        {
            if(inventory != null)
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

        if(other.tag == "enemy")
        {
            if(inventory != null)
            {
                inventory.TakeDamage();
                Debug.Log("Took damage!");
            }

            //add audio/visual effects of damage here

        }
    }
}
