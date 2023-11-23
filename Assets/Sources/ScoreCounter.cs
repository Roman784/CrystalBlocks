using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreCounter : MonoBehaviour
{
    private int _score;
    [SerializeField] private TMP_Text _renderer;

    public static UnityEvent<int> BestScoreChanged = new UnityEvent<int>();

    private void Awake()
    {
        GameLoop.Defeated.AddListener(ChangeBestScore);

        UpdateRenderer();
    }

    public void Increase(int destroyedBlocksCount)
    {
        _score += destroyedBlocksCount * 10;
        UpdateRenderer();
    }

    public void ChangeBestScore()
    {
        int bestValue = Repository.Instance.GameData.BestScore;

        if (_score > bestValue)
            BestScoreChanged.Invoke(_score);
    }

    private void UpdateRenderer()
    {
        _renderer.text = _score.ToString();
    }
}
