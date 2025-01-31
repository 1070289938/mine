using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilManager : MonoBehaviour
{
 

    public GameObject floatingTextPrefab; // 漂浮文字的预制体
    public Transform canvasTransform; // Canvas，用于显示 UI



 // 显示漂浮文字
    public void ShowFloatingText(string text)
    {
        // 实例化漂浮文字预制体
        GameObject floatingText = Instantiate(floatingTextPrefab, canvasTransform);

        // 将漂浮文字设置为屏幕中心
        floatingText.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        // 设置漂浮文字内容
        floatingText.GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }

}
