using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("回転")]
    [SerializeField] Transform rotationCenter;
    [SerializeField] float maxSpeed = 1000f;        // 最大の回転速度
    [SerializeField] float baseSpeed = 90f;         // 初期の回転速度（度/秒）
    [SerializeField] float speedIncrement = 10f;    // countごとの増加速度
    [SerializeField] float randomErrorRange = 3f;   // 誤差の最大幅
    [Header("UI")]
    [SerializeField] Text JumpCountText;
    [Space(10)]
    [Header("リザルト")]
    [SerializeField] GameObject resultPanel;
    [SerializeField] Text resultCountText; 

    private float totalRotation = 0f;               // 累積回転角度
    private int jumpCount = 0;
    private int resultJumpCount = 0;

    private bool isGame = true;

    void Update()
    {
        if (isGame)
        {
            RotateRope();
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); // リトライ
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                SceneManager.LoadScene("Title"); // タイトル
            }
        }
    }

    /// <summary>
    /// 縄を回す。
    /// </summary>
    void RotateRope()
    {
        // 加速後の速度を計算し、maxSpeedを超えないように制限
        float intendedSpeed = baseSpeed + jumpCount * speedIncrement;
        float currentSpeed = Mathf.Min(intendedSpeed, maxSpeed);

        // ランダム誤差（-randomErrorRange〜+randomErrorRange）
        float randomError = Random.Range(-randomErrorRange, randomErrorRange);
        // このフレームの回転角度（誤差込み）
        float rotationThisFrame = (currentSpeed + randomError) * Time.deltaTime;

        // 回転処理
        rotationCenter.Rotate(rotationThisFrame, 0f, 0f);

        // 累積回転角度を更新
        totalRotation += rotationThisFrame;

        // 360度回転したらカウントアップ
        if (totalRotation >= 360f)
        {
            jumpCount++;
            totalRotation -= 360f;
            JumpCountUI();
            Debug.Log("Count: " + jumpCount + " | Speed: " + currentSpeed);
        }
    }

    public void GameEnd()
    {
        isGame = false;

        // リザルトスコア
        resultJumpCount = jumpCount;
        resultCountText.text = resultJumpCount.ToString();
        // リザルト表示
        resultPanel.SetActive(true);
    }

    private void JumpCountUI()
    {
        JumpCountText.text = jumpCount.ToString();
    }
}
