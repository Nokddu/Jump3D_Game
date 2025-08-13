using UnityEngine;
public interface IInteractable
{
    public string GetInteractPrompt(); // UI에 넘겨줄 string 값
    public void Oninteract(); // 인터렉션 됐을때 사용될 코드
}

public class ItemObject : MonoBehaviour , IInteractable
{
    public ItemData itemData;

    public string GetInteractPrompt()
    {
        string str = $"{itemData.displayName}";
        return str;
    }

    public void Oninteract()
    {
        Character_Manager.Instance.Player.itemData = itemData;
        Destroy(gameObject);
    }
}
