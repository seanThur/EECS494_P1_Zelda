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
    private float heartsTrack = 3.0f;
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
        heartsImage = three;
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
    private void heartImageFlip(float f, bool write)
    {
        switch(f)
        {
            case 0.5f:
                half.enabled = write;
                break;
            case 1.0f:
                one.enabled = write;
                break;
            case 1.5f:
                oneAndHalf.enabled = write;
                break;
            case 2.0f:
                two.enabled = write;
                break;
            case 2.5f:
                twoAndHalf.enabled = write;
                break;
            case 3.0f:
                Debug.Log("Name = " + gameObject.name);
                Debug.Log("Three = "+three);
                three.enabled = write;
                break;
        }
    }
    public void displayHearts(float f)
    {
        f = Mathf.Round(f * 2) / 2.0f;
        heartImageFlip(heartsTrack,false);
        heartsTrack = f;
        heartImageFlip(f, true);
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
