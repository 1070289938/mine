using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class VerifyMsgManager : MonoBehaviour
{


    public TextMeshProUGUI detailsText;

    public Button off;

    public Button verify;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Install(string msg, Action action)
    {
        detailsText.text = msg;


        verify.onClick.RemoveAllListeners();
        verify.onClick.AddListener(() =>
        {

            action.Invoke();


            off.onClick.Invoke();

        });



    }





}
