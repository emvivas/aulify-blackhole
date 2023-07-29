using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzControl : MonoBehaviour
{
    [SerializeField]
    Light luz;
    // Start is called before the first frame update
    void Start()
    {
        luz = GetComponent<Light>();
        luz.color = Color.white;
        AnimationOff();
        Panic();
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void Red()
    {
        luz.color = Color.red;
    }

    public void White()
    {
        luz.color = Color.white;
    }

    public void AnimationOff()
    {
        GetComponent<Animator>().enabled = false;
    }

    public void AnimationOn()
    {
        GetComponent<Animator>().enabled = true;
    }

    public void Panic()
    {
        StartCoroutine(PanicRoutine());
    }

    public void GreenBlink ()
    {
        StartCoroutine(GreenBlinking());
    }
    public void IntensityStart()
    {
        luz.intensity = 1.5f;
    }
    IEnumerator GreenBlinking()
    {
        AnimationOff();
        luz.color = Color.green;
        yield return new WaitForSeconds(3.0f);
        luz.color = Color.red;
        AnimationOn();
    }

    IEnumerator PanicRoutine(){
        yield return new WaitForSeconds(8.0f);
        AnimationOn();
        Red();
    }
}
