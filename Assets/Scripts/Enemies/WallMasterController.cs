using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMasterController : MonoBehaviour//MoveOnGrid
{
    Vector3 forewardTarget;
    Vector3 mainTarget;
    Vector3 graveTarget;
    int state;
    public float movementSpeed = 1.0f;
/*
    static bool initialized;
    static int wallMasterCount;
    static int wmADR;
    static int goGuy;
*/
    static bool planMade;
    int playerDir = 0;
    public Rigidbody rb;
    public SpriteRenderer sr;
    public BoxCollider bc;

    bool carryingPlayer;

    // Start is called before the first frame update
    private void Start()
    {
        carryingPlayer = false;
        state = 0;
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider>();
        deactivate();
        /*
        if (!(initialized))
        {
            wallMasterCount = 0;
            initialized = true;
            goGuy = -1;
            planMade = false;
        }
        wallMasterCount++;
        wmADR = wallMasterCount;
        */
    }

    private bool withinTolerance(Vector3 a, Vector3 b)
    {
        Vector3 diff = b - a;
        float final = Mathf.Abs(diff.magnitude);
        return (final <= 0.05f);
    }

    private void setTarget(Vector3 t)
    {
        rb.velocity = movementSpeed*((t - transform.position).normalized);
    }

    private void deactivate()
    {
        sr.enabled = false;
        bc.enabled = false;
    }

    private void activate()
    {
        sr.enabled = true;
        bc.enabled = true;
    }

    private void Update()
    {
        if(!(planMade))
        {
            checkPlayerIsNextToWall();
        }
        if(carryingPlayer)
        {
            PlayerController.playerInstance.transform.position = transform.position + (Vector3.down * 0.25f) + (Vector3.left * 0.25f);
        }
        switch(state)
        {
            case 1:
                if(withinTolerance(transform.position, forewardTarget))
                {
                    transform.position = forewardTarget;
                    state = 2;
                    setTarget(mainTarget);
                    return;
                }
                break;
            case 2:
                if (withinTolerance(transform.position, mainTarget))
                {
                    transform.position = mainTarget;
                    state = 3;
                    setTarget(graveTarget);
                    return;
                }
                break;
            case 3:
                if (withinTolerance(transform.position, graveTarget))
                {
                    transform.position = graveTarget;
                    state = 0;
                    deactivate();
                    if(carryingPlayer)
                    {
                        GameController.gameInstance.gameOver();
                        carryingPlayer = false;
                    }
                    return;
                }
                break;
        }
    }

    public void beTheGoGuy()
    {
        if (state != 0)
            return;
        state = 1;
        activate();
        switch(playerDir)
        {
            case 1://wall is above player
                mainTarget = PlayerController.playerInstance.transform.position;
                forewardTarget = mainTarget + (Vector3.right * 3);
                graveTarget = mainTarget + (Vector3.up * 2);
                transform.position = forewardTarget + (Vector3.up * 2);
                break;
            case 2://wall is to the right of the player
                mainTarget = PlayerController.playerInstance.transform.position;
                forewardTarget = mainTarget + (Vector3.down * 3);
                graveTarget = mainTarget + (Vector3.right * 2);
                transform.position = forewardTarget + (Vector3.right * 2);
                break;
            case 3://wall is below player
                mainTarget = PlayerController.playerInstance.transform.position;
                forewardTarget = mainTarget + (Vector3.left * 3);
                graveTarget = mainTarget + (Vector3.down * 2);
                transform.position = forewardTarget + (Vector3.down * 2);
                break;
            case 4://wall is to the left of the player
                mainTarget = PlayerController.playerInstance.transform.position;
                forewardTarget = mainTarget + (Vector3.up * 3);
                graveTarget = mainTarget + (Vector3.left * 2);
                transform.position = forewardTarget + (Vector3.left * 2);
                break;
        }
        setTarget(forewardTarget);
    }

    IEnumerator unmakeOutdatedPlan()
    {
        yield return (new WaitForSeconds(1.0f));

        planMade = false;
    }

    public void makePlanToSendHand()
    {
        planMade = true;
        StartCoroutine(unmakeOutdatedPlan());
        beTheGoGuy();
    }
    public void checkPlayerIsNextToWall()
    {
        if (!(planMade))
        {
            Vector3 playerLoc = PlayerController.playerInstance.transform.position;
            RaycastHit hitData;
            bool wallAbove = Physics.Raycast(playerLoc, new Vector3(0.0f, 1.0f, 0.0f), out hitData, 1.0f, 3);
            if (wallAbove && hitData.collider.CompareTag("wall") == false)
                wallAbove = false;

            bool wallRight = Physics.Raycast(playerLoc, new Vector3(1.0f, 0.0f, 0.0f), out hitData, 1.0f, 3);
            if (wallRight && hitData.collider.CompareTag("wall") == false)
                wallRight = false;

            bool wallBelow = Physics.Raycast(playerLoc, new Vector3(0.0f, -1.0f, 0.0f), out hitData, 1.0f, 3);
            if (wallBelow && hitData.collider.CompareTag("wall") == false)
                wallBelow = false;

            bool wallLeft = Physics.Raycast(playerLoc, new Vector3(-1.0f, 0.0f, 0.0f), out hitData, 1.0f, 3);
            if (wallLeft && hitData.collider.CompareTag("wall") == false)
                wallLeft = false;

            if (wallAbove)
            {
                //Debug.Log("  WALL_ABOVE");
                playerDir = 1;
                makePlanToSendHand();
            }
            if (wallRight)
            {
                //Debug.Log("  WALL_Right");
                playerDir = 2;
                makePlanToSendHand();
            }
            if (wallBelow)
            {
                //Debug.Log("  WALL_BELOW");
                playerDir = 3;
                makePlanToSendHand();
            }
            if (wallLeft)
            {
                //Debug.Log("  WALL_LEFT");
                playerDir = 4;
                makePlanToSendHand();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            other.GetComponent<PlayerController>().enabled = false;
            carryingPlayer = true;
        }
    }

}
