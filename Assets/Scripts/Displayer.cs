using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Displayer : MonoBehaviour
{
    //public AltWeaponScroller aws;
    public Text rupeeText;
    public Text keysText;
    public Text bombsText;

    public Image heartsImage;
    public Image hearts3;
    public Image hearts25;
    public Image hearts2;
    public Image hearts15;
    public Image hearts1;
    public Image hearts05;

    public Image altWeaponImage;
    public Image bow;
    public Image boomerang;
    public Image bomb;
    public Image empty;

    private WeaponType altWeaponTrack = WeaponType.Empty;
    private float heartsTrack = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        //heartsImage = GetComponent<Image>();
        //hearts3 = GetComponent<Image>();
        //hearts25 = GetComponent<Image>();
        //hearts2 = GetComponent<Image>();
        //hearts15 = GetComponent<Image>();
        //hearts1 = GetComponent<Image>();
        //hearts05 = GetComponent<Image>();
        //altWeaponImage = GetComponent<Image>();
        //bow = GetComponent<Image>();
        //boomerang = GetComponent<Image>();
        //bomb = GetComponent<Image>();
        //empty = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        displayHearts();

        if(PlayerController.playerInstance.inventory != null)
        {
            displayRupees();
            displayKeys();
            displayBombs();
            displayAltWeapon();
        }
    }
     private void heartImageFlip(float f, bool write)
    {

        switch (f)
         {
             case 0.5f:
                 hearts05.enabled = write;
                 break;
             case 1.0f:
                Debug.Log("GOT 1 " + PlayerController.playerInstance.health.hearts);
                hearts1.enabled = write;
                 break;
             case 1.5f:
                 hearts15.enabled = write;
                 break;
             case 2.0f:
                Debug.Log("GOT 2 " + PlayerController.playerInstance.health.hearts);
                 hearts2.enabled = write;
                 break;
             case 2.5f:
                 hearts25.enabled = write;
                 break;
             case 3.0f:
                //Debug.Log("Name = " + gameObject.name);
                //Debug.Log("Three = "+ hearts3);
                //hearts3.enabled = write;
                 break;
         }
     }

     public void displayHearts()
     {
         float f = Mathf.Round(PlayerController.playerInstance.health.hearts * 2) / 2.0f;
        //Debug.Log("f" + f.ToString());
        if (f != PlayerController.playerInstance.health.hearts || (f % .5 != 0))
        {
            Debug.Log(f.ToString() + " " + PlayerController.playerInstance.health.hearts);
        }
        if (f == heartsTrack)
         {
             return;
         }
         

         heartImageFlip(heartsTrack, false);
         heartsTrack = f;
         heartImageFlip(heartsTrack, true);
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

    public void displayAltWeapon()
    {
        Weapon alt = PlayerController.playerInstance.altWeapon;
        if(alt.weaponType == altWeaponTrack)
        {
            return;
        }
        Debug.Log(alt);
        altWeaponFlip(altWeaponTrack, false);
        altWeaponTrack = alt.weaponType;
        altWeaponFlip(altWeaponTrack, true);
    }

    public void displayRupees()
    {
        rupeeText.text = PlayerController.playerInstance.inventory.rupeeCount.ToString();
    }

    public void displayKeys()
    {
        keysText.text = PlayerController.playerInstance.inventory.keyCount.ToString();
    }

    public void displayBombs()
    {
        //FIX
        //bombsText.text = PlayerController.playerInstance.inventory.bombCount.ToString();
    }
}
