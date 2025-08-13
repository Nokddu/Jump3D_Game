public class UI_Manager : Singleton<UI_Manager>
{
    public UI_Conditions ui_Conditions;

    private void Awake()
    {
        ui_Conditions = GetComponentInChildren<UI_Conditions>();
    }
}
