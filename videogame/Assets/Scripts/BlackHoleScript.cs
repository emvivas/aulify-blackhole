using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleScript : MonoBehaviour
{
    public int time = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

//make it bigger for 1 second and then make it smaller
/*
    void Big(){
        time++;
        if(time < 10){
            transform.localScale += new Vector3(1f, 1f, 1f);
        }
        else if(time > 10 && time < 20){
            transform.localScale -= new Vector3(2f, 2f, 2f);
        }
    }
*/
    public void ActivateAnimation(){
        GetComponent<Animator>().enabled = true;
    }
}
