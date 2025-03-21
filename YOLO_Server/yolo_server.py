import socket
import torch
import numpy as np
import cv2
import json
import struct
from ultralytics import YOLO

# YOLOv5 모델 로드 (yolov5m.pt 사용 예시)
# 모델을 custom 로드하려면 아래와 같이 수정할 수 있습니다.
# Load a COCO-pretrained YOLO11n model
model = YOLO("yolo11n.pt")


HOST = "127.0.0.1"  # Unity에서 연결할 IP
PORT = 50000        # Unity에서 보낸 데이터를 받을 포트

def receive_image(conn):
    print("서버: 이미지 데이터 크기(4바이트) 수신 대기 중...")
    data_size = conn.recv(4)
    if not data_size:
        print("서버: 데이터 크기를 받지 못했습니다.")
        return None
    
    img_size = struct.unpack("<I", data_size)[0]
    print(f"서버: 이미지 데이터 크기: {img_size} 바이트")
    img_data = b""
    
    while len(img_data) < img_size:
        packet = conn.recv(2995667127)
        if not packet:
            print("서버: 패킷 수신 실패, 클라이언트 연결 종료.")
            return None
        img_data += packet

    print(f"서버: 전체 이미지 데이터 수신 완료, 총 {len(img_data)} 바이트")
    nparr = np.frombuffer(img_data, np.uint8)
    img = cv2.imdecode(nparr, cv2.IMREAD_COLOR)
    if img is None:
        print("서버: 이미지 디코딩 실패!")
    else:
        print("서버: 이미지 디코딩 성공. 이미지 shape:", img.shape)
        # 원본 이미지도 확인 (디버깅용)
        cv2.imshow("Original Image", img)
        cv2.waitKey(1)
    return img


def detect_objects(img):
    print("서버: YOLO 모델로 이미지 분석 시작...")
    results = model(img)
    print("서버: YOLO 분석 완료.")

    # ultralytics YOLO는 결과를 리스트로 반환함.
    if len(results) > 0:
        # 첫 번째 결과 객체의 plot() 메서드를 호출하여 이미지에 경계 상자와 라벨을 표시한 이미지를 얻음.
        img_rendered = results[0].plot()
        cv2.imshow("YOLO Detection", img_rendered)
        cv2.waitKey(1)
    else:
        print("서버: 결과 이미지가 없습니다.")

    detections = []
    try:
        # 첫 번째 결과 객체의 pandas() 메서드를 사용해 감지된 객체 정보를 추출
        for result in results[0].pandas().xyxy[0].itertuples():
            detections.append({
                "class": result.name,
                "confidence": float(result.confidence),
                "x_min": float(result.xmin),
                "y_min": float(result.ymin),
                "x_max": float(result.xmax),
                "y_max": float(result.ymax)
            })
    except Exception as e:
        print("서버: 객체 감지 결과 파싱 중 오류 발생:", e)
    print(f"서버: 감지된 객체 수: {len(detections)}")
    return detections


def start_server():
    """TCP 서버 실행 및 디버그 로그 출력"""
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as server:
        server.bind((HOST, PORT))
        server.listen()
        print(f"서버: YOLO TCP 서버 실행 중... {HOST}:{PORT}")

        while True:
            conn, addr = server.accept()
            print(f"서버: 클라이언트 연결됨: {addr}")

            with conn:
                while True:
                    img = receive_image(conn)
                    if img is None:
                        print("서버: 클라이언트 연결 종료")
                        break

                    detections = detect_objects(img)
                    response = json.dumps({"detections": detections}).encode("utf-8")
                    print("서버: 분석 결과 JSON 전송 중...")
                    # JSON 데이터 전송
                    conn.sendall(response)
                    print("서버: 분석 결과 JSON 전송 완료.")

if __name__ == "__main__":
    start_server()
