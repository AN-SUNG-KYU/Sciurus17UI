using System;
using System.Threading;
using System.Collections.Generic;

using static Sciurus17.Dynamixel.Robotis_def;

namespace Sciurus17.Dynamixel.ReadWrite
{
    public  static class Reader
    {
        private static readonly int STA_ID = 4;
        private static readonly int STA_PARA = 9;

        public static int[] ModelNumber(byte id)
        {
            var data = PacketHandler.Read(id, 0, 2);
            return new int[2] { data[STA_ID], Convert2byte(data) };
        }
        public static List<int[]> ModelNumber(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 0, 2);
            var ret = new List<int[]>();
            foreach(byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert2byte(d) });
            }
            return ret;
        }

        public static int[] ModelInformation(byte id)
        {
            var data = PacketHandler.Read(id, 2, 4);
            return new int[2] { data[STA_ID], Convert4byte(data) };
        }
        public static List<int[]> ModelInformation(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 2, 4);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert4byte(d) });
            }
            return ret;
        }

        public static int[] VersionOfFirmware(byte id)
        {
            var data = PacketHandler.Read(id, 6, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> VersionOfFirmware(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 6, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] ID(byte id)
        {
            var data = PacketHandler.Read(id, 7, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> ID(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 7, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] Baudrate(byte id)
        {
            var data = PacketHandler.Read(id, 8, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> Baudrate(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 8, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] RetuenDelayTime(byte id)
        {
            var data = PacketHandler.Read(id, 9, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> RetuenDelayTime(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 9, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] DriveMode(byte id)
        {
            var data = PacketHandler.Read(id, 10, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> DriveMode(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 10, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] OperatingMode(byte id)
        {
            var data = PacketHandler.Read(id, 11, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> OperatingMode(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 11, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] SecondaryID(byte id)
        {
            var data = PacketHandler.Read(id, 12, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> SecondaryID(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 12, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] ProtocolVersion(byte id)
        {
            var data = PacketHandler.Read(id, 13, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> ProtocolVersion(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 13, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] HomingOffset(byte id)
        {
            var data = PacketHandler.Read(id, 20, 4);
            return new int[2] { data[STA_ID], Convert4byte(data) };
        }
        public static List<int[]> HomingOffset(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 20, 4);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert4byte(d) });
            }
            return ret;
        }

        public static int[] MovingThreshold(byte id)
        {
            var data = PacketHandler.Read(id, 24, 4);
            return new int[2] { data[STA_ID], Convert4byte(data) };
        }
        public static List<int[]> MovingThreshold(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 24, 4);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert4byte(d) });
            }
            return ret;
        }

        public static int[] TemperatureLimit(byte id)
        {
            var data = PacketHandler.Read(id, 31, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> TemperatureLimit(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 31, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] MaxVoltageLimit(byte id)
        {
            var data = PacketHandler.Read(id, 32, 2);
            return new int[2] { data[STA_ID], Convert2byte(data) };
        }
        public static List<int[]> MaxVoltageLimit(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 32, 2);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert2byte(d) });
            }
            return ret;
        }

        public static int[] MinVoltageLimit(byte id)
        {
            var data = PacketHandler.Read(id, 34, 2);
            return new int[2] { data[STA_ID], Convert2byte(data) };
        }
        public static List<int[]> MinVoltageLimit(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 34, 2);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert2byte(d) });
            }
            return ret;
        }

        public static int[] PWMLimit(byte id)
        {
            var data = PacketHandler.Read(id, 36, 2);
            return new int[2] { data[STA_ID], Convert2byte(data) };
        }
        public static List<int[]> PWMLimit(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 36, 2);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert2byte(d) });
            }
            return ret;
        }

        public static int[] CurrentLimit(byte id)
        {
            var data = PacketHandler.Read(id, 38, 2);
            return new int[2] { data[STA_ID], Convert2byte(data) };
        }
        public static List<int[]> CurrentLimit(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 38, 2);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert2byte(d) });
            }
            return ret;
        }

        public static int[] AccelerationLimit(byte id)
        {
            var data = PacketHandler.Read(id, 40, 4);
            return new int[2] { data[STA_ID], Convert4byte(data) };
        }
        public static List<int[]> AccelerationLimit(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 40, 4);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert4byte(d) });
            }
            return ret;
        }

        public static int[] VelocityLimit(byte id)
        {
            var data = PacketHandler.Read(id, 44, 4);
            return new int[2] { data[STA_ID], Convert4byte(data) };
        }
        public static List<int[]> VelocityLimit(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 44, 4);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert4byte(d) });
            }
            return ret;
        }

        public static int[] MaxPositionLimit(byte id)
        {
            var data = PacketHandler.Read(id, 48, 4);
            return new int[2] { data[STA_ID], Convert4byte(data) };
        }
        public static List<int[]> MaxPositionLimit(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 48, 4);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert4byte(d) });
            }
            return ret;
        }

        public static int[] MinPositionLimit(byte id)
        {
            var data = PacketHandler.Read(id, 52, 4);
            return new int[2] { data[STA_ID], Convert4byte(data) };
        }
        public static List<int[]> MinPositionLimit(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 52, 4);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert4byte(d) });
            }
            return ret;
        }

        public static int[] ExternalPortMode1(byte id)
        {
            var data = PacketHandler.Read(id, 56, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> ExternalPortMode1(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 56, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] ExternalPortMode2(byte id)
        {
            var data = PacketHandler.Read(id, 57, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> ExternalPortMode2(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 57, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] ExternalPortMode3(byte id)
        {
            var data = PacketHandler.Read(id, 58, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> ExternalPortMode3(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 58, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] Shutdown(byte id)
        {
            var data = PacketHandler.Read(id, 63, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> Shutdown(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 63, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] TorqueEnable(byte id)
        {
            var data = PacketHandler.Read(id, 64, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> TorqueEnable(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 64, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] LED(byte id)
        {
            var data = PacketHandler.Read(id, 65, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> LED(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 65, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] StatusReturnLevel(byte id)
        {
            var data = PacketHandler.Read(id, 68, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> StatusReturnLevel(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 68, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] RegisteredInstruction(byte id)
        {
            var data = PacketHandler.Read(id, 69, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> RegisteredInstruction(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 69, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] HardwareErrorStatus(byte id)
        {
            var data = PacketHandler.Read(id, 70, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> HardwareErrorStatus(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 70, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] Velocity_I_Gain(byte id)
        {
            var data = PacketHandler.Read(id, 76, 2);
            return new int[2] { data[STA_ID], Convert2byte(data) };
        }
        public static List<int[]> Velocity_I_Gain(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 76, 2);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert2byte(d) });
            }
            return ret;
        }

        public static int[] Velocity_P_Gain(byte id)
        {
            var data = PacketHandler.Read(id, 78, 2);
            return new int[2] { data[STA_ID], Convert2byte(data) };
        }
        public static List<int[]> Velocity_P_Gain(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 78, 2);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert2byte(d) });
            }
            return ret;
        }

        public static int[] Position_D_Gain(byte id)
        {
            var data = PacketHandler.Read(id, 80, 2);
            return new int[2] { data[STA_ID], Convert2byte(data) };
        }
        public static List<int[]> Position_D_Gain(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 80, 2);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert2byte(d) });
            }
            return ret;
        }

        public static int[] Position_I_Gain(byte id)
        {
            var data = PacketHandler.Read(id, 82, 2);
            return new int[2] { data[STA_ID], Convert2byte(data) };
        }
        public static List<int[]> Position_I_Gain(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 82, 2);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert2byte(d) });
            }
            return ret;
        }

        public static int[] Position_P_Gain(byte id)
        {
            var data = PacketHandler.Read(id, 84, 2);
            return new int[2] { data[STA_ID], Convert2byte(data) };
        }
        public static List<int[]> Position_P_Gain(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 84, 2);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert2byte(d) });
            }
            return ret;
        }

        public static int[] FeedforwardAccelerationGain(byte id)
        {
            var data = PacketHandler.Read(id, 88, 2);
            return new int[2] { data[STA_ID], Convert2byte(data) };
        }
        public static List<int[]> FeedforwardAccelerationGain(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 88, 2);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert2byte(d) });
            }
            return ret;
        }

        public static int[] FeedforwardVelocityGain(byte id)
        {
            var data = PacketHandler.Read(id, 90, 2);
            return new int[2] { data[STA_ID], Convert2byte(data) };
        }
        public static List<int[]> FeedforwardVelocityGain(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 90, 2);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert2byte(d) });
            }
            return ret;
        }

        public static int[] BusWatchdog(byte id)
        {
            var data = PacketHandler.Read(id, 98, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> BusWatchdog(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 98, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] GoalPWM(byte id)
        {
            var data = PacketHandler.Read(id, 100, 2);
            return new int[2] { data[STA_ID], Convert2byte(data) };
        }
        public static List<int[]> GoalPWM(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 100, 2);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert2byte(d) });
            }
            return ret;
        }

        public static int[] GoalCurrent(byte id)
        {
            var data = PacketHandler.Read(id, 102, 2);
            return new int[2] { data[STA_ID], Convert2byte(data) };
        }
        public static List<int[]> GoalCurrent(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 102, 2);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert2byte(d) });
            }
            return ret;
        }

        public static int[] GoalVelocity(byte id)
        {
            var data = PacketHandler.Read(id, 104, 4);
            return new int[2] { data[STA_ID], Convert4byte(data) };
        }
        public static List<int[]> GoalVelocity(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 104, 4);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert4byte(d) });
            }
            return ret;
        }

        public static int[] ProfileAcceleration(byte id)
        {
            var data = PacketHandler.Read(id, 108, 4);
            return new int[2] { data[STA_ID], Convert4byte(data) };
        }
        public static List<int[]> ProfileAcceleration(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 108, 4);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert4byte(d) });
            }
            return ret;
        }

        public static int[] ProfileVelocity(byte id)
        {
            var data = PacketHandler.Read(id, 112, 4);
            return new int[2] { data[STA_ID], Convert4byte(data) };
        }
        public static List<int[]> ProfileVelocity(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 112, 4);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert4byte(d) });
            }
            return ret;
        }

        public static int[] GoalPosition(byte id)
        {
            var data = PacketHandler.Read(id, 116, 4);
            return new int[2] { data[STA_ID], Convert4byte(data) };
        }
        public static List<int[]> GoalPosition(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 116, 4);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert4byte(d) });
            }
            return ret;
        }

        public static int[] RealtimeTick(byte id)
        {
            var data = PacketHandler.Read(id, 120, 2);
            return new int[2] { data[STA_ID], Convert2byte(data) };
        }
        public static List<int[]> RealtimeTick(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 120, 2);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert2byte(d) });
            }
            return ret;
        }

        public static int[] Moving(byte id)
        {
            var data = PacketHandler.Read(id, 122, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> Moving(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 122, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] MovingStatus(byte id)
        {
            var data = PacketHandler.Read(id, 123, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> MovingStatus(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 123, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }

        public static int[] PresentPWM(byte id)
        {
            var data = PacketHandler.Read(id, 124, 2);
            return new int[2] { data[STA_ID], Convert2byte(data) };
        }
        public static List<int[]> PresentPWM(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 124, 2);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert2byte(d) });
            }
            return ret;
        }

        public static int[] PresentCurrent(byte id)
        {
            var data = PacketHandler.Read(id, 126, 2);
            return new int[2] { data[STA_ID], Convert2byte(data) };
        }
        public static List<int[]> PresentCurrent(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 126, 2);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert2byte(d) });
            }
            return ret;
        }

        public static int[] PresentVelocity(byte id)
        {
            var data = PacketHandler.Read(id, 128, 4);
            return new int[2] { data[STA_ID], Convert4byte(data) };
        }
        public static List<int[]> PresentVelocity(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 128, 4);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert4byte(d) });
            }
            return ret;
        }

        public static int[] PresentPosition(byte id)
        {
            var data = PacketHandler.Read(id, 132, 4);
            return new int[2] { data[STA_ID], Convert4byte(data) };
        }
        public static List<int[]> PresentPosition(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 132, 4);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert4byte(d) });
            }
            return ret;
        }

        public static int[] VelocityTrajectory(byte id)
        {
            var data = PacketHandler.Read(id, 136, 4);
            return new int[2] { data[STA_ID], Convert4byte(data) };
        }
        public static List<int[]> VelocityTrajectory(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 136, 4);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert4byte(d) });
            }
            return ret;
        }

        public static int[] PositionTrajectory(byte id)
        {
            var data = PacketHandler.Read(id, 140, 4);
            return new int[2] { data[STA_ID], Convert4byte(data) };
        }
        public static List<int[]> PositionTrajectory(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 140, 4);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert4byte(d) });
            }
            return ret;
        }

        public static int[] PresentInputVoltage(byte id)
        {
            var data = PacketHandler.Read(id, 144, 2);
            return new int[2] { data[STA_ID], Convert2byte(data) };
        }
        public static List<int[]> PresentInputVoltage(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 144, 2);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], Convert2byte(d) });
            }
            return ret;
        }

        public static int[] PresentTemperature(byte id)
        {
            var data = PacketHandler.Read(id, 145, 1);
            return new int[2] { data[STA_ID], data[STA_PARA] };
        }
        public static List<int[]> PresentTemperature(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 145, 1);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[2] { d[STA_ID], d[STA_PARA] });
            }
            return ret;
        }



        /// <summary>
        /// These functions obtain the Inromations of the Joint IDs.
        /// </summary>
        /// <param name="id">Joint ID</param>
        /// <returns>
        /// [0] : ID
        /// [1] : ModelNumber
        /// [2] : ModelInformation
        /// [3] : VersionOfFirmware
        /// [4] : ID
        /// [5] : Baudrate
        /// [6] : ReturnDelayTime
        /// [7] : DriveMode
        /// [8] : OperatingMode
        /// [9] : SecondaryId
        /// [10] : ProtocolVersion
        /// </returns>
        public static int[] Informations(byte id)
        {
            var data = PacketHandler.Read(id, 0, 14);
            return new int[11] { data[STA_ID],
                                 Convert2byte(data, STA_PARA),
                                 Convert4byte(data, STA_PARA + 2 ),
                                 data[STA_PARA + 3],
                                 data[STA_PARA + 4],
                                 data[STA_PARA + 5],
                                 data[STA_PARA + 6],
                                 data[STA_PARA + 7],
                                 data[STA_PARA + 8],
                                 data[STA_PARA + 9],
                                 data[STA_PARA + 10]};
        }
        public static List<int[]> Informations(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 0, 14);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[11] { d[STA_ID],
                                 Convert2byte(d, STA_PARA),
                                 Convert4byte(d, STA_PARA + 2 ),
                                 d[STA_PARA + 3],
                                 d[STA_PARA + 4],
                                 d[STA_PARA + 5],
                                 d[STA_PARA + 6],
                                 d[STA_PARA + 7],
                                 d[STA_PARA + 8],
                                 d[STA_PARA + 9],
                                 d[STA_PARA + 10]});
            }
            return ret;
        }


        /// <summary>
        /// These functions obtain the PresentCurrent(address:126,len:2), PresentVelocity(address:128,len:4) and PresentPosition(address:132,len:4) of the Joint IDs.
        /// </summary>
        /// <param name="id">Joint ID</param>
        /// <returns>
        /// [0] : ID
        /// [1] : current
        /// [2] : velocity
        /// [3] : position
        /// </returns>
        public static int[] PresentCurrVeloPosi(byte id)
        {
            var data = PacketHandler.Read(id, 126, 10);
            return new int[4] { data[STA_ID], Convert2byte(data, STA_PARA), Convert4byte(data, STA_PARA + 2), Convert4byte(data, STA_PARA + 6) };
        }
        public static List<int[]> PresentCurrVeloPosi(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 126, 10);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[4] { d[STA_ID], Convert2byte(d, STA_PARA), Convert4byte(d, STA_PARA + 2), Convert4byte(d, STA_PARA + 6) });
            }
            return ret;
        }

        /// <summary>
        /// These functions obtain the PresentVelocity(address:128,len:4) and PresentPosition(address:132,len:4) of the Joint IDs.
        /// </summary>
        /// <param name="id">Joint ID</param>
        /// <returns>
        /// [0] : ID
        /// [1] : velocity
        /// [2] : position
        /// </returns>
        public static int[] PresentVeloPosi(byte id)
        {
            var data = PacketHandler.Read(id, 128, 8);
            return new int[3] { data[STA_ID], Convert4byte(data, STA_PARA), Convert4byte(data, STA_PARA + 4) };
        }
        public static List<int[]> PresentVeloPosi(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 128, 8);
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[3] { d[STA_ID], Convert4byte(d, STA_PARA), Convert4byte(d, STA_PARA + 4) });
            }
            return ret;
        }

        public static int[] PresentCurrPosi(byte id)
        {
            var data = PacketHandler.Read(id, 126, 10);
            
            return new int[3] { data[STA_ID], Convert2byte(data, STA_PARA), Convert4byte(data, STA_PARA + 6) };
        }
        public static List<int[]> PresentCurrPosi(byte[] id)
        {
            var data = PacketHandler.SYNC_Read(id, 126, 10);
            
            var ret = new List<int[]>();
            foreach (byte[] d in data)
            {
                ret.Add(new int[3] { d[STA_ID], Convert2byte(d, STA_PARA), Convert4byte(d, STA_PARA + 6) });
            }
            return ret;
        }
    }
}
