using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyQuantity : MonoBehaviour
{
    /// <summary>
    /// 已生成的敌人数量
    /// </summary>
    public int  enemyQuantity;//已生成的敌人数量
    /// <summary>
    /// 敌人数量文本
    /// </summary>
    public Text enemyQuantityText;


    /// <summary>
    /// 目前敌人数量文本
    /// </summary>
    public int currentEnemyQuantity;
    /// <summary>
    /// 目前敌人数量文本
    /// </summary>
    public Text currentEnemyQuantityText;

    /// <summary>
    /// 当前波次敌人总数
    /// </summary>
    public int TcurrentWavesEnemytotal;


    /// <summary>
    /// 已死亡人数
    /// </summary>
    public  int deadEnemy;


    /// <summary>
    /// 波次
    /// </summary>
    public int waves;
    public Text wavesQuantityText;
    public int maxWaves;//最大波次

    /// <summary>
    /// 空投
    /// </summary>
    public GameObject airdrop;
    public Transform airdropPoint;


    /// <summary>
    /// 休整时间
    /// </summary>
    public int resttime;
    public Text resttimeText;
    public Text resttimeTexttrue;
    public static EnemyQuantity instance;//生成静态指针

    public GameObject finishs;//游戏结束


    private void Awake()
    {
        //第一次打开, 指针指向自己的指针,内部所有代码都能访问
        instance = this;
       //如果有存档
        if (PlayerPrefs.HasKey("waves"))
        {
            //波次被赋值为当前关卡
          waves = PlayerPrefs.GetInt("waves");
        }
        wavesQuantityText.text = waves + "";//更新波次文本
        enemyQuantityText.text = enemyQuantity + "";//更新
        currentEnemyQuantityText.text = currentEnemyQuantity + "";//更新
    }

    public void UpdateEnemyQuantity()
    {
        enemyQuantity++;//敌人数量+1；
        enemyQuantityText.text = enemyQuantity + "";//更新
        currentEnemyQuantity++;
        currentEnemyQuantityText.text = currentEnemyQuantity + "";//更新
    }

    public void EnemyDeath()
    {
        currentEnemyQuantity--;//当前怪物的总数-1
        currentEnemyQuantityText.text = currentEnemyQuantity + "";//更新文本
        deadEnemy++;//已阵亡怪物+1
        if (deadEnemy== TcurrentWavesEnemytotal + EnemyQuantity.instance.waves * 9)//如果已死亡敌人数为最大敌人数，则通进入下一波
        {
            
  
            if (waves < maxWaves)//如果当前波次小于最大波次
            {
                print("调用下一波次" + "当前已阵亡敌方" + deadEnemy + "下一波次" + waves+"最大波次为"+ maxWaves+"当前阵亡怪物");
                Airdrop_award();

            }
            else
            {
                print("游戏结束");
            }
        }
    }


    public void Airdrop_award()//空投奖励
    {
        //airdrop.SetActive(true);
        Instantiate(airdrop, airdropPoint.position,Quaternion.identity);//生成空投
        resttimeTexttrue.gameObject.SetActive(true);//打开休整时间显示
        StartCoroutine("startCountDown");//调用计时
    }

    public IEnumerator startCountDown()//开始倒计时
    {
        while (resttime >= 0)
        {
            resttime--;//总时间 单位 秒，倒计时
            resttimeText.text = "" + resttime;//时间显示UI
            if (resttime == 0)//如果休整时间为0
            {
        
                waves++;//波次++
                deadEnemy = 0;
                resttime = 30;
                wavesQuantityText.text = waves + "";//更新波次文本
                enemyQuantity = 0;//将已生成的敌人数量设置为0
                currentEnemyQuantity = 0; //将目前的敌人数量设置为0
                enemyQuantityText.text = enemyQuantity + "";//更新
                currentEnemyQuantityText.text = currentEnemyQuantity + "";//更新
                resttimeTexttrue.gameObject.SetActive(false);//关闭休整时间显示
                yield break;//停止 协程
            }
            else if (resttime > 0)
            {
                yield return new WaitForSeconds(1);// 每次 自减1，等待 1 秒
            }
        }
    }
}
