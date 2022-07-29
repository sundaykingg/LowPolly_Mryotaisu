using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodScript : MonoBehaviour
{

    public float healthMeter = 100.0f;
    public float HealthPoints = 100.0f;
    public Image healthPoint;
    public Text text;



    public void Harm()
    {
        healthMeter += -5;
        Amount();

        Debug.Log("扣血-5");
    }
    public void Amount()
    {
        healthPoint.fillAmount = healthMeter / HealthPoints;
        text.text = $"{healthMeter}/{HealthPoints}";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (healthMeter > 0 && collision.gameObject.tag == "Enemy")
        {
            Harm();
            Debug.Log("我被" + collision.gameObject.tag + "攻击了,生命剩余：" + healthMeter);
        }
        if (healthMeter == 0)
        {
            Debug.Log("我死啦");
        }

    }


}
