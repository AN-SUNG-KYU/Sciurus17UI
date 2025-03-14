using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEditor.PackageManager;

public class TcpClientSelf : MonoBehaviour
{
    public string serverIP = "127.0.0.1";
    public int serverPort = 50000;

    private TcpClient client;
    private NetworkStream stream;
    private Thread clientThread;
    private bool isConnected = false;
    private bool isReceiving = false;

    private TcpUI uiController;

    void Start()
    {
        Debug.Log("Unity: TCPクライアントの初期化を開始します。");
        uiController = FindObjectOfType<TcpUI>();
    }

    public void ConnectToServer()
    {
        if (isConnected)
        {
            Debug.LogWarning("Unity: 既にサーバーに接続されています。");
            return;
        }

        try
        {
            client = new TcpClient();
            client.Connect(serverIP, serverPort);
            stream = client.GetStream();
            isConnected = true;
            isReceiving = true;

            Debug.Log($"Unity: サーバーに接続しました。IP: {serverIP}、ポート: {serverPort}");

            clientThread = new Thread(ReceiveData);
            clientThread.IsBackground = true;
            clientThread.Start();
        }
        catch (Exception ex)
        {
            Debug.LogError("Unity: サーバー接続に失敗しました。エラー: " + ex.Message);
        }
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

    void ReceiveData()
    {
        byte[] buffer = new byte[1024];
        Debug.Log("Unity: サーバーからのデータを待機しています。");

        while (isReceiving)
        {
            try
            {
                if (!isConnected || stream == null) break;

                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                {
                    Debug.Log("Unity: サーバーから接続終了通知を受け取りました。");
                    break;
                }

                string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Debug.Log("Unity: サーバーから受信したデータ: " + dataReceived);

                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    var uiController = FindObjectOfType<TcpUI>();
                    if (uiController != null)
                        uiController.UpdateReceivedData(dataReceived);
                });
            }
            catch (Exception ex)
            {
                if (isReceiving)  // 클라이언트가 강제 종료 중이 아닐 때만 에러 출력
                {
                    Debug.LogError("Unity: データ受信中にエラーが発生しました: " + ex.Message);
                }
                break;
            }
        }

        isConnected = false;
        isReceiving = false;
    }

    public void DisconnectFromServer()
    {
        if (!isConnected)
        {
            Debug.LogWarning("Unity: サーバーとの接続は既に切断されています。");
            return;
        }

        Debug.Log("Unity: サーバーとの接続を切断します。");
        isReceiving = false;
        isConnected = false;

        try
        {
            if (stream != null)
            {
                stream.Close();
                stream = null;
            }

            if (client != null)
            {
                client.Client.Shutdown(SocketShutdown.Both);
                client.Close();
                client = null;
                Debug.Log("Unity: サーバーとの接続を正常に終了しました。(4-way handshake 完了)");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Unity: 接続終了中にエラーが発生しました: " + ex.Message);
        }

        if (clientThread != null && clientThread.IsAlive)
        {
            clientThread.Join();
        }

        Debug.Log("Unity: TCPクライアント終了完了");
    }

    void OnApplicationQuit()
    {
        DisconnectFromServer();
    }
}
