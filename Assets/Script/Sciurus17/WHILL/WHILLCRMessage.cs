using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUSWHILLCR
{
    public class WHILLCRMessage
    {
        const byte command_start = 0xAF;
        
        const byte command_size_start_data = 0x06;
        const byte command_size_stop_data = 0x02;
        const byte command_size_power = 0x03;
        const byte command_size_joystick = 0x05;

        const byte command_id_start_data = 0x00;
        const byte command_id_stop_data = 0x01;
        const byte command_id_power = 0x02;
        const byte command_id_joystick = 0x03;
        const byte command_id_speed_profile = 0x04;
        const byte command_id_velocity = 0x08;


        byte command_mode;
        byte command_forward;
        byte command_turn;
        byte checksum;

        public byte[] sendMessage;
        public int sendMessage_size;
        public byte[] receivedMessage;
        public int receivedMessage_size;

        byte[] command_interval;

        public WHILLCRMessage()
        {
            sendMessage = new byte[8];
            receivedMessage = new byte[34];
            command_interval = new byte[2];
        }

        public int setCommandPower(int mode=1)
        {
            command_mode =(byte) (mode & 0x01);
            checksum = (byte)(command_start ^ command_size_power ^ command_id_power ^ command_mode);

            sendMessage_size = 5;
            sendMessage[0] = command_start;
            sendMessage[1] = command_size_power;
            sendMessage[2] = command_id_power;
            sendMessage[3] = command_mode;
            sendMessage[4] = checksum;

            return sendMessage_size;
        }

        public int setCommandStartSendingData(int D0=1, int D1=100, int D2 = 0)
        {
            sendMessage_size = 8;
            command_interval = BitConverter.GetBytes((ushort)D1);
            sendMessage[0] = command_start;
            sendMessage[1] = command_size_start_data;
            sendMessage[2] = command_id_start_data;
            sendMessage[3] = (byte)(D0 & 0x01);
            sendMessage[4] = command_interval[1];
            sendMessage[5] = command_interval[0];
            sendMessage[6] = (byte)(D2 & 0xff);
            sendMessage[7] = (byte)(sendMessage[0] ^ sendMessage[1] ^ sendMessage[2] ^ sendMessage[3] ^ sendMessage[4] ^ sendMessage[5] ^ sendMessage[6]);

            return sendMessage_size;
        }

        public int setCommandStopSendingData()
        {
            sendMessage_size = 4;
            sendMessage[0] = command_start;
            sendMessage[1] = command_size_stop_data;
            sendMessage[2] = command_id_stop_data;
            sendMessage[3] = (byte)(sendMessage[0] ^ sendMessage[1] ^ sendMessage[2]);

            return sendMessage_size;
        }

        public int setCommandSetJoystick(int U0=0, int forward = 0, int turn = 0)
        {
            command_mode = (byte)(U0 & 0x01);
            command_forward = (byte)(forward & 0xff);
            command_turn = (byte)(turn & 0xff);

            sendMessage_size = 7;
            sendMessage[0] = command_start;
            sendMessage[1] = command_size_joystick;
            sendMessage[2] = command_id_joystick;
            sendMessage[3] = command_mode;
            sendMessage[4] = command_forward;
            sendMessage[5] = command_turn;
            sendMessage[6] = (byte)(sendMessage[0] ^ sendMessage[1] ^ sendMessage[2] ^ sendMessage[3] ^ sendMessage[4] ^ sendMessage[5]);

            return sendMessage_size;
        }
    
    }
}
