using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class damageText : MonoBehaviour
{
    public GameObject text, textClone;
    public float level, floor;   
    
    void Start()
    {
        level = 1;
        floor = 1;
    }
    
    //Changes the background and creates a new enemy when a boss is beaten
    IEnumerator bossBeaten (float level, float floor)
    {
        GameObject enemy = GameObject.Find("Minion");
        minionScript enemyScr = enemy.GetComponent<minionScript>();
        GameObject background = GameObject.Find("Background");
        Transform backgroundTrans = background.GetComponent<Transform>();
        yield return new WaitForSeconds(0.5f);
        backgroundTrans.position = new Vector2(Random.Range(2.75f, -2.75f), 2.3f);
        
        enemyScr.newMinionFunction(level, floor);
    }
    
    
    IEnumerator movePlayerShell ()
    {
        GameObject shell = GameObject.Find("PlayerShellAlt");
        playerShell1 altScr = shell.GetComponent<playerShell1>();
        Transform altTrans = shell.GetComponent<Transform>();
        altTrans.position = new Vector2(1.5f, 3.5f);
        while (altTrans.position.y > -5f)
        {
            shell.transform.Translate(new Vector2(-0.1f, -1f) * 0.2f);
            yield return 0;
        }
        altTrans.position = new Vector2(20f, 20f);
        altScr.shellAlt = 0;
    }
  
    void Update()
    {
        GameObject player = GameObject.Find("Player");          
        charController playerScr = player.GetComponent<charController>();
        GameObject enemy = GameObject.Find("Minion");
        minionScript enemyScr = enemy.GetComponent<minionScript>();
        Transform enemyTrans = enemy.GetComponent<Transform>();
        Transform canvas = GameObject.Find("Canvas").transform;
        
        if (playerScr.playerAttacking == true)
        {
            textClone = Instantiate(text, new Vector3(playerScr.enemyX + Random.Range(-0.5f, 0.5f), playerScr.enemyY + Random.Range(-0.5f, 0.5f), 0), Quaternion.identity, canvas);
            textClone.GetComponent<UnityEngine.UI.Text>().text = playerScr.player1Attack.ToString();
            enemyScr.healthE -= playerScr.player1Attack;
            if (enemyScr.healthE <= 0)//if enemy dies
            {
                if (level % 6 == 0)//if enemy was a boss
                {
                   newCharacterFunction(enemyScr.bossInt);
                   floor += 1;
                    playerScr.player1Exp += 400;
                    
                    level += 1;
                    enemyTrans.position = new Vector2(100, 100);
                    StartCoroutine(bossBeaten(level, floor));
                    GameObject shell = GameObject.Find("PlayerShellAlt");
                    playerShell1 altScr = shell.GetComponent<playerShell1>();
                    switch (enemyScr.bossInt)
                    {
                        case 11:
                            altScr.shellAlt = 16;
                            break;
                        case 12:
                            altScr.shellAlt = 1;
                            break;
                        case 13:
                            altScr.shellAlt = 10;
                            break;
                        case 14:
                            altScr.shellAlt = 19;
                            break;
                        case 15:
                            altScr.shellAlt = 13;
                            break;
                        case 16:
                            altScr.shellAlt = 4;
                            break;
                        case 17:
                            altScr.shellAlt = 22;
                            break;
                        case 18:
                            altScr.shellAlt = 7;
                            break;
                    }
                    StartCoroutine(movePlayerShell());
                }
                else
                if (level % 3 == 0)//if enemy is miniboss
                {
                    playerScr.player1Exp += 200;
                    level += 1;
                    enemyScr.newMinionFunction(level, floor);
                }
                else
                {
                    playerScr.player1Exp += 100;
                    level += 1;
                    enemyScr.newMinionFunction(level, floor);
                }
                if (playerScr.player1Level == 1 && playerScr.player1Exp >= 1000)
                {
                    playerScr.player1 += 1;
                    /*textLevelClone = Instantiate(text, new Vector2(-playerScr.enemyX, playerScr.enemyY + 0.5f), Quaternion.identity, canvas);
                    textLevelClone.GetComponent<UnityEngine.UI.Text>().text = "Level Up";*/
                    playerScr.player1Level = 2;
                    playerScr.player1Attack = playerScr.statCalcAttack(playerScr.player1, playerScr.player1Level);
                    playerScr.player1MaxHealth = playerScr.statCalcMaxHealth(playerScr.player1, playerScr.player1Level);
                    playerScr.player1Health = playerScr.player1MaxHealth;
                    playerScr.player1Exp -= 1000;
                    

                }
                else
                if (playerScr.player1Level == 2 && playerScr.player1Exp >= 1500)
                {
                    playerScr.player1 += 1;
                    /*textLevelClone = Instantiate(text, new Vector2(-playerScr.enemyX, playerScr.enemyY + 0.5f), Quaternion.identity, canvas);
                    textLevelClone.GetComponent<UnityEngine.UI.Text>().text = "Level Up";*/
                    playerScr.player1Level = 4;
                    playerScr.player1Attack = playerScr.statCalcAttack(playerScr.player1, playerScr.player1Level);
                    playerScr.player1MaxHealth = playerScr.statCalcMaxHealth(playerScr.player1, playerScr.player1Level);
                    playerScr.player1Health = playerScr.player1MaxHealth;
                    playerScr.player1Exp -= 1500;
                    
                }
                if (playerScr.player1Level == 4 && playerScr.player1Exp >= 1000)
                {
                    /*textLevelClone = Instantiate(text, new Vector2(-playerScr.enemyX, playerScr.enemyY + 0.5f), Quaternion.identity, canvas);
                    textLevelClone.GetComponent<UnityEngine.UI.Text>().text = "Level Up";*/
                    playerScr.player1Health = playerScr.player1MaxHealth;
                    playerScr.player1Exp -= 1000;
                }
            }
            Destroy(textClone, 0.5f);
            playerScr.playerAttacking = false;
        }
        if (enemyScr.enemyAttacking == true)
        {
            textClone = Instantiate(text, new Vector2(-playerScr.enemyX + Random.Range(-0.5f, 0.5f), playerScr.enemyY + Random.Range(-0.5f, 0.5f)), Quaternion.identity, canvas);
            textClone.GetComponent<UnityEngine.UI.Text>().text = enemyScr.attackE.ToString();
            playerScr.player1Health -= enemyScr.attackE;
            if (playerScr.player1Health <= 0 && playerScr.player2Health <= 0 && playerScr.player3Health <= 0 && playerScr.player4Health <= 0)
            {
                SceneManager.LoadScene(2);
            }else 
                if (playerScr.player1Health <= 0)
            {
                playerScr.player1 = playerScr.player2;
                playerScr.player1Attack = playerScr.player2Attack;
                playerScr.player1MaxHealth = playerScr.player2MaxHealth;
                playerScr.player1Health = playerScr.player2Health;
                playerScr.player1Level = playerScr.player2Level;
                playerScr.player1Exp = playerScr.player2Exp;

                playerScr.player2 = 0;
                playerScr.player2Attack = 0;
                playerScr.player2MaxHealth = 0;
                playerScr.player2Health = 0;
                playerScr.player2Level = 0;
                playerScr.player2Exp = 0;

                if (playerScr.player3Health > 0)
                {
                    playerScr.player2 = playerScr.player3;
                    playerScr.player2Attack = playerScr.player3Attack;
                    playerScr.player2MaxHealth = playerScr.player3MaxHealth;
                    playerScr.player2Health = playerScr.player3Health;
                    playerScr.player2Level = playerScr.player3Level;
                    playerScr.player2Exp = playerScr.player3Exp;

                    playerScr.player3 = 0;
                    playerScr.player3Attack = 0;
                    playerScr.player3MaxHealth = 0;
                    playerScr.player3Health = 0;
                    playerScr.player3Level = 0;
                    playerScr.player3Exp = 0;
                }
                if (playerScr.player4Health > 0)
                {
                    playerScr.player3 = playerScr.player4;
                    playerScr.player3Attack = playerScr.player4Attack;
                    playerScr.player3MaxHealth = playerScr.player4MaxHealth;
                    playerScr.player3Health = playerScr.player4Health;
                    playerScr.player3Level = playerScr.player4Level;
                    playerScr.player3Exp = playerScr.player4Exp;

                    playerScr.player4 = 0;
                    playerScr.player4Attack = 0;
                    playerScr.player4MaxHealth = 0;
                    playerScr.player4Health = 0;
                    playerScr.player4Level = 0;
                    playerScr.player4Exp = 0;
                }
            }
            Destroy(textClone, 0.5f);
            enemyScr.enemyAttacking = false;
        }
    }
  
    public void newCharacterFunction(int newCharNumber)
    {
        GameObject player = GameObject.Find("Player");
        charController playerScr = player.GetComponent<charController>();
        if (playerScr.player2 == 0)
        {
            switch (newCharNumber)
            {
                case 11:
                    playerScr.player2 = 16;
                    break;
                case 12:
                    playerScr.player2 = 1;
                    break;
                case 13:
                    playerScr.player2 = 10;
                    break;
                case 14:
                    playerScr.player2 = 19;
                    break;
                case 15:
                    playerScr.player2 = 13;
                    break;
                case 16:
                    playerScr.player2 = 4;
                    break;
                case 17:
                    playerScr.player2 = 22;
                    break;
                case 18:
                    playerScr.player2 = 7;
                    break;
            }
            playerScr.player2Level = 1;
            playerScr.player2Attack = playerScr.statCalcAttack(playerScr.player2, playerScr.player2Level);
            playerScr.player2MaxHealth = playerScr.statCalcMaxHealth(playerScr.player2, playerScr.player2Level);
            playerScr.player2Health = playerScr.player2MaxHealth;
        }
        else if (playerScr.player3 == 0)
        {
            switch (newCharNumber)
            {
                case 11:
                    playerScr.player3 = 16;
                    break;
                case 12:
                    playerScr.player3 = 1;
                    break;
                case 13:
                    playerScr.player3 = 10;
                    break;
                case 14:
                    playerScr.player3 = 19;
                    break;
                case 15:
                    playerScr.player3 = 13;
                    break;
                case 16:
                    playerScr.player3 = 4;
                    break;
                case 17:
                    playerScr.player3 = 22;
                    break;
                case 18:
                    playerScr.player3 = 7;
                    break;
            }
            playerScr.player3Level = 1;
            playerScr.player3Attack = playerScr.statCalcAttack(playerScr.player3, playerScr.player3Level);
            playerScr.player3MaxHealth = playerScr.statCalcMaxHealth(playerScr.player3, playerScr.player3Level);
            playerScr.player3Health = playerScr.player3MaxHealth;
        } else if(playerScr.player4 == 0)
        {
            switch (newCharNumber)
            {
                case 11:
                    playerScr.player4 = 16;
                    break;
                case 12:
                    playerScr.player4 = 1;
                    break;
                case 13:
                    playerScr.player4 = 10;
                    break;
                case 14:
                    playerScr.player4 = 19;
                    break;
                case 15:
                    playerScr.player4 = 13;
                    break;
                case 16:
                    playerScr.player4 = 4;
                    break;
                case 17:
                    playerScr.player4 = 22;
                    break;
                case 18:
                    playerScr.player4 = 7;
                    break;
            }
            playerScr.player4Level = 1;
            playerScr.player4Attack = playerScr.statCalcAttack(playerScr.player4, playerScr.player4Level);
            playerScr.player4MaxHealth = playerScr.statCalcMaxHealth(playerScr.player4, playerScr.player4Level);
            playerScr.player4Health = playerScr.player4MaxHealth;
        }
    }
}
