using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Intro : MonoBehaviour
{
    float minDistance = 2;
    float distance;
    bool activated;
    bool completed;
    [SerializeField]
    Transform ProblemaPanel;
    [SerializeField]
    TextMeshProUGUI ProblemaTexto;
    [SerializeField]
    GameObject player;
    FirstPersonController blockMovement;
    [SerializeField]
    GameObject cameraProblem;
    GameObject cameraAnim;
    [SerializeField]
    GameObject hud;
    Timer timerHud;
    [SerializeField]
    TMP_InputField answer;
    [SerializeField]
    GameObject wrongPanel;
    [SerializeField]
    GameObject correctPanel;
    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        player = GameObject.Find("Player");
        blockMovement = player.GetComponent<FirstPersonController>();
        timerHud = hud.GetComponent<Timer>();
        cameraAnim = GameObject.Find("CameraAnimation");
        Transform canvas = GameObject.Find("Canvas1").transform;
    }

    void Update(){
        if(ProblemaPanel.gameObject.activeSelf){
            blockMovement.playerCanMove = false;
            blockMovement.cameraCanMove = false;
        }
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        if(Time.timeSinceLevelLoad >= 11){
            distance = Vector3.Distance(player.transform.position, this.gameObject.transform.position);
            if(activated == false){ 
                if(distance < minDistance){
                    player.SetActive(false);
                    if(cameraAnim != null)
                        Destroy(cameraAnim);
                    cameraProblem.SetActive(true);
                    StartCoroutine(OpenPanel());  
                }
                activated = true;
            }
        }
    }
    public void ClosePanel(){
        cameraProblem.SetActive(false);
        player.SetActive(true);
        Destroy(cameraAnim);
        hud.SetActive(true);
        timerHud.StartTimer();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ProblemaPanel.gameObject.SetActive(false);
        blockMovement.playerCanMove = true;
        blockMovement.cameraCanMove = true;
        timerHud.ShowMessage();
    }


    public void ReadAnswer(){
        if(answer.text == "4"){
            StartCoroutine(CorrectAnswerPanel());
            ClosePanel();
        }
        else{
            StartCoroutine(WrongAnswerPanel());
        }
    }
    IEnumerator OpenPanel(){
        yield return new WaitForSeconds(1);
        ProblemaPanel.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        yield return new WaitForSeconds(1);
    }

    IEnumerator WrongAnswerPanel(){
        wrongPanel.SetActive(true);
        yield return new WaitForSeconds(1);
        wrongPanel.SetActive(false);
    }

    IEnumerator CorrectAnswerPanel(){
        correctPanel.SetActive(true);
        yield return new WaitForSeconds(1);
        correctPanel.SetActive(false);
    }
}

