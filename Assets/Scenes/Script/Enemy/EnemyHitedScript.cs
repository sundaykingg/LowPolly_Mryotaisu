using Assets.Low_Poly_FPS_Pack.Components.Scripts.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHitedScript : MonoBehaviour
{

    EnemyPool m_enemyPool;


    //HealthBar
    public GameObject enemyHealthBar;

    public bool alwaysVisiable = true;

    public float visiableTime;

    public Transform healthBarPoint;

    Image healthImage;

    Canvas enemyHealthBarCanvas;

    Transform UIbar;

    Transform cam;



    // Start is called before the first frame update
    void Start()
    {
        m_enemyPool = GameObjectPoolManager.instance.CreatGameObjectPool<EnemyPool>("Enemy");
    }
    private void Awake()
    {
        cam = Camera.allCameras[1].transform;
        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if (canvas.tag == "EnemyHealthUI")
            {
                UIbar = Instantiate(enemyHealthBar, canvas.transform).transform;
                healthImage = UIbar.GetChild(0).GetComponent<Image>();
                /*if (this.gameObject.GetComponent<GameObjectPoolInfo>() != null) {
                    Amount(this.gameObject.GetComponent<GameObjectPoolInfo>().blood, this.gameObject.GetComponent<GameObjectPoolInfo>().maxBlood);
                } */
                UIbar.gameObject.SetActive(!alwaysVisiable);
            }
        }
    }

    public void Amount(float currentHealth, float maxHealth)
    {
        healthImage.fillAmount = currentHealth / maxHealth;
    }

    private void LateUpdate()
    {
        UIbar.position = healthBarPoint.position;
        UIbar.forward = -cam.forward;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (this.gameObject.GetComponent<GameObjectPoolInfo>().blood != 0) {
                UIbar.gameObject.SetActive(alwaysVisiable);
            }
            this.gameObject.GetComponent<GameObjectPoolInfo>().blood += -10;
            Debug.Log("我被" + collision.gameObject.name + "击中了，血量剩余：" + this.gameObject.GetComponent<GameObjectPoolInfo>().blood);
            Amount(this.gameObject.GetComponent<GameObjectPoolInfo>().blood, this.gameObject.GetComponent<GameObjectPoolInfo>().maxBlood);
            if (this.gameObject.GetComponent<GameObjectPoolInfo>().isDeath())
            {
                m_enemyPool.Remove(this.gameObject);
                UIbar.gameObject.SetActive(!alwaysVisiable);
            }
        }

    }
}
