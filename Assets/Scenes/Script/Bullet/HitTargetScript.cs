using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTargetScript : MonoBehaviour
{
    // Start is called before the first frame update
    int countHit = 0;
    int blood = 100;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        countHit++;
        blood--;
        Debug.Log("�ұ�"+collision.gameObject.name+"������"+countHit+"��");
        if (blood == 0) { 
            
        }
    }
}
