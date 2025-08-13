using UnityEngine;
using UnityEngine.UI;

public class UI_Conditions : MonoBehaviour
{
    public Image StaminaBar;

    public void UpdateStaminaBar(float amount, float max)
    {
        StaminaBar.fillAmount = amount / max;
    }
}
