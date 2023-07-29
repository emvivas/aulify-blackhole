using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyCapsule());
        
    }

    IEnumerator DestroyCapsule()
    {
        yield return new WaitForSeconds(8);
        Destroy(this.gameObject);
    } 
}
