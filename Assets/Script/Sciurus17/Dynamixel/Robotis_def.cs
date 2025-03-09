
namespace Sciurus17.Dynamixel
{
    public static class Robotis_def
    {
        public static ushort uMakeWord(byte a, byte b)
        {
            return (ushort)((a & 0xFF) | ((b & 0xFF) << 8));
        }
        public static short MakeWord(byte a, byte b)
        {
            return (short)((a & 0xFF) | ((b & 0xFF) << 8));
        }
        public static uint uMakeDWord(int a, int b)
        {
            return (uint)((a & 0xFFFF) | ((b & 0xFFFF) << 16));
        }
        public static int MakeDWord(int a, int b)
        {
            return (a & 0xFFFF) | ((b & 0xFFFF) << 16);
        }
        public static byte GetLoByte(int b)
        {
            return (byte)(b & 0xFF);
        }
        public static byte GetHiByte(int b)
        {
            return (byte)((b >> 8) & 0xFF);
        }
        public static int GetLoWord(int s)
        {
            return s & 0xFFFF;
        }
        public static int GetHiWord(int s)
        {
            return (s >> 16) & 0xFFFF;
        }
        public static int Convert4byte(byte[] data, int index = 9)
        {
            return MakeDWord(MakeWord(data[index], data[index + 1]),
                             MakeWord(data[index + 2], data[index + 3]));
        }
        public static int Convert2byte(byte[] data, int index = 9)
        {
            return MakeWord(data[index], data[index + 1]);
        }
        
    }
}
