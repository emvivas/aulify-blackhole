using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchNumbers : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI numero1; //inferior izquierda
    [SerializeField]
    TextMeshProUGUI numero2; // inferior derecha
    [SerializeField]
    TextMeshProUGUI numero3; // superior izquierda
    [SerializeField]
    TextMeshProUGUI numero4; // superior derecha
    [SerializeField]
    TMP_InputField answer;
    private int _numero1;
    private int _numero2;
    private int _numero3;
    private int _numero4;
    Switch switchScript;
    [SerializeField]
    GameObject laptop;
    Preguntas preguntasScript;
    int index1;
    [SerializeField]
    GameObject hud;
    Timer timerScript;
    [SerializeField]
    GameObject wrongPanel;
    // Start is called before the first frame update
    void Start()
    {
        timerScript = hud.GetComponent<Timer>();
        preguntasScript = laptop.GetComponent<Preguntas>();
        switchScript = GameObject.Find("Switch").GetComponent<Switch>();
        _numero1 = Random.Range(10, 90);
        _numero2 = Random.Range(2, 9);
        _numero3 = Random.Range(10, 90);
        _numero4 = Random.Range(2, 9);
        assingnNumberstoTMP();
        index1 = switchScript.RandomSwitch();
        Game();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Game(){
        if(index1 == 0){
            _numero1 = _numero1 - _numero4 + 2;
        }
        else if(index1 == 1){
            _numero1 = _numero3 - _numero2 + 2;
        }
        else if(index1 == 2){
            _numero1 = (_numero1 + _numero4) * 3 ;
        }
        else if(index1 == 3){
            _numero1 = (_numero2 +_numero3) * 3;
        }
        else if(index1 == 4){
            _numero1 = (_numero1 * _numero1) - 4;
        }
        else if(index1 == 5){
            _numero1 = (_numero3 * _numero3) - 4;
        }
        else if(index1 == 6){ 
            _numero1 = _numero1 * _numero4;
        }
        else if(index1 == 7){
            _numero1 = _numero3 * _numero2;
        }
        else if(index1 == 8){
            _numero1 = (_numero1 * _numero4) + 5;   //pentagono azul
        }
        else if(index1 == 9){
            _numero1 = (_numero3 * _numero2) + 5;
        }
        Debug.Log(_numero1);       
    }


    public void assingnNumberstoTMP()
    {
        numero1.text = _numero1.ToString();
        numero2.text = _numero2.ToString();
        numero3.text = _numero3.ToString();
        numero4.text = _numero4.ToString();
    }

    public void ReadAnswer()
    {
        if(answer.text == _numero1.ToString())
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
