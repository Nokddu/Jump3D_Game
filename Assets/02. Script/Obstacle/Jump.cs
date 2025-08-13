using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private float JumpForce;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                player.jumping_platform(JumpForce);
            }
        }
    }
}
