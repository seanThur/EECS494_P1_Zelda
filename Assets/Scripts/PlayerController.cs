using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public Inventory inventory;
    public Collector collector;
    Rigidbody rb;
    public float movementSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inventory = GetComponent<Inventory>();
        collector = GetComponent<Collector>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentInput = GetInput();
        rb.velocity = currentInput * movementSpeed;
    }

    Vector2 GetInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        
        if (Mathf.Abs(xInput) > 0.0f) {
            yInput = 0;
        }
        

        return new Vector2(xInput, yInput);
    }
}
