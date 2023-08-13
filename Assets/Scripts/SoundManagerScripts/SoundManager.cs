using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private bool isMuted = false;//This variable affects flipFlop function its initial value must be false
    
    [SerializeField]
    private Sprite[] buttonSprites;//Sprites used for toggling mute button

    [SerializeField]
    private Image targetButton;//Target button for sprite change

    public void musicFlipFlop() //This function works like a switch that can be either 1 or 0
    {
        if (!isMuted)//Checks if the button is already muted
        {
            AudioListener.volume = 0;
            isMuted = true;
            
        }
      
        else
        {
            AudioListener.volume = 1;
            isMuted = false;
        }

    }

    public void ChangeSprite()//This function is called on 'On Click' event for mute button.
    {                         //This function changes the sprites based on the array we gave it.
        if (targetButton.sprite == buttonSprites[0])//It is similiar to toggling a switch.
        {
            targetButton.sprite = buttonSprites[1];
            return;
        }

        targetButton.sprite = buttonSprites[0];
    }


}
