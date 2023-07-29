using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickControl : MonoBehaviour
{
    [SerializeField]
    GameObject text;
    bool isClicked = false;
    [SerializeField]
    GameObject book;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject manualHUD;
    public bool bookFlipping = false;
    FirstPersonController blockMovement;
    // Start is called before the first frame update
    void Start()
    {
        blockMovement = player.GetComponent<FirstPersonController>();

    }

    // Update is called once per frame
    void Update(){
     if(Time.timeSinceLevelLoad > 11 && isClicked == false)
        {
           text.SetActive(true);
           if(Input.GetMouseButtonDown(0))
            {
                isClicked = true;
                text.SetActive(false);
            }
        }
    }


    public void OpenBook(){
        if(Input.GetKeyDown(KeyCode.Z)){
            book.SetActive(true);
            manualHUD.SetActive(false);
            blockMovement.playerCanMove = false;
            blockMovement.cameraCanMove = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void CloseBook(){
        if(bookFlipping == false){
            book.SetActive(false);
            blockMovement.playerCanMove = true;
            blockMovement.cameraCanMove = true;
            manualHUD.SetActive(true);
            if(player.activeSelf){
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void BookFlipping(){
        StartCoroutine(WaitBookStopFlipping());
    }

    IEnumerator WaitBookStopFlipping(){
        bookFlipping = true;
        yield return new WaitForSeconds(0.7f);
        bookFlipping = false;
    }
}
