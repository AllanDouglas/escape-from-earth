using UnityEngine;
using Interfaces;
using System;

namespace Behaviour
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(HitPointsBarBehaviourScript))]

    public abstract class Ship : MonoBehaviour, IMovable, IGun, IDamageable, IDestructive
    {

        #region Inspector
        [Header("Generic Information")]
        [SerializeField]
        protected Transform _enemyTarget;
        [SerializeField]
        protected Transform _body;
        [SerializeField]
        protected int _maxHitPoints;
        [SerializeField]
        protected int _power;
        [SerializeField]
        protected float _shootCadence = .5f;
        [SerializeField]
        protected float _rotationSpeed = 10f;
        [SerializeField]
        protected float _moveSpeed = 2f;
        [SerializeField]
        protected ProjectileBehaviourScript _projectilePrefab;
        [SerializeField]
        protected float _sensorFieldSize = 1f;
        [SerializeField]
        protected GameObject _sensorField;
        [SerializeField]
        protected string _enemyTag;
        [SerializeField]
        protected string _enemyLayer;
        #endregion

        #region Properties
        /// <summary>
        /// The power
        /// </summary>
        public int Power
        {
            get { return _power; }
            private set { _power = value; }
        }
        /// <summary>
        /// Amount hit points
        /// </summary>
        public int HitPoints
        {
            get
            {
                return _hitPoints;
            }

            protected set
            {
                this._hitPoints = value;
            }

        }
        /// <summary>
        /// The Shoot Cadence
        /// </summary>
        public float ShootCandece
        {
            get
            {
                return _shootCadence;
            }
        }

        /// <summary>
        /// Position to Move
        /// </summary>
        public Vector2 ToPosition
        {
            get
            {
                return _movimentPosition;
            }
        }

        /// <summary>
        /// Moving 
        /// </summary>
        public bool Moving
        {
            get
            {
                return _moving;
            }
        }

        #endregion

        #region Components
        // Componets
        protected Rigidbody2D _rigidbody2D;
        protected HitPointsBarBehaviourScript _healthBar;
        #endregion

        #region Fields

        //Auxiliaries
        protected bool _moving;
        protected bool _takeOff = false;
        protected bool _landing = false;
        protected int _hitPoints;
        protected Vector2 _movimentPosition;
        protected float _sensorShootSize = 0.2f;
        protected ProjectileBehaviourScript[] _projectilePool;
        #endregion

        #region Unity Methods
        protected virtual void Start()
        {
            _hitPoints = _maxHitPoints;

            _healthBar = GetComponent<HitPointsBarBehaviourScript>();
            _healthBar.SetMaxHitPoints(_maxHitPoints);


            this._movimentPosition = transform.position;

            this._rigidbody2D = GetComponent<Rigidbody2D>();

            this.UpdateSensorSize();

            LoadProjectile();

        }
        #endregion

        #region Interface Methods
        /// <summary>
        /// Prepare the moviment do position
        /// </summary>
        /// <param name="position"></param>
        public virtual void MoveTo(Vector2 position)
        {

            if (Vector2.Distance(position, this._movimentPosition) < 0.3f)
                return;

            this._moving = true;

            this._movimentPosition = position;

        }

        /// <summary>
        /// Shoot a projectil
        /// </summary>
        public virtual void Shoot()
        {
            RaycastHit2D hit = Physics2D.Raycast(
              _body.position,
              _body.up,
              _sensorFieldSize + 0.2f, LayerMask.GetMask(_enemyLayer));

#if UNITY_EDITOR
            Debug.DrawRay(_body.position, _body.up * _sensorFieldSize);
#endif
            if (hit.collider == null)
                return;

            if (!hit.transform.CompareTag(_enemyTag))
                return;

            ProjectileBehaviourScript projectile = null;

            for (int i = 0; i < _projectilePool.Length; i++)
            {
                if (_projectilePool[i].gameObject.activeSelf == false)
                {
                    projectile = _projectilePool[i];
                    projectile.gameObject.tag = gameObject.tag;
                    projectile.transform.position = _body.position;
                    projectile.transform.rotation = _body.rotation;
                    projectile.Shoot();
                    break;

                }
            }
        }

        /// <summary>
        /// Destroy the ship
        /// </summary>
        public abstract void Kill();

        /// <summary>
        /// Stop Movimente
        /// </summary>
        public virtual void StopMoviment()
        {
            _moving = false;
        }

        /// <summary>
        /// Take Damage
        /// </summary>
        /// <param name="damage"></param>
        public virtual void Damage(int damage)
        {
            this._hitPoints -= damage;

            this._healthBar.Increment(-damage);

            if (_hitPoints <= 0)
            {
                Kill();
            }

        }
        #endregion

        #region Aux Methods
        /// <summary>
        /// Rotate to taget position
        /// </summary>
        /// <param name="targetPosition"></param>
        protected virtual void Rotate(Vector3 targetPosition, float deltaTime)
        {
            Quaternion deltaRotation = Quaternion.LookRotation(_body.position - targetPosition, Vector3.forward);
            deltaRotation.x = 0;
            deltaRotation.y = 0;

            _body.rotation = Quaternion.RotateTowards(_body.rotation, deltaRotation, deltaTime * _rotationSpeed);
        }

        /// <summary>
        /// Land
        /// </summary>
        protected virtual void Land(float interval)
        {
            Vector2 newScale = Vector2.MoveTowards(transform.localScale, Vector2.zero, interval);
            transform.localScale = newScale;

            if (newScale == Vector2.zero)
            {
                _landing = false;
            }

        }

        /// <summary>
        /// Take Off
        /// </summary>
        protected virtual void TakeOff(float interval)
        {
            Vector2 newScale = Vector2.MoveTowards(transform.localScale, Vector2.one, interval);

            transform.localScale = newScale;

            if (newScale == Vector2.one)
            {
                _takeOff = false;
            }
        }

        /// <summary>
        /// Execute the Moviment
        /// </summary>
        protected virtual void Move(float deltaTime)
        {
            if (Vector2.Distance(transform.position, this._movimentPosition) < 0.1f)
            {
                this.StopMoviment();
                return;
            }

            //var deltaRotation = Quaternion.LookRotation(
            //    boy.position - (Vector3)_movimentPosition, Vector3.forward);

            //deltaRotation.x = 0;
            //deltaRotation.y = 0;

            //_body.rotation = deltaRotation;

            Rotate(_movimentPosition, deltaTime);

            Vector2 newPosition = Vector2.MoveTowards(transform.position, _movimentPosition, deltaTime * _moveSpeed);

            this._rigidbody2D.MovePosition(newPosition);

        }

        /// <summary>
        /// Set the size of sensor
        /// </summary>
        /// <param name="newSize"></param>
        protected virtual void UpdateSensorSize()
        {
            if (_sensorField != null)
            {

                Vector2 sensorSize = _sensorField.transform.localScale;
                sensorSize.Set(_sensorFieldSize * 2, _sensorFieldSize * 2);
                _sensorField.transform.localScale = sensorSize;
            }

            _sensorShootSize += _sensorFieldSize;

        }

        /// <summary>
        /// Load the prjectiles
        /// </summary>
        protected virtual void LoadProjectile()
        {

            _projectilePool = new ProjectileBehaviourScript[2];

            for (int i = 0; i < _projectilePool.Length; i++)
            {
                _projectilePool[i] = Instantiate<ProjectileBehaviourScript>(this._projectilePrefab, transform.position, Quaternion.identity);

                _projectilePool[i].gameObject.SetActive(false);
            }


        }

        /// <summary>
        /// Check the Area
        /// </summary>
        protected void Sensor()
        {
            Collider2D other = Physics2D.OverlapCircle(transform.position, this._sensorFieldSize, LayerMask.GetMask(_enemyLayer), 0);

            if (other == null)
            {
                _enemyTarget = null;
                return;
            }          

            if (!other.gameObject.CompareTag(_enemyTag))
                return;

            //if (this._enemyTarget == null)
            //{
            //    this._enemyTarget = other.transform;
            //    //StopMoviment();
            //}

            this._enemyTarget = other.transform;


        }

      

        #endregion

    }
}
