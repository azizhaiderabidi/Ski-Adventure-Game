using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ComboFeedBack : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI trickName;
    [SerializeField] TextMeshProUGUI combo;
    [SerializeField] TextMeshProUGUI comboCount;
    [SerializeField] Image progress;


    Timer comboTimeFrame;

    int ComboCount;
    string comboName;



    private void OnEnable()
    {
        EventManager.Register<GameEvents.FlipPerformed>(OnFlipPerformed);

        trickName.transform.localScale = Vector3.zero;
        combo.transform.localScale = Vector3.zero;
        comboCount.transform.localScale = Vector3.zero;

    }
    private void OnDisable()
    {
        EventManager.Unregister<GameEvents.FlipPerformed>(OnFlipPerformed);
    }

    private void Update()
    {
        progress.gameObject.SetActive(comboTimeFrame != null);

        if (comboTimeFrame != null)
            progress.fillAmount = 1 - (comboTimeFrame.Time / comboTimeFrame.StopAt);
    }


    private void OnFlipPerformed(GameEvents.FlipPerformed flip)
    {
        string name = flip.isBackFlip ? "FrontFlip" : "BackFlip";

        AddCombo(name);
    }

    public void AddCombo(string comboName)
    {
        ComboCount++;

        trickName.text = "+" + comboName;
        comboCount.text = ComboCount.ToString("0x");


        AnimateCombo(comboName);


        this.comboName = comboName;

        if (comboTimeFrame == null)
            comboTimeFrame = new Timer(5, OnComboMissed);
        else
            comboTimeFrame.Time = 0;
    }

    public void AnimateCombo(string comboName)
    {
        Sequence sequence = DOTween.Sequence();

        if (this.comboName != comboName)
        {
            trickName.transform.localScale = Vector3.zero;

            sequence.Append(trickName.transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.OutBounce))
                    .Join(trickName.transform.DOPunchRotation(new Vector3(0,0,90), 1 , 5 , .25f));
        }

        if (comboTimeFrame == null)
        {
            combo.transform.localScale = Vector3.zero;

            sequence.Append(combo.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBounce))
                    .Join(combo.transform.DOPunchRotation(new Vector3(0, 0, 90), 1, 5, .25f));
        }

        comboCount.transform.localScale = Vector3.zero;

        sequence.Append(comboCount.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBounce))
                .Join(comboCount.transform.DOPunchRotation(new Vector3(0, 0, 90), 1, 5, .25f));

        sequence.Play();
    }

    private void OnComboMissed()
    {
        comboTimeFrame.Kill();
        comboTimeFrame = null;

        trickName.transform.localScale = Vector3.zero;
        combo.transform.localScale = Vector3.zero;
        comboCount.transform.localScale = Vector3.zero;

        for (int i = 0; i < ComboCount; i++)
        {
            CoinEffect.instance.Emit(transform.position);
            CurrencyManager.Add(10);
            EventManager.Raise(new GameEvents.ScoreAdded(10));

        }

        comboName = "";
        ComboCount = 0;
    }
}
