using Code.Common;
using Code.Extensions;
using Code.Gameplay.Hittables.Behaviours;
using UnityEngine;

namespace Code.Gameplay.Projectiles.Behaviours
{
  public class ProjectileDamage : MonoBehaviour
  {
    public GameObject DamageFxPrefab;
    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.IsOfLayer(CollisionLayer.Hittable))
      {
        var hittableObject = other.GetComponent<HittableObject>();
        if (hittableObject)
          hittableObject.Destruct();

        Instantiate(DamageFxPrefab, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
      }
    }
  }
}