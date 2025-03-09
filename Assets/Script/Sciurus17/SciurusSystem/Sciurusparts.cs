using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sciurus17.Dynamixel.ReadWrite;
using static Sciurus17.Dynamixel.Converter.SimpleConvert;
using Sciurus17.Port;
using SharpDX;
using System.Net.Security;
using System.Runtime.Remoting;
using static Sciurus17.SciurusSystem.Limit.Limitdegree;
using System.Diagnostics;
using static Sciurus17.Time.Time_management;


namespace Sciurus17.SciurusSystem
{
    public class Sciurusparts : ISciurusparts
    {
        public byte[] Id { get; set; }
        public byte[] Mode { get; set; }
        public double[] PWM { get; set; }
        public double[] Position { get; set; }
        public int[] GoalPosition { get; set; }
        public double[] Velocity { get; set; }
        public int[] GoalVelocity { get; set; }
        public double[] Current { get; set; }
        public int[] GoalCurrent { get; set; }
        public bool Runnig { get; set; } = true;//updateされているか確認する変数

        public bool FlagCurrntMode { get; set; } = false;
        public bool FlagVelocityMode { get; set; } = false;
        public bool FlagPositionMode { get; set; } = false;

        public bool FlagfeedbackAll { get; set; } = false;
        public bool FlagfeedbackCurrPosi { get; set; } = false;
        public bool FlagfeedbackVeloPosi { get; set; }  = false;

        private byte[] Id_SendGoalPosition;
        private byte[] Id_SendGoalVelocity;
        private byte[] Id_SendGoalCurrent;
        private int[] SendGoalPosition;
        private int[] SendGoalVelocity;
        private int[] SendGoalCurrent;
        int countMode0;
        int countMode1;
        int countMode3;
        private int CountMakesend0;
        private int CountMakesend1;
        private int CountMakesend3;

        private List<int[]> data;
        private double[] feedback_state;

        public Sciurusparts(byte[] id, byte[] mode) ///idは配列で設定 //portも指定
        {
            Id = new byte[id.Length];
            Id = id;
            Mode = new byte[id.Length];
            Mode = mode;
            Position = new double[id.Length];
            GoalPosition = new int[id.Length];
            Velocity = new double[id.Length];
            GoalVelocity = new int[id.Length];
            Current = new double[id.Length];
            GoalCurrent = new int[id.Length];
            SetSendGoalstate();

        }
        public sbyte Update()///更新
        {
            if (FlagfeedbackAll)Feedback_all(Id);   ///すべてのフィードバックを受け取る
            else if (FlagfeedbackCurrPosi)Feedback_CurrPosi(Id); ///電流と位置のみのフィードバックを受け取る
            else if (FlagfeedbackVeloPosi) Feedback_VeloPosi(Id); ///速度と位置のみのフィードバックを受け取る

            /*            SendGoalState();
            */
            if (Protectiondegree()) return -1;//角度保護 危険時-1

            MakeSendGoalstate();
            SendGoalState();

            return 0;
        }

        private void Feedback_all(byte id)///モーターの情報を取得
        {
            var data = Reader.PresentCurrVeloPosi(id);
            Current[0] = ConvertValueIntoCurrent(data[1]);
            Velocity[0] = ConvertValueIntoVelocity(data[2]);
            Position[0] = ConvertValueIntoPosition(data[3]);
        }
        private void Feedback_all(byte[] id)///モーターの電流、速度、位置を取得
        {
            if (id.Length == 1)
            {
                Feedback_all(id[0]);
            }
            else
            {
                data = Reader.PresentCurrVeloPosi(id);
                feedback_state = new double[id.Length * 3];

                for (int i = 0; i < id.Length; i++)
                {
                    Current[i] = ConvertValueIntoCurrent(data.Find(c => c[0] == id[i])[1]);
                    Velocity[i] = ConvertValueIntoVelocity(data.Find(v => v[0] == id[i])[2]);
                    Position[i] = ConvertValueIntoPosition(data.Find(p => p[0] == id[i])[3]);

                }
            }
        }

        private void Feedback_CurrPosi(byte id)///モーターの情報を取得
        {
            var data = Reader.PresentCurrPosi(id);
            Current[0] = ConvertValueIntoCurrent(data[1]);
            Position[0] = ConvertValueIntoPosition(data[2]);
        }
        private void Feedback_CurrPosi(byte[] id)///モーターの電流、速度、位置を取得
        {
            if (id.Length == 1)
            {
                Feedback_CurrPosi(id[0]);
            }
            else
            {
                data = Reader.PresentCurrPosi(id);
                feedback_state = new double[id.Length * 2];

                for (int i = 0; i < id.Length; i++)
                {
                    Current[i] = ConvertValueIntoCurrent(data.Find(c => c[0] == id[i])[1]);
                    Position[i] = ConvertValueIntoPosition(data.Find(p => p[0] == id[i])[2]);

                }
            }
        }

        private void Feedback_VeloPosi(byte id)///モーターの情報を取得
        {
            var data = Reader.PresentVeloPosi(id);
            Velocity[0] = ConvertValueIntoVelocity(data[1]);
            Position[0] = ConvertValueIntoPosition(data[2]);
        }
        private void Feedback_VeloPosi(byte[] id)///モーターの電流、速度、位置を取得
        {
            if (id.Length == 1)
            {
                Feedback_VeloPosi(id[0]);
            }
            else
            {
                data = Reader.PresentVeloPosi(id);
                feedback_state = new double[id.Length * 2];

                for (int i = 0; i < id.Length; i++)
                {
                    Velocity[i] = ConvertValueIntoVelocity(data.Find(c => c[0] == id[i])[1]);
                    Position[i] = ConvertValueIntoPosition(data.Find(p => p[0] == id[i])[2]);

                }
            }
        }

        private void SetSendGoalstate()
        {
            foreach (var item in Mode)Console.WriteLine("Mode{0}",item);
            
            countMode0 = Mode.Count(item => item == 0);
            if (countMode0 > 0)
            {
                SendGoalCurrent = new int[countMode0];
                Id_SendGoalCurrent = new byte[countMode0];
                FlagCurrntMode = true;
            }

            countMode1 = Mode.Count(item => item == 1);
            if (countMode1 > 0)
            {
                SendGoalVelocity = new int[countMode1];
                Id_SendGoalVelocity = new byte[countMode1];
                FlagVelocityMode = true;
            }

            countMode3 = Mode.Count(item => item == 3);
            if (countMode3 > 0)
            {
                SendGoalPosition = new int[countMode3];
                Id_SendGoalPosition = new byte[countMode3];
                FlagPositionMode = true;
            }
        }


        private void MakeSendGoalstate()
        {

            for (int i = 0; i < Id.Length; i++)
            {
                switch (Mode[i])
                {
                    case 0:
                        Id_SendGoalCurrent[CountMakesend0] = Id[i];
                        SendGoalCurrent[CountMakesend0] = GoalCurrent[i];
                        CountMakesend0++;
                        break;
                    case 1:
                        Id_SendGoalVelocity[CountMakesend1] = Id[i];
                        SendGoalVelocity[CountMakesend1] = GoalVelocity[i];
                        CountMakesend1++;
                        break;
                    case 3:
                        Id_SendGoalPosition[CountMakesend3] = Id[i];
                        SendGoalPosition[CountMakesend3] = GoalPosition[i];
                        CountMakesend3++;
                        break;
                    default:
                        throw new InvalidOperationException($"無効なモード値: {Mode[i]}");
                }
            }
            CountMakesend0 = 0; CountMakesend1 = 0; CountMakesend3 = 0;
        }


        private void SendGoalState()
        {
            if (Id.Length == 1)
            {
                if (FlagCurrntMode)
                {
                    Writer.GoalCurrent(Id[0], GoalCurrent[0]);
                    Sciurus_SleepTime(300.0);
                }
                if (FlagVelocityMode)
                {
                    Writer.GoalVelocity(Id[0], GoalVelocity[0]);
                    Sciurus_SleepTime(300.0);
                }
                if (FlagPositionMode)
                {
                    Writer.GoalPosition(Id[0], GoalPosition[0]);
                }
            }
            else
            {
                if (FlagCurrntMode)
                {
                    Writer.GoalCurrent(Id_SendGoalCurrent, SendGoalCurrent);
                    Sciurus_SleepTime(500.0);
                }
                if (FlagVelocityMode)
                {
                    Writer.GoalVelocity(Id_SendGoalVelocity, SendGoalVelocity);
                    Sciurus_SleepTime(500.0);
                }
                if (FlagPositionMode)
                {
                    Writer.GoalPosition(Id_SendGoalPosition, SendGoalPosition);
                    Sciurus_SleepTime(500.0);
                }
            }
        }

        private bool Protectiondegree() ///falseの時、安全角度内　trueの時、危険角度
        {
            foreach (byte id in Id)
            {
                if (!(judgedegree(id, GetstatePosition(id)))) return true; 
            }
            return false;
        }


        public void Motorboot()//複数のモーターをモードを決めて起動する
        {
            if(Mode.Length == 1)
            {
                Writer.TorqueEnable(Id[0], 0);
                Thread.Sleep(10);
                Writer.OperatingMode(Id[0], Mode[0]);
                Thread.Sleep(10);
                Writer.TorqueEnable(Id[0], 1);
                Thread.Sleep(10);
            }
            else
            {
                Writer.TorqueEnable(Id, 0);//0にすると書き込み可能
                Thread.Sleep(10);
                Writer.OperatingMode(Id, Mode);//ここで変更
                Thread.Sleep(10);
                Writer.TorqueEnable(Id, 1);//１に戻して保護している？
                Thread.Sleep(10);
            }
        }

        public void Motoroff()//複数のモーターのトルクをoffにする
        {
            if (Id.Length == 1) Writer.TorqueEnable(Id[0], 0);
            else Writer.TorqueEnable(Id, 0);
            Thread.Sleep(10);
        }


/*        public void SetPort(string port) ///1度だけ呼ぶ
        {   //指定のポートopen
            PortHandler.SetPortName(port);
            PortHandler.PortOpen();
        }*/
        
        ///これより下インターフェイスのメソッド
        
        public double GetstatePosition(byte id)
        {
            return Position[Array.IndexOf(Id, id)];
        }

        public double[] GetstatePosition(byte[] id)
        {
            feedback_state = new double[id.Length];
            for (int i = 0; i < id.Length; i++)
            {
                feedback_state[i] = Position[Array.IndexOf(Id, id[i])];
            }
            return feedback_state;
        }

        public double GetstateVelocity(byte id)
        {
            return Velocity[Array.IndexOf(Id, id)];
        }

        public double[] GetstateVelocity(byte[] id)
        {
            feedback_state = new double[id.Length];
            for (int i = 0; i < id.Length; i++)
            {
                feedback_state[i] = Velocity[Array.IndexOf(Id, id[i])];
            }
            return feedback_state;
        }

        public double GetstateCurrent(byte id)
        {
            return Current[Array.IndexOf(Id, id)];
        }

        public double[] GetstateCurrent(byte[] id)
        {
            feedback_state = new double[id.Length];
            for (int i = 0; i < id.Length; i++)
            {
                feedback_state[i] = Current[Array.IndexOf(Id, id[i])];
            }
            return feedback_state;
        }

        public void SetGoalPosition(byte id, double position)
        {

            GoalPosition[Array.IndexOf(Id, id)] = ConvertPositionIntoValue(position);
        }

        public void SetGoalPosition(byte[] id, double[] position)
        {
            for (int i = 0; i < id.Length; i++)
            {
                GoalPosition[Array.IndexOf(Id, id[i])] = ConvertPositionIntoValue(position[i]);
            }
        }

        public void SetGoalVelocity(byte id, double velocity)
        {
            GoalVelocity[Array.IndexOf(Id, id)] = ConvertVelocityIntoValue(velocity);

        }
        

        public void SetGoalVelocity(byte[] id, double[] velocity)
        {
            for (int i = 0; i < id.Length; i++)
            {
                GoalVelocity[Array.IndexOf(Id, id[i])] = ConvertVelocityIntoValue(velocity[i]);
            }
        }

        public void SetGoalCurrent(byte id, double currnt)
        {
            GoalCurrent[Array.IndexOf(Id, id)] = ConvertCurrentIntoValue(currnt);
        }

        public void SetGoalCurrent(byte[] id, double[] currnt)
        {
            for (int i = 0; i < id.Length ; i++)
            {
                GoalCurrent[Array.IndexOf(Id, id[i])] = ConvertCurrentIntoValue(currnt[i]);
            }
        }


    }




}
