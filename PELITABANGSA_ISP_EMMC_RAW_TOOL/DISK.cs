using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;
namespace PELITABANGSA_ISP_EMMC_RAW_TOOL
{
    public class DISK
    {

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern SafeFileHandle CreateFile(string lpFileName, FileAccess dwDesiredAccess, FileShare dwShareMode, uint lpSecurityAttributes, FileMode dwCreationDisposition, uint dwFlagsAndAttributes, uint hTemplateFile);

        [DllImport("kernel32", CharSet = CharSet.Ansi, EntryPoint = "CreateFileA", ExactSpelling = true, SetLastError = true)]
        private static extern long CreateFiles(ref string lpFileName, int dwDesiredAccess, int dwShareMode, int lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplateFile);

        public static streamer CreateStream(string drive, FileAccess type)
        {
            DISK.streamer _streamer;
            DISK.streamer _streamer1;
            DISK.streamer _streamer2 = new DISK.streamer();
            int num = 0;
            while (true)
            {
                SafeFileHandle safeFileHandle = DISK.CreateFile(string.Concat("\\\\.\\", drive), type, FileShare.None, 0, FileMode.Open, 0, 0);
                if (safeFileHandle.IsInvalid)
                {
                    num++;
                    if (num > 6)
                    {
                        _streamer2.isERROR = true;
                        _streamer1 = _streamer2;
                        break;
                    }
                }
                else
                {
                    if (safeFileHandle.IsInvalid)
                    {
                        _streamer2.isERROR = true;
                        _streamer = _streamer2;
                    }
                    else
                    {
                        Stream fileStream = new FileStream(safeFileHandle, type);
                        _streamer2.SH = safeFileHandle;
                        _streamer2.STR = fileStream;
                        _streamer2.isERROR = false;
                        _streamer = _streamer2;
                    }
                    _streamer1 = _streamer;
                    break;
                }
            }
            return _streamer1;
        }

        [DllImport("kernel32", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        private static extern bool DeviceIoControl(IntPtr hDevice, int dwIoControlCode, IntPtr lpInBuffer, int nInBufferSize, IntPtr lpOutBuffer, int nOutBufferSize, ref int lpBytesReturned, IntPtr lpOverlapped);

        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false, SetLastError = true)]
        public static extern bool DeviceIoControl(IntPtr hDevice, uint dwIoControlCode, IntPtr lpInBuffer, uint nInBufferSize, ref long lpOutBuffer, uint nOutBufferSize, out uint lpBytesReturned, IntPtr lpOverlapped);
        public static object dismount(string vol)
        {
            object obj;
            string text = "\\\\.\\" + vol + ":";
            int value = (int)CreateFiles(ref text, -1073741824, 3, 0, 3, 0, 0);
            int num = 1;
            uint num1 = 0;
            while (true)
            {
                IntPtr hDevice = (IntPtr)value;
                int dwIoControlCode = 589856;
                IntPtr lpInBuffer = (IntPtr)0;
                int nInBufferSize = 0;
                IntPtr lpOutBuffer = (IntPtr)0;
                int nOutBufferSize = 0;
                int num2 = (int)num1;
                bool flag = DeviceIoControl(hDevice, dwIoControlCode, lpInBuffer, nInBufferSize, lpOutBuffer, nOutBufferSize, ref num2, (IntPtr)0);
                num1 = (uint)num2;
                if (!flag)
                {
                    num = num + 1;
                    if (num > 5)
                    {
                        obj = false;
                        break;
                    }
                }
                else
                {
                    obj = true;
                    break;
                }
            }
            return obj;
        }

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern int ReadFile(int hFile, ref string lpBuffer, int nNumberOfBytesToRead, ref int lpNumberOfBytesRead, int lpOverlapped);

        public static byte[] ReadSector(long startingsector, int numberofsectors, DISK.streamer iface)
        {
            byte[] numArray;
            byte[] numArray1 = new byte[numberofsectors];
            if (!iface.SH.IsInvalid)
            {
                if (iface.STR.CanRead)
                {
                    iface.STR.Seek(startingsector, SeekOrigin.Begin);
                    iface.STR.Read(numArray1, 0, numberofsectors);
                    numArray = numArray1;
                    return numArray;
                }
            }
            numArray = null;
            return numArray;
        }

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern int WriteFile(int hFile, ref int lpBuffer, int nNumberOfBytesToWrite, ref int lpNumberOfBytesWritten, int lpOverlapped);

        public static int WriteSector(long startingsector, int numberofsectors, byte[] data, DISK.streamer iface)
        {
            int num;
            if (!iface.SH.IsInvalid)
            {
                if (iface.STR.CanRead)
                {
                    iface.STR.Seek(startingsector, SeekOrigin.Begin);
                    iface.STR.Write(data, 0, numberofsectors);
                    iface.STR.Flush();
                    num = 0;
                    return num;
                }
            }
            num = -1;
            return num;
        }

        public static byte[] EraseSector(long start, int number, streamer iface)
        {
            byte[] numArray;
            byte[] numArray1 = new byte[number];
            if (!iface.SH.IsInvalid)
            {
                if (iface.STR.CanRead)
                {
                    iface.STR.Seek(start, SeekOrigin.Begin);
                    iface.STR.Write(numArray1, 0, number);
                    numArray = numArray1;
                    return numArray;
                }
            }
            numArray = null;
            return numArray;
        }
        
        public static long GetDiskSize(SafeFileHandle Handle)
        {
            long num = 0L;
            uint num2;
            long result;
            if (DeviceIoControl(Handle.DangerousGetHandle(), 475228U, IntPtr.Zero, 0U, ref num, 8U, out num2, IntPtr.Zero))
            {
                result = num;
            }
            else
            {
                result = 0L;
            }
            return result;
        }

        public static bool DropStream(streamer iface)
        {
            bool result;
            try
            {
                iface.STR.Close();
                iface.SH.Close();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public DISK()
        {
        }

        

        public struct streamer
        {
            public Stream STR;
            public SafeFileHandle SH;
            public bool isERROR;
        }
    }
}
