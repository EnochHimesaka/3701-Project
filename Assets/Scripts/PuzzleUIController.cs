using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleUIController : MonoBehaviour, IPointerClickHandler
{
    public GameObject puzzleUI; // UI ����
    public PlayerController playerController; // ��ҽű�
    public float rotationAngle = 90f; // ÿ�ε����ת�Ƕ�
    public float rotationSpeed = 200f; // ��ת�ٶ�
    private bool isRotating = false;
    private float targetRotation;
    private bool isPuzzleActive = false; // �Ƿ񼤻���ս���

    void Start()
    {
        targetRotation = transform.eulerAngles.z;
        puzzleUI.SetActive(false); // ��ʼʱ UI �ر�
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TogglePuzzleUI();
        }
    }

    public void TogglePuzzleUI()
    {
        isPuzzleActive = !isPuzzleActive;
        puzzleUI.SetActive(isPuzzleActive);

        if (isPuzzleActive)
        {
            // �������
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // ������ҿ���
            if (playerController != null)
            {
                playerController.enabled = false; // �ر���ҿ���
            }
        }
        else
        {
            // �������
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // ������ҿ���
            if (playerController != null)
            {
                playerController.enabled = true; // ����������ҿ���
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isRotating)
        {
            targetRotation += rotationAngle; // �� UI ��ת 90��
            StartCoroutine(RotateSmoothly());
        }
    }

    private System.Collections.IEnumerator RotateSmoothly()
    {
        isRotating = true;
        float currentRotation = transform.eulerAngles.z;
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime * (rotationSpeed / rotationAngle);
            float newRotation = Mathf.Lerp(currentRotation, targetRotation, time);
            transform.eulerAngles = new Vector3(0, 0, newRotation);
            yield return null;
        }

        transform.eulerAngles = new Vector3(0, 0, targetRotation); // ȷ����ת����ͣ����ȷ�ĽǶ�
        isRotating = false;
    }
}
