using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{

    bool ishuiandan=false;

    public GameObject zidan;//子弹预制件

    public GameObject tx;//子弹预制件

    public Transform weizhi;//子弹发射位置

    public GameObject texiao;//生成的特效

    public Transform txwz;//子弹发射位置

    public int zidangesu = 20;//子弹
    public int morenzidan = 20;//默认子弹
    public int zuidazidangesu = 150;//最大子弹

    public float cd;//子弹cd
    public float huandancd;//huandan子弹cd

    private AudioSource sy;
    public AudioClip clip;

    public Text zidanText;//子弹

    public Text houbeizidanText;//后背子弹

    private Animator anim;

    public static Attack instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //anim = GameObject.Find("ren/Main Camera").GetComponent<Animator>();
        //anim = GetComponent<Animator>();
        sy = GetComponent<AudioSource>();
        zidanText.text = zidangesu + "";//子弹文本=子弹
        houbeizidanText.text = zuidazidangesu + ""; //后备子弹文本 = 后备子弹
    }

    // Update is called once per frame
    void Update()
    {
        cd -= Time.deltaTime;
        huandancd -= Time.deltaTime;
        houbeizidanText.text = zuidazidangesu + ""; //后备子弹文本 = 后备子弹
        if (Input.GetMouseButton(0)&&cd<=0&&ishuiandan==false)
        {
            if (zidangesu > 0)
            {
                Instantiate(zidan, weizhi.position, weizhi.rotation);
                //GameObject.Find("ren/Main Camera").GetComponent<Animator>().SetTrigger("GJ");
                cd = 0.1f;
                zidangesu--;
                Instantiate(tx, txwz.position, txwz.rotation);
                zidanText.text = zidangesu + "";//子弹文本=子弹

                if (sy.isPlaying == false)
                {
                    sy.Play();
                     

                }
                //else
                //{
                //    sy.Stop();

                //}


            }
            else
            {
                ishuiandan = true;
                if(zuidazidangesu >= 1)
                huan();
            }

        }





    }

    private void huan()
    {
        GameObject.Find("ren/Main Camera").GetComponent<Animator>().SetTrigger("DH");

        Invoke("huanzidan", 2f);
        //huanzidan();
        //huandancd = 2.3f;
    }

    private void huanzidan()
    {
        

         if (zuidazidangesu < 20)
        {
            zidangesu = zuidazidangesu;
            zuidazidangesu -= zuidazidangesu;

        }
        else
        {
            zidangesu = morenzidan;
            zuidazidangesu -= morenzidan;
        }
     
     
        zidanText.text = zidangesu + "";//子弹文本=子弹
        houbeizidanText.text = zuidazidangesu + ""; //后备子弹文本 = 后备子弹
        ishuiandan = false;
    }



}
