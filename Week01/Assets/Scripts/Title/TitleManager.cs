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
                // エディタ上での動作確認用（Unity EditorではQuitが効かないため）
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
