using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInteractionsIntro : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void HelloWorld()
    {
        text.text = "Hello World!";
    }
}
