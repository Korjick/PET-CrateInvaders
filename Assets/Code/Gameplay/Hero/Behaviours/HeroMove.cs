using Code.Extensions;
using Code.Gameplay.Input;
using Code.Infrastructure.Time;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Hero.Behaviours
{
  public class HeroMove : MonoBehaviour
  {
    public HeroAnimator Animator;
    public Rigidbody2D Rigidbody;
    public float Speed = 3;

    private IInputService _input;
    private ITimeService _time;

    public int FacingDirection => (int)Mathf.Sign(transform.localScale.x);

    [Inject]
    private void Construct(IInputService inputService, ITimeService timeService)
    {
      _input = inputService;
      _time = timeService;
    }

    private void Update()
    {
      float inputHorizontal = _input.Horizontal;
      
      if (Mathf.Abs(inputHorizontal) <= float.Epsilon)
        Animator.PlayIdle();
      else
      {
        Animator.PlayMove();
        Rigidbody.position = Rigidbody.position.AddX(inputHorizontal * Speed * _time.DeltaTime);

        UpdateDirection(DirectionOfInput(inputHorizontal));
      }
    }
    
    private void UpdateDirection(float direction)
    {
      float currentXScale = Mathf.Abs(transform.localScale.x);
      transform.LocalScaleX(currentXScale * direction);
    }
    
    private static float DirectionOfInput(float xDistance) => Mathf.Sign(xDistance);
  }
}