using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeseRoomDoorLock : MonoBehaviour
{
    public GameObject k1;
    public GameObject k2;
    public GameObject k3;
    public GameObject k4;
    public GameObject k5;
    public Sprite unlocked;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!(k1) && !(k2) && !(k3) && !(k4) && !(k5))
        {
            Debug.Log("tttpttptpptptppt");
            GetComponent<SpriteRenderer>().sprite = unlocked;
            gameObject.tag = "eastDoor";
            Destroy(this);
        }
    }
}
