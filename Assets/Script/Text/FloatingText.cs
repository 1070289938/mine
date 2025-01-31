using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 50f; // 向上移动的速度
    public float fadeDuration = 1f; // 透明度渐变时长

    private TMPro.TextMeshProUGUI textMesh;
    private Color textColor;
    private float lifetime;

    private void Awake()
    {
        textMesh = GetComponent<TMPro.TextMeshProUGUI>();
        textColor = textMesh.color;
        lifetime = fadeDuration;
    }

    private void Update()
    {
        // 向上移动
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // 渐变透明
        lifetime -= Time.deltaTime;
        textColor.a = Mathf.Clamp01(lifetime / fadeDuration);
        textMesh.color = textColor;

        // 超时销毁
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
