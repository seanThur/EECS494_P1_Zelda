using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeTimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timedDie());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator timedDie()
    {
        yield return (new WaitForSeconds(0.25f));
        Destroy(gameObject);
    }
}
