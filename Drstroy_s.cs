using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drstroy_s : MonoBehaviour
{
    public float tims;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, tims);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
