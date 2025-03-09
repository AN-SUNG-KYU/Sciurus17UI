using System;
using System.Collections.Generic;
using System.Text;
using SharpDX.XInput;
using Sciurus17.TcpIp;
using System.Collections;

namespace Sciurus17.XBOXInput
{
    class XInput
    {
        State state;
        private Controller controller;
        public double[] input;
        public bool back = false;
        public bool start=false;
        double[] padstate;

        public void Setting()
        {
            controller=new Controller(UserIndex.One);
            input = new double[2];
        }

        /*public double[] GetInput()
        {
            state = controller.GetState();
            System.Threading.Thread.Sleep(10);

            if(!controller.IsConnected)
            {
                Console.WriteLine("XBOXのコントローラが接続されていません");
            }

            JoypadState st = new JoypadState();
            padstate = st.GetState();                       
            
            input[0] = padstate[st.Convert("LeftThumbY")] *20.0; //LeftThumbY
            input[1] = padstate[st.Convert("LeftThumbX")]*20.0; //LeftThumbX
         
            return input;
        }*/
    }
}
