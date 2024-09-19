using Code.Gameplay.Cameras;
using Code.Gameplay.Hero;
using Code.Gameplay.Hittables;
using Code.Gameplay.Input;
using Code.Gameplay.Levels;
using Code.Gameplay.Projectiles.Factories;
using Code.Gameplay.Score;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Loading;
using Code.Infrastructure.Time;
using Code.UI;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable
  {
    public LoadingCurtain CurtainPrefab;
    
    public override void InstallBindings()
    {
      BindInfrastructureServices();
      BindFactories();
      BindGameplayServices();
      BindCameraService();
      BindLoadingCurtain();
    }

    private void BindLoadingCurtain()
    {
      Container.BindInterfacesAndSelfTo<LoadingCurtain>().FromComponentInNewPrefab(CurtainPrefab).AsSingle();
    }

    private void BindCameraService()
    {
      Container.Bind<ICameraProvider>().To<CameraProvider>().AsSingle();
    }

    private void BindInfrastructureServices()
    {
      Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
      Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
      Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle(); 
      Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle(); 
      Container.Bind<IAssetDownloadReporter>().To<AssetDownloadReporter>().AsSingle(); 
      Container.Bind<IAssetDownloadService>().To<LabeledAssetDownloadService>().AsSingle(); 
    }

    private void BindGameplayServices()
    {
      Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
      Container.Bind<IScoreService>().To<ScoreService>().AsSingle();
      Container.Bind<ILevelSessionService>().To<LevelSessionService>().AsSingle();
      Container.Bind<IGameOverService>().To<GameOverService>().AsSingle();
      Container.Bind<IHeroProvider>().To<HeroProvider>().AsSingle();
    }

    private void BindFactories()
    {
      Container.Bind<IProjectileFactory>().To<ProjectileFactory>().AsSingle();
      Container.Bind<IHittableFactory>().To<HittableFactory>().AsSingle();
      Container.Bind<IHeroFactory>().To<HeroFactory>().AsSingle();
    }

    public void Initialize()
    {
      LoadMenu();
    }

    private void LoadMenu()
    {
      Container.Resolve<ISceneLoader>().LoadScene(Scenes.Loading);
    }
  }
}