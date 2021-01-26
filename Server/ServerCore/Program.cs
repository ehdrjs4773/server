﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{

    // [ JobQueue ]

    class Program
    {
        static ThreadLocal<string> ThreadName = new ThreadLocal<string>(() => { return $"My Name Is { Thread.CurrentThread.ManagedThreadId}"; });
        
        static void WhoAmI()
        {
            bool repeat = ThreadName.IsValueCreated;

            if(repeat)
                  Console.WriteLine(ThreadName.Value + "(repeat)");
            else
                Console.WriteLine(ThreadName.Value);
        }

        static void Main(string[] args) // 메인 직원
        {
            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(3,3);
            Parallel.Invoke(WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI);


            ThreadName.Dispose();
        }
    }
}
