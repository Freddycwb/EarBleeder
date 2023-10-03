using UnityEngine;
using TMPro;

public class TextLocalize : MonoBehaviour
{
    private bool isInUI;

    private TextMeshProUGUI _TMProUIText;
    private TextMeshPro _TMProText;

    [SerializeField] private string _key;


    private void Awake()
    {
        if (GetComponent<TextMeshProUGUI>() != null)
        {
            _TMProUIText = GetComponent<TextMeshProUGUI>();
            isInUI = true;
        }
        else
        {
            _TMProText = GetComponent<TextMeshPro>();
        }
        Localization.updateL += OnEnable;
    }
    private void OnEnable()
    {
        if (isInUI)
        {
            _TMProUIText.text = Localization.Localize(_key);
        }
        else
        {
            _TMProText.text = Localization.Localize(_key);
        }
    }

    public void ChangeText()
    {
        if (isInUI)
        {
            _TMProUIText.text = Localization.Localize(_key);
        }
        else
        {
            _TMProText.text = Localization.Localize(_key);
        }
    }

    public void LocalizeText(string value)
    {
        if (isInUI)
        {
            _TMProUIText.text = Localization.Localize(value);
        }
        else
        {
            _TMProText.text = Localization.Localize(value);
        }
    }
}