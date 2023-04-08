using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float movementSpeed;
    public float jumpSpeed;
    public float runMultiplier;
    public float gravity = -9.81f;    
    Vector3 velocity;//用于存储角色的速度向量
    public Animator anim;//anim是一个Animator类型的变量，用于控制角色的动画
    public bool isAttack;//isAttack是一个布尔类型的变量，表示角色是否正在进行攻击
    private CharacterController characterController;//characterController是一个CharacterController类型的变量，用于控制角色的移动和碰撞
    public static MoveController instance;
    private void Awake()
    {
        instance = this;//在Awake()函数中，获取了角色的CharacterController组件，并将当前实例赋值给静态变量instance，以便在其他脚本中使用该实例。
        characterController = GetComponent<CharacterController>();//获取刚体组件

    }
    public bool isRun;//0,1
    private void Start()
    {
        anim = transform.Find("GunCamera/MainObjectOfTheQun/手枪"/*+FirearmsCollection.instacne.lists[0].name*/).GetComponent<Animator>();//获取子物体的子物体的ak47枪械的动画
        //isRun = transform.Find("GunCamera/MainObjectOfTheQun/AK47").GetComponent<Shoot>().isRun;
        //在Start()函数中，通过查找子物体的方式获取了枪械的动画，并将其赋值给anim变量。
    }
    void Update()
    {
        anim.SetBool("Run", isRun);//绑定跑步动画
        if (characterController.isGrounded && velocity.y < 0)//如果已为跳起，或者y不为0，则下降在Update()函数中，首先通过anim.SetBool()函数将isRun变量绑定到角色的动画上，以便在奔跑时播放奔跑动画。
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");//获取x轴向
        float z = Input.GetAxis("Vertical");//获取z轴向

        //通过Input.GetAxis()函数获取了角色在水平和垂直方向上的输入值，然后根据输入值计算出角色的移动向量，
        //并使用characterController.Move()函数将其传递给角色控制器，以实现角色的移动
        //if(input.GetKeyDown(KeyCode.Space){
        //gangti.AddForce(Vector3.up * 200);

        //using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float movementSpeed;
    public float jumpSpeed;
    public float runMultiplier;
    public float gravity = -9.81f;    
    public Animator anim;
    public bool isAttack;
    private Rigidbody rb;

    private void Start()
    {
        anim = transform.Find("GunCamera/MainObjectOfTheQun/手枪").GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        anim.SetBool("Run", rb.velocity.magnitude > 0.1f);//绑定跑步动画

        float x = Input.GetAxis("Horizontal");//获取x轴向
        float z = Input.GetAxis("Vertical");//获取z轴向

        Vector3 movement = transform.right * x + transform.forward * z;//movement接收向左右的x和向前后的z

        if ((x != 0 || z != 0) && isAttack == false)
        {
            anim.SetBool("Run", true);
            rb.AddForce(movement * movementSpeed * Time.fixedDeltaTime, ForceMode.Impulse);//传入移动
        }
        else
        {
            anim.SetBool("Run", false


        Vector3 movement = transform.right * x + transform.forward * z;//movement接收向左右的x和向前后的z




        characterController.Move(movement * movementSpeed * Time.deltaTime);//传入移动
                                                                            //如果当前角色正在奔跑且没有进行攻击，则将isRun变量设置为true，否则将其设置为false，以便在播放奔跑动画时正确地判断角色的状态。
        if ((x != 0 || z != 0) && isAttack == false)
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }
        velocity.y += gravity * Time.deltaTime;//跳跃
        //通过计算重力加速度，更新角色的垂直速度，以模拟跳跃效果。如果按下跳跃键并且角色正在地面上，则将垂直速度设置为一个计算出的跳跃速度

        characterController.Move(velocity * Time.deltaTime);//传入跳跃
        //果按下了左Shift键，则将角色的移动速度乘以runMultiplier，以实现奔跑效果

        if (Input.GetButton("Jump") && characterController.isGrounded)//按下跳跃
        {
            velocity.y = Mathf.Sqrt(jumpSpeed * -2f * gravity);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            characterController.Move(movement * Time.deltaTime * runMultiplier);
        }

    }
    public void OnTriggerEnter(Collider other)
    //，OnTriggerEnter() 函数用于检测主角是否进入到了特定的触发器区域（通过检测进入区域的 Collider 名称），如果是，则根据进入区域的物体名称，触发不同的事件（切换枪械）。
    //
    {
        if (other.name == "AK47"||other.name== "AK47(Clone)")
        //如果进入区域的物体名称是 "AK47" 或 "AK47(Clone)"，则检查当前是否已经拥有枪械
        {
            if (FirearmsCollection.instacne.isfirearms == true)
            //如果已经拥有则直接返回，否则通过 FirearmsCollection 类中的 AK47() 方法获取 AK47 枪械，并销毁进入区域的物体。
            {
                return;
            }
            FirearmsCollection.instacne.AK47();
            Destroy(other.gameObject);
        }
        if (other.name == "狙击枪" || other.name == "狙击枪(Clone)")
        {
            if (FirearmsCollection.instacne.isfirearms == true)
            {
                return;
            }
            FirearmsCollection.instacne.Snipe();
            Destroy(other.gameObject);
        }
        //首先获取 WSAD 按键的输入，根据输入计算移动方向向量，
        //然后调用 CharacterController 组件的 Move 方法来实现移动。
        //当玩家按下空格键时，
        //通过给角色一个向上的速度来实现跳跃，
        //跳跃的高度由 jumpSpeed 决定。
        //如果角色正在下落时，也需要设置一个向下的速度，以便让角色下落。
        //当玩家按下左 Shift 按键时，使用 runMultiplier 来调整移动速度，实现奔跑效果。
        //最后，将 isRun 的值传递给 Animator 组件的 Run 参数，以便播放奔跑动画

    }
}
     
        //通过刚体来实现位移
//public class MoveController : MonoBehaviour
//{
  ///  public float movementSpeed;
   // public float jumpSpeed;
    //public float runMultiplier;
    //public float gravity = -9.81f;
    //public Animator anim;
   // public bool isAttack;
    //private Rigidbody rb;

  //  private void Start()
    //{
        //anim = transform.Find("GunCamera/MainObjectOfTheQun/手枪").GetComponent<Animator>();
//rb = GetComponent<Rigidbody>();
  //  }

   // void FixedUpdate()
    //{
    //    anim.SetBool("Run", rb.velocity.magnitude > 0.1f);//绑定跑步动画

      //  float x = Input.GetAxis("Horizontal");//获取x轴向
     //   float z = Input.GetAxis("Vertical");//获取z轴向

      //  Vector3 movement = transform.right * x + transform.forward * z;//movement接收向左右的x和向前后的z

        //if ((x != 0 || z != 0) && isAttack == false)
       // {
        //    anim.SetBool("Run", true);
        //    rb.AddForce(movement * movementSpeed * Time.fixedDeltaTime, ForceMode.Impulse);//传入移动
        //}
       // else
       // {
          //  anim.SetBool("Run", false)
