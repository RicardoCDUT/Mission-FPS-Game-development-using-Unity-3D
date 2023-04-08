using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float li;
    float cd;
    private Rigidbody rb;
    public ParticleSystem tx;
    // Start is called before the first frame update
    void Start()
    {
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
        Instantiate(tx, transform.position, Quaternion.identity);
        //print("碰撞到了" + collision.gameObject.name);
        Destroy(gameObject);
    }
}
