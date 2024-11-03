using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchScene()
    {
        SceneManager.LoadScene("Game"); // 将"Game"替换为你第二个场景的名字
    }
}