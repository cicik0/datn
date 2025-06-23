using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class LevelControl : MonoBehaviour
{
    [SerializeField] public EnumLevelType lvType;
    [SerializeField] public LevelUI_SO levelSO;
    [SerializeField] public Player currentPlayer;
    [SerializeField] Player playerPrefab;
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] int numOfEnemy;
    [SerializeField] public int levelPoint;
    [SerializeField] NavMeshSurface lvNmSf;
    [SerializeField] MeshRenderer plane;

    public int currentLevelPoint = 0;

    private Coroutine DelaySpawnEnemyCoroutine;

    List<Enemy> enemys = new List<Enemy>();
    private void OnEnable()
    {
        lvNmSf.BuildNavMesh();
        if(DelaySpawnEnemyCoroutine != null)
        {
            StopCoroutine(DelaySpawnEnemyCoroutine);
        }
    }

    private void OnDestroy()
    {
        currentPlayer.OnPlayerDead -= HandlePlayerDead;

        foreach(Enemy e in enemys)
        {
            e.OnEnemyDead -= HandleEnemyDead;
        }

        if (DelaySpawnEnemyCoroutine != null)
        {
            StopCoroutine(DelaySpawnEnemyCoroutine);
            DelaySpawnEnemyCoroutine = null;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnInitLevel();
    }

    public void SetCurrentPlayer(Player player)
    {
        currentPlayer = player;
        currentPlayer.OnPlayerDead -= HandlePlayerDead;
        currentPlayer.OnPlayerDead += HandlePlayerDead;
    }

    public void OnInitLevel()
    {
        SetCurrentPlayer(LeanPool.Spawn(playerPrefab, this.transform));
        LevelManager.Ins.SetCamerafollower();
        currentPlayer.SetJoystick(LevelManager.Ins.GetFloatJoystick());

        for(int i = 0; i < numOfEnemy; i++)
        {
            //e.gameObject.SetActive(false);
            if(GetRandomPointInBounds(plane.bounds, 1.5f, out Vector3 spawnPos))
            {
                Enemy e = LeanPool.Spawn(enemyPrefab, this.transform);
                enemys.Add(e);
                e.OnEnemyDead -= HandleEnemyDead;
                e.OnEnemyDead += HandleEnemyDead;
                e.transform.position = spawnPos;
            }
            //e.gameObject.SetActive(true);
        }
    }

    public bool GetRandomPointInBounds(Bounds bounds, float rayHeight, out Vector3 spawnPoint, int maxAttempts = 20)
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float z = Random.Range(bounds.min.z, bounds.max.z);
            Vector3 rayOrigin = new Vector3(x, bounds.max.y + rayHeight, z);

            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, rayHeight * 2f))
            {
                if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 1f, NavMesh.AllAreas))
                {
                    spawnPoint = navHit.position;
                    return true;
                }
            }
        }

        spawnPoint = Vector3.zero;
        return false;
    }

    public void ResetLevel()
    {
        ResetPoint();
        foreach(Enemy e in enemys)
        {
            if (GetRandomPointInBounds(plane.bounds, 1.5f, out Vector3 spawnPos))
            {
                e.transform.position = spawnPos;
                e.ResetCharacter();
            }
        }

        currentPlayer.transform.position = new Vector3(0, currentPlayer.transform.position.y, 0);
        currentPlayer.ResetCharacter();
    }

    private void HandlePlayerDead()
    {
        //Debug.Log("plaery is dead");
        GameManager.ChangeState(GameState.LOSE);
    }

    private void HandleEnemyDead(Enemy e)
    {
        Debug.Log("enemy is dead, do something");
        enemys.Remove(e);
        currentLevelPoint++;

        if (currentLevelPoint < levelPoint)
        {
            if (DelaySpawnEnemyCoroutine != null)
            {
                StopCoroutine(DelaySpawnEnemyCoroutine);
            }
            DelaySpawnEnemyCoroutine = StartCoroutine(WaiteEnemyAnimationDead(e));
            LevelManager.Ins.SetPointForCurrentLevel(currentLevelPoint);
        }
        else
        {
            int bounusGold = levelPoint + Random.Range(0, 6);
            for(int i = 0; i < levelSO.leveUIDataListSO.Count; i++)
            {
                if(lvType == levelSO.leveUIDataListSO[i].levelType)
                {
                    DataManager.CompleteLevelStatus(i);
                }
            }
            DataManager.SetGold(bounusGold);
            GameManager.ChangeState(GameState.WIN);
        }
    }
    public void ResetPoint()
    {
        currentLevelPoint = 0;
    }

    private IEnumerator WaiteEnemyAnimationDead(Enemy e)
    {
        Debug.Log("start coroutine");
        yield return new WaitForSeconds(1f);
        //logic spawn
        SpawnEnemyAgain(e);
    }

    private void SpawnEnemyAgain(Enemy e)
    {
        Debug.Log($"<color=red>enemy spawn again</color>");
        LeanPool.Despawn(e);
        if (GetRandomPointInBounds(plane.bounds, 1.5f, out Vector3 spawnPos))
        {
            e = LeanPool.Spawn(enemyPrefab, this.transform);
            e.ResetCharacter();
            enemys.Add(e);
            e.transform.position = spawnPos;
        }

    }
}
