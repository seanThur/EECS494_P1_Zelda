using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attached to trigger that locks room when you enter
public class KeeseRoomDoorLock : MonoBehaviour
{
    public GameObject k1;
    public GameObject k2;
    public GameObject k3;
    public GameObject k4;
    public GameObject k5;

    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(door);
    }

    // Update is called once per frame
    void Update()
    {
        if (!(k1) && !(k2) && !(k3) && !(k4) && !(k5))
        {
            unlockDoor();
        }
    }

    public void lockDoor()
    {
        door.transform.Find("Unlocked").gameObject.SetActive(false);
        door.transform.Find("Locked").gameObject.SetActive(true);
    }

    public void unlockDoor()
    {
        door.transform.Find("Unlocked").gameObject.SetActive(true);
        door.transform.Find("Locked").gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
