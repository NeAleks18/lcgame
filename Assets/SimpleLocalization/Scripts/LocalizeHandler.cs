using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using TMPro;

public enum Type
{
    LanguageSelect,
    Test
}

public class LocalizeHandler : MonoBehaviour
{
    public Type type;

    private void Awake()
    {
        if (type == Type.LanguageSelect)
        {
            GetComponent<TMP_Dropdown>().captionText.text = LocalizationManager.Language;
        }
    }

    public void SelectLanguage(TextMeshProUGUI label)
    {
        LocalizationManager.Language = label.text.ToString();
    }
}
