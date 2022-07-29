using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiScript : MonoBehaviour
{
    private Transform target;   //����׷��Ŀ���λ��
    public float MoveSpeed = 2.5f; //�����ƶ��ٶ�
    private NavMeshAgent navMeshAgent;  //����Ѱ·���

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;  //��ȡ��Ϸ�����ǵ�λ�ã����ҵĹ����������ǵı�ǩ��Player
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = MoveSpeed;  //����Ѱ·���������ٶ�
        if (navMeshAgent == null)
        {
            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        }
    }


    void Update()
    {
        navMeshAgent.SetDestination(target.transform.position); //����Ѱ·Ŀ��
    }
}
