using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Infrastructure.AssetManagement
{
  public class AssetProvider : IAssetProvider
  {
    public async UniTask<GameObject> LoadAsset(string path)
    {
      return await Addressables.LoadAssetAsync<GameObject>(path).ToUniTask();
    }
  }
}