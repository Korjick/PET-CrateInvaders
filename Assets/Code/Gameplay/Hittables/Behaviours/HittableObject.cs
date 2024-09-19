using System;
using Code.Gameplay.Cameras;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Hittables.Behaviours
{
  public class HittableObject : MonoBehaviour
  {
    public SpriteRenderer SpriteRenderer;
    public Collider2D HitBox;
    private ICameraProvider _cameraProvider;

    public event Action Destroyed;

    [Inject]
    private void Construct(ICameraProvider cameraProvider)
    {
      _cameraProvider = cameraProvider;
    }
    
    public void Destruct()
    {
      _cameraProvider.Punch();
      
      HitBox.enabled = false;
      
      Destroyed?.Invoke();
      
      SpriteRenderer.DOColor(Color.black, 0.3f)
        .OnComplete(() => SpriteRenderer.DOColor(Color.clear, 0.3f)
          .OnComplete(() => Destroy(gameObject)));
    }
  }
}