using UnityEngine;

public class Pacdot : MonoBehaviour
{
<<<<<<< Updated upstream
=======
    public AudioSource pelletEatenSound; // 添加音频源变量

>>>>>>> Stashed changes
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Pacman")
        {
<<<<<<< Updated upstream
            Destroy(gameObject);
        }
    }
}
=======
            if (pelletEatenSound != null) // 检查是否分配了音频源
            {
                pelletEatenSound.Play(); // 播放声音
            }
            Destroy(gameObject); // 销毁豆子对象
        }
    }
}
>>>>>>> Stashed changes
