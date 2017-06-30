using UnityEngine;

namespace Behaviour
{
    
    [RequireComponent(typeof(LineRenderer))]

    public class MovimentLineBehaviourScript : MonoBehaviour
    {

        // components
        private LineRenderer _lineRenderer;

        // Use this for initialization
        void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }        

        /// <summary>
        /// Hides the line
        /// </summary>
        internal void Hide()
        {            
            this._lineRenderer.enabled = false;
        }

        /// <summary>
        /// Set the path
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        internal void SetDirection(Vector2 to , Vector2 from)
        {
            this._lineRenderer.enabled = true;

            _lineRenderer.positionCount = 2;

            _lineRenderer.SetPosition(0, to);
            _lineRenderer.SetPosition(1, from);
        }
        
    }
}