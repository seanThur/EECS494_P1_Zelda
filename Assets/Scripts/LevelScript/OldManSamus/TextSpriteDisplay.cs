using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSpriteDisplay : MonoBehaviour
{
    public int id = -1;
    public static int total;
    public static int next = -1;
    public static bool initialized;
    private bool gone = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!(initialized))
        {
            initialized = true;
            total = 0;
            //StartCoroutine(initSetup());

        }
        total++;
    }

    // Update is called once per frame
    void Update()
    {
        if(next == id && !(gone))
        {
            go();
        }
    }

    public IEnumerator initSetup()
    {
        yield return (new WaitForSeconds(1.0f));

        next++;
    }

    IEnumerator setUpNext()
    {
        yield return (new WaitForSeconds(0.333f));

        next++;
    }
    private void go()
    {
        gone = true;
        GetComponent<SpriteRenderer>().enabled = true;
        StartCoroutine(setUpNext());
    }
}
