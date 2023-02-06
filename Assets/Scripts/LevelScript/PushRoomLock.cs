using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushRoomLock : MonoBehaviour
{
    public GameObject gel1;
    public GameObject gel2;
    public GameObject gel3;


    // Update is called once per frame
    void Update()
    {
        if(!gel1 && !gel2 && !gel3)
        {
            GameController.gameInstance.pushRoomLocked = false;
        }
    }

    public void unlock()
    {
        Debug.Log("PRL unlock");
        GameController.gameInstance.unlockDoor(gameObject);
    }
}
