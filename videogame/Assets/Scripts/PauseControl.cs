using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField]
    public GameObject pauseMenuUI;
    FirstPersonController blockRotation;
    GameObject cameraAnim;
    [SerializeField]
    GameObject cameraPause;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject canvas1;
    GameObject backgroundMusic;
    [SerializeField]
    GameObject libro;
    [SerializeField]
    GameObject book;
    AutoFlip autoFlip;
    public bool bookFlipping = false;
    [SerializeField]
    GameObject cameraIntro;
    [SerializeField]
    GameObject panelMessage;
    // Start is called before the first frame update
    void Start()
    {
        autoFlip = book.GetComponent<AutoFlip>();
        blockRotation = GameObject.Find("Player").GetComponent<FirstPersonController>();
        cameraAnim = GameObject.Find("CameraAnimation");
        backgroundMusic = GameObject.Find("BackgroundMusic");
    }

    // Update is called once per frame
    void Update()
    {
        Validation();
    }

    public void Validation(){
        if(bookFlipping == false){
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    canvas1.SetActive(true);
                    Resume();
                }
                else
                {
                    autoFlip.isFlipping = false;
                    canvas1.SetActive(false);
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        cameraPause.SetActive(false);
        pauseMenuUI.SetActive(false);
        if(backgroundMusic != null){
            backgroundMusic.GetComponent<AudioSource>().UnPause();
        }
        Time.timeScale = 1f;
        GameIsPaused = false;
        canvas1.SetActive(true);
        if(player.activeSelf){
            if(!libro.activeSelf){
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else{
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        if(cameraAnim == null){
            if(libro.activeSelf){
                blockRotation.cameraCanMove = false;
            }
            else{
                blockRotation.cameraCanMove = true;
            }
        }
        if(cameraIntro.activeSelf){
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void Pause()
    {
        if(panelMessage.activeSelf)
            panelMessage.SetActive(false);
        cameraPause.SetActive(true);
        pauseMenuUI.SetActive(true);
        if(backgroundMusic != null){
            backgroundMusic.GetComponent<AudioSource>().Pause();
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        GameIsPaused = true;
        blockRotation.cameraCanMove = false;
    }


    public void BookFlipping(){
        StartCoroutine(WaitBookStopFlipping());
    }

    IEnumerator WaitBookStopFlipping(){
        bookFlipping = true;
        yield return new WaitForSeconds(1f);
        bookFlipping = false;
    }
}
