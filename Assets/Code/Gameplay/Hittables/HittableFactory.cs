using Code.Infrastructure.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Hittables
{
  public class HittableFactory : IHittableFactory
  {
    private readonly IInstantiator _instantiator;
    private readonly IAssetProvider _assetProvider;

    public HittableFactory(IInstantiator instantiator, IAssetProvider assetProvider)
    {
      _instantiator = instantiator;
      _assetProvider = assetProvider;
    }
    
    public async UniTask<GameObject> CreateCrate(Vector3 at, Transform parent)
    {
      GameObject prefab =  await _assetProvider.LoadAsset(AssetPath.Crate);
      return _instantiator.InstantiatePrefab(prefab, at, Quaternion.identity, parent);
    }
  }
}