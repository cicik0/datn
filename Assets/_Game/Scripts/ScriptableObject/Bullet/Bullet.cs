using System;
using System.Collections;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Transform bulletView;

    protected float timeToDeSpawn = 1f;
    protected Coroutine DeSpawnBulletCoroutine;
    protected Character attacker;
    protected Action<Character, Character> onHit;

    protected Coroutine EffectCourotine;

    private void OnDisable()
    {
        if (EffectCourotine != null)
        {
            StopCoroutine(EffectCourotine);
            EffectCourotine = null;
        }
    }

    public virtual void OnInit(Character attacker, Action<Character, Character> onHit, Vector3 director, float speed)
    {
        this.attacker = attacker;
        this.onHit = onHit;
        Debug.Log(speed);
        if(EffectCourotine != null) StopCoroutine(EffectCourotine);
        EffectCourotine = StartCoroutine(EffectThrowCounrotine());
        rb.linearVelocity = director.normalized * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == attacker)
        {
            Debug.LogError("shot myselft");
            return;
        }

        if (other.CompareTag(Constant.LAYER_CHARACTER))
        {
            Character victim = Cache_character.GetChar(other);
            onHit?.Invoke(attacker, victim);
        }
    }

    public void SetAttacker(Character atker)
    {
        this.attacker = atker;
    }

    protected virtual void EffectThrow()
    {
        Debug.Log("base bullet");
    }

    private IEnumerator DeSpawnAfterTime()
    {
        yield return new WaitForSeconds(timeToDeSpawn);
    }

    private IEnumerator EffectThrowCounrotine()
    {
        while (true)
        {
            Debug.Log("rotate");
            EffectThrow();
            yield return null;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
