using System;
using UnityEngine;

namespace Code.Gameplay.Cameras
{
  public interface ICameraProvider
  {
    void SetCamera(Camera camera);
    void Punch();
    void Vibrate(Action onFinish = null);
  }
}