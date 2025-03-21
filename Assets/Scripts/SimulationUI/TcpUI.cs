using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class TcpUI : MonoBehaviour
{
    private TextField data1Field;
    private Label data2Label;
    private Button sendButton;
    private Button onOffButton; // 🔥 OnOff 버튼 추가
    private Button exit;
    private VisualElement tcpWindow;

    private TcpClientSelf tcpClient;
    private bool isConnected = true;  // 🔥 현재 연결 상태를 저장하는 변수
    private bool isWindowOpen = true;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        data1Field = root.Q<TextField>("Data1");
        data2Label = root.Q<Label>("Data2");
        sendButton = root.Q<Button>("SendButton");
        onOffButton = root.Q<Button>("OnOff");
        exit = root.Q<Button>("Exit");
        tcpWindow = root.Q<VisualElement>("DataSheet");

        // 🔥 추가된 null 체크
        if (data1Field == null) Debug.LogError("UI ToolkitでData1が見つかりません！");
        if (data2Label == null) Debug.LogError("UI ToolkitでData2が見つかりません！");
        if (sendButton == null) Debug.LogError("UI ToolkitでSendButtonが見つかりません！");
        if (onOffButton == null) Debug.LogError("UI ToolkitでOnOffButtonが見つかりません！");

        tcpWindow.style.display = DisplayStyle.Flex;
        tcpClient = FindObjectOfType<TcpClientSelf>();
        UpdateOnOffButtonText(); // 버튼 텍스트 초기화

        Button();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ToggleWindow();
        }
    }

    void OnSendButtonClicked()
    {
        if (tcpClient != null && !string.IsNullOrEmpty(data1Field.text))
        {
            string messageToSend = data1Field.text;
            tcpClient.SendData(messageToSend);
            Debug.Log($"[UI] 送信データ: {messageToSend}");
            data1Field.value = "";  // 입력 필드 초기화
            data1Field.Focus();     // 커서를 다시 입력 필드로 이동
        }
        else
        {
            Debug.LogWarning("[UI] 送信データが空です！");
        }
    }

    void OnOffButtonClicked()
    {
        if (tcpClient == null) return;

        if (isConnected)
        {
            tcpClient.DisconnectFromServer();
        }
        else
        {
            tcpClient.ConnectToServer();
        }

        isConnected = !isConnected;
        UpdateOnOffButtonText();
    }

    // 🔥 현재 상태에 따라 OnOff 버튼 텍스트 업데이트
    void UpdateOnOffButtonText()
    {
        if (onOffButton != null)
        {
            onOffButton.text = isConnected ? "On" : "Off";
        }
    }

    public void UpdateReceivedData(string receivedData)
    {
        if (data2Label != null)
        {
            data2Label.text = receivedData;
            Debug.Log($"[UI] 受信データ: {receivedData}");
        }
        else
        {
            Debug.LogError("[UI] Data2Labelが初期化されていません。");
        }
    }

    private void CloseWindow()
    {
        tcpWindow.style.display = DisplayStyle.None;
        isWindowOpen = false;
    }

    private void ToggleWindow()
    {
        if (isWindowOpen)
        {
            tcpWindow.style.display = DisplayStyle.None;
        }
        else
        {
            tcpWindow.style.display = DisplayStyle.Flex;
        }

        isWindowOpen = !isWindowOpen;
    }

    private void Button()
    {
        sendButton.clicked += OnSendButtonClicked;
        data1Field.RegisterCallback<KeyDownEvent>(evt =>
        {
            if (evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.KeypadEnter)
            {
                OnSendButtonClicked();
                evt.StopPropagation(); // 이벤트 전파 방지 (다른 UI 요소에 영향 주지 않도록)
            }
        });
        onOffButton.clicked += OnOffButtonClicked; // 🔥 OnOff 버튼 이벤트 추가
        exit.clicked += CloseWindow;
    }


}
