using System;
using DG.Tweening;
using UnityEngine;

namespace Code.Gameplay.Cameras
{
  public class CameraProvider : ICameraProvider
  {
    private Camera _camera;

    public void SetCamera(Camera camera)
    {
      _camera = camera;
    }

    public void Punch()
    {
      if (DOTween.IsTweening(_camera.transform))
        return;

      _camera.transform.DOPunchPosition(
        punch: new Vector3(0.1f, 0.1f, 0),
        duration: 0.5f,
        vibrato: 10,
        elasticity: 0.25f);
    }
    
    public void Vibrate(Action onFinish = null)
    {
      _camera.transform.DOShakePosition(
          duration: 1f,
          strength: new Vector3(0.1f, 0.1f, 0),
          vibrato: 50,
          randomness: 90,
          snapping: false,
          fadeOut: false)
        .OnComplete(() => onFinish?.Invoke());
    }
  }
}