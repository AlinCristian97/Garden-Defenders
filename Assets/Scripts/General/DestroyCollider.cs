using UnityEngine;

namespace General
{
    public class DestroyCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(other.gameObject);
        }
    }
}
