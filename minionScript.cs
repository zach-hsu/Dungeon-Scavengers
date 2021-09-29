using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minionScript : MonoBehaviour
{
    public float baseAttackE;
    public float baseHealthE;
    public float baseMultiplierE;
    public float attackE;
    public float maxHealthE;
    public float healthE;
    public GameObject enemyUsed;
    public Animator animator;
    public bool turnBackup; //turnBackup is used as a backup measure to make sure the character isnt attacking multiple times
    public bool enemyAttacking;
    private bool startup;
    public int bossInt;
    
    void Start()
    {
        startup = true;
        GameObject dmgTxt = GameObject.Find("DamageText");
        damageText dmgTxtScr = dmgTxt.GetComponent<damageText>();
        newMinionFunction(dmgTxtScr.level, dmgTxtScr.floor);
        turnBackup = true;
    }

    IEnumerator entrance(GameObject enemyMoved)
    {
        while (transform.position.x >= 1.75)
        {
            enemyMoved.transform.Translate(new Vector2(-1, 0) * 0.25f);
            yield return 0;
        }
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------//
    public void newMinionFunction(float baseLevel, float floorMltp)
    {
        if (baseLevel % 6 == 0 && startup == false) //Boss Battle
        {
            bossInt = Random.Range(11, 19);
            animator.SetInteger("minionSprite", bossInt);
          switch (bossInt)//not using stats from notebook, dividing stat ranges into 8 different groups
            {
                case 11://Golem
                    baseAttackE = 29;
                    baseHealthE = 276;
                    break;

                case 12://Blob
                    baseAttackE = 32;
                    baseHealthE = 250;
                    break;

                case 13://Giant
                    baseAttackE = 35;
                    baseHealthE = 229;
                    break;

                case 14://monkey
                    baseAttackE = 38;
                    baseHealthE = 211;
                    break;

                case 15://Dog
                    baseAttackE = 41;
                    baseHealthE = 195;
                    break;

                case 16://Hound
                    baseAttackE = 44;
                    baseHealthE = 182;
                    break;

                case 17://Dragon
                    baseAttackE = 47;
                    baseHealthE = 170;
                    break;

                case 18://Demon
                    baseAttackE = 50;
                    baseHealthE = 160;
                    break;
            }
            baseMultiplierE = floorMltp * 0.5f;
        }
        else if(baseLevel % 3 == 0 && startup == false) //MiniBoss Battle
        {
            int miniBossInt = Random.Range(5, 11);
            animator.SetInteger("minionSprite", miniBossInt);
            switch (miniBossInt)
            {
                case 5://GreyBeast
                    baseAttackE = 31;
                    baseHealthE = 258;
                    break;

                case 6://YellowFatty
                    baseAttackE = 34;
                    baseHealthE = 235;
                    break;

                case 7://RedFatty
                    baseAttackE = 37;
                    baseHealthE = 296;
                    break;

                case 8://BlueZombies
                    baseAttackE = 43;
                    baseHealthE = 186;
                    break;

                case 9://BlueElemental
                    baseAttackE = 46;
                    baseHealthE = 174;
                    break;

                case 10://GreenBug
                    baseAttackE = 49;
                    baseHealthE = 163;
                    break;
            }
            baseMultiplierE = floorMltp * 0.3f;
        }
        else //Normal Minion
        {
            animator.SetInteger("minionSprite", Random.Range(1, 5));
            baseAttackE = 40;
            baseHealthE = 200;
            if (startup == true)
            {
                baseMultiplierE = 1 * 0.25f;
            }
            else
            {
                baseMultiplierE = floorMltp * 0.15f;
            }
        }
        GameObject player = GameObject.Find("Player");
        charController playerCtrl = player.GetComponent<charController>();
        transform.position = new Vector2(5f, 1.5f);
        StartCoroutine(entrance(enemyUsed));
        enemyAttacking = false;
        attackE = Mathf.Floor(baseAttackE * baseMultiplierE);
        attackE += Mathf.Floor(Random.Range(-attackE / 10, attackE / 10));
        maxHealthE = Mathf.Floor(baseHealthE * baseMultiplierE);
        maxHealthE += Mathf.Floor(Random.Range(-maxHealthE / 10, maxHealthE / 10));
        healthE = maxHealthE;
        startup = false;
    }
  //-----------------------------------------------------------------------------------------------------------------------------------------------------//
    void Update()
    {
        GameObject player = GameObject.Find("Player");
        charController playerCtrl = player.GetComponent<charController>();
        if (playerCtrl.playerTurn == false && turnBackup == true)
        {
            StartCoroutine(enemyAttack(enemyUsed));
            turnBackup = false;
        }
    }
    IEnumerator enemyAttack(GameObject enemyMoved)
    {
        yield return new WaitForSeconds(1);
        while (transform.position.x >= 0)
        {
            enemyMoved.transform.Translate(new Vector2(-1, 0) * 0.25f);
            yield return 0;

        }
        enemyAttacking = true;
        while (transform.position.x <= (1.5f - 0.25f))
        {
            enemyMoved.transform.Translate(new Vector2(1, 0) * 0.25f);
            yield return 0;

        }
        yield return new WaitForSeconds(1);
        GameObject player = GameObject.Find("Player");
        charController playerCtrl = player.GetComponent<charController>();
        playerCtrl.playerTurn = true;
        GameObject button = GameObject.Find("attackButton");
        buttonScript buttonScr = button.GetComponent<buttonScript>();
        buttonScr.attackEnable = false;
        turnBackup = true;
    }
}
