using UnityEngine;

namespace Code.Gameplay.Input
{
  public class StandaloneInputService : IInputService
  {
    public float Horizontal => UnityEngine.Input.GetAxisRaw("Horizontal");
    public float Jump => UnityEngine.Input.GetAxisRaw("Jump");

    public bool IsAttackButtonUp => UnityEngine.Input.GetKeyUp(KeyCode.Space);
  }
}