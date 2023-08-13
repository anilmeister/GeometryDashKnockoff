using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //Player based variables.
    private Rigidbody2D playerRb;

    [SerializeField]
    private float sideScrollingSpeed;

    [SerializeField]
    private float jumpStrength;

    [SerializeField]
    private float flightStrength;

    //Checking the right side and below for obstacles.
    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float groundCheckRadius;

    [SerializeField]
    private LayerMask obstacleMask;

    [SerializeField]
    private Transform sideCheck;

    //Getting the transform of sprite
    public Transform spriteTransform;

    //Gamemode enum, can further be improved by implementing other kind of gamemodes.
    private enum gameMode { jumpMode = 0, flyMode = 1 };

    private gameMode currentGameMode;

    //Restart transform
    [SerializeField]
    private Transform startingPoint;

    //INITIAL CODE START
    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        currentGameMode = gameMode.jumpMode;
    }

    private void Update()
    {
        switch ((int)currentGameMode)//This switch statement chooses whether we fly or not.
        {
            case 0:
                jumpAndRotate();//Original jumping mode.
                break;

            case 1:
                flyMode(); //Flight mode.
                break;
        }

        checkVelocity(); //In every update this function checks whether the player hit an obstacle.
    }

    private void FixedUpdate()
    {
        sideScrolling();//This function moves player to the right side of the screen constantly.
    }

    private bool onGround()//This function checks if the player can jump with the help of
    {                      //Ground position game object.
        return Physics2D.OverlapBox(groundCheck.position, Vector2.right * 1.1f + Vector2.up * groundCheckRadius, 0, obstacleMask);
    }

    private void jumpVertically()//This function makes the player jump.
    {
        playerRb.velocity = Vector2.zero;
        playerRb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
    }

    private void jumpAndRotate()
    {

        if (onGround())
        {
            //Rounding euler rotation for smoothing.

            Vector3 Rotation = spriteTransform.rotation.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
            spriteTransform.rotation = Quaternion.Euler(Rotation);

            if (Input.GetMouseButton(0))
            {
                jumpVertically();
            }

        }

        else
        {   //With this line player cube rotates whilst airborne.
            spriteTransform.Rotate(Vector3.back, 452.4152186f * Time.deltaTime);
        }

    }

    private void sideScrolling()
    {   //Simple sidescrolling here, nothing fancy.
        transform.position += Vector3.right * sideScrollingSpeed * Time.deltaTime;
    }

    public void changeToFlyMode()
    {   //With this we change the currentGameMode variable to flyMode.
        currentGameMode = gameMode.flyMode;
    }

    private void flyMode()
    {   //In this scope gravity changes and sprite matches the flight route of player.
        spriteTransform.rotation = Quaternion.Euler(0, 0, playerRb.velocity.y * 2);
        playerRb.gravityScale = 4.5f;

        if (Input.GetMouseButton(0))
        {   //This scope adds force according to the flightStrength to the player vertically.
            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(Vector2.up * flightStrength, ForceMode2D.Impulse);
        }

    }

    private void checkVelocity() //This function checks velocity and collision to restart the level
    {
        //Checks if the player has stopped and has collided with a wall.
        if (playerRb.velocity.x <= 0 && isTouchingWall())
        {
            restart();//This means game over and restarts the scene.
        }
    }

    private void restart()
    {
        playerRb.gravityScale = 12.41067f;//restoring old settings while restarting
        currentGameMode = gameMode.jumpMode;
        transform.position = startingPoint.position;//Restarting the same scene
    }

    private bool isTouchingWall()
    {   //This works exactly like onGround function but rather having different Vector directions
        //and it is related to SidePosition.
        return Physics2D.OverlapBox(sideCheck.position, Vector2.up * 1.1f + Vector2.right * groundCheckRadius, 0, obstacleMask);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   //This function checks whether player has collided with a spike.
        if (collision.CompareTag("Spike"))
        {
            restart();
        }
    }
}