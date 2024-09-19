using System;
using Code.Gameplay.Cameras;
using Code.Gameplay.Hero;
using Code.Gameplay.Levels;
using Code.Gameplay.Levels.Behaviours;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class LevelInstaller : MonoInstaller, IInitializable, IDisposable
  {
    public CrateSpawner CrateSpawner;
    public Transform StartPoint;
    public Transform HeroParent;

    private ILevelSessionService _levelSessionService;
    private ICameraProvider _cameraProvider;
    private IHeroProvider _heroProvider;

    [Inject]
    private void Construct(ILevelSessionService levelSessionService, ICameraProvider cameraProvider, IHeroProvider heroProvider)
    {
      _heroProvider = heroProvider;
      _cameraProvider = cameraProvider;
      _levelSessionService = levelSessionService;
    }
    
    public override void InstallBindings()
    {
      Container.BindInterfacesTo<LevelInstaller>().FromInstance(this).AsSingle();
    }

    public void Initialize()
    {
      _heroProvider.SetHeroParent(HeroParent);
      _cameraProvider.SetCamera(Camera.main);
      _levelSessionService.SetUp(StartPoint, CrateSpawner);
      _levelSessionService.Run();
    }

    public void Dispose()
    {
      _levelSessionService.Cleanup();
    }
  }
}