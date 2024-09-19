using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Gameplay.Hero
{
  public interface IHeroProvider
  {
    GameObject Hero { get; }
    UniTaskVoid CreateHero(Vector3 at);
    void Cleanup();
    void SetHeroParent(Transform parent);
  }
}