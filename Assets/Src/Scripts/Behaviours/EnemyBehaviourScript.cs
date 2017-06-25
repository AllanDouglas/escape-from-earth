using UnityEngine;
namespace Behaviour
{
    public class EnemyBehaviourScript : Ship
    {


        #region Fields  
        private PlanetBehaviourScript _planetTarget;
        private float _shootCadenceController;
        #endregion


        #region Unity Methods
        // Use this for initialization
        protected override void Start()
        {
            base.Start();

            
            this._planetTarget = FindObjectOfType<PlanetBehaviourScript>();

        }
        // Update is called once per frame
        void Update()
        {
            _shootCadenceController += Time.deltaTime;

            this.UpdateSensorSize();

            this.Sensor();
        }

        // constant update
        private void FixedUpdate()
        {

            if (_landing)
            {
                Land(Time.fixedDeltaTime * 0.5f);
            }

            Vector3 position = (_enemyTarget == null) ? _planetTarget.transform.position : _enemyTarget.position;

            Rotate(position, Time.fixedDeltaTime);

            if (this._enemyTarget == null)
                Move(Time.fixedDeltaTime);

            if (this._enemyTarget != null & _shootCadenceController >= _shootCadence)
            {
                Shoot();
                _shootCadenceController = 0.0f;
            }


        }
        #endregion

        /// <summary>
        /// Execute the moviment
        /// </summary>
        protected override void Move(float deltaTime)
        {

            if (Vector2.Distance(transform.position, this._planetTarget.transform.position) < 0.1f)
            {
                _moving = false;
                return;
            }

            Vector2 newPosition = Vector2.MoveTowards(transform.position, this._planetTarget.transform.position, deltaTime * _moveSpeed);

            this._rigidbody2D.MovePosition(newPosition);

        }

        protected override void Land(float interval)
        {
            base.Land(interval);

            if(_landing == true)
            {
                gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Start the Land process
        /// </summary>
        protected void Land()
        {
            this._landing = true;
        }


        #region Collision Methods

        public void OnTriggerEnter2D(Collider2D other)
        {


            if (!other.gameObject.CompareTag("Planet"))
                return;

            other.gameObject.GetComponent<PlanetBehaviourScript>().Damage(Power);
            Land();


        }

        /*
        public void OnTriggerExit2D(Collider2D other)
        {

            Debug.Log("protetor saindo da area");

            if (other.transform == _enemyTarget)
                this._enemyTarget = null;
        }
        */
        #endregion
    }
}