using UnityEngine;

namespace Code.Gameplay.Projectiles.Behaviours
{
  public class ProjectileMovement : MonoBehaviour
  {
    public float speed = 10.0f;

    void Update()
    {
      transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
    }
  }
}