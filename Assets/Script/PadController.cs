using UnityEngine;

public class PadController : MonoBehaviour
{
    public Camera mainCamera;  // Main Camera (기본 카메라)
    public Camera headSensor;  // HeadSensor (전환 대상)

    private bool isMainCameraActive = true;  // 현재 활성화된 카메라 상태

    void Awake()
    {
        mainCamera.enabled = true;
        headSensor.enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump")) // A 버튼 (기본적으로 "Jump"로 매핑됨)
        {
            ToggleMode();
        }
    }

    private void ToggleMode()
    {
        isMainCameraActive = !isMainCameraActive;

        if (isMainCameraActive)
        {
            mainCamera.enabled = true;
            headSensor.enabled = false;
        }
        else
        {
            mainCamera.enabled = false;
            headSensor.enabled = true;
        }
    }
}
