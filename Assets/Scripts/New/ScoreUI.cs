using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;

    private void Update()
    {
        score.text = GameManager.Instance.GetScore();
    }
}
