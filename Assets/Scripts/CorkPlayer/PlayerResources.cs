using System;
using System.Collections;
using DistantLands.Cozy;
using UnityEngine;

namespace CorkPlayer
{
    public class PlayerResources : MonoBehaviour
    {
        [SerializeField] private float maxEnergy;
        [SerializeField] private CozyWeather _cozyWeather;
        [SerializeField] private Animator _passOutAnimator;
        private float _currentEnergy = 0;

        private void Awake()
        {
            _currentEnergy = maxEnergy;
        }

        // Start is called before the first frame update
        void Start()
        {
            IEnumerator CheckPassOut()
            {
                while (true)
                {
                    yield return new WaitForEndOfFrame();
                    if (_cozyWeather.currentTime.Equals(_cozyWeather.calendar.passOutTime)
                        || _currentEnergy <= 0)
                        BeginPassOut();
                }
            }

            StartCoroutine(CheckPassOut());
        }

        void BeginPassOut()
        {
            Debug.Log("passout");
            _passOutAnimator.SetTrigger("Play");
        }

        public void PassOut()
        {
            _cozyWeather.currentTicks = 120;
            
            //Resets the pass out animation back to before we passed out
            _passOutAnimator.SetTrigger("Back");
        }
    }
}