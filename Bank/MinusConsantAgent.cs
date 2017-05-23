﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Bank
{
    class MinusConsantAgent : Agent
    {
        private float AccountStatus { get; set; }
        int i = 0;
        bool lockToken = false;
        private int ID;
        public MinusConsantAgent()
        {
            ID = ++id;
        }
        public override void Update()
        {
            lockToken = false;
            ///mutex
            ///Program._BankServers[0].mutex.WaitOne();

            //lock(Program._BankServers[0])
            //spinlock
            Console.WriteLine("Try " + ID);
            try
            {
                ///SpinLock
                Program._BankServers[0].spinLock.TryEnter(0, ref lockToken);


                i++;
                AccountStatus = Program._BankServers[0].AccountStatus;
                AccountStatus -= 50;
                Program._BankServers[0].AccountStatus = AccountStatus;
                Console.WriteLine("Minus Constant Agent Account Status=" + AccountStatus+' '+ID);
                if (i == 10) { HasFinished = true; }
            }
            finally { if(lockToken) { Program._BankServers[0].spinLock.Exit(); } }
            //Mutex
            //Program._BankServers[0].mutex.ReleaseMutex();

        }
    }
}
