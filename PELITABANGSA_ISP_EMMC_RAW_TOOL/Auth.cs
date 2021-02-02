using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace PELITABANGSA_ISP_EMMC_RAW_TOOL
{
    internal class Auth
    {
        private static string AuthPath = "C:\\ProgramData\\PELITABANGSA";
        private string Auth_Licence = AuthPath + "\\PELITABANGSA.lic";


        public Auth()
        {
        }

        internal string ID()
        {
            return GetID("CPU >> " + cpuId() + "\nDisk >> " + diskId() + "\nMotherboard >> " + baseId() + "\nMac >> " + macId());
        }

        private string KEY()
        {
            return GetKey(ID());
        }

        internal bool IsValid()
        {
            if (File.Exists(Auth_Licence))
            {
                return true;
            }
            return false;
        }

        internal bool IsValid(string id)
        {
            var key = KEY();
            if (key.Equals(id))
            {
                GenerateLicence(key);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GenerateLicence(string key)
        {
            try
            {
                if (!Directory.Exists(AuthPath))
                {
                    Directory.CreateDirectory(AuthPath);
                    DirectoryInfo dirinfo = new DirectoryInfo(AuthPath);
                    dirinfo.Attributes = FileAttributes.Hidden | FileAttributes.System;
                }
                Write(key, Auth_Licence);
                FileInfo fileInfo = new FileInfo(Auth_Licence);
                fileInfo.Attributes = FileAttributes.Hidden | FileAttributes.System;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Data.ToString());
            }
        }

        #region Original Device ID Getting Code
        //Return a hardware identifier
        private string identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            ManagementClass mc = new ManagementClass(wmiClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return result;
        }

        private string GetID(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            //return GetHexString(sec.ComputeHash(bt));
            byte[] x = sec.ComputeHash(bt);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < x.Length; i++)
            {
                stringBuilder.Append(x[i].ToString("X2"));
            }
            return stringBuilder.ToString();
        }

        private string GetKey(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            return GetHexString(sec.ComputeHash(bt));
        }

        private string GetHexString(byte[] bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Length; i++)
            {
                byte b = bt[i];
                int n, n1, n2;
                n = b;
                n1 = n & 15;
                n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + 'A')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char)(n1 - 10 + 'A')).ToString();
                else
                    s += n1.ToString();
                if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
            }
            return s;
        }
        //Return a hardware identifier
        private string identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            ManagementClass mc = new ManagementClass(wmiClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                //Only get the first one
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }

        private string cpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as it is very time consuming
            string retVal = identifier("Win32_Processor", "UniqueId");
            if (retVal == "") //If no UniqueID, use ProcessorID
            {
                retVal = identifier("Win32_Processor", "ProcessorId");
                if (retVal == "") //If no ProcessorId, use Name
                {
                    retVal = identifier("Win32_Processor", "Name");
                    if (retVal == "") //If no Name, use Manufacturer
                    {
                        retVal = identifier("Win32_Processor", "Manufacturer");
                    }

                }
            }
            return retVal;
        }
        //BIOS Identifier
        private string biosId()
        {
            return identifier("Win32_BIOS", "Manufacturer")
            + identifier("Win32_BIOS", "SMBIOSBIOSVersion")
            + identifier("Win32_BIOS", "IdentificationCode")
            + identifier("Win32_BIOS", "SerialNumber")
            + identifier("Win32_BIOS", "ReleaseDate")
            + identifier("Win32_BIOS", "Version");
        }
        //Main physical hard drive ID
        private string diskId()
        {
            return identifier("Win32_DiskDrive", "Model")
            + identifier("Win32_DiskDrive", "Manufacturer")
            + identifier("Win32_DiskDrive", "Signature")
            + identifier("Win32_DiskDrive", "TotalHeads");
        }
        //Motherboard ID
        private string baseId()
        {
            return identifier("Win32_BaseBoard", "Model")
            + identifier("Win32_BaseBoard", "Manufacturer")
            + identifier("Win32_BaseBoard", "Name")
            + identifier("Win32_BaseBoard", "SerialNumber");
        }
        //Primary video controller ID
        private string videoId()
        {
            return identifier("Win32_VideoController", "DriverVersion") + identifier("Win32_VideoController", "Name");
        }
        //First enabled network card ID
        private string macId()
        {
            return identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
        }
        #endregion

        private string Read(string txt)
        {
            string result = string.Empty;
            using (StringReader stringReader = new StringReader(File.ReadAllText(txt)))
            {
                while (stringReader.Peek() != -1)
                {
                    result = stringReader.ReadLine();
                }
            }
            return result;
        }

        private void Write(string word, string txt)
        {
            new FileInfo(txt);
            using (StreamWriter streamWriter = new StreamWriter(txt))
            {
                streamWriter.WriteLine(word);
            }
        }
    }
}
