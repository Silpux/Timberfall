using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class ItemDataSO : ScriptableObject{
    public ItemType itemType;
    public string displayName;
    public Sprite icon;
}
