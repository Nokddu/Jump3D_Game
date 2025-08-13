using UnityEngine;

public class Player_Condition : MonoBehaviour
{
    [Header("스테미너")]
    [SerializeField] private float stamina;
    public float Stamina { get { return stamina; } }
    [SerializeField] private int minStamina;
    [SerializeField] private int maxStamina;

    [SerializeField] float StaminaCharge;
    public float Run_Stamina;
    public float Jump_Stamina;

    private void Start()
    {
        Character_Manager.Instance.Player.controller.useStamina += Using_Stamina;
    }

    private void OnDisable()
    {
        Character_Manager.Instance.Player.controller.useStamina -= Using_Stamina;
    }

    private void Update()
    {
        Stamina_Generate();

        UI_Manager.Instance.ui_Conditions.UpdateStaminaBar(stamina, maxStamina);
    }

    void Stamina_Generate()
    {
        stamina += StaminaCharge * Time.deltaTime;
        stamina = Mathf.Clamp(stamina, minStamina, maxStamina);
    }

    public void Using_Stamina(float amount)
    {
        stamina -= amount;
    }

    public void Add_Stamina(float value)
    {
        stamina += value;
    }
}
