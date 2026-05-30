using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyDisplay : MonoBehaviour
{
    public TMP_Text coinText;

    void Update()
    {
        coinText.text = $"{CurrencyManager.Coins}";
    }

}
