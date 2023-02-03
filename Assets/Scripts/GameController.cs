using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController gameInstance;
    public static bool godMode = false;
    public static bool isTransition = false;
    
    private float xCameraDist = 16f;
    private float yCameraDist = 11f;
    private float xPlayerDist = 3f;
    private float yPlayerDist = 3f;

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

        //this doesnt seem to do anything, but can't hurt
        Screen.SetResolution(1024, 960, false);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameInstance = GetComponent<GameController>();
    }

    void Update()
    {

        if (PlayerController.playerInstance.health.hearts == 0)
        {
            gameOver();
        }   

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            godMode = !godMode;
            toggleGodMode();
        }
    }
  
    public void unlockDoor(GameObject door)
    {
        if(PlayerController.playerInstance.inventory.keyCount > 0)
        {
            door.transform.parent.transform.Find("Unlocked").gameObject.SetActive(true);
            door.SetActive(false);
            PlayerController.playerInstance.inventory.useKey();
            Debug.Log("Unlocked locked door");
        }
        
    }

    //1 = N, 2 = E, 3 = S, 4 = W
    public void transition(int dir)
    {
        Vector3 cameraDest = Camera.main.transform.position;
        Vector3 playerDest = PlayerController.playerInstance.transform.position;

        switch (dir)
        {
            case (1):
                cameraDest.y += yCameraDist;
                playerDest.y += yPlayerDist;
                break;
            case (2):
                cameraDest.x += xCameraDist;
                playerDest.x += xPlayerDist;
                break;
            case (3):
                cameraDest.y -= yCameraDist;
                playerDest.y -= yPlayerDist;
                break;
            case (4):
                cameraDest.x -= xCameraDist;
                playerDest.x -= xPlayerDist;
                break;
        }

        Debug.Log("starting transition " + dir + " " + " " + playerDest);
        StartCoroutine(MoveObjectOverTime(dir, Camera.main.transform, Camera.main.transform.position, cameraDest, 2.5f));
        StartCoroutine(MoveObjectOverTime(dir, PlayerController.playerInstance.transform, PlayerController.playerInstance.transform.position, playerDest, 2.5f));
    }

    public void gameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void toggleGodMode()
    {
        godMode = !godMode;
        PlayerController.playerInstance.inventory.godMode();
    }

    //from https://github.com/ayarger/494_demos/blob/master/WorkshopCoroutines/Assets/Scripts/CoroutineUtilities.cs example
    public static IEnumerator MoveObjectOverTime(int dir, Transform target, Vector3 initial_pos, Vector3 dest_pos, float duration_sec)
    {
        isTransition = true;
        float disappearTime = 0; 
        float appearTime = 0;
        if (dir == 2 || dir == 4)
        {
            disappearTime = 0.05f;
            appearTime = 0.85f;
        }
        else
        {
            disappearTime = 0.03f;
            appearTime = 0.93f;
        }

        float initial_time = Time.time;
        // The "progress" variable will go from 0.0f -> 1.0f over the course of "duration_sec" seconds.
        float progress = (Time.time - initial_time) / duration_sec;

        while (progress < 1.0f)
        {
            // Recalculate the progress variable every frame. Use it to determine
            // new position on line from "initial_pos" to "dest_pos"
            progress = (Time.time - initial_time) / duration_sec;

            //make player disappear in between rooms
            if (target.CompareTag("Player"))
            {
                if (progress > disappearTime)
                {
                    target.localScale = new Vector3(0, 0, 0);
                }

                //bring player back
                if (progress > appearTime)
                {
                    target.localScale = new Vector3(1, 1, 1);
                }
            }
            Vector3 new_position = Vector3.Lerp(initial_pos, dest_pos, progress);
            target.position = new_position;

            // yield until the end of the frame, allowing other code / coroutines to run
            // and allowing time to pass.
            yield return null;
        }

        target.position = dest_pos;


        isTransition = false;
    }

    public IEnumerator MoveBlock(Transform tr, Vector3 dir)
    {
        Vector3 pos = tr.position;
        Vector3 playerPos = transform.position;
        Ray r = new Ray(transform.position, dir);

        if (Physics.Raycast(r))
        {
            tr.tag = "NonWallSolid";
            Vector3 destPos = pos + dir;
            float time = 1f;
            for (float t = 0; t < time; t += Time.deltaTime)
            {
                transform.position = playerPos;
                tr.position = Vector3.Lerp(pos, destPos, t);
                yield return new WaitForFixedUpdate();
            }

        }
    }

    /*private IEnumerator showForSecs(GameObject g, float s)
{
    g.SetActive(true);
    Debug.Log(g.name + " set active");
    yield return new WaitForSeconds(s);
    Debug.Log(g.name + " deactivated");
    g.SetActive(false);
}*/
}

