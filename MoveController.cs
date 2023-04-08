using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float movementSpeed;
    public float jumpSpeed;
    public float runMultiplier;
    public float gravity = -9.81f;    
    Vector3 velocity;//���ڴ洢��ɫ���ٶ�����
    public Animator anim;//anim��һ��Animator���͵ı��������ڿ��ƽ�ɫ�Ķ���
    public bool isAttack;//isAttack��һ���������͵ı�������ʾ��ɫ�Ƿ����ڽ��й���
    private CharacterController characterController;//characterController��һ��CharacterController���͵ı��������ڿ��ƽ�ɫ���ƶ�����ײ
    public static MoveController instance;
    private void Awake()
    {
        instance = this;//��Awake()�����У���ȡ�˽�ɫ��CharacterController�����������ǰʵ����ֵ����̬����instance���Ա��������ű���ʹ�ø�ʵ����
        characterController = GetComponent<CharacterController>();//��ȡ�������

    }
    public bool isRun;//0,1
    private void Start()
    {
        anim = transform.Find("GunCamera/MainObjectOfTheQun/��ǹ"/*+FirearmsCollection.instacne.lists[0].name*/).GetComponent<Animator>();//��ȡ��������������ak47ǹе�Ķ���
        //isRun = transform.Find("GunCamera/MainObjectOfTheQun/AK47").GetComponent<Shoot>().isRun;
        //��Start()�����У�ͨ������������ķ�ʽ��ȡ��ǹе�Ķ����������丳ֵ��anim������
    }
    void Update()
    {
        anim.SetBool("Run", isRun);//���ܲ�����
        if (characterController.isGrounded && velocity.y < 0)//�����Ϊ���𣬻���y��Ϊ0�����½���Update()�����У�����ͨ��anim.SetBool()������isRun�����󶨵���ɫ�Ķ����ϣ��Ա��ڱ���ʱ���ű��ܶ�����
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");//��ȡx����
        float z = Input.GetAxis("Vertical");//��ȡz����

        //ͨ��Input.GetAxis()������ȡ�˽�ɫ��ˮƽ�ʹ�ֱ�����ϵ�����ֵ��Ȼ���������ֵ�������ɫ���ƶ�������
        //��ʹ��characterController.Move()�������䴫�ݸ���ɫ����������ʵ�ֽ�ɫ���ƶ�
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
        anim = transform.Find("GunCamera/MainObjectOfTheQun/��ǹ").GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        anim.SetBool("Run", rb.velocity.magnitude > 0.1f);//���ܲ�����

        float x = Input.GetAxis("Horizontal");//��ȡx����
        float z = Input.GetAxis("Vertical");//��ȡz����

        Vector3 movement = transform.right * x + transform.forward * z;//movement���������ҵ�x����ǰ���z

        if ((x != 0 || z != 0) && isAttack == false)
        {
            anim.SetBool("Run", true);
            rb.AddForce(movement * movementSpeed * Time.fixedDeltaTime, ForceMode.Impulse);//�����ƶ�
        }
        else
        {
            anim.SetBool("Run", false


        Vector3 movement = transform.right * x + transform.forward * z;//movement���������ҵ�x����ǰ���z




        characterController.Move(movement * movementSpeed * Time.deltaTime);//�����ƶ�
                                                                            //�����ǰ��ɫ���ڱ�����û�н��й�������isRun��������Ϊtrue������������Ϊfalse���Ա��ڲ��ű��ܶ���ʱ��ȷ���жϽ�ɫ��״̬��
        if ((x != 0 || z != 0) && isAttack == false)
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }
        velocity.y += gravity * Time.deltaTime;//��Ծ
        //ͨ�������������ٶȣ����½�ɫ�Ĵ�ֱ�ٶȣ���ģ����ԾЧ�������������Ծ�����ҽ�ɫ���ڵ����ϣ��򽫴�ֱ�ٶ�����Ϊһ�����������Ծ�ٶ�

        characterController.Move(velocity * Time.deltaTime);//������Ծ
        //����������Shift�����򽫽�ɫ���ƶ��ٶȳ���runMultiplier����ʵ�ֱ���Ч��

        if (Input.GetButton("Jump") && characterController.isGrounded)//������Ծ
        {
            velocity.y = Mathf.Sqrt(jumpSpeed * -2f * gravity);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            characterController.Move(movement * Time.deltaTime * runMultiplier);
        }

    }
    public void OnTriggerEnter(Collider other)
    //��OnTriggerEnter() �������ڼ�������Ƿ���뵽���ض��Ĵ���������ͨ������������� Collider ���ƣ�������ǣ�����ݽ���������������ƣ�������ͬ���¼����л�ǹе����
    //
    {
        if (other.name == "AK47"||other.name== "AK47(Clone)")
        //���������������������� "AK47" �� "AK47(Clone)"�����鵱ǰ�Ƿ��Ѿ�ӵ��ǹе
        {
            if (FirearmsCollection.instacne.isfirearms == true)
            //����Ѿ�ӵ����ֱ�ӷ��أ�����ͨ�� FirearmsCollection ���е� AK47() ������ȡ AK47 ǹе�������ٽ�����������塣
            {
                return;
            }
            FirearmsCollection.instacne.AK47();
            Destroy(other.gameObject);
        }
        if (other.name == "�ѻ�ǹ" || other.name == "�ѻ�ǹ(Clone)")
        {
            if (FirearmsCollection.instacne.isfirearms == true)
            {
                return;
            }
            FirearmsCollection.instacne.Snipe();
            Destroy(other.gameObject);
        }
        //���Ȼ�ȡ WSAD ���������룬������������ƶ�����������
        //Ȼ����� CharacterController ����� Move ������ʵ���ƶ���
        //����Ұ��¿ո��ʱ��
        //ͨ������ɫһ�����ϵ��ٶ���ʵ����Ծ��
        //��Ծ�ĸ߶��� jumpSpeed ������
        //�����ɫ��������ʱ��Ҳ��Ҫ����һ�����µ��ٶȣ��Ա��ý�ɫ���䡣
        //����Ұ����� Shift ����ʱ��ʹ�� runMultiplier �������ƶ��ٶȣ�ʵ�ֱ���Ч����
        //��󣬽� isRun ��ֵ���ݸ� Animator ����� Run �������Ա㲥�ű��ܶ���

    }
}
     
        //ͨ��������ʵ��λ��
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
        //anim = transform.Find("GunCamera/MainObjectOfTheQun/��ǹ").GetComponent<Animator>();
//rb = GetComponent<Rigidbody>();
  //  }

   // void FixedUpdate()
    //{
    //    anim.SetBool("Run", rb.velocity.magnitude > 0.1f);//���ܲ�����

      //  float x = Input.GetAxis("Horizontal");//��ȡx����
     //   float z = Input.GetAxis("Vertical");//��ȡz����

      //  Vector3 movement = transform.right * x + transform.forward * z;//movement���������ҵ�x����ǰ���z

        //if ((x != 0 || z != 0) && isAttack == false)
       // {
        //    anim.SetBool("Run", true);
        //    rb.AddForce(movement * movementSpeed * Time.fixedDeltaTime, ForceMode.Impulse);//�����ƶ�
        //}
       // else
       // {
          //  anim.SetBool("Run", false)
