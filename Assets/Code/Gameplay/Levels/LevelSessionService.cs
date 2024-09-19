using Code.Gameplay.Hero;
using Code.Gameplay.Levels.Behaviours;
using UnityEngine;

namespace Code.Gameplay.Levels
{
  public class LevelSessionService : ILevelSessionService
  {
    private readonly IHeroFactory _heroFactory;
    private Transform _startPoint;
    private CrateSpawner _crateSpawner;
    private readonly IGameOverService _gameOverService;
    private readonly IHeroProvider _heroProvider;

    public LevelSessionService(IHeroProvider heroProvider)
    {
      _heroProvider = heroProvider;
    }

    public void SetUp(Transform startPoint, CrateSpawner crateSpawner)
    {
      _crateSpawner = crateSpawner;
      _startPoint = startPoint;
    }

    public void Run()
    {
      _heroProvider.CreateHero(_startPoint.position).Forget();
      _crateSpawner.StartSpawning();
    }

    public void Stop()
    {
      _crateSpawner.StopSpawning();
    }

    public void Cleanup()
    {
      _startPoint = null;
      _crateSpawner = null;
    }
  }
}