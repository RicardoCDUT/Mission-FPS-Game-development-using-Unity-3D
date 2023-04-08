using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirearmsCollection : MonoBehaviour
{
    public List<Transform> lists = new List<Transform>();//存储当前枪械
    public GameObject ak47;
    public GameObject snipe;
    public int index;//当前枪械的索引
    public bool isfirearms;//当前手上是否有枪械
    public GameObject sight;
    public int isak=3;//存储的枪械是否是AK
    public static FirearmsCollection instacne;
    private void Awake()
    {
        instacne = this;
        if (PlayerPrefs.HasKey("isak"))
        {
            int index = PlayerPrefs.GetInt("isak");
            if (index == 1)
            {
                AK47();
            }
            else if (index == 2)
            {
                Snipe();
            }
            print(PlayerPrefs.GetInt("isak"));
        }
        for (int i = 0; i < 1; i++)
        {
            if (transform.GetChild(i) != null)//获取子物体
            {
                lists.Add(transform.GetChild(i));//添加此子物体，也就是枪械
                Debug.Log(transform.GetChild(i));
            }

        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {


            if (lists[index].name == "AK47(Clone)" || lists[index].name == "狙击枪(Clone)")
            {
                isfirearms = false;


                Close();
                lists.Remove(lists[index]);
                index--;
                if (index < 0)
                {
                    index = lists.Count - 1;
                }
                lists[index].gameObject.SetActive(true);
                MoveController.instance.anim= transform.Find(lists[index].name).GetComponent<Animator>();
                transform.Find(lists[index].name).GetComponent<Shoot>().anim = transform.Find(lists[index].name).GetComponent<Animator>();
            }


        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && lists.Count > 1) //当使用滚轮时
        {
            Close();
            index++;
            if (index > lists.Count - 1)
            {
                index = 0;
            }
            lists[index].gameObject.SetActive(true);
            Resetting();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && lists.Count > 1) //当使用滚轮时
        {
            Close();
            index--;
            if (index < 0)
            {
                index = lists.Count - 1;
            }
            lists[index].gameObject.SetActive(true);//打开当前枪械
            Resetting();
            //print("打开" + lists[index].gameObject.name);
        }
        //if (Input.GetAxis("Mouse ScrollWheel") != 0)
        //    print(Input.GetAxis("Mouse ScrollWheel"));
    }

    public void Close()
    {

        for (int i = 0; i < lists.Count; i++)
        {
            lists[i].gameObject.SetActive(false);
        }
    }
    public void Snipe()
    {
        isak = 2;
        PlayerPrefs.SetInt("isak", FirearmsCollection.instacne.isak);
        print("调用狙击" + isak);
        GameObject snipes = Instantiate(snipe, gameObject.transform.position, Quaternion.identity);//生成狙击枪
        isfirearms = true;
        lists.Add(snipes.transform);
        snipes.transform.parent = gameObject.transform;
        snipes.transform.localPosition = Vector3.zero;
        snipes.transform.localRotation = Quaternion.identity;
        snipes.gameObject.SetActive(false);
    }
    public void AK47()
    {

        isak = 1;
        PlayerPrefs.SetInt("isak", FirearmsCollection.instacne.isak);
        print("调用AK" + isak);
        GameObject ak = Instantiate(ak47, gameObject.transform.position, Quaternion.identity);//生成AK
        isfirearms = true;
        lists.Add(ak.transform);
        ak.transform.parent = gameObject.transform;
        ak.transform.localPosition = Vector3.zero;
        ak.transform.localRotation = Quaternion.identity;
        ak.gameObject.SetActive(false);
    }

    public void Resetting()
    {

        PlayerPrefs.SetInt("isak", FirearmsCollection.instacne.isak);
        MoveController.instance.anim = transform.Find(lists[index].name).GetComponent<Animator>();//将此脚本列表的当前枪械名字的动画赋值给人物动画，供其使用移动等等
        transform.Find(lists[index].name).GetComponent<Shoot>().anim = transform.Find(lists[index].name).GetComponent<Animator>();//搜索子物体列表中的当前枪械的名字的物体的soot脚本的动画=此脚本的动画，刷新动画操作，否者动画一直为上次赋值的动画
        transform.Find(lists[index].name).GetComponent<Shoot>().isAttack = false;
        transform.Find(lists[index].name).GetComponent<Shoot>().Reloads = false;
        transform.Find(lists[index].name).GetComponent<Shoot>().isAim = false;
        transform.Find(lists[index].name).GetComponent<Shoot>().isReload = false;
        if (lists[index].name == "AK47(Clone)")
        {
            transform.Find(lists[index].name).GetComponent<Shoot>().cd = 0.5f;
        }
        else
        {
            transform.Find(lists[index].name).GetComponent<Shoot>().cd = 0.8f;
        }
        MoveController.instance.movementSpeed = 4;
    }
}
