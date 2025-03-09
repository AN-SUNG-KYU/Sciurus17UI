using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciurus17.SciurusSystem
{
    public interface ISciurus ///Sciurusのインターフェイス、制御側のスレッドは基本ここのメソッドを使う　このクラスは起動したそれぞれのSciuruspartsに指令を出す
    {

        byte[] Id { get; set; } ///2～20までの動かしたいモーターのidを入れる
        byte[] Mode { get; set; }///動かしたいモーターのmodeを指定する。
        double[] Position { get; set; } ///現在の位置　度表記
        int[] GoalPosition { get; set; } ///位置の目標値
        double[] Velocity { get; set; } ///現在の速度　rad/s表記
        int[] GoalVelocity { get; set; } ///速度の目標値
        double[] Current { get; set; } ///現在の電流値 A表記
        int[] GoalCurrent { get; set; } ///目標の電流値

        double[] PWM { get; set; }
        sbyte Runnig { get; set; } ///危険時負の値

        
        void SetSciurus(byte[] id, byte[] mode, string portname); ///Sciurusの初期化

        void FinishSciurus(); ///Sciurusのモーターをオフ，ポートを閉じる

        sbyte Update();///更新のメソッド

        void Getbootstate();///どの部位が動いているかコンソールに出力

        double GetstatePosition(byte id); ///角度を返す度表記

        double[] GetstatePosition(byte[] id);

        double GetstateVelocity(byte id); ///速度を返すrad/s表記

        double[] GetstateVelocity(byte[] id); 

        double GetstateCurrent(byte id); ///電流を返すアンペアA表記

        double[] GetstateCurrent(byte[] id);

        void SetGoalPosition(byte id, double position); ///位置の目標値をセット mode=3と対応

        void SetGoalPosition(byte[] id, double[] position);

        void SetGoalVelocity(byte id, double velocity);　 ///速度の目標値をセット mode=1と対応

        void SetGoalVelocity(byte[] id, double[] velocity);

        void SetGoalCurrent(byte id, double currnt);　 ///電流の目標値をセット　mode=0と対応

        void SetGoalCurrent(byte[] id, double[] currnt);
    }
}
