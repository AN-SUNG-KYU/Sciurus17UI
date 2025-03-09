using Sciurus17.Dynamixel.ReadWrite;
using Sciurus17.Port;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Sciurus17.Dynamixel.Converter.SimpleConvert;

namespace Sciurus17.SciurusSystem
{
    public class Sciurus : ISciurus
    {
        private Sciurusparts RightArm;
        private Sciurusparts Spin;
        private Sciurusparts LeftArm;

        public byte[] Id { get; set; }
        public byte[] Mode { get; set; }
        public double[] PWM { get; set; }
        public double[] Position { get; set; }
        public int[] GoalPosition { get; set; }
        public double[] Velocity { get; set; }
        public int[] GoalVelocity { get; set; }
        public double[] Current { get; set; }
        public int[] GoalCurrent { get; set; }
        public sbyte Runnig { get; set; } = 0;//updateされているか確認する変数


        private byte[] Id_RightAll = new byte[] { 2, 3, 4, 5, 6, 7, 8, 9 };
        private byte[] Id_SpinAll = new byte[] { 18, 19, 20 };
        private byte[] Id_LeftAll = new byte[] { 10, 11, 12, 13, 14, 15, 16, 17 };
        private List<byte> ListId_Right = new List<byte>();
        private List<byte> ListId_Spin = new List<byte>();
        private List<byte> ListId_Left = new List<byte>();
        private byte[] Id_Right { get; set; } = new byte[] { 0 };///使っていないIdがnullで判定できない可能性があるので0を入れている
        private byte[] Id_Spin { get; set; } = new byte[] { 0 };
        private byte[] Id_Left { get; set; } = new byte[] { 0 };

        private List<byte> ListMode_Right = new List<byte>();
        private List<byte> ListMode_Spin = new List<byte>();
        private List<byte> ListMode_Left = new List<byte>();
        private byte[] Mode_Right { get; set; }
        private byte[] Mode_Spin { get; set; }
        private byte[] Mode_Left { get; set; }

        private bool BootRight = false; ///RightArmが起動しているか確認する変数
        private bool BootSpin  = false; ///Spinが起動しているか確認する変数
        private bool BootLeft = false;  ///LeftArmが起動しているか確認する変数

        private double[] feedback_state; ///ユーザーがstateを受けとるとき使う、一時的な配列

        public void SetSciurus(byte[] id, byte[] mode, string portname)
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

            Separate_IdMode(id, mode);///id_Right, id_Spin, id_Left, Mode_Right, Mode_Spin, Mode_Leftの初期化
            Setparts();///sciuruspartsのインスタンス化
            SetPort(portname); ///portの設定
            Motorboot(); ///モーターの起動
            FeedbackAll();///すべてのフィードバックを取得
        }

        public void FinishSciurus()
        {
            Motoroff();
            ClosePort();
        }

        public sbyte Update()
        {
            Getfeedback();///feedbackの受け取り各Stateに値を入れる

            if(BootRight)
            {
                Runnig += RightArm.Update();
/*                Thread.Sleep(0);
*/            }
            if (BootSpin)
            {
                Runnig += Spin.Update();
/*                Thread.Sleep(1);
*/            }
            if (BootLeft)
            {
                Runnig += LeftArm.Update();
/*                Thread.Sleep(1);
*/            }
            return Runnig;
        }

        private void Separate_IdMode(byte[] id, byte[] mode)///コンストラクタの引数id、modeをlistのRight, Spin, Leftに分けて代入する
        {

            for (int i = 0; i < id.Length; i++)
            {
                if (Id_RightAll.Contains(id[i]))
                {
                    BootRight = true;
                    ListId_Right.Add(id[i]);
                    ListMode_Right.Add(mode[i]);  
                }
                else if (Id_SpinAll.Contains(id[i]))
                {
                    BootSpin = true;
                    ListId_Spin.Add(id[i]);
                    ListMode_Spin.Add(mode[i]);
                }
                else if (Id_LeftAll.Contains(id[i]))
                {
                    BootLeft = true;
                    ListId_Left.Add(id[i]);
                    ListMode_Left.Add(mode[i]);
                }
                else Console.WriteLine("ID({0}) is not available.", i);
            }
            Set_IdMode();
        }

        private void Set_IdMode()///listのid, modeをそれぞれ代入する
            { 
                if (BootRight)
                {
                    Id_Right = new byte[ListId_Right.Count];
                    Id_Right = ListId_Right.ToArray();
                    Mode_Right = new byte[ListMode_Right.Count];
                    Mode_Right = ListMode_Right.ToArray();
                }

                if (BootSpin)
                {
                    Id_Spin = new byte[ListId_Spin.Count];
                    Id_Spin = ListId_Spin.ToArray();
                    Mode_Spin = new byte[ListMode_Spin.Count];
                    Mode_Spin = ListMode_Spin.ToArray();
                }
                if (BootLeft)
                {
                    Id_Left = new byte[ListId_Left.Count];
                    Id_Left = ListId_Left.ToArray();
                    Mode_Left = new byte[ListMode_Left.Count];
                    Mode_Left = ListMode_Left.ToArray();
                }
            }

        private void Setparts() ///Bootに応じてSciuruspartsをインスタンス化
        {
            if (BootRight) RightArm = new Sciurusparts(Id_Right, Mode_Right);
            if (BootSpin) Spin = new Sciurusparts(Id_Spin, Mode_Spin);
            if (BootLeft) LeftArm = new Sciurusparts(Id_Left, Mode_Left);
        }

        private void Getfeedback() ///それぞれのpartsからfeedbackを受け取る
        {
            for (int i = 0; i < Id.Length; i++)
            {
                if (Id_Right.Contains(Id[i]))
                {
                    Position[i] = RightArm.GetstatePosition(Id[i]);
                    Velocity[i] = RightArm.GetstateVelocity(Id[i]);
                    Current[i] = RightArm.GetstateCurrent(Id[i]);
                    continue;
                }
                else if (Id_Spin.Contains(Id[i]))
                {
                    Position[i] = Spin.GetstatePosition(Id[i]);
                    Velocity[i] = Spin.GetstateVelocity(Id[i]);
                    Current[i] = Spin.GetstateCurrent(Id[i]);
                    continue;
                }
                else if (Id_Left.Contains(Id[i]))
                {
                    Position[i] = LeftArm.GetstatePosition(Id[i]);
                    Velocity[i] = LeftArm.GetstateVelocity(Id[i]);
                    Current[i] = LeftArm.GetstateCurrent(Id[i]);
                    continue;
                }
            }
        }

        private void SetPort(string port) ///1度だけ呼ぶ
        {   //指定のポートopen
            PortHandler.SetPortName(port);
            PortHandler.PortOpen();
        }
        private void ClosePort()
        {
            PortHandler.PortClose();
        }
        private void Motorboot()
        {
            if (BootRight) RightArm.Motorboot();
            if (BootSpin) Spin.Motorboot();
            if (BootLeft) LeftArm.Motorboot();
            Thread.Sleep(10);
        }

        private void Motoroff()
        {
            if (BootRight) RightArm.Motoroff();
            if (BootSpin) Spin.Motoroff();
            if (BootLeft) LeftArm.Motoroff();
            Thread.Sleep(10);
        }


        ///これより下インターフェイスのメソッド
        public void Getbootstate() ///何のSciuruspartsが起動しているかコンソールに出力する
        {
            Console.Write("Rightarm {0}, Spin {1}, LeftArm{2}", BootRight, BootSpin, BootLeft);
            Console.WriteLine();
        }

        public void FeedbackAll()
        {
            if (BootRight) RightArm.FlagfeedbackAll = true;
            if (BootSpin) Spin.FlagfeedbackAll = true;
            if (BootLeft) LeftArm.FlagfeedbackAll = true;
        }
        public void FeedbackCurrPosi()
        {
            if (BootRight) RightArm.FlagfeedbackCurrPosi = true;
            if (BootSpin) Spin.FlagfeedbackCurrPosi = true;
            if (BootLeft) LeftArm.FlagfeedbackCurrPosi = true;
        }
        public void FeedbackVeloPosi()
        {
            if (BootRight) RightArm.FlagfeedbackVeloPosi = true;
            if (BootSpin) Spin.FlagfeedbackVeloPosi = true;
            if (BootLeft) LeftArm.FlagfeedbackVeloPosi = true;
        }

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
            if (Id_Right.Contains(id))RightArm.SetGoalPosition(id, position);
            else if (Id_Spin.Contains(id))Spin.SetGoalPosition(id, position);
            else if (Id_Left.Contains(id))LeftArm.SetGoalPosition(id, position);
        }

        public void SetGoalPosition(byte[] id, double[] position)
        {
            for (int i = 0; i < id.Length; i++)
            {
                GoalPosition[Array.IndexOf(Id, id[i])] = ConvertPositionIntoValue(position[i]);
                if (Id_Right.Contains(id[i])) RightArm.SetGoalPosition(id[i], position[i]);
                else if (Id_Spin.Contains(id[i])) Spin.SetGoalPosition(id[i], position[i]);
                else if (Id_Left.Contains(id[i])) LeftArm.SetGoalPosition(id[i], position[i]);
            }
        }

        public void SetGoalVelocity(byte id, double velocity)
        {
            GoalVelocity[Array.IndexOf(Id, id)] = ConvertVelocityIntoValue(velocity);
            if (Id_Right.Contains(id)) RightArm.SetGoalVelocity(id, velocity);
            else if (Id_Spin.Contains(id)) Spin.SetGoalVelocity(id, velocity);
            else if (Id_Left.Contains(id)) LeftArm.SetGoalVelocity(id, velocity);
        }


        public void SetGoalVelocity(byte[] id, double[] velocity)
        {
            for (int i = 0; i < id.Length; i++)
            {
                GoalVelocity[Array.IndexOf(Id, id[i])] = ConvertVelocityIntoValue(velocity[i]);
                if (Id_Right.Contains(id[i])) RightArm.SetGoalVelocity(id[i], velocity[i]);
                else if (Id_Spin.Contains(id[i])) Spin.SetGoalVelocity(id[i], velocity[i]);
                else if (Id_Left.Contains(id[i])) LeftArm.SetGoalVelocity(id[i], velocity[i]);
            }
        }

        public void SetGoalCurrent(byte id, double currnt)
        {
            GoalCurrent[Array.IndexOf(Id, id)] = ConvertCurrentIntoValue(currnt);
            if (Id_Right.Contains(id)) RightArm.SetGoalCurrent(id, currnt);
            else if (Id_Spin.Contains(id)) Spin.SetGoalCurrent(id, currnt);
            else if (Id_Left.Contains(id)) LeftArm.SetGoalCurrent(id, currnt);
        }

        public void SetGoalCurrent(byte[] id, double[] currnt)
        {
            for (int i = 0; i < id.Length; i++)
            {
                GoalCurrent[Array.IndexOf(Id, id[i])] = ConvertCurrentIntoValue(currnt[i]);
                if (Id_Right.Contains(id[i])) RightArm.SetGoalCurrent(id[i], currnt[i]);
                else if (Id_Spin.Contains(id[i])) Spin.SetGoalCurrent(id[i], currnt[i]);
                else if (Id_Left.Contains(id[i])) LeftArm.SetGoalCurrent(id[i], currnt[i]);
            }
        }

    }
}
