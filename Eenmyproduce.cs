using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Eenmyproduce : MonoBehaviour
{
    private float producecd;//怪物生成cd；
    public float maxproducecd;//生成怪物的最大cd

    public float produce;//生成的范围
    private Vector3 producePoint;//生成位置
    private Vector3 initialPoint;//最开始的位置
    private bool isboss;
    public GameObject[] enemy;//生成的敌人
    public GameObject boss;
    /*------------------------------------------------------------------------------------------------------------------*/
    //生成的敌人：

    //——A为寄居蟹
    //——B为豌豆
    //——C为水战士
    //——D为小恶魔
    //——D火焰恶魔
    //——E为蠕虫


    /*------------------------------------------------------------------------------------------------------------------*/
    void Start()
    {
        initialPoint = transform.position;//将生成位置设置为孵化器的位置
        producecd = maxproducecd;
    }
    // Update is called once per frame
    void Update()
    {
        producecd -= Time.deltaTime;
        if (producecd <= 0)//生成的时间cd小于0
        {

                if (EnemyQuantity.instance.enemyQuantity >= EnemyQuantity.instance.TcurrentWavesEnemytotal+ EnemyQuantity.instance.waves*9)//敌人数量大于30
            {
                return;
            }
            Patrol();//调用生成怪物函数
        }
    }
    private Vector3 wayPoint;//当前寻路的点
    public void Patrol()
    {
        //获取range之间取x,z
        float producex = Random.Range(-produce, produce);//丶x的随机数由produce获得
        float producez = Random.Range(-produce, produce);//丶z的随机数由produce获得
        //赋值, 孵化器的位置 x + x 随机值, y, z + 随机z 某一点位
        producePoint = new Vector3(initialPoint.x + producex, initialPoint.y, initialPoint.z + producez);//将新的随机数与自身随机数相加之后，赋值给ranDompoint
        NavMeshHit hit;

        if (NavMesh.SamplePosition(producePoint, out hit, 500, 1))//如果是可移动范围
        {
            if (hit.distance < 0.1f) // 如果距离小于 0.1f，则可以生成敌人
            {
                if (EnemyQuantity.instance.waves == 7)
                {
                    if (isboss == false)
                    {
                        isboss = true;
                        //boss.SetActive(true);
                        //怪物数组 ,初始点位, 点位, 默认不转向
                        Instantiate(enemy[5], producePoint, Quaternion.identity);//生成数组里的某一个怪物
                    }

                }
                if (EnemyQuantity.instance.waves >= 6)//7波次, 单独生成boss
                                                      //如果波次为6以下，则每次生存都按最大波次生成，否则就是到了第六波，则全部生成
                {
                    //生成的 谁, 0 到 波次 (随机数不取最大值),点位, 默认不转向
                    Instantiate(enemy[Random.Range(0, enemy.Length-1)], producePoint, Quaternion.identity);//生成数组里的某一个怪物
                }
                else
                {//通过脚本调用指针指向变量
                    Instantiate(enemy[Random.Range(0, EnemyQuantity.instance.waves)], producePoint, Quaternion.identity);//生成数组里的某一个怪物
                }//波次既让 , 怪物知道自己生成哪些怪
                producecd = maxproducecd;//生成完之后将cd刷新为最大cd
                EnemyQuantity.instance.UpdateEnemyQuantity();//更新敌人数量文本
            }
            else
            {
                print("当前点位无法生成,重新切换点位");
            }
        }

        else
        {
            print("无法生成，无法在 NavMesh 上找到位置");
        }
    }
    private void OnDrawGizmosSelected()//画出生成范围
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, produce);
    }
}
