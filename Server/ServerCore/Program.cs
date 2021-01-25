using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{

    class FastLock
    {
       public int id;
    }

    //메모리 배리어
    // A) 코드 재배치 억제
    // B) 가시성

    // 1) Full Memory Barrier ( ASM MFENCE, C# Thread.MemoryBarrier) : Store/Load 둘다 막는다
    // 2) Store Memory Barrier ( ASM SFENCE) : Store 만 막는다
    // 3) Load Memory Barrier (ASM LFENCE) : Load 만 막는다.
    
   class SessionManager
    {

        static object _lock = new object();

        public static void TestSession()
        {
            lock(_lock)
            {

            }
        }
        public static void Test()
        {
            lock(_lock)
            {
                UserManager.TestUser();
            }
        }

    }
    class UserManager
    {

        static object _lock = new object();

        public static void Test()
        {

            lock (_lock)
            {
                SessionManager.TestSession();
            }
        }

        public static void TestUser()
        {
            lock(_lock)
            {

            }
        }
    }


    class Program
    {
        static volatile int number = 0;
        static object _obj = new object();

        static void Thread_1()
        {
            //atomic = 원자성

            // 집행검 User2 인벤에 넣어라
            // 집행검 User1 인벤에서 없애라

            for(int i = 0; i < 100; i++)
            {
                SessionManager.Test();
            }
        }


        // 데드락 DeadLock 더이상 사용할수 없는 자물쇠
        static void Thread_2()
        {
            for(int i =0;i < 100; i++)
            {
                UserManager.Test();
            }
        }

        static void Main(string[] args) // 메인 직원
        {

            Task t1 = new Task(Thread_1);
            Task t2 = new Task(Thread_2);

            t1.Start();

            Thread.Sleep(100);

            t2.Start();

            Task.WaitAll(t1, t2);

            Console.WriteLine(number);
            
        }
    }
}
