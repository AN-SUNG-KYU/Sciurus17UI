using System;
using System.Threading;

namespace Sciurus17.ControlSystem
{
    class MainControlSystem
    {
        [STAThread]
        public static void Main()
        {
            Usercontrol usercontrol = new Usercontrol();

            usercontrol.Update();

/*            Thread Controlcore = new Thread(new ThreadStart(usercontrol.Update));　//制御のスレッド
            Controlcore.Start();
*/
        }
    }
}
