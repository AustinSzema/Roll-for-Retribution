using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private int secondsBetweenSpawn;
    public RoundSpawnConfig _roundSpawnConfig;
    private spawnInfoVariable currentSpawnInfoVariable;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float noNoZoneDistance = 100f;
    [SerializeField] private float noNoZoneAngle = 45f;
    public static int EnemiesInScene = 0;
    private List<SpawnInfo> _spawnInfos = new List<SpawnInfo>();
    private SpawnInfo _currentSpawnInfo;
    private int _currentSpawnInfoIdx;
    private bool _changeSpawnInfo;
    private List<spawnInfoVariable> _roundSpawns;
    [SerializeField] private float startDelay = 5f;
    private Coroutine spawnCoroutine;

    public static SpawnEnemies Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (_roundSpawnConfig == null)
        {
            throw new ArgumentNullException("_roundSpawnConfig must be non-null");
        }

        _roundSpawns = _roundSpawnConfig.Value ?? throw new ArgumentNullException("roundSpawns must contain a valid list of spawnInfoVariables");

        currentSpawnInfoVariable = _roundSpawns[Mathf.Min(GameManager.Instance.currentRound, _roundSpawns.Count - 1)];
        _spawnInfos = currentSpawnInfoVariable.Value ?? throw new ArgumentNullException("spawnInfo must contain a valid list of spawninfos");

        _currentSpawnInfo = _spawnInfos[0];
        _currentSpawnInfoIdx = 0;
        _changeSpawnInfo = true;

        KillQuota.Instance.killQuotaSlider.maxValue = currentSpawnInfoVariable.killQuota;
        StartCoroutine(WaitToStart(startDelay));
    }

    private IEnumerator WaitToStart(float delay)
    {
        yield return new WaitForSeconds(delay);
        spawnCoroutine = StartCoroutine(SpawnEnemiesWithDelay());
    }

    private IEnumerator SpawnEnemiesWithDelay()
    {
        while (true)
        {
            SpawnCurrentEnemies();
            yield return new WaitForSeconds(secondsBetweenSpawn);
        }
    }

    private void Update()
    {
        if (_changeSpawnInfo)
        {
            StartCoroutine(SwapSpawnInfo());
        }
    }

    private IEnumerator SwapSpawnInfo()
    {
        if (_currentSpawnInfoIdx < _spawnInfos.Count - 1)
        {
            float timeUntilSwap = _spawnInfos[_currentSpawnInfoIdx + 1].StartTime - Time.timeSinceLevelLoad + startDelay;
            if (timeUntilSwap < 0)
            {
                _currentSpawnInfoIdx++;
                _currentSpawnInfo = _spawnInfos[_currentSpawnInfoIdx];
            }
            else
            {
                _changeSpawnInfo = false;
                yield return new WaitForSeconds(timeUntilSwap);
                _changeSpawnInfo = true;
            }
        }
        else
        {
            _changeSpawnInfo = false;
        }
    }

    private void SpawnCurrentEnemies()
    {
        if (EnemiesInScene >= _currentSpawnInfo.MaxEnemiesInScene && _currentSpawnInfo.MaxEnemiesInScene != -1) return;

        foreach (var spawnable in _currentSpawnInfo.Spawnables)
        {
            for (int i = 0; i < spawnable.number && EnemiesInScene < _currentSpawnInfo.MaxEnemiesInScene; i++)
            {
                SpawnEnemy(spawnable.enemy, playerTransform.position);
            }
        }
    }

    private bool InTriangle(Vector3 point, Vector3 a, Vector3 b, Vector3 c)
    {
        bool SameSide(Vector3 p1, Vector3 p2, Vector3 a, Vector3 b)
        {
            Vector3 cp1 = Vector3.Cross(b - a, p1 - a);
            Vector3 cp2 = Vector3.Cross(b - a, p2 - a);
            return Vector3.Dot(cp1, cp2) >= 0;
        }

        return SameSide(point, a, b, c) && SameSide(point, b, a, c) && SameSide(point, c, a, b);
    }

    private Vector3 PickEnemyPosition(Vector3 centralLocation, float minDistance = 40f, float maxDistance = 60f)
    {
        float xOffset = Random.Range(minDistance, maxDistance) * (Random.value > 0.5f ? 1 : -1);
        float zOffset = Random.Range(minDistance, maxDistance) * (Random.value > 0.5f ? 1 : -1);
        return new Vector3(centralLocation.x + xOffset, 20f, centralLocation.z + zOffset);
    }

    public void SpawnEnemy(GameObject enemy, Vector3 centralLocation, float minRange = 40f, float maxRange = 60f)
    {
        GameObject enemyInstance = EnemyPool.Instance.GetPooledObject(enemy.name);
        if (enemyInstance == null) return;

        Vector3 behind = -playerTransform.forward;
        Vector3 NoNoZoneLeft = Quaternion.AngleAxis(noNoZoneAngle, Vector3.up) * behind * noNoZoneDistance;
        Vector3 NoNoZoneRight = Quaternion.AngleAxis(-noNoZoneAngle, Vector3.up) * behind * noNoZoneDistance;

        Vector3 enemyPosition;
        do
        {
            enemyPosition = PickEnemyPosition(centralLocation, minRange, maxRange);
        }
        while (InTriangle(enemyPosition, centralLocation, NoNoZoneLeft + centralLocation, NoNoZoneRight + centralLocation));

        enemyInstance.transform.position = enemyPosition;
        enemyInstance.GetComponentInChildren<EnemyBase>(true).gameObject.SetActive(true);
        EnemiesInScene++;
    }
}
