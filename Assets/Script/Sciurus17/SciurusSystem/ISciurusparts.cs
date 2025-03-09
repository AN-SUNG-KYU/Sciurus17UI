using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciurus17.SciurusSystem
{
    interface ISciurusparts ///SciurusのRightArm,Spin,LeftArmのインターフェイス
    {
        byte[] Id { get; set; }
        byte[] Mode { get; set; }
        double[] Position { get; set; }
        int[] GoalPosition { get; set; }
        double[] Velocity { get; set; }
        int[] GoalVelocity { get; set; }
        double[] Current { get; set; }
        int[] GoalCurrent { get; set; }
        double[] PWM { get; set; }
        bool Runnig { get; set; }

        bool FlagfeedbackAll { get; set; }
        bool FlagfeedbackCurrPosi { get; set; }
        bool FlagfeedbackVeloPosi { get; set; }
        void Motorboot();
        void Motoroff();
        sbyte Update();          ///危険時-1それ以外0

        double GetstatePosition(byte id);

        double[] GetstatePosition(byte[] id);

        double GetstateVelocity(byte id);

        double[] GetstateVelocity(byte[] id);

        double GetstateCurrent(byte id);

        double[] GetstateCurrent(byte[] id);

        void SetGoalPosition(byte id, double position);

        void SetGoalPosition(byte[] id, double[] position);

        void SetGoalVelocity(byte id, double velocity);

        void SetGoalVelocity(byte[] id, double[] velocity);

        void SetGoalCurrent(byte id, double currnt);

        void SetGoalCurrent(byte[] id, double[] currnt);

    }
}
