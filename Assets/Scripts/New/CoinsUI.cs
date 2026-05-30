using TMPro;
using DG.Tweening;
using UnityEngine;
using System;

public class CoinsUI : GameEventListener<GameEvents.ScoreAdded>
{
    public TextMeshProUGUI scoreText;
    private int currentScore = 0;

    protected override void OnEnable()
    {
        Response = UpdateCoinUI;

        EventManager.Register<GameEvents.RestartLevel>(OnRestart);

        base.OnEnable();         
    }

    private void OnRestart(GameEvents.RestartLevel level)
    {
        currentScore = 0;
       // Debug.Log("Score :" + GameManager.Instance.score);
        scoreText.text = GameManager.Instance.score.ToString();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    private void UpdateCoinUI(GameEvents.ScoreAdded scoreEvent)
    {
        //Debug.LogError($"[CoinsUI] Event Received: +{scoreEvent.Amount}");

        int startScore = currentScore;
        currentScore += scoreEvent.Amount;

        Sequence seq = DOTween.Sequence();
        seq.Append(scoreText.DOColor(Color.yellow, 0.2f));
        seq.Join(scoreText.transform.DOScale(1.2f, 0.2f));
        seq.Append(DOTween.To(() => startScore, x => scoreText.text = x.ToString(), currentScore, 0.4f));
        seq.Append(scoreText.DOColor(Color.white, 0.2f));
        seq.Join(scoreText.transform.DOScale(1f, 0.2f));
    }
}
