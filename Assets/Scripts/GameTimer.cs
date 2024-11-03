using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;  // 计时器文本
    private float elapsedTime = 0f;    // 已经过去的时间（秒）

    private void Update()
    {
        // 累加时间
        elapsedTime += Time.deltaTime;

        // 计算分钟、秒和毫秒
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 100) % 100 - 1);

        // 格式化时间并显示
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}