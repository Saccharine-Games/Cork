using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CorkUtil
{
    public class InputManager : MonoBehaviour
    {
        private static Dictionary<KeyCode, List<Action>> _events;

        private Array _keycodes = null;

        public static void AddEvent(KeyCode key, Action func)
        {
            if (_events.TryGetValue(key, out var @event))
                @event.Add(func);
            else
                _events.Add(key, new List<Action> { func });
        }

        public static void RemoveEvent(KeyCode key,Action func)
        {
            if (_events.TryGetValue(key, out var @event))
                @event.Remove(func);
            else
                Debug.LogError($"Could not remove function from {key}, func not registered");
        }

        public static void RemoveAllEvents(KeyCode key)
        {
            if (!_events.ContainsKey(key))
                Debug.LogWarning($"{key} Does not have any registered events to remove");
        }

        private void Start()
        {
            _keycodes = System.Enum.GetValues(typeof(KeyCode));
        }

        // Update is called once per frame
        void Update()
        {
            foreach (var key in _keycodes)
            {
                if(Input.GetKeyDown((KeyCode)key))
                    _events[(KeyCode)key].ForEach(x => x());
            }
        }
    }
}