using Code.Common;
using Code.Extensions;
using Code.Gameplay.Levels;
using Code.Gameplay.Score;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Hittables.Behaviours
{
  public class Crate : MonoBehaviour
  {
    public HittableObject Hittable;
    public Rigidbody2D Rigidbody;
    public float ActiveVelocityThreshold = 0.25f;

    private IScoreService _score;
    private IGameOverService _gameOverService;

    [Inject]
    private void Construct(IScoreService scoreService, IGameOverService gameOverService)
    {
      _gameOverService = gameOverService;
      _score = scoreService;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.IsOfLayer(CollisionLayer.Hero) && IsFalling())
        _gameOverService.GameOver();
    }

    private bool IsFalling() => 
      Mathf.Abs(Rigidbody.velocity.y) >= ActiveVelocityThreshold;

    private void Start()
    {
      Hittable.Destroyed += UpdateCount;
    }

    private void OnDestroy()
    {
      if (Hittable != null)
        Hittable.Destroyed -= UpdateCount;
    }

    private void UpdateCount() =>
      _score.IncrementCount();
  }
}