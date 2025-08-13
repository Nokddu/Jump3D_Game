using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] private Camera MainCam;
    [SerializeField] private Camera SubCam;

    private bool isPersonal = true;

    public void CameraChange()
    {
        isPersonal = !isPersonal;
        MainCam.enabled = !isPersonal;
        SubCam.enabled = isPersonal;
    }
}
