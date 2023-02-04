using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyseDropper : MonoBehaviour
{
    public GameObject k1;
    public GameObject k2;
    public GameObject k3;
    public GameObject keyFab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!(k1) && !(k2) && !(k3))
        {
            Instantiate(keyFab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
