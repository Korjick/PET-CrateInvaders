using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Gameplay.Projectiles.Factories
{
  public interface IProjectileFactory
  {
    UniTask<GameObject> CreateFireProjectile(Vector3 at);
  }
}