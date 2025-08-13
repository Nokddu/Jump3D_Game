using UnityEngine;
public interface IInteractable
{
    public string GetInteractPrompt(); // UI�� �Ѱ��� string ��
    public void Oninteract(); // ���ͷ��� ������ ���� �ڵ�
}

public class ItemObject : MonoBehaviour , IInteractable
{
    public ItemData itemData;

    public string GetInteractPrompt()
    {
        string str = $"{itemData.ItemDescription}";
        return str;
    }

    public virtual void Oninteract()
    {
        Character_Manager.Instance.Player.itemData = itemData;
    }
}
