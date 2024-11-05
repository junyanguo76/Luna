using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    // Start is called before the first frame update
    public  GameObject luna;
    public  GameObject monster;
    public Transform transformPointLuna;
    public Transform transformPointMonster;
    public Animator lunaAnimator;
    private Vector3 monsterInitialPoint;
    private Vector3 lunaInitialPoint;
    public SpriteRenderer monsterSR;
    public SpriteRenderer lunaSR;
    public GameObject skillEffect;
    public GameObject healEffect;
    

    private void Awake()
    {
        monsterInitialPoint = transformPointMonster.localPosition;
        lunaInitialPoint = transformPointLuna.localPosition;
    }
    private void OnEnable()
    {
        lunaSR.DOFade(1, 0.01f);
        monsterSR.DOFade(1, 0.01f);
        transformPointMonster.localPosition = monsterInitialPoint ;
        transformPointLuna.localPosition = lunaInitialPoint ;
        GameManager.Instance.monsterCurrentHP = 5;
        lunaAnimator.SetBool("MoveState", false);
    }
    public void LunaAttack()
    {
        StartCoroutine(PerformAttackLogic());

    }
   

    IEnumerator PerformAttackLogic()
    {
        UIManager.Instance.SetBattleScene(false);
        lunaAnimator.SetBool("MoveState", true);
        lunaAnimator.SetFloat("MoveValue", 0);
        luna.transform.DOLocalMove(monsterInitialPoint + new Vector3(-1.4f, 0, 0), 0.5f).OnComplete
            (
            () =>
            {
                lunaAnimator.SetBool("MoveState", false);
                lunaAnimator.SetFloat("MoveValue", 0);
                lunaAnimator.CrossFade("Attack", 0);
                monsterSR.DOFade(0.3f, 0.2f).OnComplete(() => { JudgeMonsterHP(-2); });
            }
            );

        yield return new WaitForSeconds(0.683f+0.5f);
        lunaAnimator.SetBool("MoveState", true);
        lunaAnimator.SetFloat("MoveValue", 1);
        luna.transform.DOLocalMove(lunaInitialPoint, 0.5f).OnComplete(() =>
        {
            lunaAnimator.SetBool("MoveState", false);
        });
        yield return new WaitForSeconds(0.5f);
        if (GameManager.Instance.monsterCurrentHP > 0)
        {
            StartCoroutine(MonsterAttackLogic());
        }
    }

    IEnumerator MonsterAttackLogic()
    {
        monster.transform.DOLocalMove(lunaInitialPoint + new Vector3(1.4f,0,0), 0.5f).OnComplete(() =>
        {
            lunaAnimator.CrossFade("BeingHIt", 0);
            lunaSR.DOFade(0.3f, 0.2f).OnComplete(() =>
            {
                JudgeLunaHP(-2);
            });
        });
        
        yield return new WaitForSeconds(0.5f);
        monster.transform.DOLocalMove(monsterInitialPoint, 0.5f);
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.battlescene.SetActive(true);

    }

    public void Defend()
    {
        StartCoroutine(PerformDefendLogic());
    }

    IEnumerator PerformDefendLogic()
    {
        lunaAnimator.SetBool("Defend", true);
        GameManager.Instance.battlescene.SetActive(false);
        yield return new WaitForSeconds(1);
        monster.transform.DOLocalMove(lunaInitialPoint + new Vector3(1.4f, 0, 0), 0.5f).OnComplete(() =>
        {
            monster.transform.DOLocalMove(monsterInitialPoint, 0.5f);
            luna.transform.DOLocalMove(lunaInitialPoint + new Vector3(-0.5f, 0, 0), 0.25f).OnComplete(() =>
            {
                luna.transform.DOLocalMove(lunaInitialPoint, 0.25f);
                lunaAnimator.SetBool("Defend", false);
            });
        });
        yield return new WaitForSeconds(1);
        GameManager.Instance.battlescene.SetActive(true) ;
    }
    void JudgeMonsterHP(int value)
    {
        GameManager.Instance.MonsterHPDecrease(value);
        if (GameManager.Instance.monsterCurrentHP <= 0)
        {
            GameManager.Instance.killNum++;
            GameManager.Instance.DestroyEnemy();
            if(GameManager.Instance.killNum == 5) { GameManager.Instance.SetContenIndex(); }
            monsterSR.DOFade(1, 0.6f).OnComplete(() =>
            {
                GameManager.Instance.EnterorExitBattle(false);
            });

        }
        else
        {
            monsterSR.DOFade(1, 0.2f);
        }
    }

    void JudgeLunaHP(int value)
    {
        GameManager.Instance.AddOrDecreaseHealth(value);
        if(GameManager.Instance.lunaCurrentHP == 0)
        {
            lunaAnimator.CrossFade("Die", 0);
            lunaSR.DOFade(1, 0.2f).OnComplete(() => { 
                StartCoroutine(JudgeDieLogic());
                 }) ;
        }
        lunaSR.DOFade(1, 0.2f);
    }

    IEnumerator JudgeDieLogic()
    {
        yield return new WaitForSeconds(1.417f);
        GameManager.Instance.EnterorExitBattle(false);
    }


    public void LunaUseSkill()
    {
        if (!GameManager.Instance.CanUseMP(3))
        {
            Debug.Log("Not enough Mana");
            return;
        }
        StartCoroutine(PerformSkillLogic());
    }

    IEnumerator PerformSkillLogic()
    {
        UIManager.Instance.battleScene.SetActive(false) ;
        lunaAnimator.CrossFade("Skill",0);
        GameManager.Instance.AddOrDecreaseMP(-3) ;

        yield return new WaitForSeconds(0.35f);
        GameObject go= Instantiate(skillEffect,transformPointMonster) ;
        go.transform.localPosition = Vector3.zero;
        SpriteRenderer sprSkill = go.GetComponent<SpriteRenderer>();
        sprSkill.sortingOrder = 12;
        
        yield return new WaitForSeconds(0.6f);
        monsterSR.DOFade(0.3f, 0.2f).OnComplete(() =>
        {
            JudgeMonsterHP(-4);
        });
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(MonsterAttackLogic());
    }

    public void LunaHeal()
    {
        if (!GameManager.Instance.CanUseMP(5))
        {
            return;
        }
        StartCoroutine(PerformHealLogic());
    }

    IEnumerator PerformHealLogic()
    {
        UIManager.Instance .battleScene.SetActive(false) ;
        lunaAnimator.CrossFade("RecoverHP",0);
        GameObject go = Instantiate(healEffect,transformPointLuna);
        go.transform.localPosition= Vector3.zero;
        SpriteRenderer sprHeal= go.GetComponent<SpriteRenderer>();
        sprHeal.sortingOrder = 11;

        yield return new WaitForSeconds(1);
        GameManager.Instance.AddOrDecreaseHealth(10);
        GameManager.Instance.AddOrDecreaseMP(-5);
        yield return new WaitForSeconds(1);
        StartCoroutine (MonsterAttackLogic());
    }

    public void LunaEscape()
    {
        UIManager.Instance.battleScene.SetActive(false);
        lunaAnimator.SetBool("MoveState", true);
        lunaAnimator.SetFloat("MoveValue", 1);
        luna.transform.DOLocalMoveX(-8, 0.5f).OnComplete(() =>
        {
            
            GameManager.Instance.EnterorExitBattle(false);
            
        });

    }
   
}
