using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDS_sync
{
    public class ReaderWriterLockSlimCS
    {
        private static ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();
        private static int sharedData = 0;

        public static void ReadData()
        {
            while (true)
            {
                rwLock.EnterReadLock();
                try
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name}:\t \"Čítam zdieľané dáta: {sharedData}\"");
                    Thread.Sleep(500); // Simulácia čítania
                }
                finally
                {
                    rwLock.ExitReadLock();
                }
            }
        }

        public static void WriteData()
        {
            while (true)
            {
                rwLock.EnterWriteLock();
                try
                {
                    sharedData += 10;
                    Console.WriteLine($"{Thread.CurrentThread.Name}:\t \"Zapisujem do zdieľaných dát: {sharedData}\"");
                    Thread.Sleep(500); // Simulácia zápisu
                }
                finally
                {
                    rwLock.ExitWriteLock();
                    Thread.Sleep(5000); // Odmlčanie zápisu
                }
            }
        }
    }
}
