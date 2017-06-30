using UnityEngine;
using Interfaces;
using System;

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
        private Vector2 _originalScale;
        #endregion

        #region Unity Methods
        // Use this for initialization
        protected override void Start()
        {

            base.Start();

            _originalScale = transform.localScale;

            transform.localScale = Vector2.zero;

            this._movimentPosition = transform.position;
            // this._movimentPosition.x += 1;

            this._takeOff = true;
            // this._moving = true;

            this._sensorField.SetActive(false);

            this._movimentLine = GetComponent<MovimentLineBehaviourScript>();

            UpdateSensorSize();
        }

        // Update is called once per frame
        void Update()
        {

            _shootCadenceController += Time.deltaTime;

            //this.UpdateSensorSize();

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
            if (_moving)
            {
                Move(Time.fixedDeltaTime);
                this._movimentLine.SetDirection(_body.position, this._movimentPosition);
            }

            //check to rotate to enemy
            if (this._enemyTarget != null)
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

        #region Methods Overrided

        protected override void TakeOff(float interval)
        {
            Vector2 newScale = Vector2.MoveTowards(transform.localScale, _originalScale , interval);

            transform.localScale = newScale;

            if (newScale == _originalScale)
            {
                _takeOff = false;
            }
        }

        public override void Kill()
        {
            FXManagerBehaviourScript.Instance.ProtectorDestroyFX(transform.position);
            gameObject.SetActive(false);
        }

        public override void StopMoviment()
        {  
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
