using System;
using UnityEngine;
using Lean.Pool;
using System.Collections;
public class Weapon : MonoBehaviour
{
    [SerializeField] protected BulletSO bulletSO;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] float speedThorw;
    [SerializeField] GameObject wpView;

    [SerializeField] public WeaponSO wpOS;
    [SerializeField] public string wpName;
    [SerializeField] public GameObject wpPrefab;
    [SerializeField] public EnumWpType wpType;
    [SerializeField] public float buffAtkSpeed;

    Bullet currentBullet = null;

    private Coroutine deSpawnCorountione;

    public Action<bool> OnBulletDespawnAftertime;

    private void OnDisable()
    {
        if(deSpawnCorountione != null)
        {
            StopCoroutine(deSpawnCorountione);
            deSpawnCorountione = null;
        }
    }

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
                wpView = Instantiate(wpPrefab, this.transform);
                return;
            }
        }       
    }

    public void Throw(Character attacker, Vector3 directionThrow, Action<Character, Character> onHit)
    {
        SetActiveWp(false);
        Bullet bullet = Lean.Pool.LeanPool.Spawn<Bullet>(bulletPrefab,attacker.shotTranform);
        currentBullet = bullet;
        bullet.OnInit(attacker, onHit, directionThrow, speedThorw + buffAtkSpeed);
            
        if(deSpawnCorountione != null) StopCoroutine(deSpawnCorountione);
        deSpawnCorountione = StartCoroutine(DespawnBulletAftertimeCounrotine(2f));

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

    public void DeSpawnBullet()
    {
        LeanPool.Despawn(currentBullet);
        currentBullet = null;
        SetActiveWp(true);
        if(OnBulletDespawnAftertime == null)
        {
            //Debug.Log("no one listening");
        }
        OnBulletDespawnAftertime?.Invoke(false);
        //Debug.Log("this working too________");
    }

    private IEnumerator DespawnBulletAftertimeCounrotine(float delayTime)
    {
        //Debug.Log($"this working________");
        yield return new WaitForSeconds(delayTime);
        DeSpawnBullet();
    }

    public void SetActiveWp(bool value)
    {
        wpView.SetActive(value);
    }
}
