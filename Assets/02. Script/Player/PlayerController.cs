using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("이동")]
    [SerializeField] float Movespeed = 5f;
    [SerializeField] float smothMoveSpeed = 0.2f;
    private float originalSpeed;

    [Header("점프")]
    private float jumpForce = 6f;
    private float originalJumpforce;

    [Header("카메라")]
    [SerializeField] Transform cameraContainer;
    [SerializeField] float lookSensitivity = 10f;
    [SerializeField] float maxRotate = 85f;
    [SerializeField] float minRotate = -85f;
    [SerializeField] Camera main;
    [SerializeField] Camera sub;

    [Header("레이어마스크")]
    public LayerMask groundLayerMask;

    private bool isRun;
    private bool isPersonal = true;
    public event Action<float> useStamina;
    private float cameraRotate;
    private Vector2 curMovementInput;
    private Vector2 curMouseInput;

    private Rigidbody rb;
    private Player_Condition condi;

    private Coroutine speedBuff;
    private Coroutine jumpBuff;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        condi = GetComponent<Player_Condition>();

        originalSpeed = Movespeed;
        originalJumpforce = jumpForce;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        MoveCamera();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        if (isRun && IsGrounded() && condi.Stamina >= condi.Run_Stamina)
        {
            useStamina?.Invoke(condi.Run_Stamina * Time.deltaTime);
            dir *= Movespeed * 1.5f;
        }
        else
        {
            dir *= Movespeed;
        }
        dir.y = rb.velocity.y;

        rb.velocity = Vector3.Lerp(rb.velocity, dir , smothMoveSpeed);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded() && condi.Stamina >= condi.Jump_Stamina)
        {
            useStamina?.Invoke(condi.Jump_Stamina);
            Jump(jumpForce);
        }
    }

    public void Jump(float JumpForce)
    {
        rb.AddForce(new Vector3(0,jumpForce,0), ForceMode.Impulse);
    }

    public void jumping_platform(float jumpForce)
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        curMouseInput = context.ReadValue<Vector2>();
    }

    private void MoveCamera()
    {
        cameraRotate += curMouseInput.y * lookSensitivity;
        cameraRotate = Mathf.Clamp(cameraRotate, minRotate, maxRotate);
        cameraContainer.localEulerAngles = new Vector3(-cameraRotate, 0, 0);

        transform.eulerAngles += new Vector3(0, curMouseInput.x * lookSensitivity, 0);
    }

    private bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.3f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.3f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.3f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.3f) + (transform.up * 0.01f), Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            Debug.DrawRay(rays[i].origin, rays[i].direction * 1.2f, Color.red);
            if (Physics.Raycast(rays[i], 1.2f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            isRun = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isRun = false;
        }
    }

    public void OnCamera(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            isPersonal = !isPersonal;
            main.enabled = !isPersonal;
            sub.enabled = isPersonal;
        }
    }

    public void AddSpeed(float amount, float duration)
    {
        if (speedBuff != null)
        {
            StopCoroutine(speedBuff);
        }

        speedBuff = StartCoroutine(speedBuffCoroutine(amount, duration));
    }

    IEnumerator speedBuffCoroutine(float amount, float duration)
    {
        Movespeed += amount;

        yield return new WaitForSeconds(duration);

        Movespeed = originalSpeed;

        speedBuff = null;
    }

    public void AddJump(float amount, float duration)
    {
        if (jumpBuff != null)
        {
            StopCoroutine (jumpBuff);
        }

        jumpBuff = StartCoroutine(JumpBuffCoroutine(amount, duration));
    }


    private IEnumerator JumpBuffCoroutine(float amount, float duration)
    {
        jumpForce += amount;
        yield return new WaitForSeconds(duration);
        jumpForce = originalJumpforce;
        jumpBuff = null;
    }
}
