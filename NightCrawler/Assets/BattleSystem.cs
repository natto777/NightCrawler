using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private Transform[] enemies;
    [SerializeField] private ColliderScript colliderScript;
    public EnemyAI[] enemytype;
    private bool start;
    public int enemyCount;
    public GameObject battleTrigger;
    public int random;
    public GameObject clearedText;
    public bool iscleared = false;
    public GameObject portal;
    public Transform portaltrans;

    public bool withboss;
    public BossAI boss;
    public Transform bosspos;


    void Awake()
    {
        start = false;
     
    }

    void Start()
    {
        colliderScript.OnPlayerEnterTrigger += ColliderScript_OnPlayerEnterTrigger;
        enemyCount = enemies.Length;
        if (withboss) { enemyCount++; }
    }
    
    void Update()
    {
        if (enemyCount <= 0)
        {
            battleTrigger.GetComponent<ColliderScript>().areaCleared();
            if (!iscleared)
            {
                StartCoroutine(clearedSeconds());
                iscleared = true;
                if (withboss) { portal.SetActive(true); }
            }
           

        }
    }

    private IEnumerator clearedSeconds()
    {
        clearedText.SetActive(true);

        yield return new WaitForSeconds(5);
        clearedText.SetActive(false);
    }

  
    private void ColliderScript_OnPlayerEnterTrigger(object sender,System.EventArgs e)
    {
        if (!start)
        {

            StartBattle();
            iscleared = false;
            colliderScript.OnPlayerEnterTrigger -= ColliderScript_OnPlayerEnterTrigger;
            if (withboss) { portal.SetActive(false); }
        }
 
    }
    public void StartBattle()
    {

        foreach(Transform enemy in enemies)
        {
            random = Random.Range(0, enemytype.Length);
            EnemyAI enemobject = Instantiate(enemytype[random], enemy.position, Quaternion.identity) as EnemyAI;
            enemobject.home = enemy;
            enemobject.isbattlestart = true;
            enemobject.battleSystem = GetComponent<BattleSystem>();
        }
        if (withboss)
        {
            BossAI bossobject = Instantiate(boss,bosspos.position, Quaternion.identity) as BossAI;
            bossobject.isbattlestart = true;
            bossobject.battleSystem = GetComponent<BattleSystem>();
        }
        start = true;
        
    }



}
