using Code.Gameplay.Score;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI
{
  public class Hud : MonoBehaviour
  {
    public TextMeshProUGUI CratesCount;

    private IScoreService _score;

    [Inject]
    private void Construct(IScoreService scoreService)
    {
      _score = scoreService;
    }

    private void Start()
    {
      _score.ScoreUpdated += UpdateCratesCount;
      UpdateCratesCount();
    }

    private void OnDestroy()
    {
      _score.ScoreUpdated -= UpdateCratesCount;
    }

    private void UpdateCratesCount() => 
      CratesCount.text = _score.Score.ToString();
  }
}