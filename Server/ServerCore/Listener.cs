﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerCore
{
    class Listener
    {
        Socket _listenSocket;

        public void init(IPEndPoint endPoint)
        {
            _listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // 문지기 교육
            _listenSocket.Bind(endPoint);

            // 영업시작 
            // backlog : 최대 대기수
            _listenSocket.Listen(10);


        }

        public Socket Accept()
        {
            return _listenSocket.Accept();
        }
    }
}
