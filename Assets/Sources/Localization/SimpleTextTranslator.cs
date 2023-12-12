using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class SimpleTextTranslator : MonoBehaviour
{
    [SerializeField] private string _en;
    [SerializeField] private string _ru;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();

        UpdateText();
    }

    private void UpdateText()
    {
        Languages language = Language.Instance?.CurrentLanguage ?? Languages.En;
        _text.text = language == Languages.Ru ? _ru : _en;
    }
}
