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

    public Image altWeaponImage;
    public Image bow;
    public Image boomerang;
    public Image bomb;
    public Image empty;

    public WeaponType currentAltWeapon = WeaponType.Empty;
    
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

    private void altWeaponFlip(WeaponType w, bool write)
    {
        switch (w)
        {
            case WeaponType.Empty:
                empty.enabled = write;
                break;
            case WeaponType.Bow:
                bow.enabled = write;
                break;
            case WeaponType.Boomerang:
                boomerang.enabled = write;
                break;
            case WeaponType.Bomb:
                bomb.enabled = write;
                break;
        }
    }

    public void displayRight(string s)
    {

    }

    public void displayLeft(string s)
    {

    }

    public void displayAltWeapon(WeaponType w)
    {
        altWeaponFlip(currentAltWeapon, false);
        currentAltWeapon = w;
        altWeaponFlip(w, true);
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
