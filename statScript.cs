//Script tells text gameobjects what values to show for depending on the current monster being accessed

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class statScript : MonoBehaviour
{
    public int statNum, pLevel;
    public Text txt;
    private string txtString;
    public float expNeed;

    void Update()
    {
        //Retrieve Player GameObject
        GameObject player = GameObject.Find("Player");
        charController playerScr = player.GetComponent<charController>();

        //Configure Text to Represent Monster's Level
        if (statNum == 1)
        {
           if (playerScr.player1Level == 4)
            {
                pLevel = 3;
            }else
            {
                pLevel = playerScr.player1Level;
            }
            txt.text = "Level " + pLevel.ToString(); 
        }

        //Configure Text to Represent Monster's Damage
        if (statNum == 2)
        {
            txt.text = "Damage:" + playerScr.player1Attack.ToString();
        }

        //Configure Text to Represent Monster's Health
        if (statNum == 3)
        {
            txt.text = "Health:" + playerScr.player1Health.ToString() + "/" + playerScr.player1MaxHealth.ToString();
        }

        //Configure Text to Represent Player's Experience Points
        if (statNum == 4)
        {
            if (playerScr.player1Level == 4)
            {
                expNeed = 1000;
            }else
                 if (playerScr.player1Level == 2)
            {
                expNeed = 1500;
            }
            else
            {
                expNeed = 1000;
            }

                txt.text = "Exp:" + playerScr.player1Exp.ToString() + "/" + expNeed.ToString();
        }
    }
}
