using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;

public class OtherLoginManager : MonoBehaviour
{

    [SerializeField]
    TMP_InputField usernameInput;

    [SerializeField]
    TMP_InputField passwordInput;

    [System.Serializable]
    public class Credentials
    {
        public string username;
        public string password;
    }

    public GameObject errorText;

    public GameObject loadingPanel;

    [System.Serializable]
    public class Response
    {
        public int code;
        public string message;
    }

    private class loginResponse {
        public string message;
        public string token;
        public int idUser;
    }

    public void DoLogin()
    {
        loadingPanel.SetActive(true);
        StartCoroutine(ValidateCredentials(usernameInput.text,passwordInput.text));
    }

    public void Start()
    {
        errorText.SetActive(false);
        loadingPanel.SetActive(false);
    }

    private IEnumerator ValidateCredentials(string user, string password)
    {
        loadingPanel.SetActive(true);
        string url = "https://jgvuug8gmj.execute-api.us-east-1.amazonaws.com/login";

        UnityWebRequest request = UnityWebRequest.Post(url , "");
        request.SetRequestHeader("Content-Type", "application/json");
        request.timeout = 10000;
        Credentials credentials = new Credentials();
        credentials.username = user;
        credentials.password = password;

        string jsonData = JsonUtility.ToJson(credentials);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData.ToString());
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        // Envia la solicitud de forma asíncrona
        yield return request.SendWebRequest();
     
        loadingPanel.SetActive(false);
        if (request.result == UnityWebRequest.Result.Success)
        {
            loginResponse response = JsonUtility.FromJson<loginResponse>(request.downloadHandler.text);
            validateUser(response);
        }
        Debug.Log(request.error + " " + request.downloadHandler.text);
        request.Dispose();
    }

    private void validateUser(loginResponse response) {
        if (response.message == "User logged in successfully")
        {
            PlayerPrefs.SetString("token", response.token);
            PlayerPrefs.SetInt("idUser", response.idUser);
            PlayerPrefs.SetString("username", usernameInput.text);
            SceneManager.LoadScene(1);
        } else {
            ActivateErrorText();
        }
    }

    private IEnumerator ActivateErrorText(){
        errorText.SetActive(true);
        yield return new WaitForSeconds(3);
        errorText.SetActive(false);
    }
}