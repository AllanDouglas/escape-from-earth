using UnityEngine;


public class FXManagerBehaviourScript : MonoBehaviour
{
    #region Inspector
    [Header("Destruction FX")]
    [SerializeField]
    private ParticleSystem _enemyDestruction;
    [SerializeField]
    private ParticleSystem _protectorDestruction;
    [SerializeField]
    private ParticleSystem _planetDestructionPrefab;
    #endregion

    private ParticleSystem[] _poolProtectorDestruction;
    private ParticleSystem[] _poolEnemyDestruction;
    private ParticleSystem _planetDestruction;

    // Singleton
    public static FXManagerBehaviourScript Instance { get; private set; }

    #region Unity Methods
    // Awake
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("### Just there another instance for this ####");
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start()
    {
        Setup();
    }
    #endregion

    #region Interface Methods
    /// <summary>
    /// Play enemies destruction Fx
    /// </summary>
    public void EnemyDestroyFX(Vector3 position)
    {
        ParticleSystem particleSystem = null;

        for (int i = 0; i < _poolEnemyDestruction.Length; i++)
        {
            particleSystem = _poolEnemyDestruction[i];

            if (!particleSystem.isPlaying)
            {
                particleSystem.transform.position = position;
                particleSystem.Play();
                break;
            }
        }
    }

    public void ProtectorDestroyFX(Vector3 position)
    {
        ParticleSystem particleSystem = null;

        for (int i = 0; i < _poolProtectorDestruction.Length; i++)
        {
            particleSystem = _poolProtectorDestruction[i];

            if (!particleSystem.isPlaying)
            {
                particleSystem.transform.position = position;
                particleSystem.Play();
                break;
            }
        }
    }

    /// <summary>
    /// Play planet destruction
    /// </summary>
    public void PlanetDestroyFX(Vector2 position)
    {
        _planetDestruction.transform.position = position;
        _planetDestruction.Play();
    }

    #endregion
    #region Private Methods

    private void Setup()
    {
        _poolEnemyDestruction = Prepare(_enemyDestruction, 10);
        _poolProtectorDestruction = Prepare(_protectorDestruction);

        _planetDestruction = Instantiate<ParticleSystem>(_planetDestructionPrefab, transform);


    }

    private ParticleSystem[] Prepare(ParticleSystem _prefab, int length = 4)
    {

        ParticleSystem[] container = new ParticleSystem[length];

        for (int i = 0; i < container.Length; i++)
        {
            container[i] = Instantiate<ParticleSystem>(_prefab, transform);
        }

        return container;
    }

    #endregion

}
