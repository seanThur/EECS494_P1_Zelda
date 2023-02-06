using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaBallController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 2.0f;
    public void launch(Vector3 movement)
    {
        //Debug.Log("Movement = " + movement);
        rb.velocity = movement * speed;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("wall") || other.gameObject.CompareTag("NonWallSolid") || MoveOnGrid.isDoor(other.tag))
        {
            Destroy(gameObject);
        }
    }
}
