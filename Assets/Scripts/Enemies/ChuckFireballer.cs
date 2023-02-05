using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuckFireballer : MonoBehaviour
{
    public GameObject fireBall;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spitIn(4.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spitIn(float x)
    {
        yield return (new WaitForSeconds(x));

        spit();
        StartCoroutine(spitIn(x));
    }

    public void spit()
    {
        Vector3 fHeading = PlayerController.playerInstance.transform.position - transform.position;
        GameObject guy = Instantiate(fireBall);
        guy.transform.position = transform.position;
        guy.GetComponent<AquaBallController>().launch(fHeading.normalized);
    }
}
