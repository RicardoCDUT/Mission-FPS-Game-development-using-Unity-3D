using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{

    bool ishuiandan=false;

    public GameObject zidan;//�ӵ�Ԥ�Ƽ�

    public GameObject tx;//�ӵ�Ԥ�Ƽ�

    public Transform weizhi;//�ӵ�����λ��

    public GameObject texiao;//���ɵ���Ч

    public Transform txwz;//�ӵ�����λ��

    public int zidangesu = 20;//�ӵ�
    public int morenzidan = 20;//Ĭ���ӵ�
    public int zuidazidangesu = 150;//����ӵ�

    public float cd;//�ӵ�cd
    public float huandancd;//huandan�ӵ�cd

    private AudioSource sy;
    public AudioClip clip;

    public Text zidanText;//�ӵ�

    public Text houbeizidanText;//���ӵ�

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
        zidanText.text = zidangesu + "";//�ӵ��ı�=�ӵ�
        houbeizidanText.text = zuidazidangesu + ""; //���ӵ��ı� = ���ӵ�
    }

    // Update is called once per frame
    void Update()
    {
        cd -= Time.deltaTime;
        huandancd -= Time.deltaTime;
        houbeizidanText.text = zuidazidangesu + ""; //���ӵ��ı� = ���ӵ�
        if (Input.GetMouseButton(0)&&cd<=0&&ishuiandan==false)
        {
            if (zidangesu > 0)
            {
                Instantiate(zidan, weizhi.position, weizhi.rotation);
                //GameObject.Find("ren/Main Camera").GetComponent<Animator>().SetTrigger("GJ");
                cd = 0.1f;
                zidangesu--;
                Instantiate(tx, txwz.position, txwz.rotation);
                zidanText.text = zidangesu + "";//�ӵ��ı�=�ӵ�

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
     
     
        zidanText.text = zidangesu + "";//�ӵ��ı�=�ӵ�
        houbeizidanText.text = zuidazidangesu + ""; //���ӵ��ı� = ���ӵ�
        ishuiandan = false;
    }



}
