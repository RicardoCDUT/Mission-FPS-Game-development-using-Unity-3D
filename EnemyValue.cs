using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyValue : MonoBehaviour
{
    public float enemyHp;//怪物血量
    private float enemyMaxHp;//最大怪物血量
    public bool boss;
    private Slider hpslider;
    private void Start()
    {
        //int index = //波次
        //enemyHp *= (int)index * 0.2f;//血量*波次
        hpslider = transform.Find("HpSlider/Slider").GetComponent<Slider>();
        enemyMaxHp = enemyHp;//最大血量=当前血量
        hpslider.value = enemyHp / enemyMaxHp;
      


    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "Bullet"|| collision.gameObject.tag == "Dagger"|| collision.gameObject.tag == "Bullet2")//如果碰撞到的物体是子弹或者刀
        //{
        if (collision.gameObject.tag == "Bullet")//如果是子弹，
        {
            enemyHp -= 10; //怪物血量减10
            Gx();
            GetComponent<Enemy>().isPlayerPoint = true;//打开追逐玩家
        }
        else if (collision.gameObject.tag == "Bullet2")//如果是子弹，
        {
            enemyHp -= 80; //怪物血量减50
            Gx();
            GetComponent<Enemy>().isPlayerPoint = true;//打开追逐玩家

        }
        else if (collision.gameObject.tag == "Dagger")//如果是刀
        {
            enemyHp -= 60;//血量减去20
            Gx();
        }
    }
    public void Gx()
    {
        hpslider.value = enemyHp / enemyMaxHp;//更新血量条
        if (enemyHp <= 0)//如果死亡
        {
            GetComponent<Collider>().enabled = false;//关闭碰撞
            EnemyQuantity.instance.EnemyDeath();//怪物死亡数量++

            if (boss)//如果怪为boss
            {
                EnemyQuantity.instance.finishs.gameObject.SetActive(true);
                //Time.timeScale = 0;
            }
            //int index = SceneManager.GetActiveScene().buildIndex;//获取当前场景的编号，
            GetComponent<Enemy>().anim.SetTrigger("Die");//调用攻击动画
            GetComponent<Enemy>().agent.speed = 0;//关闭怪物移动速度
            Destroy(gameObject, 1.5f);//在1.5秒之后销毁
        }
    }
}
