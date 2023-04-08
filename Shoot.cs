using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FirearmsType
{
    SniperRifle,//狙击枪
    Rifle,//步枪
    Pistol//手枪
}

public class Shoot : MonoBehaviour
{

    private AudioSource audios;//子弹声音
    public ParticleSystem  particle;//开枪特效
    public ParticleSystem particle2;//开枪特效
    public Animator anim;

    public GameObject bullet;//子弹预制体
    private Transform location;//子弹生成位置


    public int bulletNumber = 20;//当前子弹数
    public Text bulletNumberText;//当前子弹数子弹文本

    public int reserveBullet = 150;//后备子弹
    public Text reserveBulletText;//后背子弹文本

    public int bulletCapacity = 20;//弹夹容量//也就是每次换单填充多少子弹的数量


    public float cd;//子弹当前的已是多少间隙
    public float shootingInterval;//射击的间隙最大值

    public bool isReload = false;//是否在换子弹

    public FirearmsType firearmsType;//什么枪支
    private GameObject dagger;//匕首

 
    public GameObject quasicenter;

    public bool isAim;//是否在瞄准
    public bool isAttack;//是否在攻击
    public bool Reloads;//是否狙击在换弹
    // Start is called before the first frame update
    void Start()
    {
        audios = GetComponent<AudioSource>();//获取音效
        anim = GetComponent<Animator>();//获取动画
        location = transform.Find("Component_Point/Bullet_Point");//获取子弹生成位置
        dagger = transform.Find("Armature/arm_L/lower_arm_L/hand_L").gameObject;//匕首
        //bullet = Resources.Load<GameObject>("Bullet");//获取文件中的子弹预制体
        UpdateText();
        if (SetUp.instance.isSoundOn == false)
        {
            audios.volume = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //timer +=Time.deltaTime; 计时器
        //if (timer>cd && Input.GetMouseButton)
        //{
        //timer=0; 重置
        //创建火焰
        //instantiate(FirePre,FirePoint.position,FirePoint.rotation);
        //创建子弹
        //Instantiate(BulletPre, BulletPoint,position, BulletPoint.rotation);
        //子弹个数减少
        //Bulletcount--;
        anim.SetBool("Attack", isAttack);//是否在攻击
        anim.SetBool("Reloads", Reloads);//是否在换弹
        anim.SetBool("Aim", isAim);//是否在瞄准
        if (Input.GetKeyDown(KeyCode.Q))//刀击
        {
            anim.SetTrigger("Dagger");
        }
        if (Input.GetKeyDown(KeyCode.R) && isReload == false)//换弹
        {
            isReload = true;//否则换弹为true
            if (reserveBullet >= 1)//如果后背子弹大于1
            {
                if (gameObject.name == "狙击枪(Clone)")
                {
                    StartCoroutine("SniperReplacement");//调用换弹
          
                }
                else
                {
                    anim.SetTrigger("Reload");//调用换弹

                }
          
                                          //Invoke("huanzidan", 2f);
            }
        }
        if (gameObject.name == "KA47"||gameObject.name== "AK47(Clone)")
        {
            if (Input.GetMouseButton(0) && isReload == false)//当按下左键
            {
                cd -= Time.deltaTime;//射击间隔减去时间
                if (cd <= 0)//如果达到了间隔
                { 
                    if (bulletNumber > 0)//如果子弹大于0
                    {
                        MoveController.instance.isAttack = true;//当前是否在射击设置为true
                        cd = shootingInterval;//重置cd
                        Instantiate(bullet, location.transform.position, location.rotation);//生成子弹
                                                                                            //isAttack = true;
                        if (isAim)
                        {
                            anim.SetTrigger("AimAttack");//调用攻击动画
                            particle.Play();//播放特效
                        }
                        else
                        {
                            anim.SetTrigger("Attack_s");//调用攻击动画
                            particle2.Play();//播放特效
                        }

                        bulletNumber--;//子弹-1
                        bulletNumberText.text = bulletNumber + "";//更新子弹文本
                        audios.Play();//播放声音

                    }
                    else
                    {
                        isReload = true;//否则换弹为true
                        if (reserveBullet >= 1)//如果后背子弹大于1
                        {
                            anim.SetTrigger("Reload");//调用换弹
                                                      //Invoke("huanzidan", 2f);
                        }

                    }
                }
            }
        }
        else if (gameObject.name == "狙击枪" || gameObject.name == "狙击枪(Clone)")
        {

            if (Input.GetMouseButtonUp(0) && isReload == false&& isAttack==false)//当按下左键
            {
     
                cd -= Time.deltaTime;//射击间隔减去时间
                if (cd <= 0)//如果达到了间隔
                {
                    if (bulletNumber > 0)//如果子弹大于0
                    {
                        FirearmsCollection.instacne.sight.gameObject.SetActive(false);
                        isAim = false;
                        isAttack = true;
                        quasicenter.SetActive(false);
                        MoveController.instance.movementSpeed = 4;
                        Invoke("IsAttack", shootingInterval);
                        MoveController.instance.isAttack = true;//当前是否在射击设置为true
                        cd = shootingInterval;//重置cd
                        Instantiate(bullet, location.transform.position, location.rotation);//生成子弹
                                                                                            //isAttack = true;
           
                        if (isAim)
                        {
                            anim.SetTrigger("AimAttack");//调用攻击动画
                            particle.Play();//播放特效
                        }
                        else
                        {
                            anim.SetTrigger("Attack_s");//调用攻击动画
                            particle2.Play();//播放特效
                        }

                        bulletNumber--;//子弹-1
                        bulletNumberText.text = bulletNumber + "";//更新子弹文本
                        audios.Play();//播放声音

                    }
                    else
                    {
                        isReload = true;//否则换弹为true
                        if (reserveBullet >= 1)//如果后背子弹大于1
                        {
                            StartCoroutine("SniperReplacement");//调用换弹
          
                        }

                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0) && isReload == false)//当按下左键
            {
                cd -= Time.deltaTime;//射击间隔减去时间
                if (cd <= 0)//如果达到了间隔
                {
                    if (bulletNumber > 0)//如果子弹大于0
                    {
                        MoveController.instance.isAttack = true;//当前是否在射击设置为true
                        cd = shootingInterval;//重置cd
                        Instantiate(bullet, location.transform.position, location.rotation);//生成子弹
                                                                                            //isAttack = true;
                        if (isAim)
                        {
                            anim.SetTrigger("AimAttack");//调用攻击动画
                            particle.Play();//播放特效
                        }
                        else
                        {
                            anim.SetTrigger("Attack_s");//调用攻击动画
                            particle2.Play();//播放特效
                        }

                        bulletNumber--;//子弹-1
                        bulletNumberText.text = bulletNumber + "";//更新子弹文本
                        audios.Play();//播放声音

                    }
                    else
                    {
                        isReload = true;//否则换弹为true
                        if (reserveBullet >= 1)//如果后背子弹大于1
                        {
                            anim.SetTrigger("Reload");//调用换弹
                                                      //Invoke("huanzidan", 2f);
                        }

                    }
                }
            }
        }

        if (gameObject.name == "狙击枪(Clone)")
        {
            if (Input.GetMouseButtonUp(0))
            {
                cd = 0f;
                //Invoke("IsAttack", shootingInterval);
                MoveController.instance.isAttack = false;//为方便控制动画而写到移动脚本的一个攻击动画变量
            }
            if (Input.GetMouseButtonDown(1) && isReload == false&& isAttack==false)//当按下左键瞄准
            {
                FirearmsCollection.instacne.sight.gameObject.SetActive(false);
                if (quasicenter.gameObject == false||isAim == false)
                {
                    if (bulletNumber == 0&&reserveBullet >= 1)//如果后背子弹大于1
                    {
                        StartCoroutine("SniperReplacement");//调用换弹
                        isReload = true;//否则换弹为true
                        return;
                    }
                    print("狙击枪开镜");
                    quasicenter.SetActive(true);
                    isAim = true;
                    MoveController.instance.movementSpeed = 2;
                    FirearmsCollection.instacne.sight.gameObject.SetActive(true);
                }
                else
                {
                    isAim = false;
                    FirearmsCollection.instacne.sight.gameObject.SetActive(false);
                    quasicenter.SetActive(false);
                    MoveController.instance.movementSpeed = 4;
                }
            }

        }
        else if (gameObject.name == "手枪")
        {
            if (Input.GetMouseButtonUp(0))
            {
                cd = 0f;
                isAttack = false;
                MoveController.instance.isAttack = false;//为方便控制动画而写到移动脚本的一个攻击动画变量
            }
            if (Input.GetMouseButtonDown(1) && isReload == false && MoveController.instance.isAttack==false)//当按下左键瞄准
            {

                if (bulletNumber >= 3)//如果子弹大于0
                {
                    MoveController.instance.isAttack = true;//当前是否在射击设置为true
                    StartCoroutine("ConsecutiveHair");//调用连发
                  

                }
                else
                {
                    isReload = true;//否则换弹为true
             
                    anim.SetTrigger("Reload");//调用换弹
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                //cd = 0f;
                isAttack = false;
                MoveController.instance.isAttack = false;//为方便控制动画而写到移动脚本的一个攻击动画变量
            }
            if (Input.GetMouseButton(1) && isReload == false)//当按下左键瞄准
            {
                print("开镜");
                isAim = true;

                MoveController.instance.movementSpeed = 2;

            }
            if (Input.GetMouseButtonUp(1))//关闭开镜
            {
                print("关镜" + gameObject.name);
                isAim = false;
                MoveController.instance.movementSpeed = 4;
            }
        }

    }
 
    public IEnumerator ConsecutiveHair()//开始连射
    {
        int indexs=0;
        while (true)
        {
   
            if (indexs >= 3)
            {
                MoveController.instance.isAttack = false;//当前是否在射击设置为true
                indexs = 0;
                yield break;//停止 协程
            }
            indexs++;
           
            Instantiate(bullet, location.transform.position, location.rotation);//生成子弹
                                                                                //isAttack = true;
            anim.SetTrigger("Attack_s");//调用攻击动画
            particle2.Play();//播放特效

            bulletNumber--;//子弹-1
            bulletNumberText.text = bulletNumber + "";//更新子弹文本
            audios.Play();//播放声音
            yield return new WaitForSeconds(0.1f);// 每次 自减1，等待 1 秒
        }
    }

    int Snipers = 0;
    private void SnipersAdd()
    {
        Snipers++;
    }

    public IEnumerator SniperReplacement()//开始换弹
    {
        isAim = false;
        isAttack = false;
        while (true)
        {
            print("换弹次数" + Snipers);

            if (Snipers < 7)
            {
                Reloads = true;
                yield return new WaitForSeconds(0.8f);// 每次 自减1，等待 1 秒
            }
            else
            {
                Snipers = 0;
                Reloads = false;
                Reload();
                yield break;
            }
        }
       
    }

    private void Reload()//换弹，动画里调用
    {


        if (reserveBullet < bulletCapacity)//如果后备子弹数<了弹夹的容量
        {
            //19                  19                   20
            if (bulletNumber + reserveBullet > bulletCapacity)//如果子弹数+后备弹夹的容量大于枪械容量
            {
                bulletNumber += bulletNumber - reserveBullet;//那么子弹数只加弹夹容量减去后备子弹那么多个;
                reserveBullet -= bulletNumber - reserveBullet;//如果当前子弹为19，后备弹夹为19，按下R，则填充20-19这么多，并且后备子弹减去填充的20-19这么多
            }
            else
            {
                bulletNumber = reserveBullet;//直接把所有的子弹都赋值给当前子弹
                reserveBullet -= reserveBullet;//减去所有后备子弹

            }
        }
        else
        {
            reserveBullet -= (bulletCapacity- bulletNumber);//后备弹夹子弹数量-=弹夹容量
            bulletNumber = bulletCapacity;//当前子弹数=弹夹容量
        
        }
        UpdateText();//更新弹夹
        isReload = false;//关闭换弹
    }
    public void UpdateText()
    {
        bulletNumberText.text = bulletNumber + "";
        reserveBulletText.text = reserveBullet + "";

    }
    private void DaggerStart()
    {
        dagger.GetComponent<Collider>().enabled = true;
    }
    private void DaggerEne()
    {
        dagger.GetComponent<Collider>().enabled = false;
        Reloads = false;
        isReload = false;
    }
    public void IsAttack()
    {
        isAttack = false;

    }
}
