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
    public bool moving = false;

    private float xCameraDist = 16f;
    private float yCameraDist = 11f;
    private float xPlayerDist = 2f;
    private float yPlayerDist = 2f;

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
        Vector2 dir = new Vector2(PlayerController.playerInstance.mog.getxInput(), PlayerController.playerInstance.mog.getyInput());
        Ray r = new Ray(PlayerController.playerInstance.transform.position, dir);
        Debug.DrawRay(r.origin, r.direction * .5f, Color.red);

        //DEBUG
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            PlayerController.playerInstance.transform.position = new Vector3(34, 27, 0);

            Camera.main.transform.position += new Vector3(0, 2 * yCameraDist, 0);
        }

        if(PlayerController.playerInstance.health.dead)
        {
            gameOver();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        { 
            toggleGodMode();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneManager.LoadScene("CustomLevel", LoadSceneMode.Single);
        }
    }
  
    public void unlockDoor(GameObject door)
    {
        if(PlayerController.playerInstance.inventory.keyCount > 0)
        {
            door.transform.parent.transform.Find("Unlocked").gameObject.SetActive(true);
            door.SetActive(false);
            PlayerController.playerInstance.inventory.useKey();
            //Debug.Log("Unlocked locked door");
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

       // Debug.Log("starting transition " + dir + " " + " " + playerDest);
        StartCoroutine(MoveObjectOverTime(Camera.main.transform, Camera.main.transform.position, cameraDest, 2.5f));
        StartCoroutine(MoveObjectOverTime(PlayerController.playerInstance.transform, PlayerController.playerInstance.transform.position, playerDest, 2.5f));
    }

    public void gameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void toggleGodMode()
    {
        godMode = !godMode;
        PlayerController.playerInstance.isInvinicible = godMode;

        if(godMode)
        {
            PlayerController.playerInstance.inventory.godMode();
            PlayerController.playerInstance.GetComponent<AltWeaponScroller>().godMode();
        }
        
        
    }

    //from https://github.com/ayarger/494_demos/blob/master/WorkshopCoroutines/Assets/Scripts/CoroutineUtilities.cs example
    public static IEnumerator MoveObjectOverTime(Transform target, Vector3 initial_pos, Vector3 dest_pos, float duration_sec)
    {
        isTransition = true;

        float initial_time = Time.time;
        // The "progress" variable will go from 0.0f -> 1.0f over the course of "duration_sec" seconds.
        float progress = (Time.time - initial_time) / duration_sec;

        while (progress < 1.0f)
        {
            // Recalculate the progress variable every frame. Use it to determine
            // new position on line from "initial_pos" to "dest_pos"
            progress = (Time.time - initial_time) / duration_sec;

            Vector3 new_position = Vector3.Lerp(initial_pos, dest_pos, progress);
            target.position = new_position;

            // yield until the end of the frame, allowing other code / coroutines to run
            // and allowing time to pass.
            yield return null;
        }

        target.position = dest_pos;



        isTransition = false;
    }

    /*public static IEnumerator MoveObjectVelocity(int dir, Transform target, float velocity, float duration_sec)
    {
        isTransition = true;

        float initial_time = Time.time;
        // The "progress" variable will go from 0.0f -> 1.0f over the course of "duration_sec" seconds.
        float progress = (Time.time - initial_time) / duration_sec;

        while (progress < 1.0f)
        {
            // Recalculate the progress variable every frame. Use it to determine
            // new position on line from "initial_pos" to "dest_pos"
            progress = (Time.time - initial_time) / duration_sec;

            Vector3 new_position = Vector3.Lerp(initial_pos, dest_pos, progress);
            target.position = new_position;

            // yield until the end of the frame, allowing other code / coroutines to run
            // and allowing time to pass.
            yield return null;
        }

        target.position = dest_pos;

        isTransition = false;
    }*/

    public IEnumerator StartMoveBlock(Collider block, Vector2 dir)
    {
        
        Ray r = new Ray(PlayerController.playerInstance.transform.position, dir);
        Debug.DrawRay(r.origin, r.direction * .5f, Color.green);
        RaycastHit cast;

        yield return new WaitForSeconds(1);

        
        if(Physics.Raycast(r, out cast, .5f))
        {
            //Debug.Log("Cast hit: " + cast.collider.name);
            //Debug.Log("dir: " + dir);
            if(cast.transform.CompareTag("movable"))
            {
                moving = true;
                PlayerController.acceptInput = false;
                yield return StartCoroutine(MoveBlock(block.transform, dir));
                moving = false;
                PlayerController.acceptInput = true;
            }
        }
        else
        {
            //Debug.Log("No hit dir " + dir);
        }

        yield return null;
    }

    public IEnumerator MoveBlock(Transform tr, Vector3 dir)
    {
        
        yield return StartCoroutine(MoveObjectOverTime(tr, tr.position, tr.position + dir, 1));
    }

}

