using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.AssetManagement
{
  public interface IAssetDownloadService
  {
    UniTask InitializeDownloadDataAsync();
    float GetDownloadSizeMb();
    UniTask UpdateContentAsync();
  }
}