using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalStep : MonoBehaviour
{
    [SerializeField]
    GameObject moduleCounter;
    [SerializeField]
    GameObject player;
    ModuleCounter counterScript;
    float distance;
    float minDistance = 1;
    private int completed = 0;
    [SerializeField]
    GameObject goodCamera;
    [SerializeField]
    GameObject badCamera;
    [SerializeField]
    GameObject hud;
    [SerializeField]
    GameObject blackHole;
    BlackHoleScript blackHoleScript;
    [SerializeField]
    GameObject canvas1;
    [SerializeField]
    GameObject salaProblemas;
    [SerializeField]
    GameObject BadEndingPanel;
    [SerializeField]
    GameObject GoodEndingPanel;
    [SerializeField]
    GameObject PauseCanvas;
    private AudioSource errorSound;
    private AudioSource blackHoleExplosion;
    [SerializeField]
    GameObject stats;
    StatScript statsScript;
    // Start is called before the first frame update
    void Start()
    {   
        statsScript = stats.GetComponent<StatScript>();
        blackHoleExplosion = blackHole.GetComponent<AudioSource>();
        errorSound = GetComponent<AudioSource>();
        counterScript = moduleCounter.GetComponent<ModuleCounter>();
        blackHoleScript = blackHole.GetComponent<BlackHoleScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        if(distance < minDistance){
            completed = counterScript.ReturnCounter();
            if(completed == 4){
                Debug.Log("You have completed the game");
                statsScript.GetStats();
                GoodEnding();
            }
            else{
                Debug.Log("You have not completed the game");
                errorSound.Play();
            }
        }
    }

    public void GoodEnding(){
        goodCamera.SetActive(true);
        player.SetActive(false);
        hud.SetActive(false);
        canvas1.SetActive(false);
        salaProblemas.SetActive(false);
        StartCoroutine(WaitGoodEnding());
        PauseCanvas.SetActive(false);
    }

    public void BadEnding(){
        badCamera.SetActive(true);
        player.SetActive(false);
        hud.SetActive(false);
        canvas1.SetActive(false);
        salaProblemas.SetActive(false);
        StartCoroutine(WaitExplotion());
        StartCoroutine(WaitBadEnding());
        PauseCanvas.SetActive(false);
    }

    IEnumerator WaitExplotion(){
        yield return new WaitForSeconds(6);
        blackHoleScript.ActivateAnimation();
        yield return new WaitForSeconds(1);
        blackHoleExplosion.Play();
    }

    IEnumerator WaitBadEnding(){
        yield return new WaitForSeconds(12);
        BadEndingPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    IEnumerator WaitGoodEnding(){
        yield return new WaitForSeconds(7);
        GoodEndingPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
