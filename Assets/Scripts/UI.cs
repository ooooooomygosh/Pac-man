using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private Button startButton;

    void Start()
    {
        // 找到带有 StartGame 标签的按钮
        startButton = GameObject.FindGameObjectWithTag("Level 1")?.GetComponent<Button>();
        
        // 确保按钮存在并添加事件监听器
        if (startButton != null)
        {
            startButton.onClick.AddListener(LoadLevelOne);
        }
        else
        {
            Debug.LogError("Start button with tag 'StartGame' not found!");
        }
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("StartGame");
    }
}