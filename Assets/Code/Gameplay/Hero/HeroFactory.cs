using Code.Infrastructure.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Hero
{
  public class HeroFactory : IHeroFactory
  {
    private readonly IInstantiator _instantiator;
    private readonly IAssetProvider _assetProvider;

    public HeroFactory(IInstantiator instantiator, IAssetProvider assetProvider)
    {
      _assetProvider = assetProvider;
      _instantiator = instantiator;
    }
    
    public async UniTask<GameObject> CreateHero(Vector3 at, Transform parent)
    {
      GameObject prefab = await _assetProvider.LoadAsset(AssetPath.Hero);
      return _instantiator.InstantiatePrefab(prefab, at, Quaternion.identity, parent.transform);
    }
  }
}