using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    GameObject cameraAnim;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject luz;
    [SerializeField]
    GameObject IntroPanel;
    FirstPersonController blockRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        blockRotation = player.GetComponent<FirstPersonController>();
        player.SetActive(true);
        blockRotation.cameraCanMove = false;
    }
}
