using NUnit.Framework;
using UnityEngine;
using static UnityEditor.LightingExplorerTableColumn;

public class Pant : MonoBehaviour
{
    [SerializeField] PantSO pantSO;
    [SerializeField] EnumPantType pantType;
    [SerializeField] SkinnedMeshRenderer pantMaterial;

    public void OnInit(EnumPantType type)
    {
        this.pantType = type;
        if(pantType == EnumPantType.NONE)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            foreach (PantData p in pantSO.pantListSO)
            {
                if (pantType == p.pantType)
                {
                    this.pantMaterial.material = p.pantMaterial;
                    this.gameObject.SetActive (true);
                    return;
                }
            }
        }
    }
}
