using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ItemData itemData;
    public PlayerController controller;
    public Player_Condition condition;
    public Action<ItemData> AddValue;
    private void Awake()
    {
        Character_Manager.Instance.Player = this;
        condition = GetComponent<Player_Condition>();
        controller = GetComponent<PlayerController>();
    }

    public void ApplyConsumable(ItemData data)
    {
        for (int i = 0; i < data.consumables.Length; i++)
        {
            switch (data.consumables[i].type)
            {
                case ConsumableType.Stamina:
                    condition.Add_Stamina(data.consumables[i].value);
                    break;
                case ConsumableType.Speed:
                    controller.AddSpeed(data.consumables[i].value, data.duration);
                    break;
                case ConsumableType.Jump:
                    controller.AddJump(data.consumables[i].value, data.duration);
                    break;
            }
        }
    }
}
