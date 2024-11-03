using UnityEngine;
using TMPro;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText;  // 倒计时的文字
    public AudioSource countdownAudio;     // 倒计时音效
    public float countdownDuration = 3f;   // 倒计时总时长

    void Awake()
    {
        Time.timeScale = 0;
    }
    private void Start()
    {
        Time.timeScale = 0;
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        // 暂停游戏
        Time.timeScale = 0;

        // 启用倒计时
        float countdown = countdownDuration;
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString("0");  // 显示倒计时
            countdownAudio.Play();                         // 播放倒计时音效
            yield return new WaitForSecondsRealtime(1f);   // 使用真实时间
            countdown--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1f);  // 显示“GO!”一秒

        // 倒计时结束后恢复游戏
        countdownText.gameObject.SetActive(false);    // 隐藏倒计时文本
        Time.timeScale = 1;                           // 恢复游戏
    }
}