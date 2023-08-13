using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        try //Try to get component and use function changeToFlyMode to change the currentGameMode.
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            playerMovement.changeToFlyMode();
        }
        catch { }
    }

    
}
