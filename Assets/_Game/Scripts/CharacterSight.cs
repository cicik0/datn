using Unity.Android.Types;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterSight : MonoBehaviour
{
    [SerializeField] SphereCollider sightCollider;
    [SerializeField] Character character;
    [SerializeField] float rangeAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInit()
    {
        sightCollider.radius = character.rangeAttack;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            //Debug.Log(other.GetType());
            Character target = Cache_character.GetChar(other);
            //Debug.Log($"{character.name}/{target.name}");
            if(character == target) return;
            character.AddTarget(target);
            SetTargetToThrow();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            Character target = Cache_character.GetChar(other);
            character.RemoveTarget(target);
            SetTargetToThrow();
        }
    }

    public void SetTargetToThrow()
    {
        if (character.enemys.Count == 0)
        {
            character.targetTranform = null;
            return;
        }

        Character charNeaset = null;
        float miniDistance = float.MaxValue;

        foreach(Character target in character.enemys)
        {
            if (target.isDead == true) continue;
            float distace = Vector3.Distance(character.transform.position, target.transform.position);
            if(distace <= miniDistance)
            {
                miniDistance = distace;
                charNeaset = target;
            }
        }
        if (charNeaset == null) return;
        character.targetTranform = charNeaset.transform;
    }

}
