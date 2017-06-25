using Interfaces;
using UnityEngine;

namespace Behaviour
{
    public class InputControllerBehaviourScript : MonoBehaviour
    {

        private ProtectorBehaviourScript objectSelected = null;

        // Use this for initialization
        void Start()
        {
            SelectableObjectBehaviourScript.OnClick += MouseClickObjectHandler;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0) & objectSelected != null)
            {
                Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                MoveObject(position);
            }
        }

        private void MoveObject(Vector2 position)
        {
            this.objectSelected.MoveTo(position);
        }

        private void MouseClickObjectHandler(ISelectable ship)
        {
            if(this.objectSelected != null)
            {
                this.objectSelected.Deselect();
            }

            ship.Select();

            this.objectSelected = ship as ProtectorBehaviourScript;
        }
    }
}