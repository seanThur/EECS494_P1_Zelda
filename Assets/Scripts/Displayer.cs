using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Displayer : MonoBehaviour
{
    public Inventory inventory;
    public Health health;

    public Text leftHand;
    public Text rupeeText;
    public Text keysText;
    public Text bombText;
    public Image heartsImage;
    public Image three;
    public Image twoAndHalf;
    public Image two;
    public Image oneAndHalf;
    public Image one;
    public Image half;
    
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
    public void displayHearts(float f)
    {
        heartsImage = three;

        if(f == 2.5f)
        {
            heartsImage = twoAndHalf;
        }
        else if (f == 2.0f)
        {
            heartsImage = two;
        }
        else if (f == 1.5f)
        {
            heartsImage = oneAndHalf;
        }
        else if (f == 1.0f)
        {
            heartsImage = one;
        }
        else if (f == 0.5f)
        {
            heartsImage = half;
        }
    }

    public void displayLeft(string item)
    {
        leftHand.text = "Left hand: " + item;
    }

    public void displayRight(string item)
    {
        //rightHand.text = "Right hand: " + item;
    }

    public void displayRupees(int r)
    {
        rupeeText.text = r.ToString();
    }

    public void displayKeys(int k)
    {
        keysText.text = k.ToString();
    }

    public void displayBomb(int b)
    {
        bombText.text = b.ToString();
    }
}
