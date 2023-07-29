using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchClocks : MonoBehaviour
{
    [SerializeField]
    TMP_InputField answerDeno;
    [SerializeField]
    TMP_InputField answerNum;
    Switch switchScript;
    [SerializeField]
    GameObject laptop;
    Preguntas preguntasScript;
    int index1;
    private int _answerDeno;
    private int _answerNum;
    [SerializeField]
    GameObject hud;
    Timer timerScript;
    [SerializeField]
    GameObject wrongPanel;
    [SerializeField]
    GameObject correctPanel;
    // Start is called before the first frame update
    void Start()
    {
        timerScript = hud.GetComponent<Timer>();
        preguntasScript = laptop.GetComponent<Preguntas>();
        switchScript = GameObject.Find("Switch").GetComponent<Switch>();
        index1 = switchScript.RandomSwitch();
        Game();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Game(){
        
        if(index1 == 0){
           _answerNum = 12;
           _answerDeno = 15;
        }
        else if(index1 == 1){
            _answerNum = 3;
            _answerDeno = 35;
        }
        else if(index1 == 2){
            _answerNum = 5;
            _answerDeno = 10;
        }
        else if(index1 == 3){
            _answerNum = 25;
            _answerDeno = 40;
        }
        else if(index1 == 4){
            _answerNum = 1;
            _answerDeno = 55;
        }
        else if(index1 == 5){
            _answerNum = 4;
            _answerDeno = 25;
        }
        else if(index1 == 6){
            _answerNum = 7;
            _answerDeno = 5;
        }
        else if(index1 == 7){
            _answerNum = 11;
            _answerDeno = 20;
        }
        else if(index1 == 8){
            _answerNum = 2;
            _answerDeno = 30;
        }
        else if(index1 == 9){
            _answerNum = 10;
            _answerDeno = 40;
        }
        else if(index1 == 10){
            _answerNum = 15;
            _answerDeno = 2;
        }
        else if(index1 == 11){
            _answerNum = 1;
            _answerDeno = 35;
        }
        else if(index1 == 12){
            _answerNum = 11;
            _answerDeno = 50;
        }
        else if(index1 == 13){
            _answerNum = 7;
            _answerDeno = 15;
        }
        else if(index1 == 14){
            _answerNum = 8;
            _answerDeno = 30;
        }
        else if(index1 == 15){
            _answerNum = 12;
            _answerDeno = 5;
        }
        else if(index1 == 16){
            _answerNum = 12;
            _answerDeno = 30;
        }
        else if(index1 == 17){
            _answerNum = 9;
            _answerDeno = 25;
        }
        else if(index1 == 18){
            _answerNum = 11;
            _answerDeno = 40;
        }
        else if(index1 == 19){
            _answerNum = 4;
            _answerDeno = 55;
        }
        else if(index1 == 20){
            _answerNum = 6;
            _answerDeno = 10;
        }
        else if(index1 == 21){
            _answerNum = 7;
            _answerDeno = 20;
        }
        else if(index1 == 22){
            _answerNum = 4;
            _answerDeno = 20;
        }
        else if(index1 == 23){
            _answerNum = 12;
            _answerDeno = 5;
        }
        else if(index1 == 24){
            _answerNum = 8;
            _answerDeno = 45;
        }
        else if(index1 == 25){
            _answerNum = 55;
            _answerDeno = 10;
        }
        else if(index1 == 26){
            _answerNum = 10;
            _answerDeno = 15;
        }
        else if(index1 == 27){
            _answerNum = 30;
            _answerDeno = 15;
        }
        else if(index1 == 28){
            _answerNum = 40;
            _answerDeno = 55;
        }
        else if(index1 == 29){
            _answerNum = 30;
            _answerDeno = 20;
        }
        else if(index1 == 30){
            _answerNum = 25;
            _answerDeno = 50;
        }
        else if(index1 == 31){
            _answerNum = 50;
            _answerDeno = 25;
        }
        else if(index1 == 32){
            _answerNum = 35;
            _answerDeno = 30;
        }
        else if(index1 == 33){
            _answerNum = 15;
            _answerDeno = 45;
        }
        else if(index1 == 34){
            _answerNum = 40;
            _answerDeno = 20;
        }
        else if(index1 == 35){
            _answerNum = 55;
            _answerDeno = 2;
        }
        else if(index1 == 36){
            _answerNum = 30;
            _answerDeno = 6;
        }
        else if(index1 == 37){
            _answerNum = 20;
            _answerDeno = 1;
        }
        else if(index1 == 38){
            _answerNum = 10;
            _answerDeno = 9;
        }
        else if(index1 == 39){
            _answerNum = 40;
            _answerDeno = 4;
        }
        else if(index1 == 40){
            _answerNum = 25;
            _answerDeno = 4;
        }
        else if(index1 == 41){
            _answerNum = 50;
            _answerDeno = 7;
        }
        else if(index1 == 42){
            _answerNum = 15;
            _answerDeno = 8;
        }
        else if(index1 == 43){
            _answerNum = 30;
            _answerDeno = 11;
        }
        else if(index1 == 44){
            _answerNum = 55;
            _answerDeno = 9;
        }
        Debug.Log(_answerNum + "/" + _answerDeno);       
    }

    public void ReadAnswer()
    {
        if(answerDeno.text ==_answerDeno.ToString() && answerNum.text == _answerNum.ToString())
        {
            preguntasScript.IsCompleted(true);
        }
        else
        {
            StartCoroutine(wrongPanelActive());
            timerScript.WrongAnswer();
        }
    }

    IEnumerator wrongPanelActive()
    {
        wrongPanel.SetActive(true);
        yield return new WaitForSeconds(1);
        wrongPanel.SetActive(false);
    }
}
