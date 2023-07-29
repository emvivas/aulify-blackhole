using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScreen : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textoTimer;
    [SerializeField]
    GameObject hud;
    Timer timer;
    private int _minutes, _seconds, _miliseconds;
    private float timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        timer = hud.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft = timer.ReturnTime();
        _minutes = (int)timeLeft / 60;
        _seconds = (int)timeLeft % 60;
        _miliseconds = (int)(timeLeft * 100) % 100;
        if(hud.activeSelf){
            textoTimer.text = string.Format("{0:00}:{1:00}:{2:00}", _minutes, _seconds, _miliseconds);
        }
        else{
            textoTimer.text = "!$#&/(%=(&%$#&/";
        }
    }
}
