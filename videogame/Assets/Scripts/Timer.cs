using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timer = 300;
    private int _minutes, _seconds, _miliseconds;
    public bool timerIsRunning;
    [SerializeField]
    public TextMeshProUGUI textoTimer;
    [SerializeField]
    GameObject blackHole;
    GameObject backgroundMusic;
    [SerializeField]
    GameObject button;
    FinalStep finalStepScript;
    private bool stopgrowing;
    private AudioSource beepSound;
    [SerializeField]
    private int wrongAnswers = 0;
    [SerializeField]
    GameObject stats;
    StatScript statsScript;
    [SerializeField]
    GameObject discount;
    [SerializeField]
    GameObject moduleCounter;
    ModuleCounter moduleCounterScript;
    int modulesCompleted;
    [SerializeField]
    GameObject canvas1;
    ClickControl clickControlScript;
    private string isOnePlayer;
    [SerializeField]
    GameObject manualHUD;
    [SerializeField]
    GameObject panelInstructions;

    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI discountText = discount.GetComponent<TextMeshProUGUI>();
        clickControlScript = canvas1.GetComponent<ClickControl>();
        statsScript = stats.GetComponent<StatScript>();
        moduleCounterScript = moduleCounter.GetComponent<ModuleCounter>();
        finalStepScript = button.GetComponent<FinalStep>();
        beepSound = GetComponent<AudioSource>();
        backgroundMusic = GameObject.Find("BackgroundMusic");
        textoTimer.fontSharedMaterial.DisableKeyword(ShaderUtilities.Keyword_Glow);
        isOnePlayer = PlayerPrefs.GetString("isOnePlayer");
        if(isOnePlayer == "True"){
            timer = 600;
            discountText.text = "-35";
            manualHUD.SetActive(true);
        }
        else{
            timer = 300;
            discountText.text = "-20";
            manualHUD.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timerIsRunning == true){
            if(isOnePlayer == "True")
                clickControlScript.OpenBook();
            timer -= Time.deltaTime;
            _minutes = (int)timer / 60;
            _seconds = (int)timer % 60;
            _miliseconds = (int)(timer * 100) % 100;
            textoTimer.text = string.Format("{0:00}:{1:00}:{2:00}", _minutes, _seconds, _miliseconds);
            BlackHoleGrowing();
            if(timer < 30){
                textoTimer.fontSharedMaterial.EnableKeyword(ShaderUtilities.Keyword_Glow);
            }
            if(timer < 11){
                if(timer % 1 < 0.1f){
                    beepSound.Play();
                }
            }
            if(timer <= 0){
                timer = 0;
                if(backgroundMusic != null){
                    backgroundMusic.GetComponent<AudioSource>().Stop();
                }
                textoTimer.text = "00:00:00";
                statsScript.GetStats();
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                finalStepScript.BadEnding();
            } 
        }
    }

    public void StartTimer(){
        timerIsRunning = true;
    }

    public void BlackHoleGrowing(){
        if(timerIsRunning == true && stopgrowing == false){
            blackHole.transform.localScale += new Vector3(0.0003f, 0.0003f, 0.0003f);
        }
         if(blackHole.transform.localScale.x >= 8f){
            stopgrowing = true;
        }
    }

    public void BlackHoleGrows(){
        if(stopgrowing == false){
            blackHole.transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);
        }
        if(blackHole.transform.localScale.x >= 8f){
            stopgrowing = true;
        }
    }

    public void WrongAnswer(){
        if(textoTimer.rectTransform.localScale.x < 1.5f){
            StartCoroutine(BigNumbers());
        }
        wrongAnswers++;
        if(isOnePlayer == "True"){
            timer -= 35;
        }
        else{
            timer -= 20;
        }
        BlackHoleGrows();
    }

    IEnumerator BigNumbers(){
        textoTimer.rectTransform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
        discount.SetActive(true);
        textoTimer.color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(1);
        textoTimer.rectTransform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
        discount.SetActive(false);
        textoTimer.color = new Color32(255, 255, 255, 255);
        yield return new WaitForSeconds(1);

    }

    public float ReturnTime(){
        return timer;
    }

    public int ReturnWrongAnswers(){
        return wrongAnswers;
    }

    public void AddTime(){
        timer += 15;
    }

    public void ShowMessage(){
        StartCoroutine(ShowMessageCoroutine());
    }

    private IEnumerator ShowMessageCoroutine(){
        panelInstructions.SetActive(true);
        yield return new WaitForSeconds(3);
        panelInstructions.SetActive(false);
    }
}
