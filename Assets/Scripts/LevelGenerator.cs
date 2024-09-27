using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject outerCornerPrefab; // 外部角落墙壁预制件
    public GameObject outerWallPrefab;   // 外部墙壁预制件
    public GameObject innerCornerPrefab; // 内部角落墙壁预制件
    public GameObject innerWallPrefab;   // 内部墙壁预制件
    public GameObject tJunctionPrefab;   // T字型墙壁预制件
    public GameObject pelletPrefab;      // 普通豆子预制件
    public GameObject powerPelletPrefab; // 能量豆子预制件

    public Vector2 startPosition = new Vector2(-67, 72); // 生成起点位置
    public float tileSpacing = 5f; // 每个物体之间的距离
    public float scaleMultiplier = 1f; // 物体的缩放比例

    // 2D数组，表示左上象限的关卡布局
    int[,] levelMap = 
    { 
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7}, 
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4}, 
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4}, 
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4}, 
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3}, 
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5}, 
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4}, 
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3}, 
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4}, 
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4}, 
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3}, 
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0}, 
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0}, 
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0}, 
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0}, 
    };

    void Start()
    {
        GenerateLevel();
    }

    // 生成四个象限的关卡
    void GenerateLevel()
    {
        // 左上象限（不进行镜像）
        GenerateQuadrant(levelMap, new Vector2(0, 0), false, false);
        // 右上象限（水平镜像）
        GenerateQuadrant(levelMap, new Vector2(1, 0), true, false);
        // 左下象限（垂直镜像）
        GenerateQuadrant(levelMap, new Vector2(0, 1), false, true);
        // 右下象限（水平和垂直镜像）
        GenerateQuadrant(levelMap, new Vector2(1, 1), true, true);
    }

    // 生成象限
    void GenerateQuadrant(int[,] map, Vector2 quadrantOffset, bool flipX, bool flipY)
    {
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                // 获取当前元素的值
                int tileType = map[y, x];

                // 计算生成物体的初始位置
                Vector2 spawnPosition = new Vector2(startPosition.x + (x * tileSpacing), startPosition.y - (y * tileSpacing));

                // 根据象限偏移来调整位置
                spawnPosition.x += quadrantOffset.x * (map.GetLength(1) * tileSpacing);
                spawnPosition.y -= quadrantOffset.y * (map.GetLength(0) * tileSpacing);

                // 正确的水平和垂直镜像计算
                if (flipX)
                {
                    spawnPosition.x = startPosition.x + (map.GetLength(1) - 1 - x) * tileSpacing + quadrantOffset.x * (map.GetLength(1) * tileSpacing);
                }
                if (flipY)
                {
                    spawnPosition.y = startPosition.y - (map.GetLength(0) - 1 - y) * tileSpacing - quadrantOffset.y * (map.GetLength(0) * tileSpacing);
                }

                // 根据tileType生成对应的物体，并对它们进行缩放
                GameObject objToInstantiate = null;

                switch (tileType)
                {
                    case 1:
                        objToInstantiate = outerCornerPrefab;
                        break;
                    case 2:
                        objToInstantiate = outerWallPrefab;
                        break;
                    case 3:
                        objToInstantiate = innerCornerPrefab;
                        break;
                    case 4:
                        objToInstantiate = innerWallPrefab;
                        break;
                    case 5:
                        objToInstantiate = pelletPrefab;
                        break;
                    case 6:
                        objToInstantiate = powerPelletPrefab;
                        break;
                    case 7:
                        objToInstantiate = tJunctionPrefab;
                        break;
                    default:
                        // 0表示空白格子，不生成任何东西
                        break;
                }

                // 如果有要生成的物体
                if (objToInstantiate != null)
                {
                    // 实例化对象并设置缩放
                    GameObject instantiatedObject = Instantiate(objToInstantiate, spawnPosition, Quaternion.identity);
                    instantiatedObject.transform.localScale *= scaleMultiplier; // 将缩放值乘以2.5倍
                }
            }
        }
    }
}