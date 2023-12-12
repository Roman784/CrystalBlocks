using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreCounter : MonoBehaviour
{
    private int _score;
    [SerializeField] private TMP_Text _renderer;

    private void Awake()
    {
        UpdateRenderer();
    }

    private void Start()
    {
        EventBus.Instance.BlocksDestroyed.AddListener(Increase);
        EventBus.Instance.GameDefeated.AddListener(ChangeBestScore);
    }

    public void Increase(int destroyedBlocksCount)
    {
        _score += destroyedBlocksCount * 10;
        UpdateRenderer();
    }

    public void ChangeBestScore()
    {
        int bestValue = Repository.Instance?.GameData?.BestScore ?? 0;

        if (_score > bestValue)
        {
            EventBus.Instance.BestScoreChanged.Invoke(_score);

            Repository.Instance?.SetBestScore(_score);
        }
    }

    private void UpdateRenderer()
    {
        _renderer.text = _score.ToString();
    }
}
