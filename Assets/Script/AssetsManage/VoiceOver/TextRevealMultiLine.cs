using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Text;
using TMPro;
using System;

public class TextRevealMultiLine : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float revealSpeed = 0.03f;
    public float fadeDuration = 0.1f; // 透明度渐变持续时间
    public float lineBreakPause = 0.5f; // 换行停顿时间
    private string[] lines;
    private bool isRevealing = false;

    public static TextRevealMultiLine Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple instances of TextRevealMultiLine found! Destroying this one.");
            Destroy(gameObject);
        }
    }

    public void StartReveal(string str, GameObject elementToFade, float waitSecondsAfterReveal, Action action)
    {
        if (string.IsNullOrEmpty(str))
        {
            Debug.LogError("Input string is null or empty!");
            return;
        }

        lines = Regex.Split(str, "\r\n|\r|\n");
        textComponent.text = "";

        if (!isRevealing)
        {
            isRevealing = true;
            StartCoroutine(RevealTextLines(elementToFade, waitSecondsAfterReveal, action));
        }
    }

    private IEnumerator RevealTextLines(GameObject elementToFade, float waitSecondsAfterReveal, Action action)
    {
        StringBuilder textBuilder = new StringBuilder();
        for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
        {
            string currentLine = "";
            for (int charIndex = 0; charIndex < lines[lineIndex].Length; charIndex++)
            {
                currentLine += lines[lineIndex][charIndex];
                textBuilder.Clear();
                for (int i = 0; i < lineIndex; i++)
                {
                    textBuilder.Append(lines[i]).Append('\n');
                }
                textBuilder.Append(currentLine);

                float startTime = Time.time;
                while (Time.time - startTime < fadeDuration)
                {
                    float alpha = Mathf.Clamp01((Time.time - startTime) / fadeDuration);
                    textComponent.text = ApplyAlphaToText(textBuilder.ToString(), lineIndex, charIndex, alpha);
                    yield return null;
                }

                // 确保最后透明度为 1
                textComponent.text = ApplyAlphaToText(textBuilder.ToString(), lineIndex, charIndex, 1f);

                yield return new WaitForSeconds(revealSpeed);
            }

            // 换行后添加停顿
            if (lineIndex < lines.Length - 1)
            {
                yield return new WaitForSeconds(lineBreakPause);
            }
        }
        isRevealing = false;


        // 等待指定秒数
        if (waitSecondsAfterReveal > 0)
        {
            yield return new WaitForSeconds(waitSecondsAfterReveal);
        }

        action?.Invoke();

        // 文本显示完毕且等待时间结束后，开始元素渐隐
        if (elementToFade != null)
        {
            StartCoroutine(FadeOutElement(elementToFade));
        }
    }

    private string ApplyAlphaToText(string text, int lineIndex, int charIndex, float alpha)
    {
        StringBuilder result = new StringBuilder();
        int startIndex = GetStartIndexOfLine(lineIndex);
        int currentCharIndex = startIndex + charIndex;

        for (int i = 0; i < text.Length; i++)
        {
            if (i == currentCharIndex)
            {
                result.Append(MakeCharacterTransparent(text[i], alpha));
            }
            else
            {
                result.Append(text[i]);
            }
        }
        return result.ToString();
    }

    private int GetStartIndexOfLine(int lineIndex)
    {
        int index = 0;
        for (int i = 0; i < lineIndex; i++)
        {
            index += lines[i].Length + 1; // +1 是为了算上换行符
        }
        return index;
    }

    private string MakeCharacterTransparent(char character, float alpha)
    {
        return $"<color=#000000{Mathf.RoundToInt(alpha * 255).ToString("X2")}>{character}</color>";
    }

    private IEnumerator FadeOutElement(GameObject element)
    {
        CanvasGroup canvasGroup = element.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = element.AddComponent<CanvasGroup>();
        }

        float elapsedTime = 0f;
        float fadeOutDuration = 1f; // 渐隐持续时间，可根据需要调整

        while (elapsedTime < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
            canvasGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 渐隐完成后销毁元素
        element.SetActive(false);
    }
}