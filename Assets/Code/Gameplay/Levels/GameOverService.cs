using Code.Gameplay.Cameras;
using Code.Gameplay.Hero;
using Code.Gameplay.Hero.Behaviours;
using Code.Infrastructure;
using Code.Infrastructure.Loading;

namespace Code.Gameplay.Levels
{
  public class GameOverService : IGameOverService
  {
    private readonly ISceneLoader _sceneLoader;
    private readonly ICameraProvider _cameraProvider;
    private readonly ILevelSessionService _levelSessionService;
    private readonly IHeroProvider _heroProvider;

    public GameOverService(
      ISceneLoader sceneLoader,
      ICameraProvider cameraProvider, 
      ILevelSessionService levelSessionService,
      IHeroProvider heroProvider)
    {
      _heroProvider = heroProvider;
      _levelSessionService = levelSessionService;
      _cameraProvider = cameraProvider;
      _sceneLoader = sceneLoader;
    }
    
    public void GameOver()
    {
      _heroProvider.Hero.GetComponent<HeroMove>().enabled = false;
      _heroProvider.Hero.GetComponent<HeroAnimator>().PlayDied();
      
      _levelSessionService.Stop();
      
      _cameraProvider.Vibrate(() =>
      {
        _sceneLoader.LoadScene(Scenes.Menu);
      });
    }
  }
}