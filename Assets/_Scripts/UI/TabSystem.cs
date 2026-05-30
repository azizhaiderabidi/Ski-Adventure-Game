using UnityEngine;
using UnityEngine.UI;

public class TabSystem : MonoBehaviour
{
    [Header("Tab Buttons")]
    public Button[] tabButtons;

    [Header("Tab Panels")]
    public GameObject[] tabPanels;

    [Header("Color Option (Optional)")]
    public bool useColorSwap = true;
    public Color selectedColor = Color.white;
    public Color normalColor = Color.gray;

    [Header("Image Option (Optional)")]
    public bool useImageSwap = false;
    public Sprite selectedSprite;
    public Sprite normalSprite;

    private int currentTabIndex = 0;

    void Awake()
    {
        InitializeTabs();
    }

    void Start()
    {
        AssignButtonListeners();
        OnTabSelected(currentTabIndex);
    }

    private void AssignButtonListeners()
    {
        for (int i = 0; i < tabButtons.Length; i++)
        {
            int index = i; // Capture loop index
            tabButtons[i].onClick.AddListener(() => OnTabSelected(index));
        }
    }

    public void OnTabSelected(int index)
    {
        currentTabIndex = index;
        Debug.Log("tab selected"+ currentTabIndex);
        for (int i = 0; i < tabPanels.Length; i++)
        {
            bool isActive = (i == index);
            tabPanels[i].SetActive(isActive);

            if (tabButtons != null && tabButtons.Length > i)
            {
                Image btnImage = tabButtons[i].GetComponent<Image>();

                if (useColorSwap && btnImage != null)
                    btnImage.color = isActive ? selectedColor : normalColor;

                if (useImageSwap && btnImage != null)
                    btnImage.sprite = isActive ? selectedSprite : normalSprite;
            }
        }
    }

    private void InitializeTabs()
    {
        for (int i = 0; i < tabPanels.Length; i++)
        {
            tabPanels[i].SetActive(false);
        }
    }
}
