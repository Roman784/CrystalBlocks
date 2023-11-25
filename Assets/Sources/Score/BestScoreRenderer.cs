using TMPro;
using UnityEngine;

public class BestScoreRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _renderer;

    private void Awake()
    {
        Repository.DataLoaded.AddListener(UpdateRenderer);
    }

    private void UpdateRenderer()
    {
        int value = Repository.Instance.GameData.BestScore;
        _renderer.text = value.ToString();
    }
}
