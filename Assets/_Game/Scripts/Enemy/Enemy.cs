using System.Linq;
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Enemy : Character
{
    [SerializeField] Material[] bodyColorList;
    [SerializeField] SkinnedMeshRenderer bodyColor;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public float radius;

    public IsState currentState;

    public Action<Enemy> OnEnemyDead;

    public void ChangeState(IsState newState)
    {
        if(currentState == null)
        {
            currentState = newState;
            currentState.OnEnter(this);
        }
        else if(currentState != newState)
        {
            currentState.OnExit(this);
            currentState = newState;
            currentState.OnEnter(this);
        }


    }
    //protected override void Awake()
    //{
    //    OnInit();
    //}

    protected override void OnEnable()
    {
        OnInit();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        ChangeState(new WatchState());
        //OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == false && currentState != null) currentState.OnExcute(this);
    }

    public override void OnInit()
    {
        RandomFashion();
        agent.speed = this.speed;
    }

    private void RandomFashion()
    {
        bodyColor.material = bodyColorList[RandomType(0, bodyColorList.Length)];

        charWpType = (EnumWpType)RandomType(1, wpSO.wpListSO.Length + 1);
        charHatType = (EnumHatType)RandomType(0, hatSO.hatListSO.Length + 1);
        charPantType = (EnumPantType) RandomType(0, pantSO.pantListSO.Length + 1);

        Debug.Log($"{ charWpType} , { charHatType}  { charPantType}");

        charWeapon.OnInit(charWpType);
        charHat.OnInit(charHatType);
        charPant.OnInit(charPantType);

        foreach(PantData data in pantSO.pantListSO)
        {
            if(data.pantType == charPantType)
            {
                speed += data.buffMoveSpeed;
                break;
            }
        }

        foreach (HatData data in hatSO.hatListSO)
        {
            if(data.hatType == charHatType)
            {
                rangeAttack += data.buffAtkRange;
                break;
            }
        }
    }

    private int RandomType(int start, int count)
    {
        int type = UnityEngine.Random.Range(start, count);
        return type;
    }

    public void Attack()
    {
        this.Throw();
    }

    public float RandomWatchTime()
    {
        float watchTime = 0.5f;
        int scale = UnityEngine.Random.Range(1, 5);
        return watchTime * scale;
    }

    public Vector3 GetRandomPointInNavmesh(Vector3 origin, float radius)
    {
        int rp = 15;
        for(int i = 0; i < rp; i++)
        {
            Vector2 randomPoint2D = UnityEngine.Random.insideUnitCircle * radius;
            Vector3 randomPointt = origin + new Vector3(randomPoint2D.x, 0, randomPoint2D.y);

            if (NavMesh.SamplePosition(randomPointt, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        return origin;
    }

    public override void OnDead()
    {
        base.OnDead();
        Debug.Log($"<color=red>{this.name} dead</color>");
        currentState.OnExit(this);
        currentState = null;
        if (OnEnemyDead == null) Debug.Log("No listen");
        OnEnemyDead?.Invoke(this);
    }

    public override void ResetCharacter()
    {
        base.ResetCharacter();
        ChangeState(new WatchState());
    }
}
