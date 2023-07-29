using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightsEE : MonoBehaviour
{
    // Referencia a LuzControl
    [SerializeField]
    LuzControl luzControl;
    float minDistance = 2;
    float distance;
    [SerializeField]
    GameObject player;
    [SerializeField]
    TextMeshProUGUI textoTimer;
    [SerializeField]
    GameObject spotlightProblema;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < minDistance)
        {
            // Aplicar la animación a todos los objetos con la etiqueta "Spotlight"
            GameObject[] spotlights = GameObject.FindGameObjectsWithTag("Spotlight");
            foreach (GameObject spotlight in spotlights)
            {
                LuzControl luzControlSpotlight = spotlight.GetComponent<LuzControl>();
                if (luzControlSpotlight != null)
                {
                    luzControlSpotlight.AnimationOff();
                    luzControlSpotlight.White();
                    luzControlSpotlight.IntensityStart();
                    textoTimer.color = Color.black;
                    spotlightProblema.GetComponent<Light>().color = Color.white;
                    
                }
            }

        }
    }
}
