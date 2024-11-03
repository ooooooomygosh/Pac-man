using UnityEngine;
using System.Collections.Generic;

public abstract class GhostBehaviour
{
    protected LayerMask wallLayer; // 墙壁图层

    public GhostBehaviour()
    {
        wallLayer = LayerMask.GetMask("Wall"); // 设置墙壁图层（请确保墙壁对象的图层为“Wall”）
    }

    public abstract Vector2 GetNextDirection(Vector2 currentPos, Vector2 playerPos);

    // 检查某个方向是否被墙壁阻挡
    protected bool IsWallInDirection(Vector2 position, Vector2 direction)
    {
        float detectionDistance = 0.5f; // 检测距离
        RaycastHit2D hit = Physics2D.Raycast(position, direction, detectionDistance, wallLayer);
        return hit.collider != null;
    }
}

public class RandomGhost : GhostBehaviour
{
    public override Vector2 GetNextDirection(Vector2 currentPos, Vector2 playerPos)
    {
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        Vector2 chosenDirection = Vector2.zero;

        // 随机选择一个没有墙壁的方向
        for (int i = 0; i < directions.Length; i++)
        {
            int index = Random.Range(0, directions.Length);
            Vector2 direction = directions[index];
            if (!IsWallInDirection(currentPos, direction))
            {
                chosenDirection = direction;
                break;
            }
        }

        return chosenDirection == Vector2.zero ? Vector2.up : chosenDirection;
    }
}

public class ChasingGhost : GhostBehaviour
{
    public override Vector2 GetNextDirection(Vector2 currentPos, Vector2 playerPos)
    {
        Vector2 preferredDirection = (playerPos - currentPos).normalized;
        Vector2[] alternateDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        // 首先尝试直接朝向玩家的方向移动
        if (!IsWallInDirection(currentPos, preferredDirection))
        {
            return preferredDirection;
        }

        // 如果有墙壁阻挡，则尝试其他方向
        foreach (var direction in alternateDirections)
        {
            if (!IsWallInDirection(currentPos, direction))
            {
                return direction;
            }
        }

        return Vector2.zero; // 如果没有找到无障碍方向，则保持不动
    }
}

public class EvadingGhost : GhostBehaviour
{
    public override Vector2 GetNextDirection(Vector2 currentPos, Vector2 playerPos)
    {
        Vector2 preferredDirection = (currentPos - playerPos).normalized;
        Vector2[] alternateDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        // 首先尝试远离玩家的方向
        if (!IsWallInDirection(currentPos, preferredDirection))
        {
            return preferredDirection;
        }

        // 如果有墙壁阻挡，则尝试其他方向
        foreach (var direction in alternateDirections)
        {
            if (!IsWallInDirection(currentPos, direction))
            {
                return direction;
            }
        }

        return Vector2.zero; // 如果没有找到无障碍方向，则保持不动
    }
}

public class PatrollingGhost : GhostBehaviour
{
    private List<Vector2> patrolPoints = new List<Vector2> {
        new Vector2(-10, 0), new Vector2(10, 0), new Vector2(10, -10), new Vector2(-10, -10)
    };
    private int currentPoint = 0;

    public override Vector2 GetNextDirection(Vector2 currentPos, Vector2 playerPos)
    {
        Vector2 target = patrolPoints[currentPoint];
        Vector2 direction = (target - currentPos).normalized;

        // 如果没有墙壁，继续朝巡逻目标点方向移动
        if (!IsWallInDirection(currentPos, direction))
        {
            if (Vector2.Distance(currentPos, target) < 0.1f)
            {
                currentPoint = (currentPoint + 1) % patrolPoints.Count;
            }
            return direction;
        }

        // 如果被墙壁阻挡，则尝试其他方向
        Vector2[] alternateDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        foreach (var altDirection in alternateDirections)
        {
            if (!IsWallInDirection(currentPos, altDirection))
            {
                return altDirection;
            }
        }

        return Vector2.zero; // 如果没有找到无障碍方向，则保持不动
    }
}