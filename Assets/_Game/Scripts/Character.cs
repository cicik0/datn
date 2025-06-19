using System.Linq;
using Unity.VisualScripting;
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
    [SerializeField] public Transform shotTranform;
    [SerializeField] protected bool isDead;
    [SerializeField] protected bool isThrow;
 
    private string currentAnim;
    DataPlayer data;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        data = DataManager.LoadDataFromLocal();
        OnInit();
        ChangeAnim(Constant.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public virtual void OnInit()
    {
        SetWp();
        SetHat();
        SetPant();
    }

    protected void ChangeAnim(string animName)
    {
        if(currentAnim != animName)
        {
            animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
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
                charHat.OnInit(charHatType);
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
                return;
            }
        }
    }

    [ContextMenu("test throw")]
    protected void Throw()
    {
        charWeapon.Throw(this, OnHitVictim);
        isThrow = false;
    }

    private void OnHitVictim(Character attack, Character victim)
    {
        isThrow = true;
    }
}
