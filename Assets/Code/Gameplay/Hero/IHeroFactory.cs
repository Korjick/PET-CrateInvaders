using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Gameplay.Hero
{
  public interface IHeroFactory
  {
    UniTask<GameObject> CreateHero(Vector3 at, Transform parent);
  }
}