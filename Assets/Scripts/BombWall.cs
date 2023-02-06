using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWall : MonoBehaviour
{
    public void unlock()
    {
        transform.parent.transform.Find("Unlocked").gameObject.SetActive(true);
        gameObject.SetActive(false);
        transform.parent.transform.Find("SouthDoor").gameObject.SetActive(true);
        transform.parent.transform.Find("SouthWall").gameObject.SetActive(false);
    }

}
