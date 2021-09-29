using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charController : MonoBehaviour
{
    public Animator animator;
    public GameObject charUsed;

    public bool playerAttacking,  //indicates if player is currently attacking
                playerSwitching,  //indicates if player is currently switching
                playerTurn;       //indicates if its players turn

    public float enemyX, enemyY;

    //Player 1 Stats
    public int player1, player1Level, player1Exp;
    public float player1Attack, player1MaxHealth, player1Health;

    //Player 2 Stats
    public int player2, player2Level, player2Exp;
    public float player2Attack, player2MaxHealth, player2Health;

    //Player 3 Stats
    public int player3, player3Level, player3Exp;
    public float player3Attack, player3MaxHealth, player3Health;

    //Player 4 Stats
    public int player4, player4Level, player4Exp;
    public float player4Attack, player4MaxHealth, player4Health;

    public int playerAlt, playerAltLevel, playerAltExp;
    public float playerAltAttack, playerAltMaxHealth, playerAltHealth;

    void Start()
    {
        player1Exp = 0;
        player2 = 0;
        player2Attack = 0;
        player2MaxHealth = 0;
        player2Health = 0;
        player2Level = 0;
        player2Exp = 0;
        player3 = 0;
        player3Attack = 0;
        player3MaxHealth = 0;
        player3Health = 0;
        player3Level = 0;
        player3Exp = 0;
        player4 = 0;
        player4Attack = 0;
        player4MaxHealth = 0;
        player4Health = 0;
        player4Level = 0;
        player4Exp = 0;
        playerAlt = 0;
        playerAltAttack = 0;
        playerAltMaxHealth = 0;
        playerAltHealth = 0;
        playerAltLevel = 0;
        playerAltExp = 0;
//----------------------------------------------------//
        enemyX = 1.5f;
        enemyY = 1.5f;
        player1Level = 1;
        playerTurn = true;
        playerAttacking = false;
        playerSwitching = false;
        player1Health = player1MaxHealth;
        newPlayerFunction();
    }
    
    //Monster enters battlefield from the left
    IEnumerator entrance(GameObject playerMoved)
    {
        while (transform.position.x <= -1.75)
        {
            playerMoved.transform.Translate(new Vector2(1, 0) * 0.25f);
            yield return 0;
        }
    }
    
    //Monster leaves battlefield
    IEnumerator exit(GameObject playerMoved)
    {
        while (transform.position.x >= -5)
        {
            playerMoved.transform.Translate(new Vector2(-1, 0) * 0.25f);
            yield return 0;

        }
        StartCoroutine(entrance(playerMoved));
    }
    
    //Creates a new monster for the player
    public void newPlayerFunction()
    {
        int random = Random.Range(1, 9);
        switch (random)
        {
            case 1://blob
                player1 = 1;
                break;

            case 2://hound
                player1 = 4;
                break;

            case 3://demon
                player1 = 7;
                break;

            case 4://giant
                player1 = 10;
                break;

            case 5://dog
                player1 = 13;
                break;

            case 6://golem
                player1 = 16;
                break;

            case 7://monkey
                player1 = 19;
                break;

            case 8://dragon
                player1 = 22;
                break;

            default:
                Debug.Log("PSEUDO_RANDOM NUMBER GENERATION ERROR");
                break;
        }
        player1Attack = statCalcAttack(player1, player1Level);
        player1MaxHealth = statCalcMaxHealth(player1, player1Level);
        player1Health = player1MaxHealth;
        transform.position = new Vector2(-5f, 1.5f);
        StartCoroutine(entrance(charUsed));
    }

    //Sets the Monster's Attack 
    public int statCalcAttack(int playerBase, int playerLevel)
    {
        int attack = 30;
        switch (playerBase)
        {
            
            case 1:
                attack = 32;
                break;

            case 4:
                attack = 44;
                break;

            case 7:
                attack = 50;
                break;

            case 10:
                attack = 35;
                break;

            case 13:
                attack = 41;
                break;

            case 16:
                attack = 29;
                break;

            case 19:
                attack = 38;
                break;

            case 22:
                attack = 47;
                break;
        }
        attack = attack * playerLevel;
        return attack;
    }
    
    //Sets the Monster's Maximum Health
    public int statCalcMaxHealth(int playerBase, int playerLevel)
    {
        int health = 200;
        switch (playerBase)
        {
            case 1:
                health = 250;
                break;

            case 4:
                health = 182;
                break;

            case 7:
                health = 160;
                break;

            case 10:
                health = 229;
                break;

            case 13:
                health = 195;
                break;

            case 16:
                health = 276;
                break;

            case 19:
                health = 211;
                break;

            case 22:
                health = 170;
                break;
        }
        health = health * playerLevel;
        return health;
        
    }

    void Update()
    {
       animator.SetInteger("Base", player1);
        GameObject attackButton = GameObject.Find("attackButton");
        buttonScript buttonScr = attackButton.GetComponent<buttonScript>();
        if (buttonScr.attackEnable == true && playerTurn == true && playerSwitching == false) 
        {
            StartCoroutine(playerAttack(charUsed));
            buttonScr.attackEnable = false;
            playerTurn = false;
        }

        GameObject switchButton = GameObject.Find("switchButton");
        switchButtonScript switchButtonScr = switchButton.GetComponent<switchButtonScript>();
        if (switchButtonScr.switchEnable == true && playerTurn == true && playerSwitching == false)
        {
            StartCoroutine(sliderUp());
            playerSwitching = true;
            switchButtonScr.switchEnable = false;
        }
        GameObject playerShell1 = GameObject.Find("PlayerShell1");
        playerShell1 shellScript1  = playerShell1.GetComponent<playerShell1>();
        if (shellScript1.beenClicked == true && playerSwitching == true)
        {
            StartCoroutine(sliderDown());
        }
        else
        {
            shellScript1.beenClicked = false;
        }

        GameObject playerShell2 = GameObject.Find("PlayerShell2");
        playerShell1 shellScript2 = playerShell2.GetComponent<playerShell1>();
        if (shellScript2.beenClicked == true && playerSwitching == true)
        {
            StartCoroutine(sliderDown());
            StartCoroutine(exit(charUsed));
            playerAlt = player1;
            player1 = player2;
            player2 = playerAlt;

            playerAltAttack = player1Attack;
            player1Attack = player2Attack;
            player2Attack = playerAltAttack;

            playerAltMaxHealth = player1MaxHealth;
            player1MaxHealth = player2MaxHealth;
            player2MaxHealth = playerAltMaxHealth;

            playerAltHealth = player1Health;
            player1Health = player2Health;
            player2Health = playerAltHealth;

            playerAltLevel = player1Level;
            player1Level = player2Level;
            player2Level = playerAltLevel;

            playerAltExp = player1Exp;
            player1Exp = player2Exp;
            player2Exp = playerAltExp;
        }
        else { shellScript2.beenClicked = false; }
        GameObject playerShell3 = GameObject.Find("PlayerShell3");
        playerShell1 shellScript3 = playerShell3.GetComponent<playerShell1>();
        if (shellScript3.beenClicked == true && playerSwitching == true)
        {
            StartCoroutine(sliderDown());
            StartCoroutine(exit(charUsed));
            playerAlt = player1;
            player1 = player3;
            player3 = playerAlt;

            playerAltAttack = player1Attack;
            player1Attack = player3Attack;
            player3Attack = playerAltAttack;

            playerAltMaxHealth = player1MaxHealth;
            player1MaxHealth = player3MaxHealth;
            player3MaxHealth = playerAltMaxHealth;

            playerAltHealth = player1Health;
            player1Health = player3Health;
            player3Health = playerAltHealth;

            playerAltLevel = player1Level;
            player1Level = player3Level;
            player3Level = playerAltLevel;

            playerAltExp = player1Exp;
            player1Exp = player3Exp;
            player3Exp = playerAltExp;
        }
        else { shellScript3.beenClicked = false; }
        GameObject playerShell4 = GameObject.Find("PlayerShell4");
        playerShell1 shellScript4 = playerShell4.GetComponent<playerShell1>();
        if (shellScript4.beenClicked == true && playerSwitching == true)
        {   
            StartCoroutine(sliderDown());
            StartCoroutine(exit(charUsed));
            playerAlt = player1;
            player1 = player4;
            player4 = playerAlt;

            playerAltAttack = player1Attack;
            player1Attack = player4Attack;
            player4Attack = playerAltAttack;

            playerAltMaxHealth = player1MaxHealth;
            player1MaxHealth = player4MaxHealth;
            player4MaxHealth = playerAltMaxHealth;

            playerAltHealth = player1Health;
            player1Health = player4Health;
            player4Health = playerAltHealth;

            playerAltLevel = player1Level;
            player1Level = player4Level;
            player4Level = playerAltLevel;

            playerAltExp = player1Exp;
            player1Exp = player4Exp;
            player4Exp = playerAltExp;
        }
        else { shellScript4.beenClicked = false; }
      

    }
    IEnumerator sliderUp()
    {
        
        GameObject slider = GameObject.Find("Slider");
        Transform sliderTrans = slider.GetComponent<Transform>();
        while(sliderTrans.position.y < -2.5)
        {
            sliderTrans.transform.Translate(new Vector2(0, 1) * 0.25f);
            yield return 0;
        }
        
    }
    IEnumerator sliderDown()
    {
        playerSwitching = false;
        GameObject slider = GameObject.Find("Slider");
        Transform sliderTrans = slider.GetComponent<Transform>();
        while (sliderTrans.position.y > -4.25)
        {
            sliderTrans.transform.Translate(new Vector2(0, -1) * 0.25f);
            yield return 0;
        }
        GameObject playerShell = GameObject.Find("PlayerShell1");
        playerShell1 shellScript = playerShell.GetComponent<playerShell1>();
        shellScript.beenClicked = false;
        yield return new WaitForSeconds(1);
        
    }
    
    //Move the player
    IEnumerator playerAttack(GameObject charMoved)
    {
        while (charMoved.transform.position.x <= -0)
        {
            charMoved.transform.Translate(new Vector2(1, 0) * 0.25f);
            yield return 0;
        }
        playerAttacking = true;
        while (charMoved.transform.position.x >= (-1.5f + 0.25f))
        {
            charMoved.transform.Translate(new Vector2(-1, 0) * 0.25f);
            yield return 0;
        }
    }
}
