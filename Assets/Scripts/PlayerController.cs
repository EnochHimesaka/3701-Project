using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;          // 玩家移动速度
    [SerializeField] private float lookSpeed = 3f;      // 玩家视角旋转速度

    private InputSystem_Actions inputActions;
    private CharacterController characterController;
    private Vector2 input;
    private Vector2 lookInput; // 鼠标输入

    private Transform cameraTransform; // 玩家相机
    private float currentVerticalAngle = 0f; // 当前垂直视角角度

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform; // 获取主相机

        if (characterController == null)
        {
            Debug.LogError("CharacterController component is missing on the Player GameObject.");
        }

        if (cameraTransform == null)
        {
            Debug.LogError("Main Camera is missing in the scene.");
        }
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void Update()
    {
        GatherInput();
        Move();
        Look();

        
    }

    private void GatherInput()
    {
        input = inputActions.Player.Move.ReadValue<Vector2>(); // 获取移动输入
        lookInput = inputActions.Player.Look.ReadValue<Vector2>(); // 获取鼠标输入
    }

    private void Move()
    {
        // 将输入转换为世界方向
        Vector3 moveDirection = new Vector3(input.x, 0, input.y).normalized;
        moveDirection = transform.TransformDirection(moveDirection) * speed * Time.deltaTime;

        // 使用 CharacterController 移动
        characterController.Move(moveDirection);
    }

    private void Look()
    {
        // 横向旋转玩家（左右转头）
        float horizontalRotation = lookInput.x * lookSpeed * Time.deltaTime;
        transform.Rotate(0, horizontalRotation, 0);

        // 累积垂直旋转角度
        float verticalRotation = -lookInput.y * lookSpeed * Time.deltaTime;
        currentVerticalAngle += verticalRotation;

        // 限制垂直角度范围（-70 到 70）
        currentVerticalAngle = Mathf.Clamp(currentVerticalAngle, -70f, 70f);

        // 应用垂直旋转（只更新相机的 X 轴）
        cameraTransform.localEulerAngles = new Vector3(currentVerticalAngle, 0, 0);
    }
   
}
