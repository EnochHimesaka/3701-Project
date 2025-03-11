using UnityEngine;
using UnityEngine.EventSystems; // �����¼�ϵͳ

public class PuzzleUIController : MonoBehaviour
{
    public GameObject puzzleUI; // UI ����
    public PlayerController playerController; // ��ҽű�

    private bool isPuzzleActive = false; // �Ƿ񼤻���ս���
    private int clickCount = 0; // ���������
    private int requiredClicks = 10; // ��Ҫ�ĵ������

    void Start()
    {
        puzzleUI.SetActive(false); // ��ʼʱ UI �ر�
    }

    void Update()
    {
        if (isPuzzleActive && Input.GetMouseButtonDown(0))
        {
            // ȷ��������ᱻ UI ����
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                clickCount++;
                Debug.Log("�������: " + clickCount);

                if (clickCount >= requiredClicks)
                {
                    WinPuzzle();
                }
            }
            else
            {
                Debug.Log("�����Ч���� UI ��ס�ˣ�");
            }
        }
    }

    public void TogglePuzzleUI()
    {
        isPuzzleActive = !isPuzzleActive;
        puzzleUI.SetActive(isPuzzleActive);

        if (isPuzzleActive)
        {
            // �� UI��������꣬��������ƶ�
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (playerController != null)
            {
                playerController.enabled = false;
            }
        }
        else
        {
            // �ر� UI��������꣬�ָ���ҿ���
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (playerController != null)
            {
                playerController.enabled = true;
            }
        }
    }

    public void WinPuzzle()
    {
        Debug.Log("���ճɹ���");
        isPuzzleActive = false;
        puzzleUI.SetActive(false); // �ر� UI

        // ��ԭ������
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // ��ԭ��ҿ���
        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }
}
