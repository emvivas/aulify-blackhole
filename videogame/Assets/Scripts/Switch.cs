using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]
    GameObject[] switches;
    public int index;
    // Start is called before the first frame update
    void Start()
    {

    }

    public int RandomSwitch()
    {
        index = Random.Range(0, switches.Length);
        switches[index].SetActive(true);
        return index;
    }   
}
