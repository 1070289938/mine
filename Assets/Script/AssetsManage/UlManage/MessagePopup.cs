using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MessagePopup : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public GameObject messagePanel;


    public static MessagePopup Instance { get; private set; }

    // Start is called before the first frame update

    void Awake()
    {
        Instance = this;
    }



    public void ShowMessage(string message)
    {
        messageText.text = message;
        messagePanel.SetActive(true);
        StartCoroutine(HideAfterDelay(1f));
    }

    IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messagePanel.SetActive(false);
    }
}    