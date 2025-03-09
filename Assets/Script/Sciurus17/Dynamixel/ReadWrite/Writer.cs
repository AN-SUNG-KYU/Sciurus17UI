namespace Sciurus17.Dynamixel.ReadWrite
{
    public static class Writer
    {
        public static void ID(byte id, byte para) => PacketHandler.Write(id, 7, para);
        public static void ID(byte[] id, byte para) => PacketHandler.SYNC_Write(id, 7, para);
        public static void ID(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 7, 1, para);

        public static void Baudrate(byte id, byte para) => PacketHandler.Write(id, 8, para);
        public static void Baudrate(byte[] id, byte para) => PacketHandler.SYNC_Write(id, 8, para);
        public static void Baudrate(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 8, 1, para);

        public static void RetuenDelayTime(byte id, byte para) => PacketHandler.Write(id, 9, para);
        public static void RetuenDelayTime(byte[] id, byte para) => PacketHandler.SYNC_Write(id, 9, para);
        public static void RetuenDelayTime(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 9, 1, para);

        public static void DriveMode(byte id, byte para) => PacketHandler.Write(id, 10, para);
        public static void DriveMode(byte[] id, byte para) => PacketHandler.SYNC_Write(id, 10, para);
        public static void DriveMode(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 10, 1, para);

        public static void OperatingMode(byte id, byte para) => PacketHandler.Write(id, 11, para);
        public static void OperatingMode(byte[] id, byte para) => PacketHandler.SYNC_Write(id, 11, para);
        public static void OperatingMode(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 11, 1, para);

        public static void SecondaryID(byte id, byte para) => PacketHandler.Write(id, 12, para);
        public static void SecondaryID(byte[] id, byte para) => PacketHandler.SYNC_Write(id, 12, para);
        public static void SecondaryID(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 12, 1, para);

        public static void ProtocolVersion(byte id, byte para) => PacketHandler.Write(id, 13, para);
        public static void ProtocolVersion(byte[] id, byte para) => PacketHandler.SYNC_Write(id, 13, para);
        public static void ProtocolVersion(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 13, 1, para);

        public static void HomingOffset(byte id, byte[] para) => PacketHandler.Write(id, 20, 4, para);
        public static void HomingOffset(byte id, int value) => PacketHandler.Write(id, 20, 4, value);
        public static void HomingOffset(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 20, 4, para);
        public static void HomingOffset(byte[] id, int[] value) =>  PacketHandler.SYNC_Write(id, 20, 4, value);

        public static void MovingThreshold(byte id, byte[] para) => PacketHandler.Write(id, 24, 4, para);
        public static void MovingThreshold(byte id, int value) => PacketHandler.Write(id, 24, 4, value);
        public static void MovingThreshold(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 24, 4, para);
        public static void MovingThreshold(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 24, 4, value);

        public static void TemperatureLimit(byte id, byte para) => PacketHandler.Write(id, 31, para);
        public static void TemperatureLimit(byte[] id, byte para) => PacketHandler.SYNC_Write(id, 31, para);
        public static void TemperatureLimit(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 31, 1, para);

        public static void MaxVoltageLimit(byte id, byte[] para) => PacketHandler.Write(id, 32, 2, para);
        public static void MaxVoltageLimit(byte id, int value) => PacketHandler.Write(id, 32, 2, value);
        public static void MaxVoltageLimit(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 32, 2, para);
        public static void MaxVoltageLimit(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 32, 2, value);

        public static void MinVoltageLimit(byte id, byte[] para) => PacketHandler.Write(id, 34, 2, para);
        public static void MinVoltageLimit(byte id, int value) => PacketHandler.Write(id, 34, 2, value);
        public static void MinVoltageLimit(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 34, 2, para);
        public static void MinVoltageLimit(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 34, 2, value);

        public static void PWMLimit(byte id, byte[] para) => PacketHandler.Write(id, 36, 2, para);
        public static void PWMLimit(byte id, int value) => PacketHandler.Write(id, 36, 2, value);
        public static void PWMLimit(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 36, 2, para);
        public static void PWMLimit(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 36, 2, value);
        
        public static void CurrentLimit(byte id, byte[] para) => PacketHandler.Write(id, 38, 2, para);
        public static void CurrentLimit(byte id, int value) => PacketHandler.Write(id, 38, 2, value);
        public static void CurrentLimit(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 38, 2, para);
        public static void CurrentLimit(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 38, 2, value);

        public static void AccelerationLimit(byte id, byte[] para) => PacketHandler.Write(id, 40, 4, para);
        public static void AccelerationLimit(byte id, int value) => PacketHandler.Write(id, 40, 4, value);
        public static void AccelerationLimit(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 40, 4, para);
        public static void AccelerationLimit(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 40, 4, value);

        public static void VelocityLimit(byte id, byte[] para) => PacketHandler.Write(id, 44, 4, para);
        public static void VelocityLimit(byte id, int value) => PacketHandler.Write(id, 44, 4, value);
        public static void VelocityLimit(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 44, 4, para);
        public static void VelocityLimit(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 44, 4, value);

        public static void MaxPositionLimit(byte id, byte[] para) => PacketHandler.Write(id, 48, 4, para);
        public static void MaxPositionLimit(byte id, int value) => PacketHandler.Write(id, 48, 4, value);
        public static void MaxPositionLimit(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 48, 4, para);
        public static void MaxPositionLimit(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 48, 4, value);

        public static void MinPositionLimit(byte id, byte[] para) => PacketHandler.Write(id, 52, 4, para);
        public static void MinPositionLimit(byte id, int value) => PacketHandler.Write(id, 52, 4, value);
        public static void MinPositionLimit(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 52, 4, para);
        public static void MinPositionLimit(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 52, 4, value);

        public static void ExternalPortMode1(byte id, byte para) => PacketHandler.Write(id, 56, para);
        public static void ExternalPortMode1(byte[] id, byte para) => PacketHandler.SYNC_Write(id, 56, para);
        public static void ExternalPortMode1(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 56, 1, para);

        public static void ExternalPortMode2(byte id, byte para) => PacketHandler.Write(id, 57, para);
        public static void ExternalPortMode2(byte[] id, byte para) => PacketHandler.SYNC_Write(id, 57, para);
        public static void ExternalPortMode2(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 57, 1, para);

        public static void ExternalPortMode3(byte id, byte para) => PacketHandler.Write(id, 58, para);
        public static void ExternalPortMode3(byte[] id, byte para) => PacketHandler.SYNC_Write(id, 58, para);
        public static void ExternalPortMode3(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 58, 1, para);

        public static void Shutdown(byte id, byte para) => PacketHandler.Write(id, 63, para);
        public static void Shutdown(byte[] id, byte para) => PacketHandler.SYNC_Write(id, 63, para);
        public static void Shutdown(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 63, 1, para);

        public static void TorqueEnable(byte id, byte para) => PacketHandler.Write(id, 64, para);
        public static void TorqueEnable(byte[] id, byte para) => PacketHandler.SYNC_Write(id, 64, para);
        public static void TorqueEnable(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 64, 1, para);

        public static void LED(byte id, byte para) => PacketHandler.Write(id, 65, para);
        public static void LED(byte[] id, byte para) => PacketHandler.SYNC_Write(id, 65, para);
        public static void LED(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 65, 1, para);

        public static void StatusReturnLevel(byte id, byte para) => PacketHandler.Write(id, 68, para);
        public static void StatusReturnLevel(byte[] id, byte para) => PacketHandler.SYNC_Write(id, 68, para);
        public static void StatusReturnLevel(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 68, 1, para);

        public static void Velocity_I_Gain(byte id, byte[] para) => PacketHandler.Write(id, 76, 2, para);
        public static void Velocity_I_Gain(byte id, int value) => PacketHandler.Write(id, 76, 2, value);
        public static void Velocity_I_Gain(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 76, 2, para);
        public static void Velocity_I_Gain(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 76, 2, value);

        public static void Velocity_P_Gain(byte id, byte[] para) => PacketHandler.Write(id, 78, 2, para);
        public static void Velocity_P_Gain(byte id, int value) => PacketHandler.Write(id, 78, 2, value);
        public static void Velocity_P_Gain(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 78, 2, para);
        public static void Velocity_P_Gain(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 78, 2, value);

        public static void Position_D_Gain(byte id, byte[] para) => PacketHandler.Write(id, 80, 2, para);
        public static void Position_D_Gain(byte id, int value) => PacketHandler.Write(id, 80, 2, value);
        public static void Position_D_Gain(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 80, 2, para);
        public static void Position_D_Gain(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 80, 2, value);


        public static void Position_I_Gain(byte id, byte[] para) => PacketHandler.Write(id, 82, 2, para);
        public static void Position_I_Gain(byte id, int value) => PacketHandler.Write(id, 82, 2, value);
        public static void Position_I_Gain(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 82, 2, para);
        public static void Position_I_Gain(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 82, 2, value);

        public static void Position_P_Gain(byte id, byte[] para) => PacketHandler.Write(id, 84, 2, para);
        public static void Position_P_Gain(byte id, int value) => PacketHandler.Write(id, 84, 2, value);
        public static void Position_P_Gain(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 84, 2, para);
        public static void Position_P_Gain(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 84, 2, value);

        public static void FeedforwardAccelerationGain(byte id, byte[] para) => PacketHandler.Write(id, 88, 2, para);
        public static void FeedforwardAccelerationGain(byte id, int value) => PacketHandler.Write(id, 88, 2, value);
        public static void FeedforwardAccelerationGain(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 88, 2, para);
        public static void FeedforwardAccelerationGain(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 88, 2, value);

        public static void FeedforwardVelocityGain(byte id, byte[] para) => PacketHandler.Write(id, 90, 2, para);
        public static void FeedforwardVelocityGain(byte id, int value) => PacketHandler.Write(id, 90, 2, value);
        public static void FeedforwardVelocityGain(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 90, 2, para);
        public static void FeedforwardVelocityGain(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 90, 2, value);

        public static void BusWatchdog(byte id, byte para) => PacketHandler.Write(id, 98, para);
        public static void BusWatchdog(byte[] id, byte para) => PacketHandler.SYNC_Write(id, 98, para);
        public static void BusWatchdog(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 98, 1, para);

        public static void GoalPWM(byte id, byte[] para) => PacketHandler.Write(id, 100, 2, para);
        public static void GoalPWM(byte id, int value) => PacketHandler.Write(id, 100, 2, value);
        public static void GoalPWM(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 100, 2, para);
        public static void GoalPWM(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 100, 2, value);

        public static void GoalCurrent(byte id, byte[] para) => PacketHandler.Write(id, 102, 2, para);
        public static void GoalCurrent(byte id, int value) => PacketHandler.Write(id, 102, 2, value);
        public static void GoalCurrent(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 102, 2, para);
        public static void GoalCurrent(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 102, 2, value);

        public static void GoalVelocity(byte id, byte[] para) => PacketHandler.Write(id, 104, 4, para);
        public static void GoalVelocity(byte id, int value) => PacketHandler.Write(id, 104, 4, value);
        public static void GoalVelocity(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 104, 4, para);
        public static void GoalVelocity(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 104, 4, value);
        public static void GoalVelocity1(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 104, 4, value);

        public static void ProfileAcceleration(byte id, byte[] para) => PacketHandler.Write(id, 108, 4, para);
        public static void ProfileAcceleration(byte id, int value) => PacketHandler.Write(id, 108, 4, value);
        public static void ProfileAcceleration(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 108, 4, para);
        public static void ProfileAcceleration(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 108, 4, value);

        public static void ProfileVelocity(byte id, byte[] para) => PacketHandler.Write(id, 112, 4, para);
        public static void ProfileVelocity(byte id, int value) => PacketHandler.Write(id, 112, 4, value);
        public static void ProfileVelocity(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 112, 4, para);
        public static void ProfileVelocity(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 112, 4, value);

        public static void GoalPosition(byte id, byte[] para) => PacketHandler.Write(id, 116, 4, para);
        public static void GoalPosition(byte id, int value) => PacketHandler.Write(id, 116, 4, value);
        public static void GoalPosition(byte[] id, byte[] para) => PacketHandler.SYNC_Write(id, 116, 4, para);
        public static void GoalPosition(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 116, 4, value);
        public static void GoalPosition1(byte[] id, int[] value) => PacketHandler.SYNC_Write(id, 116, 4, value);
    }
}
