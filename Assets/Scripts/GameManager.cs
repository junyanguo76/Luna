using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
   public static GameManager Instance;
   public float lunaHP = 10;
   public float lunaCurrentHP = 10;
    public float lunaMP = 10;
    public float lunaCurrentMP = 10;
    public GameObject battleGo;
    public GameObject battlescene;
    public Animator luna;

    public float monsterCurrentHP = 5;

    public bool canControlLuna;

    public bool hasPetTheDog;
    public int candleNum;
    public int killNum;
    public GameObject MonsterGo;
    public NPCDialog npc;
    public GameObject enemyInstance;

    public AudioSource audioSource;
    public AudioClip normalClip;
    public AudioClip battleClip;
    private void Awake()
    {
        Instance = this;
    }

    //public void HealthCaulator(float num)
    //{
    //    lunaCurrentHP = Mathf.Clamp(lunaCurrentHP + num, 0, lunaHP);
    //    Debug.Log($"You current health is {lunaCurrentHP}");
    //}
    public void Start()
    {
        PlayMusic(normalClip);
        
    }
    public void EnterorExitBattle(bool enter = true)
    {
        
        battleGo.SetActive(enter);
        battlescene.SetActive(enter);
        if(enter == true) { PlayMusic(battleClip); }
        else { PlayMusic(normalClip); }
    }

    public void AddOrDecreaseHealth(float num)
    {
        lunaCurrentHP = Mathf.Clamp(lunaCurrentHP + num, -10, lunaHP);
        Debug.Log(lunaCurrentHP);
        if(lunaCurrentHP <= 0)
        {
            lunaCurrentHP = 0;
            
        }
        UIManager.Instance.SetHpValue(lunaCurrentHP / lunaHP);
    }

    public void AddOrDecreaseMP(float num)
    {
        bool canuseMp = CanUseMP(num);
        if ( canuseMp)
        {
            lunaCurrentMP = Mathf.Clamp(lunaCurrentMP + num,0, lunaMP);
        }
        UIManager.Instance.SetMpValue(lunaCurrentMP / lunaMP);

    }

    public bool CanUseMP(float value)
    {
        return lunaCurrentMP >= value;
    }

    public float MonsterHPDecrease(float num)
    {
        monsterCurrentHP += num;
        return monsterCurrentHP;
    }

    public void ShowMonsters()
    {
        MonsterGo.SetActive(true);
    }

    public void SetContenIndex()
    {
        npc.SetContentIndex();
    }

    public void SetEnemyObject(GameObject obj)
    {
        enemyInstance = obj;
    }

    public void DestroyEnemy()
    {
        Destroy(enemyInstance);
    }

    public void PlayMusic(AudioClip audioClip)
    {
        if(audioSource.clip  != audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    public void PlayShot(AudioClip audioClip)
    {
        if (audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
