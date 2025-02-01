using UnityEngine;

public class Pickup : Interactable
{
    [SerializeField] private GameObject flashLight; // ��Ҫ��Inspector���ֶ������ֵ�Ͳ����

    private bool isPickedUp = false;

    private void Start()
    {
        if (flashLight != null)
        {
            flashLight.SetActive(false); // ��ʼʱ�ֵ�Ͳ����
        }
    }

    protected override void Interact()
    {
        if (!isPickedUp)
        {
            Debug.Log("Picked up: " + gameObject.name);
            isPickedUp = true;

            if (flashLight != null)
            {
                flashLight.SetActive(true); // ʰȡ�������ֵ�Ͳ
            }

            Destroy(gameObject); // ʰȡ������ Pickup ��Ʒ
        }
    }
}
