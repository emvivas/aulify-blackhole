using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchSymbols : MonoBehaviour
{
    [SerializeField]
    TMP_InputField answer;
    Switch switchScript;
    [SerializeField]
    GameObject laptop;
    Preguntas preguntasScript;
    int index1;
    private string _answer;
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
           _answer = "FRACCION";
        }
        else if(index1 == 1){
            _answer = "SIGNO";
        }
        else if(index1 == 2){
            _answer = "RESTA";
        }
        else if(index1 == 3){
            _answer = "ROMBO";
        }
        else if(index1 == 4){
            _answer = "SUMA";
        }
        else if(index1 == 5){
            _answer = "FIGURA";
        }
        else if(index1 == 6){
            _answer = "RECTA";
        }
        else if(index1 ==  7){
            _answer = "ARISTA";
        }
        else if(index1 == 8){
            _answer = "BASE";
        }
        else if(index1 == 9){
            _answer = "CILINDRO";
        }
        else if(index1 == 10){
            _answer = "DECIMAL";
        }
         if(index1 == 11){
           _answer = "DIVISOR";
        }
        else if(index1 == 12){
            _answer = "ESFERA";
        }
        else if(index1 == 13){
            _answer = "LONGITUD";
        }
        else if(index1 == 14){
            _answer = "NEGATIVO";
        }
        else if(index1 == 15){
            _answer = "PERIMETRO";
        }
        else if(index1 == 16){
            _answer = "POSITIVO";
        }
        else if(index1 == 17){
            _answer = "PROBLEMA";
        }
        else if(index1 ==  18){
            _answer = "RADIO";
        }
        else if(index1 == 19){
            _answer = "VERTICE";
        }
        else if(index1 == 20){
            _answer = "ANCHO";
        }
        else if(index1 == 21){
            _answer = "CIRCULO";
        }
        else if(index1 == 22){
            _answer = "DECIMO";
        }
        else if(index1 == 23){
            _answer = "DIGITO";
        }
        else if(index1 == 24){
            _answer = "DIVISION";
        }
        else if(index1 == 25){
            _answer = "DOCENA";
        }
        else if(index1 == 26){
            _answer = "HORA";
        }
        else if(index1 == 27){
            _answer = "LARGO";
        }
        else if(index1 == 28){
            _answer = "LINEA";
        }
        else if(index1 == 29){
            _answer = "MAXIMO";
        }
        else if(index1 == 30){
            _answer = "MINIMO";
        }
        else if(index1 == 31){
            _answer = "MINUTO";
        }
        else if(index1 == 32){
            _answer = "NUMERO";
        }
        else if(index1 == 33){
            _answer = "PRISMA";
        }
        else if(index1 == 34){
            _answer = "PUNTO";
        }
        else if(index1 == 35){
            _answer = "SEGUNDO";
        }
        else if(index1 == 36){
            _answer = "SUMANDO";
        }
        else if(index1 == 37){
            _answer = "TOTAL";
        }
        else if(index1 == 38){
            _answer = "UNIDAD";
        }
        else if(index1 == 39){
            _answer = "VALOR";
        }
        Debug.Log(_answer);       
    }

    public void ReadAnswer()
    {
        if(answer.text ==_answer)
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
