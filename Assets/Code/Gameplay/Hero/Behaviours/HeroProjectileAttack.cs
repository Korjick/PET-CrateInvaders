using Code.Gameplay.Input;
using Code.Gameplay.Projectiles.Behaviours;
using Code.Gameplay.Projectiles.Factories;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Hero.Behaviours
{
  public class HeroProjectileAttack : MonoBehaviour
  {
    public Transform StartPoint;
    public HeroAnimator Animator;
    public HeroMove Move;
    
    private IProjectileFactory _projectileFactory;
    private IInputService _input;

    [Inject]
    private void Construct(IInputService inputService, IProjectileFactory projectileFactory)
    {
      _projectileFactory = projectileFactory;
      _input = inputService;
    }

    
    private void Update()
    {
      if (_input.IsAttackButtonUp)
      {
        Animator.PlayAttack();
        LaunchProjectile().Forget();
      }
    }

    private async UniTaskVoid LaunchProjectile()
    {
      GameObject projectile = await _projectileFactory.CreateFireProjectile(transform.TransformPoint(StartPoint.localPosition));

      ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>();
      projectileMovement.speed *= Move.FacingDirection;
    }
  }
}