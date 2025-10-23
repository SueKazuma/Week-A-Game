using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
                // �G�f�B�^��ł̓���m�F�p�iUnity Editor�ł�Quit�������Ȃ����߁j
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
            }
            else
            {
                SceneManager.LoadScene("Game");
            }
        }
    }
}
