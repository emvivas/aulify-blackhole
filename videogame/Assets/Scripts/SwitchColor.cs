using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchColor : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI numero1; // milesima
    [SerializeField]
    TextMeshProUGUI numero2; // centena
    [SerializeField]
    TextMeshProUGUI numero3; // decena
    [SerializeField]
    TextMeshProUGUI numero4; // unidad
    [SerializeField]
    TextMeshProUGUI numero5; // decima
    [SerializeField]
    TextMeshProUGUI numero6; // centesima
    [SerializeField]
    TMP_InputField answer;
    public float minX = -190f;
    public float maxX = 190f;
    public float minY = -30f;
    public float maxY = 90f;
    public float spacing = 50f;   
    private List<Vector2> positions = new List<Vector2>();
    private int _numero1, _numero2, _numero3, _numero4, _numero5, _numero6;
    [SerializeField]
    GameObject laptop;
    Preguntas preguntasScript;
    [SerializeField]
    GameObject hud;
    Timer timerScript;
    [SerializeField]
    GameObject wrongPanel;
    private float numAnswer_;
    // Start is called before the first frame update
    void Start()
    {
        _numero1 = Random.Range(1, 9);
        _numero2 = Random.Range(1, 9);
        _numero3 = Random.Range(1, 9);
        _numero4 = Random.Range(1, 9);
        _numero5 = Random.Range(1, 9);
        _numero6 = Random.Range(1, 9);
        
        assingnNumberstoTMP();
        Vector2 position = GenerateRandomPosition();
        numero1.rectTransform.anchoredPosition = position;
        positions.Add(position);

        position = GenerateRandomPosition();
        numero2.rectTransform.anchoredPosition = position;
        positions.Add(position);

        position = GenerateRandomPosition();
        numero3.rectTransform.anchoredPosition = position;
        positions.Add(position);

        position = GenerateRandomPosition();
        numero4.rectTransform.anchoredPosition = position;
        positions.Add(position);

        position = GenerateRandomPosition();
        numero5.rectTransform.anchoredPosition = position;
        positions.Add(position);

        position = GenerateRandomPosition();
        numero6.rectTransform.anchoredPosition = position;
        positions.Add(position);

        timerScript = hud.GetComponent<Timer>();
        preguntasScript = laptop.GetComponent<Preguntas>();
     
        numAnswer_ = BuildNumber();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public float BuildNumber(){
        float numAnswer;
        numAnswer = _numero2 * 100 + _numero3 * 10 + _numero4 + _numero5 * 0.1f + _numero6 * 0.01f + _numero1 * 0.001f;
        Debug.Log(numAnswer);
        return numAnswer;
    }


    public void assingnNumberstoTMP()
    {
        numero1.text = _numero1.ToString();
        numero2.text = _numero2.ToString();
        numero3.text = _numero3.ToString();
        numero4.text = _numero4.ToString();
        numero5.text = _numero5.ToString();
        numero6.text = _numero6.ToString();
    }

    public void ReadAnswer()
    {
        if(answer.text == numAnswer_.ToString())
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

    private Vector2 GenerateRandomPosition()
    {
        Vector2 position;
        bool isClose;
        do
        {
            float x = UnityEngine.Random.Range(minX, maxX);
            float y = UnityEngine.Random.Range(minY, maxY);
            position = new Vector2(x, y);

            isClose = false;
            foreach (Vector2 pos in positions)
            {
                if (Vector2.Distance(pos, position) < spacing)
                {
                    isClose = true;
                    break;
                }
                if (Mathf.Abs(pos.x - position.x) < spacing && Mathf.Abs(pos.y - position.y) < spacing)
                {
                    isClose = true;
                    break;
                }
            }
        } while (isClose);

        return position;
    }
}
