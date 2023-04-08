using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Camera.main!=null)
        gameObject.transform.LookAt(Camera.main.transform);   //怪物的血条实时看向摄像机
    }
}
