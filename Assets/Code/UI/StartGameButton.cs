using Code.Gameplay.Cameras;
using Code.Infrastructure.Loading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI
{
  public class StartGameButton : MonoBehaviour
  {
    private ICameraProvider _cameraProvider;
    private ISceneLoader _sceneLoader;
    
    public Button ButtonStartInvasion;
    public string Scene;

    [Inject]
    private void Construct(ICameraProvider cameraProvider, ISceneLoader sceneLoader)
    {
      _sceneLoader = sceneLoader;
      _cameraProvider = cameraProvider;
    }

    private void Awake()
    {
      ButtonStartInvasion.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
      _cameraProvider.Vibrate(onFinish: () =>
      {
        _sceneLoader.LoadScene(Scene);
      });
    }

    private void OnDestroy()
    {
      ButtonStartInvasion.onClick.RemoveListener(StartGame);
    }
  }
}