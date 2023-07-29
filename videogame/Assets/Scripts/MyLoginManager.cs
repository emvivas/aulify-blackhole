using System.Collections;
using System.Net;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class MyLoginManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField userInputField;
    [SerializeField]
    TMP_InputField passwdInputField;
    [SerializeField]
    GameObject loadingPanel;

    [System.Serializable]
    public class Credentials
    {
        public string user;
        public string pass;
    }

    [System.Serializable]
    public class Response
    {
        public int code;
        public string message;
    }


    public void DoLogin() {
        // Usar el endpoint del módulo web para autenticación
        string user = userInputField.text;
        string pass = passwdInputField.text;
        loadingPanel.SetActive(true);
        StartCoroutine(ValidateCredentials(user, pass));
    }

    private IEnumerator ValidateCredentials(string user, string pass)
    {
        try
        {
            string url = "https://EndPointTest01.sergiohalamilla.repl.co/login";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.Timeout = 5000;
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";

            Credentials credentials = new Credentials();
            credentials.user = user;
            credentials.pass = pass;

            string jsonData = JsonUtility.ToJson(credentials); 
            Debug.Log("Enviando"+jsonData);

            StreamWriter streamWriter = new StreamWriter(httpRequest.GetRequestStream());        
            streamWriter.Write(jsonData);        
            streamWriter.Close();

            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream());
                string result = streamReader.ReadToEnd();
                Debug.Log("Result:" + result);
                Response response = JsonUtility.FromJson<Response>(result);
                // Mostrar los valores en la consola
                Debug.Log("Success: " + response.code);
                Debug.Log("Message: " + response.message);
                if (response.code == 1) {
                    PlayerPrefs.SetString("token", response.message);
                    SceneManager.LoadScene(1);
                    //En otra escena
                    //string token = PlayerPrefs.GetString("token");
                }
                else{  
                    loadingPanel.SetActive(false);
                }
            }
            else
            {
                Debug.Log("Error:" + httpResponse.StatusCode + " " + httpResponse.StatusDescription);
                loadingPanel.SetActive(false);
            }
        }
        catch (WebException ex) {
            loadingPanel.SetActive(false);
            if (ex.Status == WebExceptionStatus.Timeout)
            {
                
                Debug.Log("La solicitud ha excedido el tiempo de espera.");
            }
            else
            {
                
                Debug.Log("Error en la solicitud HTTP: " + ex.Message);
            }
        }
        yield return null;
    }
}

