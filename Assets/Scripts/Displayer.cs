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
    public Image snowball;
    public Image empty;

    public Image[] leftRevealPanels;
    public Image[] rightRevealPanels;

    private WeaponType altWeaponTrack = WeaponType.Empty;
    private float heartsTrack = 3.0f;

    public static Displayer instance;

    // Start is called before the first frame update
    void initRevealPanels()
    {
        leftRevealPanels[0].enabled = true;
        leftRevealPanels[1].enabled = true;
        leftRevealPanels[2].enabled = true;
        leftRevealPanels[3].enabled = true;
        leftRevealPanels[4].enabled = true;
        rightRevealPanels[0].enabled = true;
        rightRevealPanels[1].enabled = true;
        rightRevealPanels[2].enabled = true;
        rightRevealPanels[3].enabled = true;
        rightRevealPanels[4].enabled = true;

    }

    IEnumerator revealAndNext(int i)
    {
        yield return (new WaitForSeconds(0.15f));

        leftRevealPanels[i].enabled = false;
        rightRevealPanels[i].enabled = false;
        if(i <= 3)
        {
            StartCoroutine(revealAndNext(i+1));
        }
    }
    public void bigReveal()
    {
        initRevealPanels();
        StartCoroutine(revealAndNext(0));
    }

    //singleton pattern 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    /*void Start()
    {
        if(instance)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }
        bigReveal();

    }*/

    // Update is called once per frame
    void Update()
    {
        displayHearts();

        if(PlayerController.playerInstance.inventory != null)
        {
            displayRupees();
            displayKeys();
            displayBombs();
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
                hearts1.enabled = write;
                 break;
             case 1.5f:
                 hearts15.enabled = write;
                 break;
             case 2.0f:
                 hearts2.enabled = write;
                 break;
             case 2.5f:
                 hearts25.enabled = write;
                 break;
             case 3.0f:
                hearts3.enabled = write;
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
            case WeaponType.Snowball:
                snowball.enabled = write;
                break;
        }
    }

    public void displayAltWeapon()
    {
        Debug.Log("Displaying");
        Weapon alt = PlayerController.playerInstance.gameObject.GetComponent<AltWeaponScroller>().currentWeapon;
        if(alt.weaponType == altWeaponTrack)
        {
            return;
        }
        Debug.Log("Weapon = "+ alt);
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
        bombsText.text = PlayerController.playerInstance.inventory.bombCount.ToString();
    }
}
