using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;
using TMPro;

public class TextRevealMultiLine : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float revealSpeed = 0.1f;
    private string[] lines;
    private bool isRevealing = false;

    private void Start()
    {
        // 获取完整文本并按行分割
        string fullText = textComponent.text;
        lines = Regex.Split(fullText, "\r\n|\r|\n");
        textComponent.text = "";
    }

    public void StartReveal()
    {
        if (!isRevealing)
        {
            isRevealing = true;
            StartCoroutine(RevealTextLines());
        }
    }

    private IEnumerator RevealTextLines()
    {
        for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
        {
            string currentLine = "";
            for (int charIndex = 0; charIndex < lines[lineIndex].Length; charIndex++)
            {
                currentLine += lines[lineIndex][charIndex];
                string newText = BuildFullText(lineIndex, currentLine);
                textComponent.text = newText;

                // 逐个字符设置透明度
                for (int j = 0; j <= charIndex; j++)
                {
                    int startIndex = GetStartIndexOfLine(lineIndex) + j;
                    textComponent.text = textComponent.text.Remove(startIndex, 1).Insert(startIndex, MakeCharacterTransparent(lines[lineIndex][j], 1f));
                }

                yield return new WaitForSeconds(revealSpeed);
            }
        }
        isRevealing = false;
    }

    private string BuildFullText(int currentLineIndex, string currentLine)
    {
        string result = "";
        for (int i = 0; i < currentLineIndex; i++)
        {
            result += lines[i] + "\n";
        }
        result += currentLine;
        return result;
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
        return $"<color=#FFFFFF{Mathf.RoundToInt(alpha * 255).ToString("X2")}>{character}</color>";
    }
}