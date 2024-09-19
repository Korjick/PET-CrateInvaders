using Code.Gameplay.Cameras;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class MenuInstaller : MonoInstaller, IInitializable
  {
    private ICameraProvider _cameraProvider;

    [Inject]
    private void Construct(ICameraProvider cameraProvider)
    {
      _cameraProvider = cameraProvider;
    }
    
    public override void InstallBindings()
    {
      Container.BindInterfacesTo<MenuInstaller>().FromInstance(this).AsSingle();
    }

    public void Initialize()
    {
      _cameraProvider.SetCamera(Camera.main);
    }
  }
}