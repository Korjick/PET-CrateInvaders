using Code.Infrastructure.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Projectiles.Factories
{
  public class ProjectileFactory : IProjectileFactory
  {
    private readonly IInstantiator _instantiator;
    private readonly IAssetProvider _assetProvider;

    public ProjectileFactory(IInstantiator instantiator, IAssetProvider assetProvider)
    {
      _assetProvider = assetProvider;
      _instantiator = instantiator;
    }
    
    public async UniTask<GameObject> CreateFireProjectile(Vector3 at)
    {
      GameObject prefab = await _assetProvider.LoadAsset(AssetPath.FireProjectile);
      
      return _instantiator.InstantiatePrefab(prefab, at, Quaternion.identity, null);
    }
  }
}