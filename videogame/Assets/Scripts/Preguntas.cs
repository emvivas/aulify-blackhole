using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Preguntas : MonoBehaviour
{
    float minDistance = 2;
    float distance;
    bool completed;
    [SerializeField]
    TMP_InputField inputField;
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
    GameObject exclamationMark;
    [SerializeField]
    GameObject luz;
    [SerializeField]
    GameObject luz2;
    LuzControl luzControl;
    LuzControl luzControl2;
    [SerializeField]
    GameObject correctPanel;
    public int completedModules;
    GameObject moduleCounter;
    ModuleCounter counterscript;
    [SerializeField]
    GameObject led;
    // Start is called before the first frame update
    void Start()
    {
        completed = false;
        completedModules = 0;
        player = GameObject.Find("Player");
        blockMovement = player.GetComponent<FirstPersonController>();
        luzControl = luz.GetComponent<LuzControl>();
        moduleCounter = GameObject.Find("ModuleCounter_");
        counterscript = moduleCounter.GetComponent<ModuleCounter>();
        luzControl2 = luz2.GetComponent<LuzControl>();
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
        if(Time.timeSinceLevelLoad > 12){
            distance = Vector3.Distance(player.transform.position, this.gameObject.transform.position);
            if(completed == false){ 
                if(distance < minDistance){
                    player.SetActive(false);
                    if(cameraAnim != null)
                        Destroy(cameraAnim);
                    cameraProblem.SetActive(true);
                    StartCoroutine(OpenPanel());  
                }
            }
        }
    }
    public void ClosePanelWithE(){
        if(gameObject.name != "LaptopIntro"){
        
            cameraProblem.SetActive(false);
            player.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            ProblemaPanel.gameObject.SetActive(false);
            blockMovement.playerCanMove = true;
            blockMovement.cameraCanMove = true;
        }  
    }

    IEnumerator OpenPanel(){
        yield return new WaitForSeconds(1);
        ProblemaPanel.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void IsCompleted(bool com){
        StartCoroutine(CorrectPanelActive());
        completed = com;
        cameraProblem.SetActive(false);
        player.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ProblemaPanel.gameObject.SetActive(false);
        blockMovement.playerCanMove = true;
        blockMovement.cameraCanMove = true;
        exclamationMark.SetActive(false);
        led.SetActive(true);
        luzControl.GreenBlink();
        luzControl2.GreenBlink();
        counterscript.Counter();
    }

    IEnumerator CorrectPanelActive(){
        correctPanel.SetActive(true);
        yield return new WaitForSeconds(1);
        correctPanel.SetActive(false);
    }
}