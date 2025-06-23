using System.Linq;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected EnumWpType charWpType;
    [SerializeField] protected EnumHatType charHatType;
    [SerializeField] protected EnumPantType charPantType;
    [SerializeField] protected Weapon charWeapon;
    [SerializeField] protected Hat charHat;
    [SerializeField] protected Pant charPant;
    [SerializeField] protected Animator animator;
    [SerializeField] protected WeaponSO wpSO;
    [SerializeField] protected HatSO hatSO;
    [SerializeField] protected PantSO pantSO;
    [SerializeField] protected CharacterSight charSight;
    [SerializeField] public float rangeAttack;
    [SerializeField] public Transform targetTranform;
    [SerializeField] public Transform shotTranform;
    [SerializeField] public bool isDead;
    [SerializeField] public bool isThrow;

    public HashSet<Character> enemys = new HashSet<Character>();
    private string currentAnim;
    DataPlayer data;

    protected virtual void OnEnable()
    {
        //if (charWeapon != null)
        //{
        //    charWeapon.OnBulletDespawnAftertime += HandleBulletDespawnAftertime;
        //    Debug.Log("this woking on character");
        //}
        OnInit();
        
    }

    private void OnDestroy()
    {
        charWeapon.OnBulletDespawnAftertime -= HandleBulletDespawnAftertime;
    }

    protected virtual void Awake()
    {
        //OnInit();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        //data = DataManager.LoadDataFromLocal();
        //OnInit();
        charWeapon.OnBulletDespawnAftertime += HandleBulletDespawnAftertime;

        ChangeAnim(Constant.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public virtual void OnInit()
    {
        data = DataManager.LoadDataFromLocal();
        SetWp();
        //charWeapon.OnBulletDespawnAftertime -= HandleBulletDespawnAftertime;
        //charWeapon.OnBulletDespawnAftertime += HandleBulletDespawnAftertime;
        SetHat();
        SetPant();
    }

    public void ChangeAnim(string animName)
    {
        if(currentAnim != animName)
        {
            //if(animName == Constant.DEAD) Debug.Log($"<color=red>{animName}</color>");
            animator.ResetTrigger(this.currentAnim);
            //animator.ResetTrigger(1)
            currentAnim = animName;
            animator.SetTrigger(this.currentAnim);

            //Debug.Log($"SetTrigger: {currentAnim}");

            //foreach (var param in animator.parameters)
            //{
            //    if (param.name == currentAnim)
            //        Debug.Log($"Trigger {currentAnim} FOUND in Animator");
            //}
        }
    }

    protected void SetWp()
    {
        if (data == null) return;
        //Debug.Log("set wp");
        
        int wpIndex;
        for (int i = 0; i < data.wpStatus.Count; i++)
        {
            if (data.wpStatus[i] == 2)
            {
                wpIndex = i;
                charWpType = wpSO.wpListSO[i].wpType;
                charWeapon.OnInit(charWpType);
                return;
            }
        }
    }

    protected void SetHat()
    {
        if (data == null) return;

        int hatIndex;
        for(int i = 0; i < data.hatStatus.Count; i++)
        {
            if (data.hatStatus[i] == 2)
            {
                hatIndex = i;
                charHatType = hatSO.hatListSO[i].hatType;
                charHat.DesTroyHatView();
                charHat.OnInit(charHatType);
                rangeAttack += hatSO.hatListSO[i].buffAtkRange;
                return;
            }
        }
    } 

    protected void SetPant()
    {
        if (data == null) return;

        int pantIndex;
        for(int i = 0; i < data.pantStatus.Count; i++)
        {
            if (data.pantStatus[i] == 2)
            {
                pantIndex = i;
                charPantType = pantSO.pantListSO[i].pantType;
                charPant.OnInit(charPantType);
                speed += pantSO.pantListSO[i].buffMoveSpeed;
                return;
            }
        }
    }

    private void HandleBulletDespawnAftertime(bool value)
    {
        //Debug.Log($"____________{value}___________");
        SetIsThrow(value);
    }

    protected void SetIsThrow(bool value)
    {
        isThrow = value;
    }

    public void TriggerEventThrow()
    {
        if(targetTranform == null) return;
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_THROW);
        Vector3 directionThrow = (targetTranform.position - this.transform.position).normalized;
        this.transform.rotation = Quaternion.LookRotation(directionThrow);
        charWeapon.Throw(this, directionThrow, OnHitVictim);
    }

    //[ContextMenu("test throw")]
    protected void Throw()
    {
        //Debug.Log("THROW");
        if (isThrow == true) return;

        ChangeAnim(Constant.ATTACK);

        SetIsThrow(true);
    }

    private void OnHitVictim(Character attacker, Character victim)
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_ONHIT);
        victim.OnDead();
        charWeapon.SetActiveWp(true);
        SetIsThrow(false);
        charWeapon.DeSpawnBullet();
        RemoveTarget(victim);
        charSight.SetTargetToThrow();
    }

    public virtual void OnDead()
    {
        isDead = true;
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_DIE);
        //Debug.Log(currentAnim);
        ChangeAnim(Constant.DEAD);
        //Debug.Log(currentAnim);
    }

    public void AddTarget(Character target)
    {
        enemys.Add(target);
    }

    public void RemoveTarget(Character target)
    {
        enemys.Remove(target);
    }

    public virtual void ResetCharacter()
    {
        isDead = false;
        isThrow = false;
        targetTranform = null;
        enemys.Clear();
        ChangeAnim(Constant.IDLE);
    }

    public void SetItemForModel(EnumItemCategory type, int itemIndex)
    {
        switch (type)
        {
            case EnumItemCategory.PATN:
                charPantType = pantSO.pantListSO[itemIndex].pantType;
                charPant.OnInit(charPantType);
                break;
            case EnumItemCategory.HAT:
                charHat.DesTroyHatView();
                charHatType = hatSO.hatListSO[itemIndex].hatType;
                charHat.OnInit(charHatType);
                break;
        }
    }
}
