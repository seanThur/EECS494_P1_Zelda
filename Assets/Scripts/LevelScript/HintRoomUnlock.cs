using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintRoomUnlock : MonoBehaviour
{
    //ik this is not good practice but it should get the job done
    public static HintRoomUnlock instance;
    
    public GameObject lWDoor;

    public GameObject gel1;
    public GameObject gel2;
    public GameObject gel3;
    private bool unlocked = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if(!unlocked && !gel1 && !gel2 && !gel3)
        {
            gameObject.tag = "movable";
        }
    }
    public void unlock()
    {
        lWDoor.transform.Find("Unlocked").gameObject.SetActive(true);
        lWDoor.transform.Find("Locked").gameObject.SetActive(false);
        gameObject.tag = "NonWallSolid";

        unlocked = true;
    }
}
