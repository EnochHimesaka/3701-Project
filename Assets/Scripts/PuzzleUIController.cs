using UnityEngine;

public class PuzzleUIController : MonoBehaviour
{
    public GameObject puzzleUI; // UI ����
    public PlayerController playerController; // ��ҽű�

    private bool isPuzzleActive = false; // �Ƿ񼤻���ս���

    void Start()
    {
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
}
