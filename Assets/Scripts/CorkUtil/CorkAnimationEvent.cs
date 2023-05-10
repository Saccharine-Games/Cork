using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace CorkUtil
{
    public class CorkAnimationEvent : MonoBehaviour
    {
        public UnityEvent OnCorkAnimationEvent;

        public void CorkInvokeAnimationEvent()
        {
            OnCorkAnimationEvent.Invoke();
        }
    }
}
