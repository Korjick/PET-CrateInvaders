using System.Collections.Generic;
using Code.Extensions;
using Code.Gameplay.Hittables;
using Code.Infrastructure.Time;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Levels.Behaviours
{
  public class CrateSpawner : MonoBehaviour
  {
    private const float SpawnTimeout = 1f;

    public List<Transform> SpawnPoints;
    
    private IHittableFactory _hittableFactory;
    private ITimeService _time;

    private float _spawnTimer;
    
    private bool _isActive;

    [Inject]
    public void Construct(IHittableFactory hittableFactory, ITimeService timeService)
    {
      _hittableFactory = hittableFactory;
      _time = timeService;
    }

    private void Update()
    {
      if(!_isActive)
        return;

      if (_spawnTimer > 0)
        _spawnTimer -= _time.DeltaTime;
      else
      {
        SpawnCrateAtRandomPoint();
        _spawnTimer = SpawnTimeout;
      }
    }

    public void StartSpawning()
    {
      _isActive = true;
    }

    public void StopSpawning()
    {
      _isActive = false;
    }

    private void SpawnCrateAtRandomPoint()
    {
      Transform selectedPoint = SpawnPoints.PickRandom();
      _hittableFactory.CreateCrate(selectedPoint.position, transform).Forget();
    }
  }
}