using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Gameplay.Hittables
{
  public interface IHittableFactory
  {
    UniTask<GameObject> CreateCrate(Vector3 at, Transform parent);
  }
}