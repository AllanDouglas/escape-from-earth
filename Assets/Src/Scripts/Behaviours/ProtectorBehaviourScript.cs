using UnityEngine;
using Interfaces;

namespace Behaviour
{

    [RequireComponent(typeof(MovimentLineBehaviourScript))]

    public class ProtectorBehaviourScript : Ship, ISelectable
    {


        #region Properties

        private float _shootCadenceController = 1f;

        #endregion

        #region Components
        private MovimentLineBehaviourScript _movimentLine;
        #endregion

        #region Unity Methods
        // Use this for initialization
        protected override void Start()
        {

            base.Start();

            transform.localScale = Vector2.zero;

            this._movimentPosition = transform.position;
            // this._movimentPosition.x += 1;

            this._takeOff = true;
            // this._moving = true;

            this._sensorField.SetActive(false);

            this._movimentLine = GetComponent<MovimentLineBehaviourScript>();            

        }

        // Update is called once per frame
        void Update()
        {

            _shootCadenceController += Time.deltaTime;

            this.UpdateSensorSize();

            this.Sensor();

        }
        // contant update
        private void FixedUpdate()
        {

            if (_takeOff)
            {
                TakeOff(Time.fixedDeltaTime * 0.5f);
                return;
            }

            // check if the ship is moving
            if (_moving & _enemyTarget == null)
            {
                Move(Time.fixedDeltaTime);
                this._movimentLine.SetDirection(_body.position, this._movimentPosition);
            }

            //check to rotate to enemy
            if (this._enemyTarget != null && _enemyTarget.gameObject.activeSelf & !_moving)
            {
                Rotate(this._enemyTarget.position, Time.fixedDeltaTime);

                // check if is time to shoot
                if (_shootCadenceController >= _shootCadence)
                {
                    Shoot();
                    _shootCadenceController = 0.0f;
                }

            }


        }
        #endregion

        #region Methods

        public override void StopMoviment()
        {
            Debug.Log("Stoping the moviment");
            this._moving = false;
            this._movimentLine.Hide();
        }
        #endregion

        #region ISlectable 
        public void Select()
        {
            this._sensorField.SetActive(true);
        }

        public void Deselect()
        {
            this._sensorField.SetActive(false);
        }
        #endregion
    }
}
