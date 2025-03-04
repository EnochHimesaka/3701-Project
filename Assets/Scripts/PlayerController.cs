using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;          // ����ƶ��ٶ�
    [SerializeField] private float lookSpeed = 3f;      // ����ӽ���ת�ٶ�

    private InputSystem_Actions inputActions;
    private CharacterController characterController;
    private Vector2 input;
    private Vector2 lookInput; // �������

    private Transform cameraTransform; // ������
    private float currentVerticalAngle = 0f; // ��ǰ��ֱ�ӽǽǶ�

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform; // ��ȡ�����

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
        input = inputActions.Player.Move.ReadValue<Vector2>(); // ��ȡ�ƶ�����
        lookInput = inputActions.Player.Look.ReadValue<Vector2>(); // ��ȡ�������
    }

    private void Move()
    {
        // ������ת��Ϊ���緽��
        Vector3 moveDirection = new Vector3(input.x, 0, input.y).normalized;
        moveDirection = transform.TransformDirection(moveDirection) * speed * Time.deltaTime;

        // ʹ�� CharacterController �ƶ�
        characterController.Move(moveDirection);
    }

    private void Look()
    {
        // ������ת��ң�����תͷ��
        float horizontalRotation = lookInput.x * lookSpeed * Time.deltaTime;
        transform.Rotate(0, horizontalRotation, 0);

        // �ۻ���ֱ��ת�Ƕ�
        float verticalRotation = -lookInput.y * lookSpeed * Time.deltaTime;
        currentVerticalAngle += verticalRotation;

        // ���ƴ�ֱ�Ƕȷ�Χ��-70 �� 70��
        currentVerticalAngle = Mathf.Clamp(currentVerticalAngle, -70f, 70f);

        // Ӧ�ô�ֱ��ת��ֻ��������� X �ᣩ
        cameraTransform.localEulerAngles = new Vector3(currentVerticalAngle, 0, 0);
    }
   
}
