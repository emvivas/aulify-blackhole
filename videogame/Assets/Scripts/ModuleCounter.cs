using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleCounter : MonoBehaviour
{
    private int modulesCompleted = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Counter(){
        modulesCompleted++;
    }

    public int ReturnCounter(){
        return modulesCompleted;
    }
}
