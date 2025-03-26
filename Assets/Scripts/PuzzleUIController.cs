using UnityEngine;

public class PuzzleUIController : MonoBehaviour
{
    public GameObject puzzleUI;                      // 拼图UI界面
    public PlayerController playerController;        // 玩家控制器
    public UnityEngine.InputSystem.PlayerInput playerInput; // PlayerInput（新输入系统）

    public PuzzlePiece[] puzzlePieces;               // 所有拼图块
    public GameObject[] powerSwitches;               // 多个电源开关
    public GameObject completionText;               
    public Light[] sceneLights;

    public GameObject completeImage;
    private bool isPuzzleActive = false;
    private bool puzzleCompleted = false;

    void Start()
    {
        puzzleUI.SetActive(false);
        if (completionText != null)
            completionText.SetActive(false);
    }

    void Update()
    {
        if (isPuzzleActive && puzzleCompleted && Input.GetKeyDown(KeyCode.E))
        {
            ExitPuzzleUI();
        }
    }

    public void TogglePuzzleUI()
    {
        isPuzzleActive = !isPuzzleActive;
        puzzleUI.SetActive(isPuzzleActive);

        if (isPuzzleActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (playerController != null) playerController.enabled = false;
            if (playerInput != null) playerInput.enabled = false;
        }
        else
        {
            ExitPuzzleUI(); // 防止外部错误调用直接退出
        }
    }

    public void CheckPuzzleCompletion()
    {
        foreach (PuzzlePiece piece in puzzlePieces)
        {
            if (!piece.IsCorrectlyRotated())
                return;
        }

     
        puzzleCompleted = true;

        // 显示完成提示
        if (completionText != null)
            completionText.SetActive(true);

        // 开灯
        if (sceneLights != null && sceneLights.Length > 0)
        {
            foreach (Light light in sceneLights)
            {
                if (light != null)
                    light.enabled = true;
            }
        }

        if (completeImage != null)
            completeImage.SetActive(true);

        // 解锁所有电源开关
        foreach (GameObject obj in powerSwitches)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        // 禁止继续转拼图块
        foreach (PuzzlePiece piece in puzzlePieces)
        {
            piece.enabled = false;
        }
    }

    private void ExitPuzzleUI()
    {
        isPuzzleActive = false;
        puzzleUI.SetActive(false);
        if (completionText != null)
            completionText.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerController != null) playerController.enabled = true;
        if (playerInput != null) playerInput.enabled = true;
    }
}
