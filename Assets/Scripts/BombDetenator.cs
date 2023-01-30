using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDetenator : MonoBehaviour
{
    public float fuseLength = 3.0f;
    public GameObject cloud;
    private void boom()
    {
        GameObject center = Instantiate(cloud,transform.position,Quaternion.identity);
        Instantiate(cloud, center.transform.position + Vector3.left, Quaternion.identity);
        Instantiate(cloud, center.transform.position + Vector3.right, Quaternion.identity);
        Instantiate(cloud, center.transform.position + (Vector3.right*0.5f) + Vector3.up, Quaternion.identity);
        Instantiate(cloud, center.transform.position + (Vector3.left*0.5f) + Vector3.up, Quaternion.identity);
        Instantiate(cloud, center.transform.position + (Vector3.right*0.5f) + Vector3.down, Quaternion.identity);
        Instantiate(cloud, center.transform.position + (Vector3.left*0.5f) + Vector3.down, Quaternion.identity);
        Destroy(gameObject);
    }
    IEnumerator fuse()
    {
        yield return (new WaitForSeconds(fuseLength));
        boom();

    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fuse());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
