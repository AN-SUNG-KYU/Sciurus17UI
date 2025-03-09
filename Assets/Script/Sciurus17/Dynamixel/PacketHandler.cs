using System;
using System.Collections.Generic;
using Sciurus17.Port;
using static Sciurus17.Dynamixel.Robotis_def;

namespace Sciurus17.Dynamixel
{
    public static class PacketHandler
    {
        private static byte[] data;
        private static byte[] packet;
        private static byte[] p;
        private static List<byte[]> data_list;
        private static int[] crc_table = {0x0000,
                     0x8005, 0x800F, 0x000A, 0x801B, 0x001E, 0x0014, 0x8011,
                     0x8033, 0x0036, 0x003C, 0x8039, 0x0028, 0x802D, 0x8027,
                     0x0022, 0x8063, 0x0066, 0x006C, 0x8069, 0x0078, 0x807D,
                     0x8077, 0x0072, 0x0050, 0x8055, 0x805F, 0x005A, 0x804B,
                     0x004E, 0x0044, 0x8041, 0x80C3, 0x00C6, 0x00CC, 0x80C9,
                     0x00D8, 0x80DD, 0x80D7, 0x00D2, 0x00F0, 0x80F5, 0x80FF,
                     0x00FA, 0x80EB, 0x00EE, 0x00E4, 0x80E1, 0x00A0, 0x80A5,
                     0x80AF, 0x00AA, 0x80BB, 0x00BE, 0x00B4, 0x80B1, 0x8093,
                     0x0096, 0x009C, 0x8099, 0x0088, 0x808D, 0x8087, 0x0082,
                     0x8183, 0x0186, 0x018C, 0x8189, 0x0198, 0x819D, 0x8197,
                     0x0192, 0x01B0, 0x81B5, 0x81BF, 0x01BA, 0x81AB, 0x01AE,
                     0x01A4, 0x81A1, 0x01E0, 0x81E5, 0x81EF, 0x01EA, 0x81FB,
                     0x01FE, 0x01F4, 0x81F1, 0x81D3, 0x01D6, 0x01DC, 0x81D9,
                     0x01C8, 0x81CD, 0x81C7, 0x01C2, 0x0140, 0x8145, 0x814F,
                     0x014A, 0x815B, 0x015E, 0x0154, 0x8151, 0x8173, 0x0176,
                     0x017C, 0x8179, 0x0168, 0x816D, 0x8167, 0x0162, 0x8123,
                     0x0126, 0x012C, 0x8129, 0x0138, 0x813D, 0x8137, 0x0132,
                     0x0110, 0x8115, 0x811F, 0x011A, 0x810B, 0x010E, 0x0104,
                     0x8101, 0x8303, 0x0306, 0x030C, 0x8309, 0x0318, 0x831D,
                     0x8317, 0x0312, 0x0330, 0x8335, 0x833F, 0x033A, 0x832B,
                     0x032E, 0x0324, 0x8321, 0x0360, 0x8365, 0x836F, 0x036A,
                     0x837B, 0x037E, 0x0374, 0x8371, 0x8353, 0x0356, 0x035C,
                     0x8359, 0x0348, 0x834D, 0x8347, 0x0342, 0x03C0, 0x83C5,
                     0x83CF, 0x03CA, 0x83DB, 0x03DE, 0x03D4, 0x83D1, 0x83F3,
                     0x03F6, 0x03FC, 0x83F9, 0x03E8, 0x83ED, 0x83E7, 0x03E2,
                     0x83A3, 0x03A6, 0x03AC, 0x83A9, 0x03B8, 0x83BD, 0x83B7,
                     0x03B2, 0x0390, 0x8395, 0x839F, 0x039A, 0x838B, 0x038E,
                     0x0384, 0x8381, 0x0280, 0x8285, 0x828F, 0x028A, 0x829B,
                     0x029E, 0x0294, 0x8291, 0x82B3, 0x02B6, 0x02BC, 0x82B9,
                     0x02A8, 0x82AD, 0x82A7, 0x02A2, 0x82E3, 0x02E6, 0x02EC,
                     0x82E9, 0x02F8, 0x82FD, 0x82F7, 0x02F2, 0x02D0, 0x82D5,
                     0x82DF, 0x02DA, 0x82CB, 0x02CE, 0x02C4, 0x82C1, 0x8243,
                     0x0246, 0x024C, 0x8249, 0x0258, 0x825D, 0x8257, 0x0252,
                     0x0270, 0x8275, 0x827F, 0x027A, 0x826B, 0x026E, 0x0264,
                     0x8261, 0x0220, 0x8225, 0x822F, 0x022A, 0x823B, 0x023E,
                     0x0234, 0x8231, 0x8213, 0x0216, 0x021C, 0x8219, 0x0208,
                     0x820D, 0x8207, 0x0202 };

        private static readonly int PKT_HEADER0 = 0;
        private static readonly int PKT_HEADER1 = 1;
        private static readonly int PKT_HEADER2 = 2;
        private static readonly int PKT_RESERVED = 3;
        private static readonly int PKT_ID = 4;
        private static readonly int PKT_LENGTH_L = 5;
        private static readonly int PKT_LENGTH_H = 6;
        private static readonly int PKT_INSTRUCTION = 7;
        private static readonly int PKT_ERROR = 8;
        private static readonly int PKT_PARAMETERO = 8;

        private static readonly byte BROADCAST_ID = 0xFE;
        //private static readonly byte MAX_ID = 0xFC;

        private static readonly byte INST_PING = 0x01;
        private static readonly byte INST_READ = 0x02;
        private static readonly byte INST_WRITE = 0x03;
        private static readonly byte INST_REG_WRITE = 0x04;
        private static readonly byte INST_ACTION = 0x05;
        //private static readonly byte INST_FACTORY_RESET = 0x06;
        private static readonly byte INST_REBOOT = 0x08;
        //private static readonly byte INST_CLEAR = 0x10;
        //private static readonly byte INST_STATUS = 0x55;
        private static readonly byte INST_SYNC_READ = 0x82;
        private static readonly byte INST_SYNC_WRITE = 0x83;
        //private static readonly byte INST_BULK_READ = 0x92;
        //private static readonly byte INST_BULK_WRITE = 0x93;

        private static void CheckSum(ref byte[] packet)
        {
            int i;
            int crc_accum = 0;
            for (int j = 0; j < packet.Length - 2; j++)
            {
                i = ((crc_accum >> 8) ^ packet[j]) & 0xFF;
                crc_accum = ((crc_accum << 8) ^ crc_table[i]) & 0xFFFF;
            }

            packet[packet.Length - 2] = GetLoByte(crc_accum);
            packet[packet.Length - 1] = GetHiByte(crc_accum);
        }
        private static void AddStuffing(ref byte[] packet)
        {
            int index = PKT_INSTRUCTION;
            while (index < packet.Length - 2)
            {
                if (packet[index] == 0xFF && packet[index + 1] == 0xFF && packet[index + 2] == 0xFD)
                {
                    Array.Resize(ref packet, packet.Length + 1);
                    for (int i = packet.Length - 1; i > index + 3; i--) packet[i] = packet[i - 1];
                    packet[index + 3] = 0xFD;
                    packet[PKT_LENGTH_L] = GetLoByte(packet.Length - 7);
                    packet[PKT_LENGTH_H] = GetHiByte(packet.Length - 7);
                    index += 4;
                    continue;
                }
                index++;
            }
        }
        private static void RemoveStuffing(ref byte[] packet)
        {
            int index = PKT_INSTRUCTION;
            while (index < packet.Length - 3)
            {
                if (packet[index] == 0xFF && packet[index + 1] == 0xFF && packet[index + 2] == 0xFD && packet[index + 3] == 0xFD)
                {
                    for (int i = index + 3; i < packet.Length - 1; i++) packet[i] = packet[i + 1];
                    packet[packet.Length - 1] = PortHandler.ReadByte();
                    index += 3;
                    continue;
                }
                index++;
            }
        }

        private static void WritePacket(byte[] _packet)
        {
            AddStuffing(ref _packet);

            _packet[PKT_HEADER0] = 0xFF;
            _packet[PKT_HEADER1] = 0xFF;
            _packet[PKT_HEADER2] = 0xFD;
            _packet[PKT_RESERVED] = 0x00;
            CheckSum(ref _packet);

            int i = 0;
/*            foreach (var item in _packet)
            {
                Console.Write("[{0}]:{1} ",i,item);
                i++;
            }*/
            PortHandler.Write(_packet);
        }

        private static byte[] ReadPacket(int len = 0)
        {
            data = PortHandler.Read(len + 11);
            RemoveStuffing(ref data);

            return data;
        }

        public static byte[] Ping(byte id)
        {
            packet = new byte[10];
            packet[PKT_ID] = id;
            packet[PKT_LENGTH_L] = 3;
            packet[PKT_LENGTH_H] = 0;
            packet[PKT_INSTRUCTION] = INST_PING;

            WritePacket(packet);
            return ReadPacket(3);
        }

        public static byte[] Read(byte id, int address, int len)
        {
            packet = new byte[14];
            packet[PKT_ID] = id;
            packet[PKT_LENGTH_L] = 7;
            packet[PKT_LENGTH_H] = 0;
            packet[PKT_INSTRUCTION] = INST_READ;
            packet[PKT_PARAMETERO + 0] = GetLoByte(address);
            packet[PKT_PARAMETERO + 1] = GetHiByte(address);
            packet[PKT_PARAMETERO + 2] = GetLoByte(len);
            packet[PKT_PARAMETERO + 3] = GetHiByte(len);

            WritePacket(packet);
            return ReadPacket(len);
        }

        public static void Write(byte id, int address, byte param)
        {
            packet = new byte[13];
            packet[PKT_ID] = id;
            packet[PKT_LENGTH_L] = GetLoByte(6);
            packet[PKT_LENGTH_H] = GetHiByte(6);
            packet[PKT_INSTRUCTION] = INST_WRITE;
            packet[PKT_PARAMETERO + 0] = GetLoByte(address);
            packet[PKT_PARAMETERO + 1] = GetHiByte(address);
            packet[PKT_PARAMETERO + 2] = param;

            WritePacket(packet);
            ReadPacket();
        }

        public static void Write(byte id, int address, int len, byte[] param)
        {
            packet = new byte[12 + len];
            packet[PKT_ID] = id;
            packet[PKT_LENGTH_L] = GetLoByte(len + 5);
            packet[PKT_LENGTH_H] = GetHiByte(len + 5);
            packet[PKT_INSTRUCTION] = INST_WRITE;
            packet[PKT_PARAMETERO + 0] = GetLoByte(address);
            packet[PKT_PARAMETERO + 1] = GetHiByte(address);
            Array.Copy(param, 0, packet, PKT_PARAMETERO + 2, len);

            WritePacket(packet);
            ReadPacket();
        }
        public static void Write(byte id, int address, int len, int value)
        {
            p = new byte[len];
            if(len == 4)
            {
                p[0] = GetLoByte(GetLoWord(value));
                p[1] = GetHiByte(GetLoWord(value));
                p[2] = GetLoByte(GetHiWord(value));
                p[3] = GetHiByte(GetHiWord(value));
            }
            if(len == 2)
            {
                p[0] = GetLoByte(value);
                p[1] = GetHiByte(value);
            }
            Write(id, address, len, p);
        }

        public static void REG_Write(byte id, int address, int len, byte[] param)
        {
            packet = new byte[12 + len];
            packet[PKT_ID] = id;
            packet[PKT_LENGTH_L] = GetLoByte(len + 5);
            packet[PKT_LENGTH_H] = GetHiByte(len + 5);
            packet[PKT_INSTRUCTION] = INST_REG_WRITE;
            packet[PKT_PARAMETERO + 0] = GetLoByte(address);
            packet[PKT_PARAMETERO + 1] = GetHiByte(address);
            Array.Copy(param, 0, packet, PKT_PARAMETERO + 2, len);

            WritePacket(packet);
            ReadPacket();
        }

        public static void Action(byte id)
        {
            packet = new byte[10];
            packet[PKT_ID] = id;
            packet[PKT_LENGTH_L] = 3;
            packet[PKT_LENGTH_H] = 0;
            packet[PKT_INSTRUCTION] = INST_ACTION;

            WritePacket(packet);
            ReadPacket();
        }

        public static void ReBoot(byte id)
        {
            packet = new byte[10];
            packet[PKT_ID] = id;
            packet[PKT_LENGTH_L] = 3;
            packet[PKT_LENGTH_H] = 0;
            packet[PKT_INSTRUCTION] = INST_REBOOT;

            WritePacket(packet);
            ReadPacket();
        }

        public static List<byte[]> SYNC_Read(byte[] _id, int address, int len)
        {
            packet = new byte[14 + _id.Length];
            packet[PKT_ID] = BROADCAST_ID;
            packet[PKT_LENGTH_L] = GetLoByte(_id.Length + 7);
            packet[PKT_LENGTH_H] = GetHiByte(_id.Length + 7);
            packet[PKT_INSTRUCTION] = INST_SYNC_READ;
            packet[PKT_PARAMETERO + 0] = GetLoByte(address);
            packet[PKT_PARAMETERO + 1] = GetHiByte(address);
            packet[PKT_PARAMETERO + 2] = GetLoByte(len);
            packet[PKT_PARAMETERO + 3] = GetHiByte(len);
            Array.Copy(_id, 0, packet, PKT_PARAMETERO + 4, _id.Length);

            WritePacket(packet);
            
            data_list = new List<byte[]>();
            for (int i = 0; i < _id.Length; i++) data_list.Add(ReadPacket(len)); //20240917バグ発見

            return data_list;
        }

        public static void SYNC_Write(int address, int len, byte[] _param)
        {
            packet = new byte[14 + _param.Length];
            packet[PKT_ID] = BROADCAST_ID;
            packet[PKT_LENGTH_L] = GetLoByte(_param.Length + 7);
            packet[PKT_LENGTH_H] = GetHiByte(_param.Length + 7);
            packet[PKT_INSTRUCTION] = INST_SYNC_WRITE;
            packet[PKT_PARAMETERO + 0] = GetLoByte(address);
            packet[PKT_PARAMETERO + 1] = GetHiByte(address);
            packet[PKT_PARAMETERO + 2] = GetLoByte(len);
            packet[PKT_PARAMETERO + 3] = GetHiByte(len);
            Array.Copy(_param, 0, packet, PKT_PARAMETERO + 4, _param.Length);

            WritePacket(packet);
            //for (int i = 0; i < param.Length / (len + 1); i++) ReadPacket();

        }
        public static void SYNC_Write(byte[] _id, int address, byte param)
        {
            if (_id.Length == 1) Write(_id[0], address, param);
            else
            {
                p = new byte[_id.Length * 2];
                for (int i = 0; i < _id.Length; i++)
                {
                    p[2 * i] = _id[i];
                    p[2 * i + 1] = param;
                }
                SYNC_Write(address, 1, p);
            }
        }
        public static void SYNC_Write(byte[] _id, int address, int len, byte[] param)
        {
            if(_id.Length * len != param.Length) throw new ArgumentException();

            if (_id.Length == 1) Write(_id[0], address, len, param);
            else
            {
                p = new byte[_id.Length + param.Length];
                for (int i = 0; i < _id.Length; i++)
                {
                    p[i * (len + 1)] = _id[i];
                    Array.Copy(param, i * len, p, i * (len + 1) + 1, len);
                }
                SYNC_Write(address, len, p);
            }
        }

        public static void SYNC_Write(byte[] _id, int address, int len, int[] value)
        {
            if (value.Length != _id.Length) throw new ArgumentException();

            if (_id.Length == 1) Write(_id[0], address, len, value[0]);
            else
            {
                p = new byte[len * value.Length];
                if (len == 4)
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        p[4 * i] = GetLoByte(GetLoWord(value[i]));
                        p[4 * i + 1] = GetHiByte(GetLoWord(value[i]));
                        p[4 * i + 2] = GetLoByte(GetHiWord(value[i]));
                        p[4 * i + 3] = GetHiByte(GetHiWord(value[i]));
                    }
                }
                else if (len == 2)
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        p[2 * i] = GetLoByte(value[i]);
                        p[2 * i + 1] = GetHiByte(value[i]);
                    }
                }
                SYNC_Write(_id, address, len, p);
            }           
        }
    }
}
