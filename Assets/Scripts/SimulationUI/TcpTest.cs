using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;

public class TcpTest : MonoBehaviour
{
    public string serverIP = "127.0.0.1";  // 자기 자신 (localhost)
    public int serverPort = 50000;         // 연결할 포트

    void Start()
    {
        Debug.Log("Unity: TCP接続テストを開始します。");

        try
        {
            // TCP 클라이언트 생성 및 서버에 연결
            TcpClient client = new TcpClient();
            client.Connect(serverIP, serverPort);

            Debug.Log($"Unity: サーバー {serverIP}:{serverPort} に接続成功！");

            // 서버에 간단한 메시지 전송
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.UTF8.GetBytes("Unityからのテストメッセージ");
            stream.Write(data, 0, data.Length);
            Debug.Log("Unity: サーバーにメッセージを送信しました。");

            // 서버 응답 수신
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Debug.Log("Unity: サーバーからの応答: " + response);

            // 연결 종료
            stream.Close();
            client.Close();
            Debug.Log("Unity: 接続を終了しました。");
        }
        catch (Exception ex)
        {
            Debug.LogError("Unity: サーバー接続に失敗しました。エラー: " + ex.Message);
        }
    }
}
