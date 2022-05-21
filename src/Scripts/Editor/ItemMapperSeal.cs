using UnityEngine;
using UnityEngine.AddressableAssets;
namespace DokkaebiBag.Editor{
[CreateAssetMenu(fileName ="SEAL",menuName ="SO/SEAL")]
[System.Serializable]
public class ItemMapperSeal : ScriptableObject
{
  [SerializeField]
    public AssetReferenceGameObject Objectreference;
    [SerializeField]
    public AssetReferenceSprite Spritereference;    
}
}