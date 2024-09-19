using Code.Gameplay.Levels.Behaviours;
using UnityEngine;

namespace Code.Gameplay.Levels
{
  public interface ILevelSessionService
  {
    void SetUp(Transform startPoint, CrateSpawner crateSpawner);
    void Run();
    void Stop();
    void Cleanup();
  }
}