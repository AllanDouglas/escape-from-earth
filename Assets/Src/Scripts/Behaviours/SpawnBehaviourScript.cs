using UnityEngine;

namespace Behaviour
{

    public class SpawnBehaviourScript : MonoBehaviour
    {

        #region Inspector
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private LevelSpawn[] _prefabs;
        [SerializeField]
        private int _amountEach;
        #endregion

        private GameObject[] _pool;

        // Use this for initialization
        void Start()
        {
            Prepare();
        }

        /// <summary>
        /// Spawn
        /// </summary>
        public void Spawn()
        {
            for (int i = 0; i < _pool.Length; i++)
            {
                if (_pool[i].activeSelf == false)
                {
                    _pool[i].SetActive(true);
                    break;
                }

            }
        }

        /// <summary>
        /// Prepare
        /// </summary>
        void Prepare()
        {
            _pool = new GameObject[_amountEach];

            for (int i = 0; i < _amountEach; i++)
            {
                _pool[i] = Instantiate<GameObject>(_prefab, transform);
                _pool[i].transform.position = transform.position;
                _pool[i].gameObject.SetActive(false);
            }
        }

        [System.Serializable]
        public struct LevelSpawn
        {
            public int Number;
            public GameObject Prefab;

            private GameObject[] _pool;
        }
    }
}