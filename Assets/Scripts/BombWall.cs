using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWall : MonoBehaviour
{
    Health h;

    // Start is called before the first frame update
    void Start()
    {
        h = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(h.hearts <= 0)
        {
            transform.parent.transform.Find("Unlocked").gameObject.SetActive(true);
            gameObject.SetActive(false);
            transform.parent.transform.Find("SouthDoor").gameObject.SetActive(true);
            transform.parent.transform.Find("LeftEntrance").gameObject.SetActive(false);
            transform.parent.transform.Find("RightEntrance").gameObject.SetActive(false);
        }
    }
}
