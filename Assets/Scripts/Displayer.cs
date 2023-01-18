using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Displayer : MonoBehaviour
{
    public Inventory inventory;
    Text textComponent;
    public Text heartsText;
    public Text leftHand;
    public Text rightHand;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inventory != null && textComponent != null)
        {
            textComponent.text = inventory.GetRupees().ToString();
        }
    }

    public void displayHearts(float val)
    {
        heartsText.text = "Hearts: " + val;
    }

    public void displayLeft(string item)
    {
        leftHand.text = "Left hand: " + item;
    }

    public void displayRight(string item)
    {
        leftHand.text = "Right hand: " + item;
    }
}
