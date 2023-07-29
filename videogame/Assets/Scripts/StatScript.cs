using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class StatScript : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textoTimerLeft;
    [SerializeField]
    TextMeshProUGUI textoNumberOfModulesCompleted;
    [SerializeField]
    TextMeshProUGUI textoNumberOfWrongAnswers;
    [SerializeField]
    TextMeshProUGUI textoNumberOfWrongAnswers2;
    [SerializeField]
    TextMeshProUGUI textoGameMode;
    [SerializeField]
    TextMeshProUGUI textoGameMode2;
    [SerializeField]
    GameObject hud;
    Timer timer;
    private float timeLeft;
    private int wrongAnswers;
    [SerializeField]
    GameObject GoodEndingPanel;
    [SerializeField]
    GameObject statsPanel;
    [SerializeField]
    GameObject statsBadPanel;
    private int _minutes;
    private int _seconds;
    private int _miliseconds;
    [SerializeField]
    GameObject moduleCounter;
    ModuleCounter moduleCounterScript;
    int modulesCompleted;
    private string isOnePlayer;
    [SerializeField]
    GameObject User;
    UserDB userScript;
    UserStats userstats_;
    UserID userid;
    private int gameMode;
    private List<int> existingIdentifiers;
    public List<int> achievementsID_;
    public List<string> achievementsTitles_;
    private int idUser;
  
    private const string loginEndpoint = "https://0vnx9rbox9.execute-api.us-east-1.amazonaws.com/api/videogame";

    // Start is called before the first frame update
    void Start()
    {
        idUser = PlayerPrefs.GetInt("idUser");
        isOnePlayer = PlayerPrefs.GetString("isOnePlayer");
        moduleCounterScript = moduleCounter.GetComponent<ModuleCounter>();
        timer = hud.GetComponent<Timer>();
        userScript = User.GetComponent<UserDB>();
        userstats_ = User.GetComponent<UserStats>();
        userid = User.GetComponent<UserID>();
        userid = new UserID{
            userIdentificator = idUser
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowStats(){
       statsPanel.SetActive(true);
        textoTimerLeft.text = string.Format("Tiempo restante: {0:00}:{1:00}:{2:00}", _minutes, _seconds, _miliseconds);
        textoNumberOfWrongAnswers.text = ("Respuestas incorrectas: " + wrongAnswers.ToString());
        textoGameMode.text = ("Modo de juego: " + isOnePlayer);
    }

    public void ShowBadStats(){
        statsBadPanel.SetActive(true);
        textoNumberOfModulesCompleted.text = ("Módulos completados: " + modulesCompleted.ToString());
        textoNumberOfWrongAnswers2.text = ("Respuestas incorrectas: " + wrongAnswers.ToString());
        textoGameMode2.text = ("Modo de juego: " + isOnePlayer);
    }
    public void HideStats(){
        statsPanel.SetActive(false);
    }

    public void GetStats(){
        timeLeft = timer.ReturnTime();
        wrongAnswers = timer.ReturnWrongAnswers();
        _minutes = (int)timeLeft / 60;
        _seconds = (int)timeLeft % 60;
        _miliseconds = (int)(timeLeft * 100) % 100;
        modulesCompleted = moduleCounterScript.ReturnCounter();
        gameMode = GameMode();
        this.userstats_ = new UserStats{
            time = string.Format("{0:00}:{1:00}:{2:00}", _minutes, _seconds, _miliseconds),
            wrongAnswers_ = timer.ReturnWrongAnswers(), 
            mode = gameMode,
            modulesCompleted_ = moduleCounterScript.ReturnCounter(),
            userIdentificator = idUser,
            achievements = new List<int>()
        };
        AchievementsCompleted();
        StartCoroutine(PostStatsCorrutine());
    }
    public void GetBadStats(){
        GameMode();
        modulesCompleted = moduleCounterScript.ReturnCounter();
        wrongAnswers = timer.ReturnWrongAnswers();
    }
    public void HideBadStats(){
        statsBadPanel.SetActive(false);
    }

    private int GameMode(){
        if(isOnePlayer == "True"){
           isOnePlayer = "Un jugador";
           return 0;
        }
        else{
            isOnePlayer = "Dos jugadores";
            return 1;
        }
    }

    public void CloseStatBadPanel(){
        statsBadPanel.SetActive(false);
    }

     public void CloseStatPanel(){
        statsPanel.SetActive(false);
    }

    public IEnumerator PostStatsCorrutine(){
        UnityWebRequest request = UnityWebRequest.Post(loginEndpoint, "");
        request.SetRequestHeader("Content-Type", "application/json");
        string jsonData = JsonUtility.ToJson(userstats_);
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
        }
        else
        {
            // La solicitud no se pudo realizar
            Debug.Log("La solicitud no se pudo realizar. Error code:" + request.responseCode);
        }
        request.Dispose();
    }

    private void AchievementsCompleted(){
        existingIdentifiers = userScript.achievementsID;

        if(modulesCompleted > 0 && existingIdentifiers.Contains(1)){
            userstats_.achievements.Add(1);
        }
        if(wrongAnswers > 0 && existingIdentifiers.Contains(2)){
            userstats_.achievements.Add(2);
        }
        if(wrongAnswers == 3 && existingIdentifiers.Contains(3)){
            userstats_.achievements.Add(3);
        }
        if(modulesCompleted == 0 && existingIdentifiers.Contains(4)){
            userstats_.achievements.Add(4);
        }
        if(wrongAnswers > 10 && existingIdentifiers.Contains(5)){
            userstats_.achievements.Add(5);
        }
        if(wrongAnswers == 0 && existingIdentifiers.Contains(6)){
            userstats_.achievements.Add(6);
        }
        if(isOnePlayer == "Un jugador" && wrongAnswers == 0 && timeLeft > 550 && existingIdentifiers.Contains(7)){
            userstats_.achievements.Add(7);
        }
         if(isOnePlayer == "Dos jugadores" && wrongAnswers == 0 && timeLeft > 250 && existingIdentifiers.Contains(7)){
            userstats_.achievements.Add(7);
        }
        if(timeLeft < 10 && modulesCompleted == 4 && existingIdentifiers.Contains(8)){
            userstats_.achievements.Add(8);
        }
        if(wrongAnswers == 1 && existingIdentifiers.Contains(9)){
            userstats_.achievements.Add(9);
        }
        if(wrongAnswers > 5 && modulesCompleted == 4 && existingIdentifiers.Contains(10)){
            userstats_.achievements.Add(10);
        }
        if(timeLeft == 0 && modulesCompleted == 0 && existingIdentifiers.Contains(11)){
            userstats_.achievements.Add(11);
        }
        if(timeLeft == 0 && modulesCompleted == 3 && existingIdentifiers.Contains(12)){
            userstats_.achievements.Add(12);
        }
        achievementsID_ = userstats_.achievements;
        FusionIdwithTitles();
    }

    public void FusionIdwithTitles(){
        foreach(var item in userScript.achievements){
            foreach(int id in achievementsID_){
                if(item.identificator == id){
                    achievementsTitles_.Add(item.title);
                }
            }
        }
    }
}
