using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public static PlayerController player;
    public static bool godMode = false;
    public GameObject gameOverText;
    public Text godModeText;
    public bool gameOver = false;

    

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

        Screen.SetResolution(1024, 960, false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {

        if (gameOver)
        {
            //yield return new WaitForSeconds(3);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }   

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleGodMode();
        }

    }
   

    public void GameOver()
    {
        gameOver = true;
        gameOverText.SetActive(true);

        //stop player movement
        //playerInstance.movementSpeed = 0;

        //set other game over stuff

    }

    void ToggleGodMode()
    {
        godMode = !godMode;
        if(godMode)
        {
            godModeText.text = "God mode enabled.";
            player.inventory.GodMode();
        }
        else
        {
            godModeText.text = "God mode disabled.";
        }

        GameObject g = godModeText.gameObject;
        StartCoroutine(showForSecs(g, 5));
    }

    private IEnumerator showForSecs(GameObject g, float s)
    {
        g.SetActive(true);
        Debug.Log(g.name + " set active");
        yield return new WaitForSeconds(s);
        Debug.Log(g.name + " deactivated");
        g.SetActive(false);
    }

}
