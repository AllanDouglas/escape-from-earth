using UnityEngine;

namespace Behaviour
{

    public class SpawnBehaviourScript : MonoBehaviour
    {

        #region Inspector
        [SerializeField]
        private GameObject _prefabEnemyLV1;        
        [SerializeField]
        private LevelSpawn[] _prefabs;
        [SerializeField]
        private int _amountEach;
        [SerializeField]
        private Transform[] _spawnPoints;
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
        public GameObject Spawn()
        {
            for (int i = 0; i < _pool.Length; i++)
            {
                if (_pool[i].activeSelf == false)
                {
                    _pool[i].SetActive(true);
                    _pool[i].transform.position = GetSpawnPoint();
                    return _pool[i];                    
                }

            }

            return null;
        }

        Vector2 GetSpawnPoint()
        {
            int i = UnityEngine.Random.Range(0, _spawnPoints.Length);

            return _spawnPoints[i].position;

        }

        /// <summary>
        /// Prepare
        /// </summary>
        void Prepare()
        {
            _pool = new GameObject[_amountEach];

            for (int i = 0; i < _amountEach; i++)
            {
                _pool[i] = Instantiate<GameObject>(_prefabEnemyLV1, transform);
                _pool[i].transform.position = transform.position;
                _pool[i].gameObject.SetActive(false);
            }
        }

        [System.Serializable]
        public struct LevelSpawn
        {
            public int Level;
            public GameObject Prefab;
            private GameObject[] _pool;
        }
    }
}