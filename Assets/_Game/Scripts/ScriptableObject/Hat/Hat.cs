using UnityEngine;

public class Hat : MonoBehaviour
{
    [SerializeField] HatSO hatSO;
    [SerializeField] string hatName;
    [SerializeField] GameObject hatPrefab;
    [SerializeField] EnumHatType hatType;
    [SerializeField] float buffAtkRange;

    public void OnInit(EnumHatType type)
    {
        this.hatType = type;
        foreach (HatData h in hatSO.hatListSO)
        {
            if (hatType != EnumHatType.NONE && hatType == h.hatType)
            {
                this.hatType = h.hatType;
                this.hatPrefab = h.hatPrefab;
                this.buffAtkRange = h.buffAtkRange;
                Instantiate(hatPrefab, this.transform);
                return;
            }
        }
    }
}
