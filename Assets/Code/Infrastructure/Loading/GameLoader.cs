using Code.Infrastructure.AssetManagement;
using Code.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Loading
{
  public class GameLoader : MonoBehaviour
  {
    private ISceneLoader _sceneLoader;
    private LoadingCurtain _loadingCurtain;
    private IAssetDownloadService _downloadService;

    [Inject]
    private void Construct(ISceneLoader sceneLoader, IAssetDownloadService downloadService, LoadingCurtain loadingCurtain)
    {
      _downloadService = downloadService;
      _loadingCurtain = loadingCurtain;
      _sceneLoader = sceneLoader;
    }
    
    private async void Start()
    {
      await Initialize();
    }

    private async UniTask Initialize()
    {
      _loadingCurtain.Show();

      await _downloadService.InitializeDownloadDataAsync();
      float downloadSize =  _downloadService.GetDownloadSizeMb();
      
      Debug.Log($"DOWNLOAD SIZE IS {downloadSize} Mb");

      if (downloadSize > 0)
        await _downloadService.UpdateContentAsync();
      
      _sceneLoader.LoadScene(Scenes.Menu, () => _loadingCurtain.Hide());
    }
  }
}