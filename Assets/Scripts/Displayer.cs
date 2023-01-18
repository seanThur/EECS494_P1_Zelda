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
    public Text rupeeText;
    public Text keysText;
    public Text bombText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inventory != null)
        {
            displayRupees(inventory.GetRupees());
            displayKeys(inventory.GetKeys());
            displayBomb(inventory.GetBombs());
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
        rightHand.text = "Right hand: " + item;
    }

    public void displayRupees(int r)
    {
        rupeeText.text = "Rupees: " + r;
    }

    public void displayKeys(int k)
    {
        keysText.text = "Keys: " + k;
    }

    public void displayBomb(int b)
    {
        bombText.text = "Bombs: " + b;
    }
}
