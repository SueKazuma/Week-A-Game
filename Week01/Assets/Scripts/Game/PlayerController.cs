using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [Header("�W�����v�ݒ�")]
    [SerializeField] float jumpForce = 1500f;
    [SerializeField] float maxJumpTime = 0.2f;
    [Header("�ڒn�ݒ�")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheckPos;
    [SerializeField] float radius = 0.2f;

    private Rigidbody playerRigidbody;
    private Animator animator;

    private bool isJumping = false;
    private bool jumpRequested = false;
    private float jumpTimer = 0f;
 

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isJumping)
        {
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }

        // ���͎擾�̂�Update�ōs��
        CheckJumpInput();
    }

    void FixedUpdate()
    {
        // ���ۂ̃W�����v����
        ProcessJump();
    }

    /// <summary>
    /// �W�����v�����܂Ƃ�
    /// </summary>
    #region �W�����v�����܂Ƃ�

    // ���͌��m
    void CheckJumpInput()
    {
        // �������Ƃ�
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpRequested = true;
        }
        // �������Ƃ�
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }

    // �W�����v���͎擾����
    void ProcessJump()
    {
        if (jumpRequested)
        {
            StartJump();
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            // �������ݒ��Ȃ�
            ContinueJump();
        }
    }

    /// <summary>
    /// �W�����v�{�^��
    /// </summary>
    // �������u��
    void StartJump()
    {
        // �W�����v�J�n
        isJumping = true;
        jumpTimer = maxJumpTime;    // Timer�̏�����
        jumpRequested = false;
    }
    // ���������Ă����
    void ContinueJump()
    {
        if (jumpTimer > 0f)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            jumpTimer -= Time.fixedDeltaTime;
        }
        else
        {
            isJumping = false;
        }
    }
    #endregion

    /// <summary>
    /// �ڒn����B�i�����̋��Ŕ���擾�j
    /// </summary>
    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheckPos.position, radius, groundLayer);
    }

    /// <summary>
    /// ��Ɉ���������
    /// </summary>
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Rope"))
        {
            gameManager.GameEnd();
        }
    }

    
}
