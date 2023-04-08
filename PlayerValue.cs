using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValue : MonoBehaviour
{
    public float playerHp;//玩家血量
    private float playermaxHp=200;//玩家最大血量
    public Slider hpSlider;//玩家血量条
    public static PlayerValue instance;//设置为单例
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;//设置指针
        if (PlayerPrefs.HasKey("hp"))
        {
            playerHp = PlayerPrefs.GetFloat("hp");
        }
    }
    void Start()
    {
        //playermaxHp = playerHp;//游戏开始时给最大生命值复制为当前生命值，因为当前生命值是满的
        Gx();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")//如果碰到了怪物
        {
            playerHp-=5;
            Gx();
            if (playerHp <= 0)//游戏失败
            {
                //EnemyQuantity.instance.image.gameObject.SetActive(true);

            }

        }
        if (collision.gameObject.tag == "EnemyBullet2")//如果碰到了怪物
        {
            playerHp-=10;
            Gx();
            if (playerHp <= 0)//游戏失败
            {
                //EnemyQuantity.instance.image.gameObject.SetActive(true);

            }

        }
    }
    public void Gx()
    {
        hpSlider.value = playerHp / playermaxHp;
    }
}
