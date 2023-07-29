using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEEScript : MonoBehaviour
{
    [SerializeField]
    GameObject screenPanel;
    float minDistance = 2;
    float distance;
    [SerializeField]
    GameObject player;
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
            screenPanel.SetActive(!screenPanel.activeSelf);
        }
    }
}
