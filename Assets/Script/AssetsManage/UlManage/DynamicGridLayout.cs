using UnityEngine;
using UnityEngine.UI;

public class DynamicGridLayout : MonoBehaviour
{
    private GridLayoutGroup gridLayout;

    void Start()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        UpdateCellSize();


    }


    void UpdateCellSize()
    {
        // 计算40%屏幕宽度
        float cellWidth = Screen.width * 0.45f;

        // 设置单元格尺寸（保持原高度不变）
        Vector2 currentSize = gridLayout.cellSize;
        gridLayout.cellSize = new Vector2(cellWidth, currentSize.y);

        // 立即刷新布局
        LayoutRebuilder.ForceRebuildLayoutImmediate(
            (RectTransform)gridLayout.transform);
    }

}