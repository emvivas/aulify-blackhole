using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCanvasControl : MonoBehaviour
{
    [SerializeField]
    GameObject qrPanel;
    private string isOnePlayer;
    [SerializeField]
    GameObject sceneManager;
    [SerializeField]
    Toggle toggle;
    MySceneManager sceneManagerScript;
    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {
        sceneManagerScript = sceneManager.GetComponent<MySceneManager>();
       
    }
    void Update(){
        PlayerPrefs.SetString("isOnePlayer",toggle.isOn.ToString());
    }

    public void OpenQrPanel(){
        isOnePlayer = PlayerPrefs.GetString("isOnePlayer");
        Debug.Log(isOnePlayer);
        if(isOnePlayer == "True"){
            sceneManagerScript.LoadGame();
        }
        else{
            qrPanel.SetActive(true);
        }
    }
    public void CloseQrPanel(){
        qrPanel.SetActive(false);
    }
}
