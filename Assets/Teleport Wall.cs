using UnityEngine;

public class TeleportWall : MonoBehaviour
{
    public Transform targetPosition;       // Ҫ���͵���λ��
    public bool useCharacterControllerFix = true;  // �Ƿ��Զ����� CharacterController

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (useCharacterControllerFix)
            {
                CharacterController cc = other.GetComponent<CharacterController>();
                if (cc != null)
                {
                    cc.enabled = false;
                    other.transform.position = targetPosition.position;
                    cc.enabled = true;
                }
                else
                {
                    other.transform.position = targetPosition.position;
                }
            }
            else
            {
                other.transform.position = targetPosition.position;
            }
        }
    }
}
