using Sirenix.Utilities;
using UnityEngine;

namespace CorkUtil
{
    public class EnableComponents : MonoBehaviour
    {
        public void EnableAllComponents()
        {
            gameObject.GetComponents<MonoBehaviour>().ForEach(x => x.enabled = true);
        }
    }
}