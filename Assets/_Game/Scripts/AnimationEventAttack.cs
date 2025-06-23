using UnityEngine;

public class AnimationEventAttack : MonoBehaviour
{
    [SerializeField] Character character;

    public void TriggerEventThrow()
    {
        character.TriggerEventThrow();
    }
}
