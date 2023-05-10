using System;
using CorkUtil;
using UnityEngine;
using UnityEngine.Serialization;

namespace CorkCore
{
    public class Interact : MonoBehaviour
    {
        [SerializeField] private float maxRaycastDistance = 5.0f; 
        // Start is called before the first frame update

        private void Awake()
        {
            InputManager.AddEvent(KeyCode.E, () =>
            {
                if (!Physics.Raycast(transform.position, transform.forward,out var @hitInfo))
                    return;
            });
        }

        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
