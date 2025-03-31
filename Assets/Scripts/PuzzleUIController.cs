using UnityEngine;
using UnityEngine.InputSystem; // 新输入系统

public class PuzzleUIController : MonoBehaviour
{
    public static bool puzzleCleared = false; // ✅ 用于关卡判断

    [Header("拼图系统")]
    public GameObject puzzleUI;
    public PuzzlePiece[] puzzlePieces;
    public GameObject completeImage;
    public GameObject completionText;

    [Header("控制系统")]
    public PlayerController playerController;
    public PlayerInput playerInput;

    [Header("场景反馈")]
    public Light[] sceneLights;
    public GameObject[] powerSwitches;
    public AudioSource completionVoice;

    private bool isPuzzleActive = false;
    private bool puzzleCompleted = false;
    private bool voicePlayed = false;

    void Start()
    {
        puzzleUI.SetActive(false);
        if (completionText != null)
            completionText.SetActive(false);
    }

    void Update()
    {
        if (isPuzzleActive && puzzleCompleted && Keyboard.current.eKey.wasPressedThisFrame)
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
            ExitPuzzleUI();
        }
    }

    public void CheckPuzzleCompletion()
    {
        foreach (PuzzlePiece piece in puzzlePieces)
        {
            if (!piece.IsCorrectlyRotated()) return;
        }

        puzzleCompleted = true;
        puzzleCleared = true;

        if (!voicePlayed && completionVoice != null)
        {
            completionVoice.Play();
            voicePlayed = true;
        }

        if (completionText != null) completionText.SetActive(true);
        if (completeImage != null) completeImage.SetActive(true);

        foreach (Light light in sceneLights)
        {
            if (light != null) light.enabled = true;
        }

        foreach (GameObject obj in powerSwitches)
        {
            if (obj != null) obj.SetActive(true);
        }

        foreach (PuzzlePiece piece in puzzlePieces)
        {
            piece.enabled = false;
        }
    }

    private void ExitPuzzleUI()
    {
        isPuzzleActive = false;
        puzzleUI.SetActive(false);
        if (completionText != null) completionText.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerController != null) playerController.enabled = true;
        if (playerInput != null) playerInput.enabled = true;
    }
}