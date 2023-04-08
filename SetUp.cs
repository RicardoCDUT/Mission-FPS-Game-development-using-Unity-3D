using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetUp : MonoBehaviour
{
    public static SetUp instance;

    public bool isSoundOn = true;  // 音效是否开启

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {



            print("存储进度");
            PlayerPrefs.SetInt("waves", EnemyQuantity.instance.waves);
            PlayerPrefs.SetInt("isak", FirearmsCollection.instacne.isak);
            PlayerPrefs.SetFloat("hp", PlayerValue.instance.playerHp);
            print("当前iska=" +FirearmsCollection.instacne.isak + "存储之后为" + PlayerPrefs.GetInt("isak"));
            if (EnemyQuantity.instance.finishs.gameObject != null && EnemyQuantity.instance.finishs.gameObject.activeSelf == true)
            {
                PlayerPrefs.DeleteAll();
                print("已通关");
            }
            Game0();
        }

    }
    public void Game0()
    {
        SceneManager.LoadScene(0);//跳转到场景1
    }
    public void  isSoundOnfalse()
        //控制声音
    {
        isSoundOn = false;
    }
    public void isSoundOntrue()
    {
        isSoundOn = true;
    }
}
