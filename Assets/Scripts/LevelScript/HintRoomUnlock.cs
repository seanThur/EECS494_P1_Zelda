using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintRoomUnlock : MonoBehaviour
{
    public GameObject wDoor;
    public GameObject lWDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            lWDoor.SetActive(false);
            wDoor.SetActive(true);
        }
    }
}
