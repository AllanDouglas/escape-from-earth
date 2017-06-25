using UnityEngine;
using UnityEngine.UI;

namespace Behaviour
{
    public class HitPointsBarBehaviourScript : MonoBehaviour
    {

        #region Inspector
        [SerializeField]
        private Slider _sliderBar;
        #endregion

        // Use this for initialization
        void Start()
        {

        }

        /// <summary>
        /// Set the max value to the bar
        /// </summary>
        /// <param name="max"></param>
        internal void SetMaxHitPoints(int max)
        {
            this._sliderBar.maxValue = max;
            this._sliderBar.value = max;
        }
        

        /// <summary>
        /// Increment the amount hits to que bar
        /// </summary>
        /// <param name="hit"></param>
        internal void Increment(int hit)
        {
            this._sliderBar.value += hit;
        }

    }
}