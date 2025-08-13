using TMPro;
using UnityEngine;

public class Consumable : ItemObject
{
    public TextMeshPro Name;

    private void Awake()
    {
        Name.text = itemData.displayName;
    }

    private void OnTriggerEnter(Collider other)
    {
        Oninteract();
        Character_Manager.Instance.Player.ApplyConsumable(itemData);
        Debug.Log(Character_Manager.Instance.Player.itemData.displayName);
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0.2f, 0);
    }
}
