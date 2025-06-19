using System;
using UnityEngine;
using Lean.Pool;
public class Weapon : MonoBehaviour
{
    [SerializeField] protected BulletSO bulletSO;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] float speedThorw;

    [SerializeField] public WeaponSO wpOS;
    [SerializeField] public string wpName;
    [SerializeField] public GameObject wpPrefab;
    [SerializeField] public EnumWpType wpType;
    [SerializeField] public float buffAtkSpeed;

    public void OnInit(EnumWpType type)
    {
        this.wpType = type;
        foreach(WeaponData wp in wpOS.wpListSO)
        {
            SetBulletPrefab();
            if(wpType != EnumWpType.NONE && wpType == wp.wpType)
            {
                this.wpName = wp.wpName;
                this.wpPrefab = wp.wpPrefab;
                this.buffAtkSpeed = wp.buffAtkSpeed;
                Instantiate(wpPrefab, this.transform);
                return;
            }
        }       
    }

    public void Throw(Character attacker, Action<Character, Character> onHit)
    {
        Bullet bullet = Lean.Pool.LeanPool.Spawn<Bullet>(bulletPrefab,attacker.shotTranform);
        bullet.OnInit(attacker, onHit, Vector3.forward, speedThorw + buffAtkSpeed);

    }

    private void SetBulletPrefab()
    {
        foreach(BulletData b in bulletSO.bulletListSO)
        {
            if(b.bulletType == wpType)
            {
                bulletPrefab = b.bulletPrefab;
                return;
            }
        }
    }
}
