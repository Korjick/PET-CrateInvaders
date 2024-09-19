using System;

namespace Code.Gameplay.Score
{
  public interface IScoreService
  {
    event Action ScoreUpdated;
    int Score { get; }
    void IncrementCount();
  }
}