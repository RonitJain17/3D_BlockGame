using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float delayTime;

    void Start()
    {
        StartCoroutine(Go());
    }

    IEnumerator Go(){
        while(true){
            GetComponent<Animation>().Play();
            yield return new WaitForSeconds(2f);

        }
    }
}
