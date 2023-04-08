using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent agent;//寻路组件
    [HideInInspector]
    public Animator anim;//动画组件
    private Vector3 ranDompoint;//寻路终点
    private Vector3 wayPoint;//当前寻路的点
    private Vector3 initialPoint;//初始的点
    public bool isinitialPoint;//以出生点为移动范围还是以自身为移动范围计算
    public float scope;//巡逻范围
    public float pointcd=7;//寻路的cd
    private GameObject attackPlayer;//攻击目标
    public bool distantAttack;//怪物是否可以远攻

    public GameObject Enemybullet;//怪物子弹
    public Transform location;//怪物生成子弹的位置

    public int hurt;//对玩家的伤害
    private float attackspeed;//对玩家进行攻击的cd
    public float attackspeedmax;



    public bool isPlayerPoint;//是否发现了玩家
    private bool isRun;//跑步动画
    private bool isWalk;//移动动画

    private AudioSource audios;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        if (GetComponent<AudioSource>() != null)
        {
            audios = GetComponent<AudioSource>();//获取音效
            if (SetUp.instance.isSoundOn == false)
            {
                audios.volume = 0;
            }
        }

        initialPoint = transform.position;
        Patrol();
    }

    // Update is called once per frame
    void Update()
    {
        Animator_Set();
        pointcd -= Time.deltaTime;
        attackspeed -= Time.deltaTime;
        if (isPlayerPoint == true)
        {
            if (attackPlayer == null)
            {
                attackPlayer = GameObject.Find("player");
            }
            agent.destination = attackPlayer.transform.position;   //怪物的终点，是主角的位置。怪物就会向着人物移动

            if (Vector3.Distance(attackPlayer.transform.position, transform.position) <= agent.stoppingDistance)  //如果怪物和人物的位置小于2，怪物就会攻击它
            {
                isRun = false;//关闭移动动画
                transform.LookAt(attackPlayer.transform);//看向玩家
                if (attackspeed <= 0)
                {
                    attackspeed =Random.Range(attackspeedmax+0.5f, attackspeedmax - 0.5f) ;
                    anim.SetTrigger("Attack"); //调用攻击动画
                }

            }
            else if (distantAttack==true&&Vector3.Distance(attackPlayer.transform.position, transform.position) <= scope)  //如果怪物和人物的位置小于巡逻范围，则是人物到达了怪物的远程射击范围
            {
                isRun = false;
                transform.LookAt(attackPlayer.transform);
                anim.SetTrigger("Remote_attack"); //调用远程攻击动画
            }
            else
            {
                isRun = true;
            }
        }
        else
        {
            Found_D_R_F_W();
            if ((Vector3.Distance(wayPoint, transform.position) <= agent.stoppingDistance || pointcd < 0))//如果目标的点和自身的点位小于等于了停止距离，或者计时结束，则重新调用
            {
                Patrol();
                pointcd = 7;
            }
            else
            {//人物的终点设置为随机点
                agent.destination = ranDompoint;
                isRun = true;
            }

        }
    }
    public void Patrol()
    {
        float scopex = Random.Range(-scope, scope);//丶x的随机数由scopex获得
        float scopez = Random.Range(-scope, scope);//丶z的随机数由scopez获得
        if (isinitialPoint)//判断是已出生点为巡逻地点还是按当前点计算
        {
            //x+随机值 自动寻路

            ranDompoint = new Vector3(initialPoint.x + scopex, transform.position.y, initialPoint.z + scopez);//将新的随机数与自身随机数相加之后，赋值给ranDompoint
        }
        else
        {
            ranDompoint = new Vector3(transform.position.x + scopex, transform.position.y, transform.position.z + scopez);//将新的随机数与自身随机数相加之后，赋值给ranDompoint
        }
        NavMeshHit hit;
        wayPoint = NavMesh.SamplePosition(ranDompoint, out hit, scope, 1) ? hit.position : transform.position;//如果获取到的ranDompoint这个点是在可移动图层也就是1图层，那么就等于传入给射线，也就是现在的这个射线的这个点，反之就=自身的位置，然后实时判断自己的位置=了自己的位置，就会重新调用移动
        pointcd = 7;

    }
    private void OnDrawGizmosSelected()//画出巡逻范围和检测敌人范围的圆
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, scope);
    }
 
    private void Found_D_R_F_W()  //定义一个布尔值
    {
        var coll_Suo_you_wu_ti = Physics.OverlapSphere(transform.position, scope);//敌人中心点的位置的周围的半径查找所有的物体(点开怪物有个蓝色的线)（将范围里面的所有物体，储存在布尔值里面）
        //一组碰撞体
        foreach (var target in coll_Suo_you_wu_ti)
        {
            if (target.CompareTag("Player"))//如果发现了Player
            {
                attackPlayer = target.gameObject;//目标则为target.gameObject
                isPlayerPoint= true;
                return;
            }
        }
        //attackPlayer = null;   //如果目标不是Player，返回一个空值，不进行攻击
        //isPlayerPoint= false;//关闭攻击模式，切换为巡逻模式
    }
    public void Hit()
    {
        PlayerValue.instance.playerHp -= hurt;  //当主角被攻击，血量-面板设置的伤害
        PlayerValue.instance.Gx(); 
    }

    public void Remote_attack()//远程攻击
    {

        Instantiate(Enemybullet, location.transform.position, location.rotation);
    }

    public void SetGoals()
    {

    }

    private void Animator_Set()//设置动画播放还是不播放
    {
        anim.SetBool("Run", isRun);
        anim.SetBool("Walk", isWalk);
    }

}
