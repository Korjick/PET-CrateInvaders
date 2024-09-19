using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Gameplay.Hero
{
  public class HeroProvider : IHeroProvider
  {
    private GameObject _hero;
    private Transform _parent;
    
    private readonly IHeroFactory _heroFactory;

    public GameObject Hero => _hero;
    
    public HeroProvider(IHeroFactory heroFactory)
    {
      _heroFactory = heroFactory;
    }

    public void SetHeroParent(Transform parent)
    {
      _parent = parent;
    }

    public async UniTaskVoid CreateHero(Vector3 at)
    {
      _hero = await _heroFactory.CreateHero(at, _parent);
    }

    public void Cleanup()
    {
      _hero = null;
    }
  }
}