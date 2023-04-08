using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airdrop : MonoBehaviour
{
    public float speed;

    public GameObject[] firearms;//ak47枪械预制件
    //public GameObject sniperRiflePrefab;//弹药预制件
    public Transform spawnArea;//生成位置
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        //持续运动, 速度-3 下坠
        transform.Translate(0, speed * Time.deltaTime, 0);
    }
    private void OnCollisionEnter(Collision collision)
        //碰撞体脚本
    {
        speed = 0;

        spawnArea = gameObject.transform;
        //摇摆到附近, 生成枪械销毁自己
            Vector3 randomPosition = new Vector3(Random.Range(spawnArea.position.x - 0.5f, spawnArea.position.x + 0.5f), spawnArea.position.y, Random.Range(spawnArea.position.z - 0.5f, spawnArea.position.z + 0.5f));
        //生成枪械   
        GameObject games= Instantiate(firearms[Random.Range(0,firearms.Length)], randomPosition, Quaternion.identity);
        print("已生成物体"+ games.name);

        //销毁自己
        Destroy(gameObject,1);
    }
}
