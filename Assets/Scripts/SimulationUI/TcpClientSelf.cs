using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using System.Collections; // ← 추가해야 함!!!

public class TcpClientSelf : MonoBehaviour
{
    public string serverIP = "127.0.0.1"; // 서버 IP (YOLO 서버)
    public int serverPort = 50000; // 서버 포트

    private TcpClient client;
    private NetworkStream stream;
    private Thread clientThread;
    private bool isConnected = false;
    private bool isReceiving = false;

    public Camera captureCamera;
    public RenderTexture renderTexture;

    void Start()
    {
        Debug.Log("Unity: TCP 클라이언트 초기화 중...");
        ConnectToServer();
        StartCoroutine(SendFrameToYOLO()); // 이미지 캡처 및 전송 시작
    }

    public void ConnectToServer()
    {
        if (isConnected)
        {
            Debug.LogWarning("Unity: 이미 서버에 연결됨.");
            return;
        }

        try
        {
            client = new TcpClient();
            client.Connect(serverIP, serverPort);
            stream = client.GetStream();
            isConnected = true;
            isReceiving = true;

            Debug.Log($"Unity: 서버 {serverIP}:{serverPort}에 연결됨.");

            clientThread = new Thread(ReceiveData);
            clientThread.IsBackground = true;
            clientThread.Start();
        }
        catch (Exception ex)
        {
            Debug.LogError("Unity: 서버 연결 실패. 오류: " + ex.Message);
        }
    }

    IEnumerator SendFrameToYOLO()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);  // 1초마다 전송

            if (!isConnected || stream == null)
            {
                Debug.LogWarning("Unity: 서버 연결이 끊어짐.");
                continue;
            }

            try
            {
                // 이미지 캡처
                Texture2D frame = CaptureFrame();
                byte[] imageBytes = frame.EncodeToPNG();
                Destroy(frame);

                // 데이터 크기 전송 (TCP에서는 패킷 크기를 미리 보내야 안정적임)
                byte[] sizeBytes = BitConverter.GetBytes(imageBytes.Length);
                stream.Write(sizeBytes, 0, sizeBytes.Length);

                // 이미지 데이터 전송
                stream.Write(imageBytes, 0, imageBytes.Length);
                Debug.Log("Unity: 이미지 데이터 전송 완료.");
            }
            catch (Exception ex)
            {
                Debug.LogError("Unity: 이미지 데이터 전송 실패. 오류: " + ex.Message);
            }
        }
    }

    void ReceiveData()
    {
        byte[] buffer = new byte[2048];
        Debug.Log("Unity: 서버로부터 YOLO 데이터 수신 대기 중...");

        while (isReceiving)
        {
            try
            {
                if (!isConnected || stream == null) break;

                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                {
                    Debug.Log("Unity: 서버 연결 종료됨.");
                    break;
                }

                string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Debug.Log("Unity: YOLO 분석 결과 수신 - " + dataReceived);

                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    Debug.Log("Unity: YOLO 분석 결과 적용 중...");
                    ProcessYOLOData(dataReceived);
                });
            }
            catch (Exception ex)
            {
                Debug.LogError("Unity: 데이터 수신 중 오류 발생 - " + ex.Message);
                break;
            }
        }

        isConnected = false;
        isReceiving = false;
    }

    public void SendData(string message)
    {
        if (!isConnected || stream == null)
        {
            Debug.LogWarning("Unity: サーバーに接続されていません。データ送信を中止します。");
            return;
        }

        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
            Debug.Log("Unity: データ送信: " + message);
        }
        catch (Exception ex)
        {
            Debug.LogError("Unity: データ送信中にエラーが発生しました: " + ex.Message);
        }
    }
    
Texture2D CaptureFrame()
    {
        RenderTexture.active = renderTexture;
        Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tex.Apply();
        RenderTexture.active = null;
        return tex;
    }

    void ProcessYOLOData(string json)
    {
        Debug.Log("Unity: YOLO 결과 파싱 중...");

        // 받은 JSON 데이터를 파싱 (객체 인식 결과를 받는 구조체 생성)
        DetectionData detectionData = JsonUtility.FromJson<DetectionData>(json);

        // 파싱된 데이터로 게임 오브젝트를 생성
        foreach (var detection in detectionData.detections)
        {
            Debug.Log($"Class: {detection.className}, Confidence: {detection.confidence}, Position: ({detection.xMin}, {detection.yMin}) to ({detection.xMax}, {detection.yMax})");

            // 예시로 객체의 클래스에 맞춰서 게임 오브젝트를 생성
            GameObject detectionObject = new GameObject(detection.className);
            detectionObject.transform.position = new Vector3(detection.xMin, detection.yMin, 0);  // 위치 설정
            detectionObject.transform.localScale = new Vector3(detection.xMax - detection.xMin, detection.yMax - detection.yMin, 1); // 크기 설정
        }
    }

    [System.Serializable]
    public class DetectionData
    {
        public Detection[] detections;
    }

    [System.Serializable]
    public class Detection
    {
        public string className;  // 객체의 클래스 (예: "person", "car" 등)
        public float confidence;  // 신뢰도
        public float xMin, yMin;  // 객체의 좌측 상단 좌표
        public float xMax, yMax;  // 객체의 우측 하단 좌표
    }

    public void DisconnectFromServer()
    {
        if (!isConnected)
        {
            Debug.LogWarning("Unity: 이미 서버 연결이 끊어짐.");
            return;
        }

        Debug.Log("Unity: 서버 연결 종료 중...");
        isReceiving = false;
        isConnected = false;

        try
        {
            stream?.Close();
            client?.Close();
            Debug.Log("Unity: 서버 연결 정상 종료.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Unity: 연결 종료 중 오류 발생 - " + ex.Message);
        }

        if (clientThread != null && clientThread.IsAlive)
        {
            clientThread.Join();
        }
    }

    void OnApplicationQuit()
    {
        DisconnectFromServer();
    }
}
