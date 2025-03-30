using UnityEngine;
using UnityEngine.AI;

public class TriggerStopZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("npc1"))
        {
            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
            Animator animator = other.GetComponent<Animator>();

            if (agent != null)
            {
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
            }

            if (animator != null)
            {
                // 设置 Idle 状态
                animator.SetBool("isWalking",false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("npc1"))
        {
            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
            Animator animator = other.GetComponent<Animator>();

            if (agent != null)
            {
                agent.isStopped = false;
                // agent.SetDestination(...) 如果你想继续给目标
            }

            if (animator != null)
            {
                animator.SetFloat("Speed", 1f);  // 让他回到走路动画，可按需调整
            }
        }
    }
}
