using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float li;
    float cd;
    private Rigidbody rb;
    //private AudioSource aunios;
    // Start is called before the first frame update
    void Start()
    {
        //aunios.Play();
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.TransformDirection(Vector3.forward * li);
    }
    private void Update()
    {
        cd += Time.deltaTime;
        if (cd >= 3f)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        print("子弹碰撞到了" + collision.gameObject.name);
        Destroy(gameObject);
       

    }
    //private void OnCollisionExit(Collision collision)
    //{
        
    //}
}
