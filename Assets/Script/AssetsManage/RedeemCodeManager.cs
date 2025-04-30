
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.Security.Cryptography;
public class RedeemCodeManager : MonoBehaviour
{
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(ShowExchange);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ShowExchange(){
        TipsManager.Instance.ShowExchange();
    }


}
