namespace Code.Gameplay.Input
{
  public interface IInputService
  {
    float Horizontal { get; }
    float Jump { get; }
    bool IsAttackButtonUp { get; }
  }
}