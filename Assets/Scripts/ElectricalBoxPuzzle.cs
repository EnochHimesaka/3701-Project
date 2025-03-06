using UnityEngine;

public class ElectricalBoxPuzzle : MonoBehaviour
{
    public GameObject circuitPuzzleUI; // 连接电路UI
    public Light controlledLight; // 解谜成功后开启的灯光
    private bool isSolved = false;

    void Start()
    {
        if (circuitPuzzleUI != null) circuitPuzzleUI.SetActive(false);
        if (controlledLight != null) controlledLight.enabled = false; // 默认灯光关闭
    }

    public void StartPuzzle()
    {
        if (isSolved) return; // 避免重复开启

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
            controlledLight.enabled = true; // 开启灯光
        }

        isSolved = true; // 标记为已解谜
    }
}
