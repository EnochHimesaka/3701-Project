using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Transform[] patrolPoints;
    private int _currentPatrolIndex;
    public float sightRange, attackRange, interactRange = 3f;
    public bool playerInSightRange, playerInAttackRange;
    public Animator animator;
    public float patrolSpeed = 3.5f;
    public float chaseSpeed = 5.5f;
    private bool isActivated = false;
    public GameObject interactPromptUI;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        _currentPatrolIndex = 0;
    }

    private void Update()
    {
        if (!isActivated)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            bool inRange = distance <= interactRange;

            if (interactPromptUI != null)
                interactPromptUI.SetActive(inRange); // 控制 Image 显示/隐藏

            if (inRange && Input.GetKeyDown(KeyCode.E))
            {
                isActivated = true;
                if (interactPromptUI != null)
                    interactPromptUI.SetActive(false); // 关闭提示
            }
            return;
        }

        // AI 行为逻辑
        playerInSightRange = CheckPlayerInRange(sightRange);
        playerInAttackRange = CheckPlayerInRange(attackRange);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange) AttackPlayer();

        animator.SetFloat("Speed", agent.velocity.magnitude);
        float animationSpeedCoefficient = 0.2f;
        animator.SetFloat("MotionSpeed", agent.velocity.magnitude * animationSpeedCoefficient);
    }

    private bool CheckPlayerInRange(float range)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    public void OnFootstep()
    {
        // 可加脚步声等
    }

    private void Patroling()
    {
        agent.speed = patrolSpeed;
        if (patrolPoints.Length > 0 && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            agent.SetDestination(patrolPoints[_currentPatrolIndex].position);
            _currentPatrolIndex = (_currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    private void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // 攻击逻辑待添加
    }
}
