using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiScript : MonoBehaviour
{
    private Transform target;   //设置追踪目标的位置
    public float MoveSpeed = 2.5f; //敌人移动速度
    private NavMeshAgent navMeshAgent;  //设置寻路组件

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;  //获取游戏中主角的位置，在我的工程里面主角的标签是Player
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = MoveSpeed;  //设置寻路器的行走速度
        if (navMeshAgent == null)
        {
            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        }
    }


    void Update()
    {
        navMeshAgent.SetDestination(target.transform.position); //设置寻路目标
    }
}
