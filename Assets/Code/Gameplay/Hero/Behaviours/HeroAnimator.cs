using UnityEngine;

namespace Code.Gameplay.Hero.Behaviours
{
  public class HeroAnimator : MonoBehaviour
  {
    private readonly int _isMovingHash = Animator.StringToHash("isMoving");
    private readonly int _damageTakenHash = Animator.StringToHash("damageTaken");
    private readonly int _attackHash = Animator.StringToHash("attack");
    private readonly int _diedHash = Animator.StringToHash("died");

    public Animator Animator;

    public void PlayMove() => Animator.SetBool(_isMovingHash, true);
    public void PlayIdle() => Animator.SetBool(_isMovingHash, false);

    public void PlayAttack() => Animator.SetTrigger(_attackHash);

    public void PlayDied() => Animator.SetTrigger(_diedHash);

    public void Reset()
    {
      Animator.ResetTrigger(_damageTakenHash);
      Animator.ResetTrigger(_attackHash);
      Animator.ResetTrigger(_diedHash);
    }
  }
}