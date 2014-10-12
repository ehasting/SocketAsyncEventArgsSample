﻿using System;
using System.Net.Sockets;
using System.Threading;

namespace SocketAsyncServer
{
    public sealed class MessageData
    {
        public AsyncUserToken Token;
        public byte[] Message;
    }
    public sealed class AsyncUserToken : IDisposable
    {
        public AutoResetEvent AutoSendEvent;
        public Socket Socket { get; private set; }
        public SocketAsyncEventArgs SendEventArgs { get; private set; }
        public int? MessageSize { get; set; }
        public int DataStartOffset { get; set; }
        public int NextReceiveOffset { get; set; }

        public AsyncUserToken(Socket socket, SocketAsyncEventArgs sendEventArgs, AutoResetEvent autoResetEvent)
        {
            this.Socket = socket;
            this.SendEventArgs = sendEventArgs;
            this.AutoSendEvent = autoResetEvent;
        }

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                this.Socket.Shutdown(SocketShutdown.Send);
            }
            catch (Exception)
            { }

            try
            {
                this.Socket.Close();
            }
            catch (Exception)
            { }
        }

        #endregion
    }
}
