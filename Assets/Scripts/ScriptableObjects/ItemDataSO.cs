using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class ItemDataSO : ScriptableObject{
    [SerializeField] private ItemType itemType;
    public ItemType ItemType => itemType;
    [SerializeField] private string displayName;
    public string DisplayName => displayName;
    [SerializeField] private Sprite icon;
    public Sprite Icon => icon;
}
