using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public Transform targetPoint;
    public float moveSpeed = 2f;
    private bool shouldMove = false;

    private Animator animator; // ���� Animator

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ���� E �������ƶ�
        if (Input.GetKeyDown(KeyCode.E))
        {
            shouldMove = true;
            if (animator != null)
                animator.SetBool("isWalk", true); // ���� walk ����
        }

        if (shouldMove && targetPoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPoint.position) < 0.05f)
            {
                shouldMove = false;
                if (animator != null)
                    animator.SetBool("isWalk", false); // ֹͣ walk ����
            }
        }
    }
}
