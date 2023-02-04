using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMasterSpawn : MonoBehaviour
{
    public GameObject wms;
    private GameObject[] coral;
    private void OnTriggerEnter(Collider other)
    {
        coral[0] = Instantiate(wms);
        coral[1] = Instantiate(wms);
        coral[2] = Instantiate(wms);
        coral[3] = Instantiate(wms);
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(coral[0]);
        Destroy(coral[1]);
        Destroy(coral[2]);
        Destroy(coral[3]);
    }
}
