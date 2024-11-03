using UnityEngine;
using System.Collections.Generic;

public class GhostController : MonoBehaviour
{
    public GameObject[] ghosts; // 鬼魂对象数组
    public Transform player;    // 玩家对象，用于追踪和避开

    private List<GhostBehaviour> ghostBehaviours = new List<GhostBehaviour>();

    private void Start()
    {
        // 为每个鬼魂对象指定不同的行为
        ghostBehaviours.Add(new RandomGhost());
        ghostBehaviours.Add(new ChasingGhost());
        ghostBehaviours.Add(new EvadingGhost());
        ghostBehaviours.Add(new PatrollingGhost());

        // 确保鬼魂数量与行为数量一致
        if (ghosts.Length != ghostBehaviours.Count)
        {
            Debug.LogError("鬼魂数量和行为数量不一致，请检查设置！");
        }
    }

    private void Update()
    {
        // 更新每个鬼魂的位置
        for (int i = 0; i < ghosts.Length; i++)
        {
            GameObject ghost = ghosts[i];
            GhostBehaviour behaviour = ghostBehaviours[i];

            // 获取当前鬼魂的位置和下一步移动方向
            Vector2 currentPos = ghost.transform.position;
            Vector2 direction = behaviour.GetNextDirection(currentPos, player.position);

            // 移动鬼魂
            MoveGhost(ghost, direction);
        }
    }

    // 用于移动鬼魂对象的方法
    private void MoveGhost(GameObject ghost, Vector2 direction)
    {
        float speed = 2.0f; // 调整鬼魂移动速度
        ghost.transform.Translate(direction * speed * Time.deltaTime);
    }
}