using System.Collections;
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

    public override void Oninteract()
    {
        base.Oninteract();

        StartCoroutine(Eat());
    }

    IEnumerator Eat()
    {
        GetComponent<Collider>().enabled = false;

        GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSeconds(1.5f);

        GetComponent<Collider>().enabled = true;

        GetComponent<MeshRenderer>().enabled = true;
    }
}
