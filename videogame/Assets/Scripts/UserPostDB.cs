using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserPostDB : MonoBehaviour
{
    private const string userEndPoint = "http://blackholeapi.us-east-1.elasticbeanstalk.com/api/videogame/user";
    [SerializeField]
    public bool userPosted;

    void Awake()
    {
        StartCoroutine(PostUserCorrutine());
    }

    [System.Serializable]
    public class UserData
    {
        public int userIdentificator;
        public string username;
    }

    public IEnumerator PostUserCorrutine(){
        UnityWebRequest request = UnityWebRequest.Post(userEndPoint, "");
        request.SetRequestHeader("Content-Type", "application/json");
        Debug.Log(PlayerPrefs.GetString("username"));
        UserData user = new UserData{
            userIdentificator = PlayerPrefs.GetInt("idUser"),
            username = PlayerPrefs.GetString("username")
        };
        string jsonData = JsonUtility.ToJson(user);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData.ToString());
        Debug.Log("Enviando" + jsonData);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // La solicitud se realizó con éxito
            Debug.Log("La solicitud se realizó con éxito:" + request.downloadHandler.text);
            string jsonString = request.downloadHandler.text;
            jsonString = jsonString.Trim();
            jsonString = "{ \"items\": " + jsonString + "}";
            userPosted = true;
        }
        else
        {
            // La solicitud no se pudo realizar
            Debug.Log("La solicitud no se pudo realizar. Error code:" + request.responseCode);
        }
        request.Dispose();
    }
}
