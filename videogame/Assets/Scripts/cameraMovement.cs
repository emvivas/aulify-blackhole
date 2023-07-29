using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("BlackHole").transform;
        
    }

    // Update is called once per frame
    void Update()
    {   
        transform.LookAt(target);
        transform.position = new Vector3(target.position.x + Mathf.Sin(Time.time) * 6, transform.position.y, target.position.z + Mathf.Cos(Time.time) * 6);
        
    }
}
