using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public Transform targetPoint;
    public float moveSpeed = 2f;
    private bool shouldMove = false;

    private Animator animator; // 引用 Animator

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 按下 E 键触发移动
        if (Input.GetKeyDown(KeyCode.E))
        {
            shouldMove = true;
            if (animator != null)
                animator.SetBool("isWalk", true); // 播放 walk 动画
        }

        if (shouldMove && targetPoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPoint.position) < 0.05f)
            {
                shouldMove = false;
                if (animator != null)
                    animator.SetBool("isWalk", false); // 停止 walk 动画
            }
        }
    }
}
