using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMasterSpawn : MonoBehaviour
{
    public GameObject wms;
    private GameObject coral1;
    private GameObject coral2;
    private GameObject coral3;
    private GameObject coral4;
    bool spawned = false;

    void spawnEm()
    {
        Debug.Log("WM_ spawning");
        coral4 = Instantiate(wms);
        coral1 = Instantiate(wms);
        coral2 = Instantiate(wms);
        coral3 = Instantiate(wms);
    }

    void despawnEm()
    {
        Debug.Log("WM_despawning");
        Destroy(coral4);
        Destroy(coral1);
        Destroy(coral2);
        Destroy(coral3);
    }

    private void Update()
    {
        Vector3 pLoc = PlayerController.playerInstance.transform.position;
        if(pLoc.x >= 64.5f && pLoc.x <= 79 && pLoc.y >= 33 && pLoc.y <= 43)
        {
            if(!(spawned))
            {
                spawnEm();
                spawned = true;
            }
        }
        else
        {
            if (spawned)
            {
                despawnEm();
                spawned = false;
            }
        }
    }

}
