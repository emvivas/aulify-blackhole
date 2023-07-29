using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingEEScript : MonoBehaviour
{
    [SerializeField]
    GameObject vendingDisplay;
    [SerializeField]
    GameObject player;
    private int counter = 0;
    float minDistance = 2;
    float distance;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    GameObject hud;
    Timer timerScript;
    // Start is called before the first frame update
    void Start()
    {
        timerScript = hud.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        if(counter < 3){
            if (distance < minDistance )
            {
                audioSource.Play();
                counter++;
                if(counter == 3){
                    timerScript.AddTime();
                }
            }
        }
    }

}
