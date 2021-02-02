using MetroFramework.Forms;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;


namespace PELITABANGSA_ISP_EMMC_RAW_TOOL
{
    public partial class Form1 : MetroForm
    {
        private SafeFileHandle h;
        private List<RawDrives> rawdrives = new List<RawDrives>();
        private List<Partition> PartitionsList = new List<Partition>();
        private string driveselected = string.Empty;
        private string[] preset = new string[] { "128 KB", "512 KB", "1 MB", "8 MB", "16 MB", "32 MB", "64 MB", "128 MB", "256 MB", "512 MB", "1 GB", "4 GB", "8 GB", "16 GB", "32 GB", "64 GB", "128 GB" };


        public uint Revision = 0U;
        public uint HeaderCRC32 = 0U;
        public ulong HeaderLBA = 0UL;
        public uint HeaderSize = 0U;
        public uint uint_0 = 0U;
        public ulong ulong_0 = 0UL;
        public ulong HeaderBackupLBA = 0UL;
        public ulong FirstUsableLBAForPartitions = 0UL;
        public ulong LastUsableLBAForPartitions = 0UL;
        public ulong StartingLBAOfPartitionArray = 0UL;
        public uint NumPartitionEntries = 0U;
        public uint SizeOfPartitionEntry = 0U;
        public uint PartitionArrayCRC32 = 0U;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate ()
            {
                if (Width > 997)
                {
                    splitContainer1.SplitterDistance = (Width - 957) - 44;
                    splitContainer1.Panel1Collapsed = false;
                    log.Text = Log1.Text;
                }
                else
                {
                    if(splitContainer1.SplitterDistance == 266)
                    {
                        splitContainer1.SplitterDistance = 25;
                    }
                    splitContainer1.Panel1Collapsed = true;
                }
            }));
        }
        private long EmmcSize(string drive)
        {
            h = DISK.CreateFile(drive, FileAccess.ReadWrite, FileShare.None, 0, FileMode.Open, 0, 0);
            long diskSize = DISK.GetDiskSize(h);
            h.Close();
            return diskSize;
        }

        private void buttonrefreshDrive_Click(object sender, EventArgs e)
        {
            rawdrives.Clear();
            WqlObjectQuery query = new WqlObjectQuery("SELECT * FROM Win32_DiskDrive");
            ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(query);
            int num = 0;
            foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
            {
                ManagementObject managementObject = (ManagementObject)managementBaseObject;
                if (managementObject["MediaType"] != null && (managementObject["MediaType"].ToString().Contains("Removable") || managementObject["MediaType"].ToString().Contains("External")))
                {
                    RawDrives rawdrive = new RawDrives();
                    rawdrive.Index = num;
                    rawdrive.DeviceID = managementObject["DeviceID"].ToString().Replace("\\\\.\\", string.Empty);
                    rawdrive.Size = EmmcSize(managementObject["DeviceID"].ToString());
                    rawdrive.Description = managementObject["Description"].ToString();
                    rawdrive.Manufacturer = managementObject["Manufacturer"].ToString();
                    rawdrive.MediaType = managementObject["MediaType"].ToString();
                    rawdrive.Model = managementObject["Model"].ToString();
                    rawdrives.Add(rawdrive);
                    num++;
                }
            }
            comboBoxDrive.Items.Clear();
            for (int i = 0; i < rawdrives.Count; i++)
            {
                comboBoxDrive.Items.Add(rawdrives[i].DeviceID);
            }
            if (comboBoxDrive.Items.Count > 0)
            {
                comboBoxDrive.SelectedIndex = 0;
            }
            else
            {
                Log1.Clear();
            }
            statuslabelStatus.Text = "Drives refreshed";
        }

        private void comboBoxDrive_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log1.Clear();
            Log1.AppendText(string.Concat("Description \t= ", rawdrives[comboBoxDrive.SelectedIndex].Description, Environment.NewLine));
            Log1.AppendText(string.Concat("Manufacturer \t= ", rawdrives[comboBoxDrive.SelectedIndex].Manufacturer, Environment.NewLine));
            Log1.AppendText(string.Concat("MediaType \t= ", rawdrives[comboBoxDrive.SelectedIndex].MediaType, Environment.NewLine));
            Log1.AppendText(string.Concat("Model \t\t= ", rawdrives[comboBoxDrive.SelectedIndex].Model, Environment.NewLine));
            Log1.AppendText(string.Concat("Size \t\t= ", rawdrives[comboBoxDrive.SelectedIndex].Size.ToString(), " Bytes", Environment.NewLine));
            Log1.AppendText(string.Concat(new object[]
                     {
                        "Disk.Size \t\t= ",
                        long.Parse(rawdrives[comboBoxDrive.SelectedIndex].Size.ToString()) / 512L,
                        " Sectors",
                        Environment.NewLine
                     }));
            Log1.AppendText("Disk.Size \t\t= " + GetFileSizeInBytes(long.Parse(rawdrives[comboBoxDrive.SelectedIndex].Size.ToString())) + Environment.NewLine + Environment.NewLine);
            driveselected = comboBoxDrive.Text;
            Info();
            
        }

        private void Info()
        {
            try
            {
                DISK.streamer iface = DISK.CreateStream(driveselected, FileAccess.ReadWrite);
                try
                {
                    byte[] hdrBytes = DISK.ReadSector(0L, 526336, iface);
                    ulong num = BitConverter.ToUInt64(hdrBytes, 0);
                    num = BitConverter.ToUInt64(hdrBytes, 512);
                    if (num > 0UL)
                    {
                        try
                        {
                            GptHeader hdr = ByteArrayToStructureLittleEndian<GptHeader>(hdrBytes);
                            BeginInvoke(new MethodInvoker(delegate ()
                            {
                                try
                                {
                                    byte[] array = new byte[512];
                                    Array.Copy(hdrBytes, 512, array, 0, 512);
                                    hdrBytes = array;
                                    hdr = ByteArrayToStructureLittleEndian<GptHeader>(hdrBytes);
                                    Revision = ToUInt32(hdrBytes, 8U);
                                    HeaderSize = ToUInt32(hdrBytes, 12U);
                                    uint_0 = ToUInt32(hdrBytes, 16U);
                                    ulong_0 = ToUInt64(hdrBytes, 24U);
                                    HeaderBackupLBA = ToUInt64(hdrBytes, 32U);
                                    FirstUsableLBAForPartitions = ToUInt64(hdrBytes, 40U);
                                    LastUsableLBAForPartitions = ToUInt64(hdrBytes, 48U);
                                    StartingLBAOfPartitionArray = ToUInt64(hdrBytes, 72U);
                                    NumPartitionEntries = ToUInt32(hdrBytes, 80U);
                                    SizeOfPartitionEntry = ToUInt32(hdrBytes, 84U);
                                    PartitionArrayCRC32 = ToUInt32(hdrBytes, 88U);
                                    ulong startingLBAOfPartitionArray = StartingLBAOfPartitionArray;
                                    uint num2 = 512U / SizeOfPartitionEntry;
                                    Log1.ColorText("[ GPT  Header Information ]" + Environment.NewLine, Color.Brown);
                                    Log1.AppendText("[" + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.Revision = {0}", Revision) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.HeaderSize ={0}", HeaderSize) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.HeaderCRC32 = 0x{0:X8}", uint_0) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.HeaderLBA = 0x{0:X8}", ulong_0) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.HeaderBackupLBA = 0x{0:X8}", HeaderBackupLBA) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.FirstUsableLBAForPartitions = 0x{0:X8}", FirstUsableLBAForPartitions) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.LastUsableLBAForPartitions =0x{0:X8}", LastUsableLBAForPartitions) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.Start LBA of part arrray : 0x{0:X8}", StartingLBAOfPartitionArray) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.Num part entries : 0x{0:X8}", NumPartitionEntries) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.Size of part entry :{0}", SizeOfPartitionEntry) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.Part array CRC32 : 0x{0:X8}", PartitionArrayCRC32) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.BlockNum = 0x{0:X8}", startingLBAOfPartitionArray) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.EntriesPerBlock = 0x{0:X8}", num2) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.Guid = {0}", hdr.diskGuid.ToString().ToUpper()) + Environment.NewLine);
                                    Log1.AppendText("]" + Environment.NewLine);
                                }
                                catch
                                {
                                    Log1.ColorText("[ Disk Drive Not Found GPT  Header Information ]" + Environment.NewLine, Color.Red);
                                }
                                finally
                                {
                                    DISK.DropStream(iface);
                                }
                            }));
                        }
                        catch
                        {
                        }
                        finally
                        {
                            DISK.DropStream(iface);
                        }
                    }
                    else
                    {
                        try
                        {
                            GptHeader hdr = ByteArrayToStructureLittleEndian<GptHeader>(hdrBytes);
                            BeginInvoke(new MethodInvoker(delegate ()
                            {
                                try
                                {
                                    byte[] array = new byte[4096];
                                    Array.Copy(hdrBytes, 4096, array, 0, 4096);
                                    hdrBytes = array;
                                    hdr = ByteArrayToStructureLittleEndian<GptHeader>(hdrBytes);
                                    Revision = ToUInt32(hdrBytes, 8U);
                                    HeaderSize = ToUInt32(hdrBytes, 12U);
                                    uint_0 = ToUInt32(hdrBytes, 16U);
                                    ulong_0 = ToUInt64(hdrBytes, 24U);
                                    HeaderBackupLBA = ToUInt64(hdrBytes, 32U);
                                    FirstUsableLBAForPartitions = ToUInt64(hdrBytes, 40U);
                                    LastUsableLBAForPartitions = ToUInt64(hdrBytes, 48U);
                                    StartingLBAOfPartitionArray = ToUInt64(hdrBytes, 72U);
                                    NumPartitionEntries = ToUInt32(hdrBytes, 80U);
                                    SizeOfPartitionEntry = ToUInt32(hdrBytes, 84U);
                                    PartitionArrayCRC32 = ToUInt32(hdrBytes, 88U);
                                    ulong startingLBAOfPartitionArray = StartingLBAOfPartitionArray;
                                    uint num2 = 512U / SizeOfPartitionEntry;
                                    Log1.AppendText("<== GPT  Header Information ==> " + Environment.NewLine);
                                    Log1.AppendText("[" + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.Revision = {0}", Revision) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.HeaderSize ={0}", HeaderSize) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.HeaderCRC32 = 0x{0:X8}", uint_0) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.HeaderLBA = 0x{0:X8}", ulong_0) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.HeaderBackupLBA = 0x{0:X8}", HeaderBackupLBA) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.FirstUsableLBAForPartitions = 0x{0:X8}", FirstUsableLBAForPartitions) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.LastUsableLBAForPartitions =0x{0:X8}", LastUsableLBAForPartitions) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.Start LBA of part arrray : 0x{0:X8}", StartingLBAOfPartitionArray) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.Num part entries : 0x{0:X8}", NumPartitionEntries) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.Size of part entry :{0}", SizeOfPartitionEntry) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.Part array CRC32 : 0x{0:X8}", PartitionArrayCRC32) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.BlockNum = 0x{0:X8}", startingLBAOfPartitionArray) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.EntriesPerBlock = 0x{0:X8}", num2) + Environment.NewLine);
                                    Log1.AppendText(string.Format("Disk.Guid = {0}", hdr.diskGuid.ToString().ToUpper()) + Environment.NewLine);
                                    Log1.AppendText("]" + Environment.NewLine);
                                }
                                catch
                                {
                                    Log1.AppendText("[ Disk Drive Not Found GPT  Header Information ]" + Environment.NewLine);
                                }
                                finally
                                {
                                    DISK.DropStream(iface);
                                }
                            }));
                        }
                        catch
                        {
                        }
                        finally
                        {
                            DISK.DropStream(iface);
                        }
                    }
                }
                catch
                {
                }
                finally
                {
                }
            }
            catch
            {
            }
        }

        private struct GptHeader
        {
            public ulong signature;
            public uint revision;
            public uint headerSize;
            public uint headerCrc;
            public uint reserved;
            public ulong myLBA;
            public ulong alternateLBA;
            public ulong firstUsableLBA;
            public ulong lastUsableLBA;
            public Guid diskGuid;
            public ulong entryLBA;
            public uint entries;
            public uint entriesSize;
            public uint entriesCrc;
        }

        public static T ByteArrayToStructureLittleEndian<T>(byte[] bytes) where T : struct
        {
            GCHandle gchandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T result = (T)Marshal.PtrToStructure(gchandle.AddrOfPinnedObject(), typeof(T));
            gchandle.Free();
            return result;
        }

        public static uint ToUInt32(byte[] n, uint aPos)
        {
            return (uint)(n[(int)(aPos + 3U)] << 24 | n[(int)(aPos + 2U)] << 16 | n[(int)(aPos + 1U)] << 8 | n[(int)aPos]);
        }

        public static ulong ToUInt64(byte[] n, uint aPos)
        {
            return (ulong)n[(int)(aPos + 7U)] << 54 | (ulong)n[(int)(aPos + 6U)] << 48 | (ulong)n[(int)(aPos + 5U)] << 40 | (ulong)n[(int)(aPos + 4U)] << 32 | (ulong)n[(int)(aPos + 3U)] << 24 | (ulong)n[(int)(aPos + 2U)] << 16 | (ulong)n[(int)(aPos + 1U)] << 8 | n[(int)aPos];
        }

        public static string GetFileSizeInBytes(long TotalBytes)
        {
            string result;
            if (TotalBytes > 1073741824L)
            {
                decimal num = decimal.Divide(TotalBytes, 1073741824m);
                result = string.Format("{0:##.##} GB", num);
            }
            else if (TotalBytes > 1048576L)
            {
                decimal num2 = decimal.Divide(TotalBytes, 1048576m);
                result = string.Format("{0:##.##} MB", num2);
            }
            else if (TotalBytes > 1024L)
            {
                decimal num3 = decimal.Divide(TotalBytes, 1024m);
                result = string.Format("{0:##.##} KB", num3);
            }
            else if (TotalBytes > 0L)
            {
                decimal num4 = TotalBytes;
                result = string.Format("{0:##.##} Bytes", num4);
            }
            else
            {
                result = "0 Bytes";
            }
            return result;
        }

        private void buttonbrowse_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                textBoxFileName.Text = fileName;
            }
        }

        private void buttonLoadPartitions_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxFileName.Text != string.Empty)
                {
                    dataGridView1.Rows.Clear();
                    string text = textBoxFileName.Text;
                    byte[] array = new byte[512];
                    FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read);
                    try
                    {
                        fileStream.Read(array, 0, 512);
                        if (array[450] == 238)
                        {
                            fileStream.Seek(512L, SeekOrigin.Begin);
                            fileStream.Read(array, 0, 512);
                            byte[] a = new byte[] { 69, 70, 73, 32, 80, 65, 82, 84 };
                            byte[] a2 = new byte[] { array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7] };
                            byte[] array2 = new byte[] { array[72], array[73], array[74], array[75], array[76], array[77], array[78], array[79] };
                            if (!BitConverter.IsLittleEndian)
                            {
                                Array.Reverse(array2);
                            }
                            byte[] array3 = new byte[] { array[80], array[81], array[82], array[83] };
                            if (!BitConverter.IsLittleEndian)
                            {
                                Array.Reverse(array3);
                            }
                            byte[] array4 = new byte[] { array[84], array[85], array[86], array[87] };
                            if (!BitConverter.IsLittleEndian)
                            {
                                Array.Reverse(array4);
                            }
                            long num = BitConverter.ToInt64(array2, 0);
                            long num2 = BitConverter.ToInt32(array4, 0);
                            long num3 = BitConverter.ToInt32(array4, 0);
                            if (ByteArrayCompare(a, a2))
                            {
                                PartitionsList.Clear();
                                fileStream.Seek(num * 512L, SeekOrigin.Begin);
                                int num4 = 1;
                                int num5 = 1;
                                while (true)
                                {
                                    fileStream.Read(array, 0, 512);
                                    bool flag = false;
                                    int i = 0;
                                    while (true)
                                    {
                                        if (i < 4)
                                        {
                                            Partition partisi = new Partition();
                                            byte[] array5 = new byte[8];
                                            for (int j = 0; j < array5.Length; j++)
                                            {
                                                array5[j] = array[i * 128 + (32 + j)];
                                            }
                                            byte[] array6 = new byte[8];
                                            for (int k = 0; k < array6.Length; k++)
                                            {
                                                array6[k] = array[i * 128 + (40 + k)];
                                            }
                                            byte[] array7 = new byte[72];
                                            for (int l = 0; l < array7.Length; l++)
                                            {
                                                array7[l] = array[i * 128 + (56 + l)];
                                            }
                                            if (!BitConverter.IsLittleEndian)
                                            {
                                                Array.Reverse(array5);
                                                Array.Reverse(array6);
                                            }
                                            long num6 = BitConverter.ToInt64(array5, 0) * 512L;
                                            long num7 = BitConverter.ToInt64(array6, 0) * 512L;
                                            long num8 = num7 - num6 + 512L;
                                            string text2 = Encoding.Unicode.GetString(array7);
                                            partisi.Choosen = true;
                                            partisi.StartAdress = num6;
                                            partisi.EndAddress = num7;
                                            partisi.Size = num8;
                                            text2 = new string((from c in text2
                                                                where c <= '\u007f'
                                                                select c).ToArray<char>());
                                            partisi.PartitionName = text2.Trim().Replace("\0", string.Empty);
                                            if (num4 == 1 && partisi.StartAdress > 0L)
                                            {
                                                Partition partisi2 = new Partition();
                                                partisi2.Choosen = true;
                                                partisi2.ID = num4.ToString();
                                                partisi2.StartAdress = 0L;
                                                partisi2.EndAddress = partisi.StartAdress - 512L;
                                                partisi2.Size = partisi2.EndAddress - partisi2.StartAdress + 512L;
                                                partisi2.PartitionName = "MBR-GPT-OTHER";
                                                PartitionsList.Add(partisi2);
                                                num4++;
                                            }
                                            if (num8 <= 512L)
                                            {
                                                flag = true;
                                                break;
                                            }
                                            else
                                            {
                                                if (PartitionsList.Count > 0)
                                                {
                                                    if (PartitionsList[PartitionsList.Count - 1].StartAdress + PartitionsList[PartitionsList.Count - 1].Size != partisi.StartAdress)
                                                    {
                                                        Partition partisi3 = new Partition();
                                                        partisi3.Choosen = true;
                                                        partisi3.ID = "G" + num5.ToString();
                                                        partisi3.StartAdress = PartitionsList[PartitionsList.Count - 1].StartAdress + PartitionsList[PartitionsList.Count - 1].Size;
                                                        partisi3.EndAddress = partisi.StartAdress - 512L;
                                                        partisi3.Size = partisi3.EndAddress - partisi3.StartAdress + 512L;
                                                        partisi3.PartitionName = "GAPS #" + num5.ToString();
                                                        PartitionsList.Add(partisi3);
                                                        num5++;
                                                        partisi.ID = num4.ToString();
                                                        PartitionsList.Add(partisi);
                                                        num4++;
                                                    }
                                                    else
                                                    {
                                                        partisi.ID = num4.ToString();
                                                        PartitionsList.Add(partisi);
                                                        num4++;
                                                    }
                                                }
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    if (flag)
                                    {
                                        break;
                                    }
                                    num += 1L;
                                    fileStream.Seek(num * 512L, SeekOrigin.Begin);
                                }
                            }
                            else
                            {
                                MessageBox.Show("There is no partition data in the selected file or the partition type is not supported yet!", "Can't analyze file", MessageBoxButtons.OK);
                                return;
                            }
                        }
                        else if ((array[510] != 85 ? false : array[511] == 170))
                        {
                            int num9 = 1;
                            int num10 = 1;
                            byte[] array8 = new byte[16];
                            Array.Copy(array, 446, array8, 0, 16);
                            PartitionsList.Clear();
                            byte[] array9 = new byte[4];
                            byte[] array10 = new byte[4];
                            Array.Copy(array8, 8, array9, 0, 4);
                            Array.Copy(array8, 12, array10, 0, 4);
                            if (!BitConverter.IsLittleEndian)
                            {
                                Array.Reverse(array9);
                                Array.Reverse(array10);
                            }
                            Partition partisi4 = new Partition();
                            partisi4.Choosen = true;
                            partisi4.StartAdress = BitConverter.ToInt32(array9, 0) * 512;
                            partisi4.Size = BitConverter.ToInt32(array10, 0) * 512;
                            partisi4.EndAddress = partisi4.StartAdress + partisi4.Size - 1L;
                            partisi4.PartitionName = "P" + array8[4].ToString("X2");
                            if (num9 == 1 && partisi4.StartAdress > 0L)
                            {
                                Partition partisi5 = new Partition();
                                partisi5.Choosen = true;
                                partisi5.ID = num9.ToString();
                                partisi5.StartAdress = 0L;
                                partisi5.EndAddress = partisi4.StartAdress - 1L;
                                partisi5.Size = partisi5.EndAddress - partisi5.StartAdress + 1L;
                                partisi5.PartitionName = "MBR-EBR-OTHER";
                                PartitionsList.Add(partisi5);
                                num9++;
                            }
                            partisi4.ID = num9.ToString();
                            PartitionsList.Add(partisi4);
                            num9++;
                            Array.Copy(array, 462, array8, 0, 16);
                            array9 = new byte[4];
                            array10 = new byte[4];
                            Array.Copy(array8, 8, array9, 0, 4);
                            Array.Copy(array8, 12, array10, 0, 4);
                            if (!BitConverter.IsLittleEndian)
                            {
                                Array.Reverse(array9);
                                Array.Reverse(array10);
                            }
                            partisi4 = new Partition();
                            partisi4.Choosen = true;
                            partisi4.ID = num9.ToString();
                            partisi4.StartAdress = BitConverter.ToInt32(array9, 0) * 512L;
                            partisi4.Size = BitConverter.ToInt32(array10, 0) * 512L;
                            partisi4.EndAddress = partisi4.StartAdress + partisi4.Size - 1L;
                            partisi4.PartitionName = "P" + array8[4].ToString("X2");
                            PartitionsList.Add(partisi4);
                            num9++;
                            Array.Copy(array, 478, array8, 0, 16);
                            array9 = new byte[4];
                            array10 = new byte[4];
                            Array.Copy(array8, 8, array9, 0, 4);
                            Array.Copy(array8, 12, array10, 0, 4);
                            if (!BitConverter.IsLittleEndian)
                            {
                                Array.Reverse(array9);
                                Array.Reverse(array10);
                            }
                            partisi4 = new Partition();
                            partisi4.Choosen = true;
                            partisi4.ID = num9.ToString();
                            partisi4.StartAdress = BitConverter.ToInt32(array9, 0) * 512L;
                            partisi4.Size = BitConverter.ToInt32(array10, 0) * 512L;
                            partisi4.EndAddress = partisi4.StartAdress + partisi4.Size - 1L;
                            partisi4.PartitionName = "P" + array8[4].ToString("X2");
                            PartitionsList.Add(partisi4);
                            num9++;
                            Array.Copy(array, 494, array8, 0, 16);
                            array9 = new byte[4];
                            Array.Copy(array8, 8, array9, 0, 4);
                            if (!BitConverter.IsLittleEndian)
                            {
                                Array.Reverse(array9);
                            }
                            long num11 = BitConverter.ToInt32(array9, 0) * 512L;
                            byte[] array11 = new byte[524288];
                            fileStream.Seek(num11, SeekOrigin.Begin);
                            fileStream.Read(array11, 0, 524288);
                            Partition partisi6 = new Partition();
                            partisi6.Choosen = true;
                            partisi6.ID = num9.ToString();
                            partisi6.StartAdress = num11;
                            array8 = new byte[16];
                            Array.Copy(array11, 0, array, 0, 512);
                            Array.Copy(array, 446, array8, 0, 16);
                            Array.Copy(array8, 8, array9, 0, 4);
                            if (!BitConverter.IsLittleEndian)
                            {
                                Array.Reverse(array9);
                            }
                            partisi6.Size = BitConverter.ToInt32(array9, 0) * 512L + num11 + 512L - 512L - partisi6.StartAdress;
                            partisi6.EndAddress = partisi6.StartAdress + partisi6.Size - 1L;
                            partisi6.PartitionName = "EBR";
                            PartitionsList.Add(partisi6);
                            num9++;
                            long num12 = 0L;
                            long num13 = 1L;
                            while (true)
                            {
                                bool flag2 = false;
                                array8 = new byte[16];
                                Array.Copy(array11, num12, array, 0L, 512L);
                                if ((array[510] != 85 ? true : array[511] != 170))
                                {
                                    flag2 = true;
                                }
                                else
                                {
                                    Array.Copy(array, 446, array8, 0, 16);
                                    array9 = new byte[4];
                                    array10 = new byte[4];
                                    Array.Copy(array8, 8, array9, 0, 4);
                                    Array.Copy(array8, 12, array10, 0, 4);
                                    if (!BitConverter.IsLittleEndian)
                                    {
                                        Array.Reverse(array9);
                                        Array.Reverse(array10);
                                    }
                                    partisi4 = new Partition();
                                    partisi4.Choosen = true;
                                    partisi4.ID = num9.ToString();
                                    partisi4.StartAdress = BitConverter.ToInt32(array9, 0) * 512L + num11 + num13 * 512L - 512L;
                                    partisi4.Size = BitConverter.ToInt32(array10, 0) * 512L;
                                    partisi4.EndAddress = partisi4.StartAdress + partisi4.Size - 1L;
                                    partisi4.PartitionName = "P" + array8[4].ToString("X2");
                                    if (PartitionsList.Count > 0)
                                    {
                                        if (PartitionsList[PartitionsList.Count - 1].StartAdress + PartitionsList[PartitionsList.Count - 1].Size != partisi4.StartAdress)
                                        {
                                            Partition partisi7 = new Partition();
                                            partisi7.Choosen = true;
                                            partisi7.ID = "G" + num10.ToString();
                                            partisi7.StartAdress = PartitionsList[PartitionsList.Count - 1].StartAdress + PartitionsList[PartitionsList.Count - 1].Size;
                                            partisi7.EndAddress = partisi4.StartAdress - 1L;
                                            partisi7.Size = partisi7.EndAddress - partisi7.StartAdress + 1L;
                                            partisi7.PartitionName = "GAPS #" + num10.ToString();
                                            PartitionsList.Add(partisi7);
                                            num10++;
                                            partisi4.ID = num9.ToString();
                                            PartitionsList.Add(partisi4);
                                            num9++;
                                        }
                                        else
                                        {
                                            partisi4.ID = num9.ToString();
                                            PartitionsList.Add(partisi4);
                                            num9++;
                                            num13 += 1L;
                                        }
                                    }
                                }
                                if (flag2)
                                {
                                    break;
                                }
                                num12 += 512L;
                            }
                        }
                        else
                        {
                            MessageBox.Show("There is no partition data in the selected file or the partition type is not supported yet!", "Can't analyze file", MessageBoxButtons.OK);
                            return;
                        }
                        if (PartitionsList.Count > 0)
                        {
                            comboBox2.Items.Clear();
                            foreach (Partition partisi8 in PartitionsList)
                            {
                                if (!checkBoxloadpartition.Checked)
                                {
                                    if (partisi8.ID.Substring(0, 1) == "G")
                                    {
                                        continue;
                                    }
                                    int index2 = dataGridView1.Rows.Add();
                                    dataGridView1.Rows[index2].Cells["choose"].Value = partisi8.Choosen;
                                    dataGridView1.Rows[index2].Cells["id"].Value = partisi8.ID.ToString();
                                    dataGridView1.Rows[index2].Cells["name"].Value = partisi8.PartitionName.ToUpper().Replace("\0", string.Empty);
                                    dataGridView1.Rows[index2].Cells["startaddress"].Value = "0x" + partisi8.StartAdress.ToString("X16");
                                    dataGridView1.Rows[index2].Cells["endaddress"].Value = "0x" + partisi8.EndAddress.ToString("X16");
                                    dataGridView1.Rows[index2].Cells["length"].Value = "0x" + partisi8.Size.ToString("X16");
                                    comboBox2.Items.Add(partisi8.PartitionName.ToUpper().Replace("\0", string.Empty));
                                }
                                else
                                {
                                    int index = dataGridView1.Rows.Add();
                                    dataGridView1.Rows[index].Cells["choose"].Value = partisi8.Choosen;
                                    dataGridView1.Rows[index].Cells["id"].Value = partisi8.ID.ToString();
                                    dataGridView1.Rows[index].Cells["name"].Value = partisi8.PartitionName.ToUpper().Replace("\0", string.Empty);
                                    dataGridView1.Rows[index].Cells["startaddress"].Value = "0x" + partisi8.StartAdress.ToString("X16");
                                    dataGridView1.Rows[index].Cells["endaddress"].Value = "0x" + partisi8.EndAddress.ToString("X16");
                                    dataGridView1.Rows[index].Cells["length"].Value = "0x" + partisi8.Size.ToString("X16");
                                    comboBox2.Items.Add(partisi8.PartitionName.ToUpper().Replace("\0", string.Empty));
                                }
                            }
                            comboBox2.SelectedIndex = 0;
                        }
                        dataGridView1.ClearSelection();
                        VScrollBar vscrollBar = dataGridView1.Controls.OfType<VScrollBar>().First<VScrollBar>();
                        if (vscrollBar.Visible)
                        {
                            dataGridView1.Columns["startaddress"].Width = 135;
                            dataGridView1.Columns["endaddress"].Width = 135;
                            dataGridView1.Columns["length"].Width = 135;
                        }
                        else
                        {
                            dataGridView1.Columns["startaddress"].Width = 140;
                            dataGridView1.Columns["endaddress"].Width = 140;
                            dataGridView1.Columns["length"].Width = 140;
                        }
                    }
                    finally
                    {
                        fileStream.Close();
                    }
                }
                else
                {
                    MessageBox.Show("There is no file choosed, please pick one using browse button!", "No file", MessageBoxButtons.OK);
                }
            }
            catch (IOException oException)
            {
                MessageBox.Show("There's an error while trying to analyze selected file, check if the file is used by another process!", "Can't analyze file", MessageBoxButtons.OK);
            }
        }

        private static bool ByteArrayCompare(byte[] a1, byte[] a2)
        {
            return StructuralComparisons.StructuralEqualityComparer.Equals(a1, a2);
        }

        private void checkBoxloadpartition_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (PartitionsList.Count > 0)
            {
                comboBox2.Items.Clear();
                foreach (Partition partisi in PartitionsList)
                {
                    if (!checkBoxloadpartition.Checked)
                    {
                        if (partisi.ID.Substring(0, 1) == "G")
                        {
                            continue;
                        }
                        int index2 = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index2].Cells["choose"].Value = partisi.Choosen;
                        dataGridView1.Rows[index2].Cells["id"].Value = partisi.ID.ToString();
                        dataGridView1.Rows[index2].Cells["name"].Value = partisi.PartitionName.ToUpper().Replace("\0", string.Empty);
                        dataGridView1.Rows[index2].Cells["startaddress"].Value = "0x" + partisi.StartAdress.ToString("X16");
                        dataGridView1.Rows[index2].Cells["endaddress"].Value = "0x" + partisi.EndAddress.ToString("X16");
                        dataGridView1.Rows[index2].Cells["length"].Value = "0x" + partisi.Size.ToString("X16");
                        comboBox2.Items.Add(partisi.PartitionName.ToUpper().Replace("\0", string.Empty));
                    }
                    else
                    {
                        int index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells["choose"].Value = partisi.Choosen;
                        dataGridView1.Rows[index].Cells["id"].Value = partisi.ID.ToString();
                        dataGridView1.Rows[index].Cells["name"].Value = partisi.PartitionName.ToUpper().Replace("\0", string.Empty);
                        dataGridView1.Rows[index].Cells["startaddress"].Value = "0x" + partisi.StartAdress.ToString("X16");
                        dataGridView1.Rows[index].Cells["endaddress"].Value = "0x" + partisi.EndAddress.ToString("X16");
                        dataGridView1.Rows[index].Cells["length"].Value = "0x" + partisi.Size.ToString("X16");
                        comboBox2.Items.Add(partisi.PartitionName.ToUpper().Replace("\0", string.Empty));
                    }
                }
                comboBox2.SelectedIndex = 0;
            }
            dataGridView1.ClearSelection();
            VScrollBar vscrollBar = dataGridView1.Controls.OfType<VScrollBar>().First();
            if (vscrollBar.Visible)
            {
                dataGridView1.Columns["startaddress"].Width = 135;
                dataGridView1.Columns["endaddress"].Width = 135;
                dataGridView1.Columns["length"].Width = 135;
            }
            else
            {
                dataGridView1.Columns["startaddress"].Width = 140;
                dataGridView1.Columns["endaddress"].Width = 140;
                dataGridView1.Columns["length"].Width = 140;
            }
        }

        private void buttonWritePartition_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0) 
            {
                int num = 0;
                foreach (object row in dataGridView1.Rows)
                {
                    if (!(bool)((DataGridViewRow)row).Cells["choose"].Value)
                    {
                        continue;
                    }
                    num++;
                }
                if (num == 0)
                {
                    MessageBox.Show("There is no partition selected, select one ore more partition first using checkbox in first column of partition list!", "No Partition Selected", MessageBoxButtons.OK);
                }
                else if (textBoxFileName.Text != string.Empty)
                {
                    if (IsOkay())
                    {
                        StartProgress();
                        statuslabelStatus.Text = "Writing Partitions ...";
                        ReadAddress readAddress = new ReadAddress()
                        {
                            Filename = textBoxFileName.Text
                        };
                        disableall();
                        bgworkerWriteAddressPartitions.RunWorkerAsync(readAddress);
                    }
                }
                else
                {
                    MessageBox.Show("There is no file choosed, please pick one using browse button!", "No file", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("There is no partition structure layout loaded, load it first using Load Partition Structure button!", "No Partition Structure layout", MessageBoxButtons.OK);
            }
        }

        private void StartProgress()
        {
            statuslabelPartisi.Text = "-";
            statuslabelProses.Text = "0x0000000000000000";
            statuslabelSpeed.Text = "0 MB/s";
            statusProgressBar1.Value = 0;
            statuslabelProgress.Text = "0 %";
        }

        private void disableall()
        {
            BeginInvoke(new MethodInvoker(delegate ()
            {
                groupBoxDrive.Enabled = false;
                groupBoxRead.Enabled = false;
                buttonStop.Enabled = true;
                groupBoxWrite.Enabled = false;
            }));
        }

        private bool IsOkay()
        {
            if (comboBoxDrive.SelectedIndex < 0)
            {
                MessageBox.Show("There is no drives, please refresh the drive list!", "No Drives", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void bgworkerWriteAddressPartitions_DoWork(object sender, DoWorkEventArgs e)
        {
            ReadAddress readAddress = e.Argument as ReadAddress;
            FileInfo fileInfo = new FileInfo(readAddress.Filename);
            long length = fileInfo.Length;
            long num = length;
            int chunk = 10485760;
            long num2 = EmmcSize("\\\\.\\" + driveselected);
            DISK.streamer iface = DISK.CreateStream(driveselected, FileAccess.ReadWrite);
            try
            {
                try
                {
                    FileStream fileStream = new FileStream(readAddress.Filename, FileMode.Open, FileAccess.Read);
                    try
                    {
                        try
                        {
                            using (fileStream)
                            {
                                foreach (DataGridViewRow dataGridViewRow in dataGridView1.Rows)
                                {
                                    if (!(bool)dataGridViewRow.Cells["choose"].Value)
                                    {
                                        continue;
                                    }
                                    List<Partition>.Enumerator enumerator = PartitionsList.GetEnumerator();
                                    try
                                    {
                                        while (true)
                                        {
                                            if (enumerator.MoveNext())
                                            {
                                                Partition current = enumerator.Current;
                                                if (dataGridViewRow.Cells["id"].Value.ToString() == current.ID)
                                                {
                                                    dataGridView1.Rows[dataGridViewRow.Index].Selected = true;
                                                    long startAdress = current.StartAdress;
                                                    long length2 = current.Size;
                                                    readAddress.StartAddress = startAdress;
                                                    readAddress.Length = length2;
                                                    statuslabelPartisi.Text = current.PartitionName.ToUpper();
                                                    try
                                                    {
                                                        Stopwatch sw = new Stopwatch();
                                                        float speed = 0f;
                                                        if (current.StartAdress + current.Size > num2)
                                                        {
                                                            readAddress.Error = true;
                                                            readAddress.Status = "Process Canceled";
                                                            e.Result = readAddress;
                                                            MessageBox.Show("Length is bigger than disk size!", "Can't Continue", MessageBoxButtons.OK);
                                                            return;
                                                        }
                                                        else if (current.StartAdress + current.Size <= num)
                                                        {
                                                            length = current.Size;
                                                            if (chunk > length)
                                                            {
                                                                chunk = (int)length;
                                                            }
                                                            else
                                                            {
                                                                chunk = 10485760;
                                                            }
                                                            int num3 = (int)(length / chunk);
                                                            long readed = 0L;
                                                            long num4 = 0L;

                                                            while (num4 < num3)
                                                            {
                                                                long num5 = num4 * chunk + current.StartAdress;
                                                                if (num4 == num3 - 1)
                                                                {
                                                                    chunk = (int)(length - (num3 - 1) * chunk);
                                                                }
                                                                byte[] array = new byte[chunk];
                                                                fileStream.Seek(num5, SeekOrigin.Begin);
                                                                fileStream.Read(array, 0, chunk);
                                                                sw.Restart();
                                                                DISK.WriteSector(num5, chunk, array, iface);
                                                                readed += chunk;
                                                                double num6 = readed * 100.0 / length;
                                                                int percentProgress = (int)num6;
                                                                bgworkerWriteAddressPartitions.ReportProgress(percentProgress);
                                                                BeginInvoke(new MethodInvoker(delegate ()
                                                                {
                                                                    float num7 = chunk / 1024f / 1024f;
                                                                    float num8 = sw.ElapsedMilliseconds / 1000f;
                                                                    speed = num7 / num8;
                                                                    statuslabelProses.Text = "0x" + readed.ToString("X16");
                                                                    statuslabelSpeed.Text = speed.ToString("##.#") + " MB/s";
                                                                }));
                                                                fileStream.Flush();
                                                                if (bgworkerWriteAddressPartitions.CancellationPending)
                                                                {
                                                                    e.Cancel = true;
                                                                    return;
                                                                }
                                                                else
                                                                {
                                                                    num4 += 1L;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            readAddress.Error = true;
                                                            readAddress.Status = "Process Canceled";
                                                            e.Result = readAddress;
                                                            MessageBox.Show("Length is bigger than file size! maybe you haven't loaded a full dump? ", "Can't Continue", MessageBoxButtons.OK);
                                                            return;
                                                        }
                                                    }
                                                    catch (Exception exception)
                                                    {
                                                        enableall();
                                                        readAddress.Error = true;
                                                        readAddress.Status = "Error: can't write file";
                                                        e.Result = readAddress;
                                                        return;
                                                    }
                                                    if (!bgworkerWriteAddressPartitions.CancellationPending)
                                                    {
                                                        break;
                                                    }
                                                    e.Cancel = true;
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    finally
                                    {
                                        ((IDisposable)enumerator).Dispose();
                                    }
                                }
                            }
                        }
                        catch (Exception exception1)
                        {
                            enableall();
                            readAddress.Error = true;
                            readAddress.Status = "Error: write file error";
                            e.Result = readAddress;
                        }
                    }
                    finally
                    {
                        fileStream.Close();
                    }
                }
                finally
                {
                    DISK.DropStream(iface);
                }
                e.Result = readAddress;
                return;
            }
            catch (Exception exception2)
            {
                enableall();
                readAddress.Error = true;
                readAddress.Status = "Error: can't access disk";
                e.Result = readAddress;
                return;
            }
        }

        private void enableall()
        {
            base.BeginInvoke(new MethodInvoker(delegate ()
            {
                groupBoxDrive.Enabled = true;
                groupBoxRead.Enabled = true;
                buttonStop.Enabled = false;
                groupBoxWrite.Enabled = true;
            }));
        }

        private void bgworkerWriteAddressPartitions_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate ()
            {
                statusProgressBar1.Value = e.ProgressPercentage;
                statuslabelProgress.Text = e.ProgressPercentage.ToString() + " %";
            }));
        }

        private void bgworkerWriteAddressPartitions_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                statuslabelStatus.Text = "Process Canceled";
            }
            else
            {
                statuslabelStatus.Text = "Process Completed";
                ReadAddress readAddress = e.Result as ReadAddress;
                if (readAddress.Error)
                {
                    statuslabelStatus.Text = readAddress.Status;
                }
            }
            enableall();
        }

        private void buttonReadPartitionOnly_Click(object sender, EventArgs e)
        {
            if (IsOkay())
            {
                StartProgress();
                statuslabelStatus.Text = "Reading partition table ...";
                disableall();
                StatusError argument = new StatusError();
                bgworkerReadPartition.RunWorkerAsync(argument);
            }
        }

        private void bgworkerReadPartition_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                StatusError result = new StatusError();
                e.Result = result;
                BeginInvoke(new MethodInvoker(delegate ()
                {
                    dataGridView1.Rows.Clear();
                }));
                byte[] array = new byte[512];
                DISK.streamer iface = DISK.CreateStream(driveselected, FileAccess.ReadWrite);
                try
                {
                    byte[] sourceArray = DISK.ReadSector(0L, 526336, iface);
                    Array.Copy(sourceArray, 0, array, 0, 512);
                    if (array[450] == 238)
                    {
                        Array.Copy(sourceArray, 512, array, 0, 512);
                        byte[] a = new byte[] { 69, 70, 73, 32, 80, 65, 82, 84 };
                        byte[] a2 = new byte[] { array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7] };
                        byte[] array2 = new byte[] { array[72], array[73], array[74], array[75], array[76], array[77], array[78], array[79] };
                        if (!BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(array2);
                        }
                        byte[] array3 = new byte[] { array[80], array[81], array[82], array[83] };
                        if (!BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(array3);
                        }
                        byte[] array4 = new byte[] { array[84], array[85], array[86], array[87] };
                        if (!BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(array4);
                        }
                        long num = BitConverter.ToInt64(array2, 0);
                        long num2 = BitConverter.ToInt32(array4, 0);
                        long num3 = BitConverter.ToInt32(array4, 0);
                        if (Form1.ByteArrayCompare(a, a2))
                        {
                            PartitionsList.Clear();
                            Array.Copy(sourceArray, num * 512L, array, 0L, 512L);
                            int num4 = 1;
                            int num5 = 1;
                            while (true)
                            {
                                bool flag = false;
                                int i = 0;
                                while (true)
                                {
                                    if (i < 4)
                                    {
                                        Partition partisi = new Partition();
                                        byte[] array5 = new byte[8];
                                        for (int j = 0; j < array5.Length; j++)
                                        {
                                            array5[j] = array[i * 128 + (32 + j)];
                                        }
                                        byte[] array6 = new byte[8];
                                        for (int k = 0; k < array6.Length; k++)
                                        {
                                            array6[k] = array[i * 128 + (40 + k)];
                                        }
                                        byte[] array7 = new byte[72];
                                        for (int l = 0; l < array7.Length; l++)
                                        {
                                            array7[l] = array[i * 128 + (56 + l)];
                                        }
                                        if (!BitConverter.IsLittleEndian)
                                        {
                                            Array.Reverse(array5);
                                            Array.Reverse(array6);
                                        }
                                        long num6 = BitConverter.ToInt64(array5, 0) * 512L;
                                        long num7 = BitConverter.ToInt64(array6, 0) * 512L;
                                        long num8 = num7 - num6 + 512L;
                                        string text = Encoding.Unicode.GetString(array7);
                                        partisi.Choosen = true;
                                        partisi.StartAdress = num6;
                                        partisi.EndAddress = num7;
                                        partisi.Size = num8;
                                        text = new string((from c in text
                                                           where c <= '\u007f'
                                                           select c).ToArray());
                                        partisi.PartitionName = text.Trim().Replace("\0", string.Empty);
                                        if (num4 == 1 && partisi.StartAdress > 0L)
                                        {
                                            Partition partisi2 = new Partition();
                                            partisi2.Choosen = true;
                                            partisi2.ID = num4.ToString();
                                            partisi2.StartAdress = 0L;
                                            partisi2.EndAddress = partisi.StartAdress - 512L;
                                            partisi2.Size = partisi2.EndAddress - partisi2.StartAdress + 512L;
                                            partisi2.PartitionName = "MBR-GPT-OTHER";
                                            PartitionsList.Add(partisi2);
                                            num4++;
                                        }
                                        if (num8 <= 512L)
                                        {
                                            flag = true;
                                            break;
                                        }
                                        else
                                        {
                                            if (PartitionsList.Count > 0)
                                            {
                                                if (PartitionsList[PartitionsList.Count - 1].StartAdress + PartitionsList[PartitionsList.Count - 1].Size != partisi.StartAdress)
                                                {
                                                    Partition partisi3 = new Partition();
                                                    partisi3.Choosen = true;
                                                    partisi3.ID = "G" + num5.ToString();
                                                    partisi3.StartAdress = PartitionsList[PartitionsList.Count - 1].StartAdress + PartitionsList[PartitionsList.Count - 1].Size;
                                                    partisi3.EndAddress = partisi.StartAdress - 512L;
                                                    partisi3.Size = partisi3.EndAddress - partisi3.StartAdress + 512L;
                                                    partisi3.PartitionName = "GAPS #" + num5.ToString();
                                                    PartitionsList.Add(partisi3);
                                                    num5++;
                                                    partisi.ID = num4.ToString();
                                                    PartitionsList.Add(partisi);
                                                    num4++;
                                                }
                                                else
                                                {
                                                    partisi.ID = num4.ToString();
                                                    PartitionsList.Add(partisi);
                                                    num4++;
                                                }
                                            }
                                            i++;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                if (flag)
                                {
                                    break;
                                }
                                num += 1L;
                                Array.Copy(sourceArray, num * 512L, array, 0L, 512L);
                            }
                        }
                        else
                        {
                            MessageBox.Show("There is no partition data in the selected drive or the partition type is not supported yet!", "Can't analyze drive", MessageBoxButtons.OK);
                            return;
                        }
                    }
                    else if ((array[510] != 85 ? false : array[511] == 170))
                    {
                        int num9 = 1;
                        int num10 = 1;
                        byte[] array8 = new byte[16];
                        Array.Copy(array, 446, array8, 0, 16);
                        PartitionsList.Clear();
                        byte[] array9 = new byte[4];
                        byte[] array10 = new byte[4];
                        Array.Copy(array8, 8, array9, 0, 4);
                        Array.Copy(array8, 12, array10, 0, 4);
                        if (!BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(array9);
                            Array.Reverse(array10);
                        }
                        Partition partisi4 = new Partition();
                        partisi4.Choosen = true;
                        partisi4.StartAdress = BitConverter.ToInt32(array9, 0) * 512;
                        partisi4.Size = BitConverter.ToInt32(array10, 0) * 512;
                        partisi4.EndAddress = partisi4.StartAdress + partisi4.Size - 1L;
                        partisi4.PartitionName = "P" + array8[4].ToString("X2");

                        if (num9 == 1 && partisi4.StartAdress > 0L)
                        {
                            Partition partisi5 = new Partition();
                            partisi5.Choosen = true;
                            partisi5.ID = num9.ToString();
                            partisi5.StartAdress = 0L;
                            partisi5.EndAddress = partisi4.StartAdress - 1L;
                            partisi5.Size = partisi5.EndAddress - partisi5.StartAdress + 1L;
                            partisi5.PartitionName = "MBR-EBR-OTHER";
                            PartitionsList.Add(partisi5);
                            num9++;
                        }
                        partisi4.ID = num9.ToString();
                        PartitionsList.Add(partisi4);
                        num9++;
                        Array.Copy(array, 462, array8, 0, 16);
                        array9 = new byte[4];
                        array10 = new byte[4];
                        Array.Copy(array8, 8, array9, 0, 4);
                        Array.Copy(array8, 12, array10, 0, 4);
                        if (!BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(array9);
                            Array.Reverse(array10);
                        }
                        partisi4 = new Partition();
                        partisi4.Choosen = true;
                        partisi4.ID = num9.ToString();
                        partisi4.StartAdress = BitConverter.ToInt32(array9, 0) * 512L;
                        partisi4.Size = BitConverter.ToInt32(array10, 0) * 512L;
                        partisi4.EndAddress = partisi4.StartAdress + partisi4.Size - 1L;
                        partisi4.PartitionName = "P" + array8[4].ToString("X2");
                        PartitionsList.Add(partisi4);
                        num9++;
                        Array.Copy(array, 478, array8, 0, 16);
                        array9 = new byte[4];
                        array10 = new byte[4];
                        Array.Copy(array8, 8, array9, 0, 4);
                        Array.Copy(array8, 12, array10, 0, 4);
                        if (!BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(array9);
                            Array.Reverse(array10);
                        }
                        partisi4 = new Partition();
                        partisi4.Choosen = true;
                        partisi4.ID = num9.ToString();
                        partisi4.StartAdress = BitConverter.ToInt32(array9, 0) * 512L;
                        partisi4.Size = BitConverter.ToInt32(array10, 0) * 512L;
                        partisi4.EndAddress = partisi4.StartAdress + partisi4.Size - 1L;
                        partisi4.PartitionName = "P" + array8[4].ToString("X2");
                        PartitionsList.Add(partisi4);
                        num9++;
                        Array.Copy(array, 494, array8, 0, 16);
                        array9 = new byte[4];
                        Array.Copy(array8, 8, array9, 0, 4);
                        if (!BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(array9);
                        }
                        long num11 = BitConverter.ToInt32(array9, 0) * 512L;
                        sourceArray = DISK.ReadSector(num11, 524288, iface);
                        Partition partisi6 = new Partition();
                        partisi6.Choosen = true;
                        partisi6.ID = num9.ToString();
                        partisi6.StartAdress = num11;
                        array8 = new byte[16];
                        Array.Copy(sourceArray, 0, array, 0, 512);
                        Array.Copy(array, 446, array8, 0, 16);
                        Array.Copy(array8, 8, array9, 0, 4);
                        if (!BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(array9);
                        }
                        partisi6.Size = BitConverter.ToInt32(array9, 0) * 512L + num11 + 512L - 512L - partisi6.StartAdress;
                        partisi6.EndAddress = partisi6.StartAdress + partisi6.Size - 1L;
                        partisi6.PartitionName = "EBR";
                        PartitionsList.Add(partisi6);
                        num9++;
                        long num12 = 0L;
                        long num13 = 1L;
                        while (true)
                        {
                            bool flag2 = false;
                            array8 = new byte[16];
                            Array.Copy(sourceArray, num12, array, 0L, 512L);
                            if ((array[510] != 85 ? true : array[511] != 170))
                            {
                                flag2 = true;
                            }
                            else
                            {
                                Array.Copy(array, 446, array8, 0, 16);
                                array9 = new byte[4];
                                array10 = new byte[4];
                                Array.Copy(array8, 8, array9, 0, 4);
                                Array.Copy(array8, 12, array10, 0, 4);
                                if (!BitConverter.IsLittleEndian)
                                {
                                    Array.Reverse(array9);
                                    Array.Reverse(array10);
                                }
                                partisi4 = new Partition();
                                partisi4.Choosen = true;
                                partisi4.ID = num9.ToString();
                                partisi4.StartAdress = BitConverter.ToInt32(array9, 0) * 512L + num11 + num13 * 512L - 512L;
                                partisi4.Size = BitConverter.ToInt32(array10, 0) * 512L;
                                partisi4.EndAddress = partisi4.StartAdress + partisi4.Size - 1L;
                                partisi4.PartitionName = "P" + array8[4].ToString("X2");
                                if (PartitionsList.Count > 0)
                                {
                                    if (PartitionsList[PartitionsList.Count - 1].StartAdress + PartitionsList[PartitionsList.Count - 1].Size != partisi4.StartAdress)
                                    {
                                        Partition partisi7 = new Partition();
                                        partisi7.Choosen = true;
                                        partisi7.ID = "G" + num10.ToString();
                                        partisi7.StartAdress = PartitionsList[PartitionsList.Count - 1].StartAdress + PartitionsList[PartitionsList.Count - 1].Size;
                                        partisi7.EndAddress = partisi4.StartAdress - 1L;
                                        partisi7.Size = partisi7.EndAddress - partisi7.StartAdress + 1L;
                                        partisi7.PartitionName = "GAPS #" + num10.ToString();
                                        PartitionsList.Add(partisi7);
                                        num10++;
                                        partisi4.ID = num9.ToString();
                                        PartitionsList.Add(partisi4);
                                        num9++;
                                    }
                                    else
                                    {
                                        partisi4.ID = num9.ToString();
                                        PartitionsList.Add(partisi4);
                                        num9++;
                                        num13 += 1L;
                                    }
                                }
                            }
                            if (flag2)
                            {
                                break;
                            }
                            num12 += 512L;
                        }
                    }
                    else
                    {
                        MessageBox.Show("There is no partition data in the selected drive or the partition type is not supported yet!", "Can't analyze drive", MessageBoxButtons.OK);
                        return;
                    }
                    if (PartitionsList.Count > 0)
                    {
                        BeginInvoke(new MethodInvoker(delegate ()
                        {
                            comboBox1.Items.Clear();
                            foreach (string item in preset)
                            {
                                comboBox1.Items.Add(item);
                            }
                            foreach (Partition partisi8 in PartitionsList)
                            {
                                if (checkBoxreadpartisi.Checked)
                                {
                                    int index = dataGridView1.Rows.Add();
                                    dataGridView1.Rows[index].Cells["choose"].Value = partisi8.Choosen;
                                    dataGridView1.Rows[index].Cells["id"].Value = partisi8.ID.ToString();
                                    dataGridView1.Rows[index].Cells["name"].Value = partisi8.PartitionName.ToUpper().Replace("\0", string.Empty);
                                    dataGridView1.Rows[index].Cells["startaddress"].Value = "0x" + partisi8.StartAdress.ToString("X16");
                                    dataGridView1.Rows[index].Cells["endaddress"].Value = "0x" + partisi8.EndAddress.ToString("X16");
                                    dataGridView1.Rows[index].Cells["length"].Value = "0x" + partisi8.Size.ToString("X16");
                                    comboBox1.Items.Add(partisi8.PartitionName.ToUpper().Replace("\0", string.Empty));
                                }
                                else if (partisi8.ID.Substring(0, 1) != "G")
                                {
                                    int index2 = dataGridView1.Rows.Add();
                                    dataGridView1.Rows[index2].Cells["choose"].Value = partisi8.Choosen;
                                    dataGridView1.Rows[index2].Cells["id"].Value = partisi8.ID.ToString();
                                    dataGridView1.Rows[index2].Cells["name"].Value = partisi8.PartitionName.ToUpper().Replace("\0", string.Empty);
                                    dataGridView1.Rows[index2].Cells["startaddress"].Value = "0x" + partisi8.StartAdress.ToString("X16");
                                    dataGridView1.Rows[index2].Cells["endaddress"].Value = "0x" + partisi8.EndAddress.ToString("X16");
                                    dataGridView1.Rows[index2].Cells["length"].Value = "0x" + partisi8.Size.ToString("X16");
                                    comboBox1.Items.Add(partisi8.PartitionName.ToUpper().Replace("\0", string.Empty));
                                }
                            }
                            VScrollBar vscrollBar = dataGridView1.Controls.OfType<VScrollBar>().First<VScrollBar>();
                            if (vscrollBar.Visible)
                            {
                                dataGridView1.Columns["startaddress"].Width = 135;
                                dataGridView1.Columns["endaddress"].Width = 135;
                                dataGridView1.Columns["length"].Width = 135;
                            }
                            else
                            {
                                dataGridView1.Columns["startaddress"].Width = 140;
                                dataGridView1.Columns["endaddress"].Width = 140;
                                dataGridView1.Columns["length"].Width = 140;
                            }
                        }));
                    }
                    if (bgworkerReadPartition.CancellationPending)
                    {
                        e.Cancel = true;
                    }
                }
                finally
                {
                    DISK.DropStream(iface);
                }
            }
            catch (Exception exception)
            {
                enableall();
                StatusError statusError = e.Argument as StatusError;
                statusError.Error = true;
                statusError.Status = "Error: can't access disk";
                e.Result = statusError;
            }
        }

        private void bgworkerReadPartition_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate ()
            {
                statusProgressBar1.Value = e.ProgressPercentage;
                statuslabelProgress.Text = e.ProgressPercentage.ToString() + " %";
            }));
        }

        private void bgworkerReadPartition_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                statuslabelStatus.Text = "Process Canceled";
            }
            else
            {
                statuslabelStatus.Text = "Process Completed";
                StatusError statusError = e.Result as StatusError;
                if (statusError.Error)
                {
                    statuslabelStatus.Text = statusError.Status;
                }
            }
            dataGridView1.ClearSelection();
            enableall();
        }

        private void buttonReadFull_Click(object sender, EventArgs e)
        {
            if (IsOkay())
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = "Full_image.bin";
                saveFileDialog.Filter = "Bin files (*.bin)|*.bin|Image files (*.img)|*.img|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StartProgress();
                    statuslabelStatus.Text = "Reading full image ...";
                    disableall();
                    ReadFUllImage readFUllImage = new ReadFUllImage();
                    readFUllImage.Filename = saveFileDialog.FileName;
                    bgworkerReadFull.RunWorkerAsync(readFUllImage);
                }
            }
        }

        private void bgworkerReadFull_DoWork(object sender, DoWorkEventArgs e)
        {
            ReadFUllImage readFUllImage = e.Argument as ReadFUllImage;
            try
            {
                Stopwatch sw = new Stopwatch();
                float speed = 0f;
                int chunk = 10485760;
                long num = EmmcSize("\\\\.\\" + driveselected);
                if (chunk > num)
                {
                    chunk = (int)num;
                }
                int num2 = (int)(num / chunk);
                long readed = 0L;
                DISK.streamer iface = DISK.CreateStream(driveselected, FileAccess.ReadWrite);
                try
                {
                    try
                    {
                        FileStream fileStream = new FileStream(readFUllImage.Filename, FileMode.Create);
                        try
                        {
                            try
                            {
                                using (fileStream)
                                {
                                    for (long num3 = 0L; num3 < num2; num3 += 1L)
                                    {
                                        sw.Restart();
                                        long num4 = num3 * chunk;
                                        if (num3 == (num2 - 1))
                                        {
                                            chunk = (int)(num - ((num2 - 1) * chunk));
                                        }
                                        byte[] buffer = new byte[chunk];
                                        fileStream.Seek(num4, SeekOrigin.Begin);
                                        buffer = DISK.ReadSector(num4, chunk, iface);
                                        readed += chunk;
                                        double num5 = readed * 100.0 / num;
                                        int percentProgress = (int)num5;
                                        bgworkerReadFull.ReportProgress(percentProgress);
                                        BeginInvoke(new MethodInvoker(delegate ()
                                        {
                                            float num6 = chunk / 1024f / 1024f;
                                            float num7 = sw.ElapsedMilliseconds / 1000f;
                                            speed = num6 / num7;
                                            statuslabelProses.Text = "0x" + readed.ToString("X16");
                                            statuslabelSpeed.Text = speed.ToString("##.#") + " MB/s";
                                        }));
                                        fileStream.Write(buffer, 0, chunk);
                                        fileStream.Flush();
                                        if (bgworkerReadFull.CancellationPending)
                                        {
                                            e.Cancel = true;
                                            return;
                                        }
                                    }
                                }
                            }
                            catch (Exception exception)
                            {
                                enableall();
                                readFUllImage.Error = true;
                                readFUllImage.Status = "Error: write file error";
                                e.Result = readFUllImage;
                            }
                        }
                        finally
                        {
                            fileStream.Close();
                        }
                    }
                    catch (Exception exception1)
                    {
                        enableall();
                        readFUllImage.Error = true;
                        readFUllImage.Status = "Error: can't create file";
                        e.Result = readFUllImage;
                    }
                    if (bgworkerReadFull.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                finally
                {
                    DISK.DropStream(iface);
                }
            }
            catch (Exception exception2)
            {
                enableall();
                readFUllImage.Error = true;
                readFUllImage.Status = "Error: can't access disk";
                e.Result = readFUllImage;
            }
            e.Result = readFUllImage;
        }

        private void bgworkerReadFull_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate ()
            {
                statusProgressBar1.Value = e.ProgressPercentage;
                statuslabelProgress.Text = e.ProgressPercentage.ToString() + " %";
            }));
        }

        private void bgworkerReadFull_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                statuslabelStatus.Text = "Process Canceled";
            }
            else
            {
                statuslabelStatus.Text = "Process Completed";
                ReadFUllImage readFUllImage = e.Result as ReadFUllImage;
                if (readFUllImage.Error)
                {
                    statuslabelStatus.Text = readFUllImage.Status;
                }
            }
            enableall();
        }

        private void checkBoxreadpartisi_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (PartitionsList.Count > 0)
            {
                comboBox1.Items.Clear();
                foreach (Partition partisi in PartitionsList)
                {
                    if (!checkBoxreadpartisi.Checked)
                    {
                        if (partisi.ID.Substring(0, 1) == "G")
                        {
                            continue;
                        }
                        int index2 = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index2].Cells["choose"].Value = partisi.Choosen;
                        dataGridView1.Rows[index2].Cells["id"].Value = partisi.ID.ToString();
                        dataGridView1.Rows[index2].Cells["name"].Value = partisi.PartitionName.ToUpper().Replace("\0", string.Empty);
                        dataGridView1.Rows[index2].Cells["startaddress"].Value = "0x" + partisi.StartAdress.ToString("X16");
                        dataGridView1.Rows[index2].Cells["endaddress"].Value = "0x" + partisi.EndAddress.ToString("X16");
                        dataGridView1.Rows[index2].Cells["length"].Value = "0x" + partisi.Size.ToString("X16");
                        comboBox1.Items.Add(partisi.PartitionName.ToUpper().Replace("\0", string.Empty));
                    }
                    else
                    {
                        int index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells["choose"].Value = partisi.Choosen;
                        dataGridView1.Rows[index].Cells["id"].Value = partisi.ID.ToString();
                        dataGridView1.Rows[index].Cells["name"].Value = partisi.PartitionName.ToUpper().Replace("\0", string.Empty);
                        dataGridView1.Rows[index].Cells["startaddress"].Value = "0x" + partisi.StartAdress.ToString("X16");
                        dataGridView1.Rows[index].Cells["endaddress"].Value = "0x" + partisi.EndAddress.ToString("X16");
                        dataGridView1.Rows[index].Cells["length"].Value = "0x" + partisi.Size.ToString("X16");
                        comboBox1.Items.Add(partisi.PartitionName.ToUpper().Replace("\0", string.Empty));
                    }
                }
                comboBox1.SelectedIndex = 0;
            }
            dataGridView1.ClearSelection();
            if (!dataGridView1.Controls.OfType<VScrollBar>().First<VScrollBar>().Visible)
            {
                dataGridView1.Columns["startaddress"].Width = 140;
                dataGridView1.Columns["endaddress"].Width = 140;
                dataGridView1.Columns["length"].Width = 140;
            }
            else
            {
                dataGridView1.Columns["startaddress"].Width = 135;
                dataGridView1.Columns["endaddress"].Width = 135;
                dataGridView1.Columns["length"].Width = 135;
            }
        }

        private void buttonReadSelected_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("There is no partition structure layout loaded, load it first using Read Partition Structure button!", "No Partition Structure layout", MessageBoxButtons.OK);
            }
            else
            {
                int num = 0;
                foreach (DataGridViewRow dataGridViewRow in dataGridView1.Rows)
                {
                    if ((bool)dataGridViewRow.Cells["choose"].Value)
                    {
                        num++;
                    }
                }
                if (num == 0)
                {
                    MessageBox.Show("There is no partition selected, select one ore more partition first using checkbox in first column of partition list!", "No Partition Selected", MessageBoxButtons.OK);
                }
                else
                {
                    if (IsOkay())
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.FileName = "Image.bin";
                        saveFileDialog.Filter = "Bin files (*.bin)|*.bin|Image files (*.img)|*.img";
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            StartProgress();
                            statuslabelStatus.Text = "Reading Partitions ...";
                            ReadAddress readAddress = new ReadAddress();
                            readAddress.Filename = saveFileDialog.FileName;
                            disableall();
                            bgworkerReadAddressPartitions.RunWorkerAsync(readAddress);
                        }
                    }
                }
            }
        }

        private void bgworkerReadAddressPartitions_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (DataGridViewRow dataGridViewRow in dataGridView1.Rows)
            {
                if ((bool)dataGridViewRow.Cells["choose"].Value)
                {
                    foreach (Partition partisi in PartitionsList)
                    {
                        if (dataGridViewRow.Cells["id"].Value.ToString() == partisi.ID.ToString())
                        {
                            dataGridView1.Rows[dataGridViewRow.Index].Selected = true;
                            ReadAddress readAddress = e.Argument as ReadAddress;
                            ReadAddress readAddress2 = new ReadAddress();
                            string text = readAddress.Filename.Substring(0, readAddress.Filename.Length - 4);
                            string text2 = readAddress.Filename.Substring(readAddress.Filename.Length - 4, 4);
                            long startAdress = partisi.StartAdress;
                            long length = partisi.Size;
                            string text3 = startAdress.ToString("X16");
                            string text4 = length.ToString("X16");
                            string filename = string.Concat(new string[]
                            {
                                text,
                                "_",
                                partisi.PartitionName.ToUpper(),
                                "_0x",
                                text3,
                                "_0x",
                                text4,
                                text2
                            });
                            readAddress2.Filename = filename;
                            readAddress2.StartAddress = startAdress;
                            readAddress2.Length = length;
                            statuslabelPartisi.Text = partisi.PartitionName.ToUpper();
                            try
                            {
                                Stopwatch sw = new Stopwatch();
                                float speed = 0f;
                                int chunk = 10485760;
                                long num = EmmcSize("\\\\.\\" + driveselected);
                                if (readAddress2.StartAddress + readAddress2.Length > num)
                                {
                                    readAddress2.Error = true;
                                    readAddress2.Status = "Process Canceled";
                                    e.Result = readAddress2;
                                    MessageBox.Show("Length is bigger than disk size, try to lower length and or start address!", "Can't Continue", MessageBoxButtons.OK);
                                    return;
                                }
                                num = readAddress2.Length;
                                if (chunk > num)
                                {
                                    chunk = (int)num;
                                }
                                int num2 = (int)(num / chunk);
                                long readed = 0L;
                                DISK.streamer iface = DISK.CreateStream(driveselected, FileAccess.ReadWrite);
                                try
                                {
                                    try
                                    {
                                        FileStream fileStream = new FileStream(readAddress2.Filename, FileMode.Create);
                                        try
                                        {
                                            using (fileStream)
                                            {
                                                for (long num3 = 0L; num3 < num2; num3 += 1L)
                                                {
                                                    sw.Restart();
                                                    long offset = num3 * chunk;
                                                    long startingsector = num3 * chunk + readAddress2.StartAddress;
                                                    if (num3 == (num2 - 1))
                                                    {
                                                        chunk = (int)(num - ((num2 - 1) * chunk));
                                                    }
                                                    byte[] buffer = new byte[chunk];
                                                    fileStream.Seek(offset, SeekOrigin.Begin);
                                                    buffer = DISK.ReadSector(startingsector, chunk, iface);
                                                    readed += chunk;
                                                    double num4 = readed * 100.0 / num;
                                                    int percentProgress = (int)num4;
                                                    bgworkerReadAddressPartitions.ReportProgress(percentProgress);
                                                    BeginInvoke(new MethodInvoker(delegate ()
                                                    {
                                                        float num5 = chunk / 1024f / 1024f;
                                                        float num6 = sw.ElapsedMilliseconds / 1000f;
                                                        speed = num5 / num6;
                                                        statuslabelProses.Text = "0x" + readed.ToString("X16");
                                                        statuslabelSpeed.Text = speed.ToString("##.#") + " MB/s";
                                                    }));
                                                    fileStream.Write(buffer, 0, chunk);
                                                    fileStream.Flush();
                                                    if (bgworkerReadAddressPartitions.CancellationPending)
                                                    {
                                                        e.Cancel = true;
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            enableall();
                                            readAddress2.Error = true;
                                            readAddress2.Status = "Error: write file error";
                                            e.Result = readAddress2;
                                        }
                                        finally
                                        {
                                            fileStream.Close();
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        enableall();
                                        readAddress2.Error = true;
                                        readAddress2.Status = "Error: can't create file";
                                        e.Result = readAddress2;
                                    }
                                    if (bgworkerReadAddressPartitions.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }
                                }
                                finally
                                {
                                    DISK.DropStream(iface);
                                }
                            }
                            catch (Exception)
                            {
                                enableall();
                                readAddress2.Error = true;
                                readAddress2.Status = "Error: can't access disk";
                                e.Result = readAddress2;
                            }
                            e.Result = readAddress2;
                        }
                    }
                }
            }
        }

        private void bgworkerReadAddressPartitions_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate ()
            {
                statusProgressBar1.Value = e.ProgressPercentage;
                statuslabelProgress.Text = e.ProgressPercentage.ToString() + " %";
            }));
        }

        private void bgworkerReadAddressPartitions_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                statuslabelStatus.Text = "Process Canceled";
            }
            else
            {
                statuslabelStatus.Text = "Process Completed";
                ReadAddress readAddress = e.Result as ReadAddress;
                if (readAddress.Error)
                {
                    statuslabelStatus.Text = readAddress.Status;
                }
            }
            enableall();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = false;
            List<Partition>.Enumerator enumerator = PartitionsList.GetEnumerator();
            try
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Partition current = enumerator.Current;
                        if (current.PartitionName.ToUpper().Replace("\0", string.Empty) == comboBox1.Text)
                        {
                            numericStartRead.Text = current.StartAdress.ToString("X16");
                            numericLengthRead.Text = current.Size.ToString("X16");
                            flag = true;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            finally
            {
                ((IDisposable)enumerator).Dispose();
            }
            if (!flag)
            {
                long num = Convert.ToInt64(numericLengthRead.Text, 16);
                string[] array = comboBox1.Text.Split(new char[] { ' ' });
                long num2;
                long.TryParse(array[0], out num2);
                if (array[1] == "KB")
                {
                    num = num2 * 1024L;
                }
                else if (array[1] == "MB")
                {
                    num = num2 * 1024L * 1024L;
                }
                else if (array[1] == "GB")
                {
                    num = num2 * 1024L * 1024L * 1024L;
                }
                numericLengthRead.Text = num.ToString("X16");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Partition>.Enumerator enumerator = PartitionsList.GetEnumerator();
            try
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Partition current = enumerator.Current;
                        if (current.PartitionName.ToUpper().Replace("\0", string.Empty) == comboBox2.Text)
                        {
                            numericStartWrite.Text = current.StartAdress.ToString("X16");
                            numericLengthWrite.Text = current.Size.ToString("X16");
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            finally
            {
                ((IDisposable)enumerator).Dispose();
            }
        }

        private void numericStartWrite_TextChanged(object sender, EventArgs e)
        {
            string text = numericStartWrite.Text;
            long num = 0L;
            if ((long.TryParse(text, NumberStyles.HexNumber, NumberFormatInfo.CurrentInfo, out num) ? false : text != string.Empty))
            {
                numericStartWrite.Text = text.Remove(text.Length - 1, 1);
                numericStartWrite.SelectionStart = numericStartWrite.Text.Length;
            }
        }

        private void numericLengthWrite_TextChanged(object sender, EventArgs e)
        {
            string text = numericLengthWrite.Text;
            long num = 0L;
            if ((long.TryParse(text, NumberStyles.HexNumber, NumberFormatInfo.CurrentInfo, out num) ? false : text != string.Empty))
            {
                numericLengthWrite.Text = text.Remove(text.Length - 1, 1);
                numericLengthWrite.SelectionStart = numericLengthWrite.Text.Length;
            }
        }

        private void numericStartRead_TextChanged(object sender, EventArgs e)
        {
            string text = numericStartRead.Text;
            long num = 0L;
            if ((long.TryParse(text, NumberStyles.HexNumber, NumberFormatInfo.CurrentInfo, out num) ? false : text != string.Empty))
            {
                numericStartRead.Text = text.Remove(text.Length - 1, 1);
                numericStartRead.SelectionStart = numericStartRead.Text.Length;
            }
        }

        private void numericLengthRead_TextChanged(object sender, EventArgs e)
        {
            string text = numericLengthRead.Text;
            long num = 0L;
            if ((long.TryParse(text, NumberStyles.HexNumber, NumberFormatInfo.CurrentInfo, out num) ? false : text != string.Empty))
            {
                numericLengthRead.Text = text.Remove(text.Length - 1, 1);
                numericLengthRead.SelectionStart = numericLengthRead.Text.Length;
            }
        }

        private void buttonReadManual_Click(object sender, EventArgs e)
        {
            if (IsOkay())
            {
                long num = Convert.ToInt64(numericLengthRead.Text, 16);
                if (num == 0L)
                {
                    MessageBox.Show("Length is 0, please input read length or choose from presets!", "Length is 0", MessageBoxButtons.OK);
                }
                else if (num % 512L <= 0L)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog()
                    {
                        FileName = "Image.bin",
                        Filter = "Bin files (*.bin)|*.bin|Image files (*.img)|*.img"
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        StartProgress();
                        statuslabelStatus.Text = "Reading image ...";
                        ReadAddress readAddress = new ReadAddress();
                        string text = saveFileDialog.FileName.Substring(0, saveFileDialog.FileName.Length - 4);
                        string text2 = saveFileDialog.FileName.Substring(saveFileDialog.FileName.Length - 4, 4);
                        long num2 = Convert.ToInt64(numericStartRead.Text, 16);
                        if (num2 % 512L <= 0L)
                        {
                            string text3 = num2.ToString("X16");
                            string text4 = num.ToString("X16");
                            readAddress.Filename = string.Concat(new string[] { text, "_0x", text3, "_0x", text4, text2 });
                            readAddress.StartAddress = num2;
                            readAddress.Length = num;
                            disableall();
                            bgworkerReadAddress.RunWorkerAsync(readAddress);
                        }
                        else
                        {
                            MessageBox.Show("Start Address must be multiple by 512 in decimal or 0x200 in hex, please input correct start address!", "Start Address is not correct", MessageBoxButtons.OK);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Length must be multiple by 512 in decimal or 0x200 in hex, please input correct read length or choose from presets!", "Length is not correct", MessageBoxButtons.OK);
                }
            }
        }

        private void bgworkerReadAddress_DoWork(object sender, DoWorkEventArgs e)
        {
            ReadAddress readAddress = e.Argument as ReadAddress;
            try
            {
                Stopwatch sw = new Stopwatch();
                float speed = 0f;
                int chunk = 10485760;
                long num = EmmcSize("\\\\.\\" + driveselected);
                if (readAddress.StartAddress + readAddress.Length > num)
                {
                    readAddress.Error = true;
                    readAddress.Status = "Process Canceled";
                    e.Result = readAddress;
                    MessageBox.Show("Length is bigger than disk size, try to lower length and or start address!", "Can't Continue", MessageBoxButtons.OK);
                    return;
                }
                num = readAddress.Length;
                if (chunk > num)
                {
                    chunk = (int)num;
                }
                int num2 = (int)(num / chunk);
                long readed = 0L;

                DISK.streamer iface = DISK.CreateStream(driveselected, FileAccess.ReadWrite);
                try
                {
                    try
                    {
                        FileStream fileStream = new FileStream(readAddress.Filename, FileMode.Create);
                        try
                        {
                            try
                            {
                                using (fileStream)
                                {
                                    for (long num3 = 0L; num3 < num2; num3 += 1L)
                                    {
                                        sw.Restart();
                                        long offset = num3 * chunk;
                                        long startingsector = num3 * chunk + readAddress.StartAddress;
                                        if (num3 == (num2 - 1))
                                        {
                                            chunk = (int)(num - ((num2 - 1) * chunk));
                                        }
                                        byte[] buffer = new byte[chunk];
                                        fileStream.Seek(offset, SeekOrigin.Begin);
                                        buffer = DISK.ReadSector(startingsector, chunk, iface);
                                        readed += chunk;
                                        double num4 = readed * 100.0 / num;
                                        int percentProgress = (int)num4;
                                        bgworkerReadAddress.ReportProgress(percentProgress);
                                        BeginInvoke(new MethodInvoker(delegate ()
                                        {
                                            float num5 = chunk / 1024f / 1024f;
                                            float num6 = sw.ElapsedMilliseconds / 1000f;
                                            speed = num5 / num6;
                                            statuslabelProses.Text = "0x" + readed.ToString("X16");
                                            statuslabelSpeed.Text = speed.ToString("##.#") + " MB/s";
                                        }));

                                        fileStream.Write(buffer, 0, chunk);
                                        fileStream.Flush();
                                        if (bgworkerReadAddress.CancellationPending)
                                        {
                                            e.Cancel = true;
                                            return;
                                        }
                                    }
                                }
                            }
                            catch (Exception exception)
                            {
                                enableall();
                                readAddress.Error = true;
                                readAddress.Status = "Error: write file error";
                                e.Result = readAddress;
                            }
                        }
                        finally
                        {
                            fileStream.Close();
                        }
                    }
                    catch (Exception exception1)
                    {
                        enableall();
                        readAddress.Error = true;
                        readAddress.Status = "Error: can't create file";
                        e.Result = readAddress;
                    }
                    if (bgworkerReadAddress.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                finally
                {
                    DISK.DropStream(iface);
                }
            }
            catch (Exception)
            {
                enableall();
                readAddress.Error = true;
                readAddress.Status = "Error: can't access disk";
                e.Result = readAddress;
            }
            e.Result = readAddress;
        }

        private void bgworkerReadAddress_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate ()
            {
                statusProgressBar1.Value = e.ProgressPercentage;
                statuslabelProgress.Text = e.ProgressPercentage.ToString() + " %";
            }));
        }

        private void bgworkerReadAddress_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                statuslabelStatus.Text = "Process Canceled";
            }
            else
            {
                statuslabelStatus.Text = "Process Completed";
                ReadAddress readAddress = e.Result as ReadAddress;
                if (readAddress.Error)
                {
                    statuslabelStatus.Text = readAddress.Status;
                }
            }
            enableall();
        }

        private void buttonWriteManual_Click(object sender, EventArgs e)
        {
            if (textBoxFileName.Text != string.Empty)
            {
                if (IsOkay())
                {
                    StartProgress();
                    statuslabelStatus.Text = "Writing image ...";
                    ReadAddress readAddress = new ReadAddress();
                    readAddress.Filename = textBoxFileName.Text;
                    long num = Convert.ToInt64(numericStartWrite.Text, 16);
                    long length = Convert.ToInt64(numericLengthWrite.Text, 16);
                    if (num % 512L <= 0L)
                    {
                        readAddress.StartAddress = num;
                        readAddress.Length = length;
                        disableall();
                        bgworkerWriteAddress.RunWorkerAsync(readAddress);
                    }
                    else
                    {
                        MessageBox.Show("Start Address must be multiple by 512 in decimal or 0x200 in hex, please input correct start address!", "Start Address is not correct", MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("There is no file choosed, please pick one using browse button!", "No file", MessageBoxButtons.OK);
            }
        }

        private void bgworkerWriteAddress_DoWork(object sender, DoWorkEventArgs e)
        {
            ReadAddress readAddress = e.Argument as ReadAddress;
            try
            {
                Stopwatch sw = new Stopwatch();
                float speed = 0f;
                FileInfo fileInfo = new FileInfo(readAddress.Filename);
                long length = fileInfo.Length;
                int chunk = 10485760;
                long num = EmmcSize("\\\\.\\" + driveselected);
                if (readAddress.StartAddress + readAddress.Length <= num)
                {
                    length = readAddress.Length;
                    if ((long)chunk > length)
                    {
                        chunk = (int)length;
                    }
                    int num2 = (int)(length / (long)chunk);
                    long readed = 0L;
                    DISK.streamer iface = DISK.CreateStream(driveselected, FileAccess.ReadWrite);
                    try
                    {
                        try
                        {
                            FileStream fileStream = new FileStream(readAddress.Filename, FileMode.Open, FileAccess.Read);
                            try
                            {
                                try
                                {
                                    using (fileStream)
                                    {
                                        long num3 = 0L;
                                        while (num3 < num2)
                                        {
                                            long offset = num3 * chunk;
                                            long startingsector = num3 * chunk + readAddress.StartAddress;
                                            if (num3 == num2 - 1)
                                            {
                                                chunk = (int)(length - (num2 - 1) * chunk);
                                            }
                                            byte[] array = new byte[chunk];
                                            fileStream.Seek(offset, SeekOrigin.Begin);
                                            fileStream.Read(array, 0, chunk);
                                            sw.Restart();
                                            DISK.WriteSector(startingsector, chunk, array, iface);
                                            readed += chunk;
                                            double num4 = readed * 100.0 / length;
                                            int percentProgress = (int)num4;
                                            bgworkerWriteAddress.ReportProgress(percentProgress);
                                            BeginInvoke(new MethodInvoker(delegate ()
                                            {
                                                float num5 = chunk / 1024f / 1024f;
                                                float num6 = sw.ElapsedMilliseconds / 1000f;
                                                speed = num5 / num6;
                                                statuslabelProses.Text = "0x" + readed.ToString("X16");
                                                statuslabelSpeed.Text = speed.ToString("##.#") + " MB/s";
                                            }));
                                            fileStream.Flush();
                                            if (bgworkerWriteAddress.CancellationPending)
                                            {
                                                e.Cancel = true;
                                                return;
                                            }
                                            else
                                            {
                                                num3 += 1L;
                                            }
                                        }
                                    }
                                }
                                catch (Exception exception)
                                {
                                    enableall();
                                    readAddress.Error = true;
                                    readAddress.Status = "Error: write error";
                                    e.Result = readAddress;
                                }
                            }
                            finally
                            {
                                fileStream.Close();
                            }
                        }
                        catch (Exception exception1)
                        {
                            enableall();
                            readAddress.Error = true;
                            readAddress.Status = "Error: can't open file";
                            e.Result = readAddress;
                        }
                        if (bgworkerWriteAddress.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                    finally
                    {
                        DISK.DropStream(iface);
                    }
                }
                else
                {
                    readAddress.Error = true;
                    readAddress.Status = "Process Canceled";
                    e.Result = readAddress;
                    MessageBox.Show("Length is bigger than disk size, try to lower length and or start address!", "Can't Continue", MessageBoxButtons.OK);
                    return;
                }
            }
            catch (Exception exception2)
            {
                enableall();
                readAddress.Error = true;
                readAddress.Status = "Error: can't access disk";
                e.Result = readAddress;
            }
            e.Result = readAddress;
        }

        private void bgworkerWriteAddress_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate ()
            {
                statusProgressBar1.Value = e.ProgressPercentage;
                statuslabelProgress.Text = e.ProgressPercentage.ToString() + " %";
            }));
        }

        private void bgworkerWriteAddress_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                statuslabelStatus.Text = "Process Completed";
                ReadAddress result = e.Result as ReadAddress;
                if (result.Error)
                {
                    statuslabelStatus.Text = result.Status;
                }
            }
            else
            {
                statuslabelStatus.Text = "Process Canceled";
            }
            enableall();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (bgworkerReadPartition.IsBusy)
            {
                bgworkerReadPartition.CancelAsync();
            }
            if (bgworkerReadFull.IsBusy)
            {
                bgworkerReadFull.CancelAsync();
            }
            if (bgworkerReadAddress.IsBusy)
            {
                bgworkerReadAddress.CancelAsync();
            }
            if (bgworkerReadAddressPartitions.IsBusy)
            {
                bgworkerReadAddressPartitions.CancelAsync();
            }
            if (bgworkerWriteAddress.IsBusy)
            {
                bgworkerWriteAddress.CancelAsync();
            }
            if (bgworkerWriteAddressPartitions.IsBusy)
            {
                bgworkerWriteAddressPartitions.CancelAsync();
            }
        }

        private void cbpartisi_CheckedChanged(object sender, EventArgs e)
        {
            if (PartitionsList.Count > 0)
            {
                List<Partition> list = new List<Partition>();
                foreach (object obj in ((IEnumerable)dataGridView1.Rows))
                {
                    DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
                    Partition partisi = new Partition();
                    if (cbpartisi.Checked)
                    {
                        partisi.Choosen = true;
                    }
                    else
                    {
                        partisi.Choosen = false;
                    }
                    partisi.ID = dataGridViewRow.Cells["id"].Value.ToString();
                    partisi.PartitionName = dataGridViewRow.Cells["name"].Value.ToString();
                    partisi.StartAdress = Convert.ToInt64(dataGridViewRow.Cells["startaddress"].Value.ToString().Replace("0x", string.Empty), 16);
                    partisi.EndAddress = Convert.ToInt64(dataGridViewRow.Cells["endaddress"].Value.ToString().Replace("0x", string.Empty), 16);
                    partisi.Size = Convert.ToInt64(dataGridViewRow.Cells["length"].Value.ToString().Replace("0x", string.Empty), 16);
                    list.Add(partisi);
                }
                dataGridView1.Rows.Clear();
                foreach (Partition partisi2 in list)
                {
                    int index = dataGridView1.Rows.Add();
                    if (cbpartisi.Checked)
                    {
                        dataGridView1.Rows[index].Cells["choose"].Value = true;
                    }
                    else
                    {
                        dataGridView1.Rows[index].Cells["choose"].Value = false;
                    }
                    dataGridView1.Rows[index].Cells["id"].Value = partisi2.ID.ToString();
                    dataGridView1.Rows[index].Cells["name"].Value = partisi2.PartitionName.ToUpper();
                    dataGridView1.Rows[index].Cells["startaddress"].Value = "0x" + partisi2.StartAdress.ToString("X16");
                    dataGridView1.Rows[index].Cells["endaddress"].Value = "0x" + partisi2.EndAddress.ToString("X16");
                    dataGridView1.Rows[index].Cells["length"].Value = "0x" + partisi2.Size.ToString("X16");
                }
            }
            dataGridView1.ClearSelection();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Log1_TextChanged(object sender, EventArgs e)
        {
            Log1.SelectionStart = Log1.Text.Length;
            Log1.ScrollToCaret();

        }

        private void log_TextChanged(object sender, EventArgs e)
        {
            log.SelectionStart = log.Text.Length;
            log.ScrollToCaret();
        }
    }
}
