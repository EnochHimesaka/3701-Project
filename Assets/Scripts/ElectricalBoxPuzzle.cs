using UnityEngine;

public class ElectricalBoxPuzzle : MonoBehaviour
{
    public GameObject circuitPuzzleUI; // ���ӵ�·UI
    public Light controlledLight; // ���ճɹ������ĵƹ�
    private bool isSolved = false;

    void Start()
    {
        if (circuitPuzzleUI != null) circuitPuzzleUI.SetActive(false);
        if (controlledLight != null) controlledLight.enabled = false; // Ĭ�ϵƹ�ر�
    }

    public void StartPuzzle()
    {
        if (isSolved) return; // �����ظ�����

        if (circuitPuzzleUI != null)
        {
            circuitPuzzleUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }

    public void CompletePuzzle()
    {
        if (circuitPuzzleUI != null)
        {
            circuitPuzzleUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }

        if (controlledLight != null)
        {
            controlledLight.enabled = true; // �����ƹ�
        }

        isSolved = true; // ���Ϊ�ѽ���
    }
}
