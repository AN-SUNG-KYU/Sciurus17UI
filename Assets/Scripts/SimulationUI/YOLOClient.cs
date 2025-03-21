using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class YOLOClient : MonoBehaviour
{
    private string serverUrl = "http://localhost:8000/detect";  // YOLOv5 FastAPI 서버 주소
    public Camera captureCamera;
    public RenderTexture renderTexture;

    void Start()
    {
        StartCoroutine(SendFrameToYOLO());
    }

    IEnumerator SendFrameToYOLO()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);  // 1초마다 전송 (조절 가능)

            // 카메라 프레임을 캡처
            Texture2D frame = CaptureFrame();
            byte[] imageBytes = frame.EncodeToPNG();
            Destroy(frame);

            // 이미지 전송
            WWWForm form = new WWWForm();
            form.AddBinaryData("file", imageBytes, "image.png", "image/png");

            using (UnityWebRequest www = UnityWebRequest.Post(serverUrl, form))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    string jsonResult = www.downloadHandler.text;
                    Debug.Log("YOLOv5 Detection: " + jsonResult);
                    ProcessYOLOData(jsonResult);
                }
                else
                {
                    Debug.LogError("Error: " + www.error);
                }
            }
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
        // JSON 데이터를 파싱하여 Unity 내 오브젝트로 변환하는 로직 추가
    }
}
