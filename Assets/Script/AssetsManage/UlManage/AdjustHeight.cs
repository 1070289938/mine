using UnityEngine;
using UnityEngine.UI;

public class AdjustHeight : MonoBehaviour
{
 public RectTransform parentRect; // 父节点（当前脚本调整的节点）
    public LayoutGroup parentLayoutGroup; // 父节点的父节点的布局组件（如 Vertical Layout Group）
    public bool adjustWidth = false; // 是否调整宽度
    public bool adjustHeight = true; // 是否调整高度
    public float padding = 10f; // 父节点与子节点的额外间距
    public float spacing = 5f; // 子节点之间的间距

    void Update()
    {
        AdjustParentSize();
    }

    private void AdjustParentSize()
    {
        if (parentRect == null) return;

        float totalHeight = 0f;
        float maxWidth = 0f;

        // 遍历所有子节点
        for (int i = 0; i < parentRect.childCount; i++)
        {
            RectTransform childRect = parentRect.GetChild(i) as RectTransform;
            if (childRect == null || !childRect.gameObject.activeSelf)
                continue;

            // 累计子节点高度和宽度
            if (adjustHeight)
                totalHeight += childRect.sizeDelta.y + spacing;

            if (adjustWidth)
                maxWidth = Mathf.Max(maxWidth, childRect.sizeDelta.x);
        }

        // 去除最后一个子节点的多余间距
        if (adjustHeight && parentRect.childCount > 0)
            totalHeight -= spacing;

        // 调整父节点尺寸
        Vector2 newSize = parentRect.sizeDelta;
        if (adjustHeight)
            newSize.y = totalHeight + padding;

        if (adjustWidth)
            newSize.x = maxWidth + padding;

        parentRect.sizeDelta = newSize;

        // 通知父节点的父级 Layout Group 重新布局
        if (parentLayoutGroup != null)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(parentLayoutGroup.GetComponent<RectTransform>());
        }
    }
}
