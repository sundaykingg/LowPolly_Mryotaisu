using Assets.Low_Poly_FPS_Pack.Components.Scripts.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    BulletPool m_bollet_pool;

    void Start()
    {
        m_bollet_pool = GameObjectPoolManager.instance.CreatGameObjectPool<BulletPool>("SMG_05_Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("·¢ÉúÅö×²ÁË£º" + collision.gameObject.name);
        m_bollet_pool.Remove(this.gameObject);
    }
}
