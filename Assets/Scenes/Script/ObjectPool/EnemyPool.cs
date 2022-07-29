using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Low_Poly_FPS_Pack.Components.Scripts.ObjectPool
{
    class EnemyPool:BaseGameObjectPool
    {

        public  GameObject Get(Vector3 position, float lifetime, Quaternion rotation,int blood,int maxblood)
        {
            GameObject returnobj = base.Get(position, lifetime);
            returnobj.GetComponent<Transform>().rotation = rotation;
            returnobj.GetComponent<GameObjectPoolInfo>().blood = blood;
            returnobj.GetComponent<GameObjectPoolInfo>().maxBlood = maxblood;
            return returnobj;
        }

        public override void Remove(GameObject obj)
        {
            base.Remove(obj);
        }
    }
}
