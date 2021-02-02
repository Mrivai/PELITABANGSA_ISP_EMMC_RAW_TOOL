using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PELITABANGSA_ISP_EMMC_RAW_TOOL
{
    internal class Scanner
    {
        private static HashSet<string> BadProcessnameList = new HashSet<string>();
        private static HashSet<string> BadWindowTextList = new HashSet<string>();

        public static void ScanAndKill()
        {
            if (Scan(true) != 0)
            {
                // Take action, or not - produce late crash; entirely up to you
            }
        }

        /// <summary>
        /// Simple scanner for "bad" processes (debuggers) using .NET code only. (for now)
        /// </summary>
        private static int Scan(bool KillProcess)
        {
            int isBadProcess = 0;

            if (BadProcessnameList.Count == 0 && BadWindowTextList.Count == 0)
            {
                Init();
            }

            Process[] processList = Process.GetProcesses();

            foreach (Process process in processList)
            {
                if (BadProcessnameList.Contains(process.ProcessName) || BadWindowTextList.Contains(process.MainWindowTitle))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("BAD PROCESS FOUND: " + process.ProcessName);

                    isBadProcess = 1;

                    if (KillProcess)
                    {
                        try
                        {
                            process.Kill();
                        }
                        catch (System.ComponentModel.Win32Exception w32ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Win32Exception: " + w32ex.Message);

                            break;
                        }
                        catch (System.NotSupportedException nex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("NotSupportedException: " + nex.Message);

                            break;
                        }
                        catch (System.InvalidOperationException ioex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("InvalidOperationException: " + ioex.Message);

                            break;
                        }
                    }

                    break;
                }
            }

            return isBadProcess;
        }

        /// <summary>
        /// Populate "database" with process names/window names.
        /// Using HashSet for maximum performance
        /// </summary>
        private static int Init()
        {
            if (BadProcessnameList.Count > 0 && BadWindowTextList.Count > 0)
            {
                return 1;
            }
            BadProcessnameList.Add("JustDecompile");
            BadProcessnameList.Add("dnspy");
            BadProcessnameList.Add("dnSpy");
            BadProcessnameList.Add("dnSpy-x86");
            BadProcessnameList.Add("Simple Assembly Explorer");
            BadProcessnameList.Add("de4dot");
            BadProcessnameList.Add("de4dot-x64");
            BadProcessnameList.Add("PE Tool");
            BadProcessnameList.Add("ollydbg");
            BadProcessnameList.Add("ida");
            BadProcessnameList.Add("ida64");
            BadProcessnameList.Add("idag");
            BadProcessnameList.Add("idag64");
            BadProcessnameList.Add("idaw");
            BadProcessnameList.Add("idaw64");
            BadProcessnameList.Add("idaq");
            BadProcessnameList.Add("idaq64");
            BadProcessnameList.Add("idau");
            BadProcessnameList.Add("idau64");
            BadProcessnameList.Add("scylla");
            BadProcessnameList.Add("scylla_x64");
            BadProcessnameList.Add("scylla_x86");
            BadProcessnameList.Add("protection_id");
            BadProcessnameList.Add("x64dbg");
            BadProcessnameList.Add("x32dbg");
            BadProcessnameList.Add("windbg");
            BadProcessnameList.Add("reshacker");
            BadProcessnameList.Add("ImportREC");
            BadProcessnameList.Add("IMMUNITYDEBUGGER");
            BadProcessnameList.Add("MegaDumper");

            BadWindowTextList.Add("JustDecompile");
            BadWindowTextList.Add("dnspy");
            BadWindowTextList.Add("dnSpy");
            BadWindowTextList.Add("dnSpy-x86");
            BadWindowTextList.Add("Simple Assembly Explorer");
            BadWindowTextList.Add("de4dot");
            BadWindowTextList.Add("de4dot-x64");
            BadWindowTextList.Add("PE Tool");
            BadWindowTextList.Add("OLLYDBG");
            BadWindowTextList.Add("ida");
            BadWindowTextList.Add("disassembly");
            BadWindowTextList.Add("scylla");
            BadWindowTextList.Add("Debug");
            BadWindowTextList.Add("[CPU");
            BadWindowTextList.Add("Immunity");
            BadWindowTextList.Add("WinDbg");
            BadWindowTextList.Add("x32dbg");
            BadWindowTextList.Add("x64dbg");
            BadWindowTextList.Add("Import reconstructor");
            BadWindowTextList.Add("MegaDumper");
            BadWindowTextList.Add("MegaDumper 1.0 by CodeCracker / SnD");
            BadWindowTextList.Add("Unpacker");
            BadWindowTextList.Add("unpaker");
            BadWindowTextList.Add("Decryptor");
            BadWindowTextList.Add("decryptor");
            BadWindowTextList.Add("Deobfuscator");
            BadWindowTextList.Add("deobfuscator");
            BadWindowTextList.Add("Confuser");
            BadWindowTextList.Add("confuser");
            BadWindowTextList.Add("ConfuserEX");
            BadWindowTextList.Add("confuserEX");
            BadWindowTextList.Add("NoFuser");
            BadWindowTextList.Add("noFuser");
            BadWindowTextList.Add("UnConfuserEX");
            BadWindowTextList.Add("unConfuserEX");
            BadWindowTextList.Add("Fixer");
            BadWindowTextList.Add("fixer");
            BadWindowTextList.Add("Clarifier");
            BadWindowTextList.Add("clarifier");
            BadWindowTextList.Add("Killer");
            BadWindowTextList.Add("killer");

            return 0;
        }

    }
}
