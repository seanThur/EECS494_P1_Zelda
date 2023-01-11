using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    public bool gameOver = false;
    private float hearts = 3.0f;
    private int rupeeCount = 0;
    private int bombCount = 0;
    private int keyCount = 0;

    public void AddRupees(int num)
    {
        rupeeCount += num;
    }

    public int GetRupees()
    {
        return rupeeCount;
    }

    public void GodMode()
    {
        hearts = 3;
        rupeeCount = 999;
        bombCount = 99;
        keyCount = 99;
    }
    public void TakeDamage()
    {
        hearts -= 0.5f;
        if(hearts <= 0)
        {
            GameController.gameInstance.GameOver();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
