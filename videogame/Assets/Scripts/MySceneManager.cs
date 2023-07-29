using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MySceneManager : MonoBehaviour
{

    [SerializeField]
    Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(1); 
        Time.timeScale = 1f; 
    }

    public void LoadGame()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("isOnePlayer",toggle.isOn.ToString());
        StartCoroutine(LoadGameRoutine());
    }

    IEnumerator LoadGameRoutine(){
        SceneManager.LoadScene(2);
        yield return new WaitForSeconds(5.0f);
    }
}
