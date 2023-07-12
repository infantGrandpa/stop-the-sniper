using UnityEngine;

namespace SniperProject
{
    public class DestroyObject : MonoBehaviour
    {
        public void DestroyThisObject()
        {
            Destroy(gameObject);
        }
    }
}
