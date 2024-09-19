using System;

namespace Code.Gameplay.Score
{
  public class ScoreService : IScoreService
  {
    public event Action ScoreUpdated;
    public int Score { get; set; }

    public void IncrementCount()
    {
      Score++;
      ScoreUpdated?.Invoke();
    }
  }
}