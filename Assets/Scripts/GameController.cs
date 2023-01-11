using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController gameInstance;
    public static PlayerController playerInstance;
    public static bool godMode = false;
    public GameObject gameOverText;
    public Text godModeText;
    public bool gameOver = false;
    
    //singleton pattern 
    private void Awake()
    {
        if (gameInstance == null)
        {
            gameInstance = this;
        }
        else if (gameInstance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //game startup here
        playerInstance = GetComponent<PlayerController>();
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.SetActive(true);

        //stop player movement
        playerInstance.movementSpeed = 0;

        //set other game over stuff

    }

    //restart
    void Update()
    {
        if (gameOver && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            ToggleGodMode();
        }
    }


    void ToggleGodMode()
    {
        godMode = !godMode;
        if(godMode)
        {
            godModeText.text = "God mode enabled.";
            playerInstance.inventory.GodMode();
        }
        else
        {
            godModeText.text = "God mode disabled.";
        }

        godModeText.gameObject.SetActive(true);
    }
}
