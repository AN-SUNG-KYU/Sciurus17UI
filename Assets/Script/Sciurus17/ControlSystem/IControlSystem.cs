using Sciurus17.SciurusSystem;
using CR.WHILL.ControlSystem;
using Sciurus17.Input;

namespace Sciurus17.ControlSystem
{
    public interface IControlSystem
    {
        IController Padinput { get; set; }
        ISciurus Sciurus { get; set; }
        ICRControlSystem CR {  get; set; }
        void SetControlsystem(byte[] Id, byte[] Mode, string Sciurus_Portname, string Whill_Portname, string ip_address, bool Sciurus_On, bool Whill_On, bool Pad_online);


        /// <summary>
        /// idの角度、速度、電流値をコンソールに表示
        /// </summary>
        /// <param name="id"></param>
        void Write_Parameter(byte id);


        /// <summary>
        /// Sciurusの通信ループ周期[ms]をコンソールに表示
        /// </summary>
        void Write_time_Robotloop();


    }
}
