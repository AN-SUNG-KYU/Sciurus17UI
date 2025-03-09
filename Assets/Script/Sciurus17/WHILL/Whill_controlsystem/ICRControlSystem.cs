using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CR.WHILL.ControlSystem
{
    public interface ICRControlSystem
    {
        /// <summary>
        /// [0]+:前進,-:後進_[1]+:右旋回,-:左旋回
        /// </summary>
        sbyte[] whill_input { get; set; }

        void SetPort(string portname);

        void Run();
    }
}

