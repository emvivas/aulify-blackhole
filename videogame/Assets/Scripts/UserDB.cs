using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using System.Collections.Generic;


public class UserDB : MonoBehaviour
{
    private const string loginEndpoint = "https://0vnx9rbox9.execute-api.us-east-1.amazonaws.com/api/videogame";
    [SerializeField]
    GameObject Stats_;
    StatScript statsScript;
    UserStats userstats_;
    UserID userid;
    public List<Achievement> achievements;
    public List <int> achievementsID;
    public List <string> achievementsTitles;
    public List <Achievement> achievementsList;
    private int idUser;

    public void Awake()
    {
        idUser = PlayerPrefs.GetInt("idUser");
        LogrosPosibles();
        statsScript = Stats_.GetComponent<StatScript>();
        userstats_ = GetComponent<UserStats>();
        userid = GetComponent<UserID>();
    }

    public void LogrosPosibles()
    {
        StartCoroutine(Validate());
    }

    [System.Serializable]
    public class Response
    {
        public int code;
        public string message;
    }

    [System.Serializable]
    public class Achievement
    {
        public int identificator;
        public string title;
        public string content;
    }

    [System.Serializable]
    public class AchievementsList
    {
        public List<Achievement> items;

        public List<int> GetExistingIdentifiers()
        {
        List<int> existingIdentifiers = new List<int>();
        foreach (Achievement achievement in items)
            {
            existingIdentifiers.Add(achievement.identificator);
            }
        return existingIdentifiers;
        }

        public List<string> GetExistingTitles(){
            List<string> existingTitles = new List<string>();
            foreach (Achievement achievement in items)
            {
                existingTitles.Add(achievement.title);
            }
            return existingTitles;
        }

        public List<Achievement> GetExistingAchievements(){
            List<Achievement> existingAchievements = new List<Achievement>();
            foreach (Achievement achievement in items)
            {
                existingAchievements.Add(achievement);
            }
            return existingAchievements;
        }
    }

    public IEnumerator Validate()
    {
        UnityWebRequest request = UnityWebRequest.Get(loginEndpoint);
        request.SetRequestHeader("Content-Type", "application/json");
        UserID user = new UserID{
            userIdentificator = idUser
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
            //string jsonString = "[{\"identificator\":1,\"title\":\"Ni planty llego a tanto\",\"content\":\"Echale ganas, mijo\"},{\"identificator\":2,\"title\":\"Ni planty llego\",\"content\":\"Echale ganas, mijo\"},{\"identificator\":3,\"title\":\"Ni planty llego OTRA VEZ\",\"content\":\"Echale ganas, mijo\"},{\"identificator\":7,\"title\":\"jaqer\",\"content\":\" vivasputo\"}]";
            string jsonString = request.downloadHandler.text;
            jsonString = jsonString.Trim();
            jsonString = "{ \"items\": " + jsonString + "}";
            List<Achievement> achievements = JsonUtility.FromJson<AchievementsList>(jsonString).items;
            foreach (var item in achievements)
            {
                Debug.Log(item.identificator);
            }
            this.achievements = achievements;
            achievementsID = JsonUtility.FromJson<AchievementsList>(jsonString).GetExistingIdentifiers();
            achievementsList = JsonUtility.FromJson<AchievementsList>(jsonString).GetExistingAchievements();

           
        }
        else
        {
            // La solicitud no se pudo realizar
            Debug.Log("La solicitud no se pudo realizar. Error code:" + request.responseCode);
        }
        request.Dispose();
    }    

    public void PrintAchievements(){
        foreach (var item in achievements)
        {
            Debug.Log(item);
            Debug.Log(item.identificator);
            Debug.Log(item.title);
            Debug.Log(item.content);
        }
    }

    public void PrintAchievements2(List<Achievement> achievements){
        foreach (var item in achievements)
        {
            Debug.Log(item);
            Debug.Log(item.identificator);
            Debug.Log(item.title);
            Debug.Log(item.content);
        }
    }
}
