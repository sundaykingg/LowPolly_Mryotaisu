using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Low_Poly_FPS_Pack.Components.Scripts.ObjectPool
{
    class BulletPool : BaseGameObjectPool
    {
        public BulletPool() : base() { 
        
        }
        

        public GameObject Get(Vector3 position, float lifetime, Quaternion rotation) {
            GameObject returnobject = base.Get(position, lifetime);
            returnobject.GetComponent<Transform>().rotation = rotation;
            return returnobject;
        }

        public override void Remove(GameObject obj)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0,0,0);
            rb.angularVelocity = new Vector3(0, 0, 0);
            base.Remove(obj);
        }

    }
}
