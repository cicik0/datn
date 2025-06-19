using System.Linq;
using NUnit.Framework.Internal;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] Material[] bodyColorList;
    [SerializeField] SkinnedMeshRenderer bodyColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        //OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInit()
    {
        RandomFashion();
    }

    private void RandomFashion()
    {
        charWpType = (EnumWpType)RandomType(1, wpSO.wpListSO.Length + 1);
        charHatType = (EnumHatType)RandomType(0, hatSO.hatListSO.Length + 1);
        charPantType = (EnumPantType) RandomType(0, pantSO.pantListSO.Length + 1);

        Debug.Log($"{ charWpType} , { charHatType}  { charPantType}");

        charWeapon.OnInit(charWpType);
        charHat.OnInit(charHatType);
        charPant.OnInit(charPantType);

        bodyColor.material = bodyColorList[RandomType(0, bodyColorList.Length)];
    }

    private int RandomType(int start, int count)
    {
        int type = Random.Range(start, count);
        return type;
    }
}
