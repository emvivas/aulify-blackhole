using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Achievements : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI logroTexto; 
    private float tiempoEspera = 2f; 
    private List<int> listaLogros;
    private List<string> listaLogrosTitulos;
    [SerializeField] 
    private GameObject panelLogros;
    [SerializeField]
    GameObject User;
    UserDB userScript;
    [SerializeField]
    GameObject gameOverCanvas;
    StatScript statScript;
    [SerializeField]
    private int indiceActual = 0;

    void Start()
    {
        statScript = gameOverCanvas.GetComponent<StatScript>();
        userScript = User.GetComponent<UserDB>();
        listaLogros = statScript.achievementsID_;
        listaLogrosTitulos = statScript.achievementsTitles_;
        if (listaLogros.Count == 0)
        {
            panelLogros.SetActive(false);
            return;
        }
        else{
            panelLogros.SetActive(true);
        }
        MostrarLogro();

    }

    void MostrarLogro()
    {
        logroTexto.text = "Logro completado: " + listaLogros[indiceActual].ToString() + ". " + listaLogrosTitulos[indiceActual].ToString();
        StartCoroutine(DesvanecerPanel());
        indiceActual++;
        if (indiceActual < listaLogros.Count)
        {
            Invoke("MostrarLogro", tiempoEspera);
        }
        else{
            Invoke("DesactivarPanel", tiempoEspera);
        }
    }

    IEnumerator DesvanecerPanel()
    {
        Image panelImagen = panelLogros.GetComponent<Image>();
        TextMeshProUGUI textoMesh = logroTexto.GetComponent<TextMeshProUGUI>();
        panelImagen.color = new Color(panelImagen.color.r, panelImagen.color.g, panelImagen.color.b, 0f);
        textoMesh.color = new Color(textoMesh.color.r, textoMesh.color.g, textoMesh.color.b, 0f);

        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < tiempoEspera)
        {
            float t = tiempoTranscurrido / tiempoEspera;
            float opacidadActual = Mathf.Lerp(0f, 1f, t);
            panelImagen.color = new Color(panelImagen.color.r, panelImagen.color.g, panelImagen.color.b, opacidadActual);
            textoMesh.color = new Color(textoMesh.color.r, textoMesh.color.g, textoMesh.color.b, opacidadActual);
            yield return null;
            tiempoTranscurrido += Time.deltaTime;
        }

        panelImagen.color = new Color(panelImagen.color.r, panelImagen.color.g, panelImagen.color.b, 1f);
        textoMesh.color = new Color(textoMesh.color.r, textoMesh.color.g, textoMesh.color.b, 1f);
    }

    void DesactivarPanel()
    {
        StartCoroutine(DeactivatePanel());
    }

    private IEnumerator DeactivatePanel(){
        yield return new WaitForSeconds(2f);
        panelLogros.SetActive(false);
    }
}
