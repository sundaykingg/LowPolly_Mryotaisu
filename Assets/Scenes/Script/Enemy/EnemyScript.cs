using Assets.Low_Poly_FPS_Pack.Components.Scripts.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int blood;
    [SerializeField] List<GameObject> m_enemyPrefab;
    [SerializeField] GameObject m_enemySpawnPoint;
    private int idx = 0;

    EnemyPool m_enemyPool;




    //HealthBar

    void Start()
    {
        m_enemyPool = GameObjectPoolManager.instance.CreatGameObjectPool<EnemyPool>("Enemy");
        //m_enemyPool.prefab = m_enemyPrefab;
    }

    // Update is called once per frame


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) {
            Vector3 posi = m_enemySpawnPoint.transform.position;
            m_enemyPool.prefab = m_enemyPrefab[++idx%m_enemyPrefab.Count];
            GameObject m_enemy = m_enemyPool.Get(posi,3600, new Quaternion(0.707f, 0, 0, 0.707f),blood,blood);
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.gameObject.GetComponent<GameObjectPoolInfo>().blood -= 10;
        Debug.Log("我被"+collision.gameObject.name+"击中了，血量剩余：" + this.gameObject.GetComponent<GameObjectPoolInfo>().blood);
        if (this.gameObject.GetComponent<GameObjectPoolInfo>().isDeath()) {
            m_enemyPool.Remove(this.gameObject);
        }
    }
}
