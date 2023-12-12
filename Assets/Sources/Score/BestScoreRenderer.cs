using TMPro;
using UnityEngine;

public class BestScoreRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _renderer;

    private void Start()
    {
        EventBus.Instance.DataLoaded.AddListener(UpdateRenderer);
        EventBus.Instance.BestScoreChanged.AddListener(UpdateRenderer);

        UpdateRenderer();
    }

    private void UpdateRenderer()
    {
        int value = Repository.Instance?.GameData?.BestScore ?? 0;
        UpdateRenderer(value);
    }

    private void UpdateRenderer(int value)
    {
        _renderer.text = value.ToString();
    }
}
