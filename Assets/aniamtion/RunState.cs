using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunState : StateMachineBehaviour
{
    List<Transform> wayPoints = new List<Transform>();
    NavMeshAgent agent;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent not found on the NPC!");
            return;
        }

        agent.stoppingDistance = 0.1f;
       

        // 获取所有路径点
        GameObject wpRoot = GameObject.FindGameObjectWithTag("WayPoints");
        if (wpRoot != null)
        {
            wayPoints.Clear();
            foreach (Transform t in wpRoot.transform)
            {
                wayPoints.Add(t);
            }
        }

        // 设置初始目标
        MoveToRandomWaypoint();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null || wayPoints.Count == 0) return;

        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance &&
            agent.velocity.sqrMagnitude < 0.1f)
        {
            MoveToRandomWaypoint();
        }

       
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent != null)
        {
            agent.SetDestination(agent.transform.position); // 停止移动
        }
    }

    void MoveToRandomWaypoint()
    {
        if (wayPoints.Count == 0) return;

        Transform nextPoint = wayPoints[Random.Range(0, wayPoints.Count)];
        agent.SetDestination(nextPoint.position);
    }
}
