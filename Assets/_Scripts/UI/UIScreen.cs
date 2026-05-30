using UnityEngine;
using DG.Tweening;

public enum UIAnimationType
{
    None,
    Fade,
    Scale,
    SlideFromTop,
    SlideFromBottom
}

[RequireComponent(typeof(CanvasGroup))]
public class UIScreen : MonoBehaviour
{
    [Header("Animation Settings")]
    public UIAnimationType showAnimation = UIAnimationType.Fade;
    public UIAnimationType hideAnimation = UIAnimationType.Fade;
    public float animationDuration = 0.5f;

    protected CanvasGroup canvasGroup;
    protected RectTransform rectTransform;

    private Tween currentTween;
    public virtual void Init()
    {

        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        AnimateIn();
        OnShow();
    }

    public virtual void Hide()
    {
        AnimateOut(() =>
        {
            gameObject.SetActive(false);
            OnHide();
        });
    }

   

    private void AnimateIn()
    {
        KillCurrentTween();

        Sequence sequence = DOTween.Sequence();

        switch (showAnimation)
        {
            case UIAnimationType.Fade:
                canvasGroup.alpha = 0;
                sequence.Append(canvasGroup.DOFade(1, animationDuration).SetUpdate(true));
                break;

            case UIAnimationType.Scale:
                rectTransform.localScale = Vector3.zero;
                sequence.Append(rectTransform.DOScale(1f, animationDuration).SetEase(Ease.OutBack).SetUpdate(true));
                break;

            case UIAnimationType.SlideFromTop:
                rectTransform.anchoredPosition = new Vector2(0, Screen.height * 2);
                sequence.Append(rectTransform.DOAnchorPos(Vector2.zero, animationDuration).SetEase(Ease.OutCubic).SetUpdate(true));
                break;

            case UIAnimationType.SlideFromBottom:
                canvasGroup.alpha = 0;
                rectTransform.anchoredPosition = new Vector2(0, -Screen.height * 2);
                sequence.Append(rectTransform.DOAnchorPos(Vector2.zero, animationDuration).SetEase(Ease.OutCubic).SetUpdate(true));
                sequence.Join(canvasGroup.DOFade(1, animationDuration).SetUpdate(true));
                break;

            default:
                canvasGroup.alpha = 1;
                break;
        }

        sequence.SetUpdate(true);
        currentTween = sequence;
        sequence.Play();
    }

    private void AnimateOut(System.Action onComplete)
    {
        KillCurrentTween();

        Sequence sequence = DOTween.Sequence();

        switch (hideAnimation)
        {
            case UIAnimationType.Fade:
                sequence.Append(canvasGroup.DOFade(0, animationDuration).SetUpdate(true));
                break;

            case UIAnimationType.Scale:
                sequence.Append(rectTransform.DOScale(0f, animationDuration).SetEase(Ease.InBack).SetUpdate(true));
                break;

            case UIAnimationType.SlideFromTop:
                sequence.Append(rectTransform.DOAnchorPos(new Vector2(0, Screen.height * 2), animationDuration).SetEase(Ease.InCubic).SetUpdate(true));
                break;

            case UIAnimationType.SlideFromBottom:
                sequence.Append(rectTransform.DOAnchorPos(new Vector2(0, -Screen.height * 2), animationDuration).SetEase(Ease.InCubic).SetUpdate(true));
                break;

            default:
                break;
        }
        sequence.SetUpdate(true);

        sequence.OnComplete(() => onComplete?.Invoke());
        currentTween = sequence;
    }

    private void KillCurrentTween()
    {
        if (currentTween != null && currentTween.IsActive())
            currentTween.Kill();
    }

    /// <summary>
    /// Override to respond when screen appears.
    /// </summary>
    protected virtual void OnShow() { }

    /// <summary>
    /// Override to respond when screen disappears.
    /// </summary>
    protected virtual void OnHide() { }

    /// <summary>
    /// Override this to allow passing custom data to screens.
    /// </summary>
    public virtual void SetData(object data) { }
}
