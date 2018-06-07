using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Huffman.Common.DongleHelper
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe class Dog
    {
        public static readonly uint SN = 218263;

        public uint DogBytes, DogAddr;
        public byte[] DogData;
        public uint Retcode;

        [DllImport("Win32dll.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint DogRead(uint idogBytes, uint idogAddr, byte* pdogData);
        [DllImport("Win32dll.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint DogWrite(uint idogBytes, uint idogAddr, byte* pdogData);

        public unsafe Dog()
        {
            DogData = new byte[100];
            DogBytes = 100;
            DogAddr = 0;
        }

        public unsafe void ReadDog()
        {
            DogAddr = 0;
            DogBytes = 100;

            fixed (byte* pDogData = &DogData[0])
            {
                Retcode = DogRead(DogBytes, DogAddr, pDogData);
            }
        }

        public unsafe void WriteDog()
        {
            DogAddr = 0;
            DogBytes = 100;

            fixed (byte* pDogData = &DogData[0])
            {
                Retcode = DogWrite(DogBytes, DogAddr, pDogData);
            }
        }

        public unsafe uint GetSerialNo()
        {
            uint result = 0;

            DogAddr = 0;
            DogBytes = 0;

            fixed (byte* pDogData = &DogData[0])
            {
                Retcode = DogRead(DogBytes, DogAddr, pDogData);
            }

            if (Retcode == 0)		//Success
            {
                fixed (byte* pB = DogData)
                {
                    uint* pUI = (uint*)pB;
                    result = *(pUI);
                }
            }

            return result;
        }

        public void CheckDog(string key, bool trial)
        {
            uint sn = this.GetSerialNo();
            if (0 == sn)
            {
                if (trial)
                {
                    uint trialCount = Properties.Settings.Default.TrialCount;
                    if (0 == trialCount) throw new Exception("试用版过期，请购买正版");
                    Properties.Settings.Default.TrialCount = trialCount - 1;
                    Properties.Settings.Default.Save();
                    return;
                }
                throw new Exception("没有发现加密狗");
            }
            if (Dog.SN != sn) throw new Exception("加密狗序列号不正确");

            this.ReadDog();
            string dogString = "";
            foreach (byte b in this.DogData)
            {
                if (b == 0) break;
                dogString += (char)b;
            }
            dogString = dogString.ToLower();

            if (dogString.IndexOf("huffman.hwang") == 0) return;//The Root Dog
            if (dogString.IndexOf("huffman") < 0) throw new Exception("加密狗没有公司特征");

            if (string.IsNullOrEmpty(key)) return;
            if (dogString.IndexOf(key.ToLower()) < 0) throw new Exception("加密狗中没找到相关信息");
        }
    }
}
