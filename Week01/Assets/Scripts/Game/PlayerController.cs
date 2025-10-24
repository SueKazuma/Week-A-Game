using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [Header("ジャンプ設定")]
    [SerializeField] float jumpForce = 1500f;
    [SerializeField] float maxJumpTime = 0.2f;
    [Header("接地設定")]
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

        // 入力取得のみUpdateで行う
        CheckJumpInput();
    }

    void FixedUpdate()
    {
        // 実際のジャンプ処理
        ProcessJump();
    }

    /// <summary>
    /// ジャンプ処理まとめ
    /// </summary>
    #region ジャンプ処理まとめ

    // 入力検知
    void CheckJumpInput()
    {
        // 押したとき
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpRequested = true;
        }
        // 離したとき
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }

    // ジャンプ入力取得処理
    void ProcessJump()
    {
        if (jumpRequested)
        {
            StartJump();
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            // 押し込み中なら
            ContinueJump();
        }
    }

    /// <summary>
    /// ジャンプボタン
    /// </summary>
    // 押した瞬間
    void StartJump()
    {
        // ジャンプ開始
        isJumping = true;
        jumpTimer = maxJumpTime;    // Timerの初期化
        jumpRequested = false;
    }
    // 押し続けている間
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
    /// 接地判定。（足元の球で判定取得）
    /// </summary>
    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheckPos.position, radius, groundLayer);
    }

    /// <summary>
    /// 縄に引っかかる
    /// </summary>
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Rope"))
        {
            gameManager.GameEnd();
        }
    }

    
}
