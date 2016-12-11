// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Proto.ConnectRequest
// Assembly: ZooKeeperNet, Version=3.4.6.1, Culture=neutral, PublicKeyToken=fefd2c046da35b56
// MVID: 50E921CD-B200-42A1-B32B-D0116EE2CA01
// Assembly location: C:\Users\wangy\Documents\Visual Studio 2015\Projects\zkDemo\zkDemo\bin\Debug\ZooKeeperNet.dll

using log4net;
using Org.Apache.Jute;
using System;
using System.IO;
using System.Text;
using ZooKeeperNet.IO;

namespace Org.Apache.Zookeeper.Proto
{
    public class ConnectRequest : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(ConnectRequest));

        public int ProtocolVersion { get; set; }

        public long LastZxidSeen { get; set; }

        public int TimeOut { get; set; }

        public long SessionId { get; set; }

        public byte[] Passwd { get; set; }

        public ConnectRequest()
        {
        }

        public ConnectRequest(int protocolVersion, long lastZxidSeen, int timeOut, long sessionId, byte[] passwd)
        {
            this.ProtocolVersion = protocolVersion;
            this.LastZxidSeen = lastZxidSeen;
            this.TimeOut = timeOut;
            this.SessionId = sessionId;
            this.Passwd = passwd;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteInt(this.ProtocolVersion, "protocolVersion");
            a_.WriteLong(this.LastZxidSeen, "lastZxidSeen");
            a_.WriteInt(this.TimeOut, "timeOut");
            a_.WriteLong(this.SessionId, "sessionId");
            a_.WriteBuffer(this.Passwd, "passwd");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.ProtocolVersion = a_.ReadInt("protocolVersion");
            this.LastZxidSeen = a_.ReadLong("lastZxidSeen");
            this.TimeOut = a_.ReadInt("timeOut");
            this.SessionId = a_.ReadLong("sessionId");
            this.Passwd = a_.ReadBuffer("passwd");
            a_.EndRecord(tag);
        }

        public override string ToString()
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                using (EndianBinaryWriter writer = new EndianBinaryWriter((EndianBitConverter)EndianBitConverter.Big, (Stream)memoryStream, Encoding.UTF8))
                {
                    BinaryOutputArchive binaryOutputArchive = new BinaryOutputArchive(writer);
                    binaryOutputArchive.StartRecord((IRecord)this, string.Empty);
                    binaryOutputArchive.WriteInt(this.ProtocolVersion, "protocolVersion");
                    binaryOutputArchive.WriteLong(this.LastZxidSeen, "lastZxidSeen");
                    binaryOutputArchive.WriteInt(this.TimeOut, "timeOut");
                    binaryOutputArchive.WriteLong(this.SessionId, "sessionId");
                    binaryOutputArchive.WriteBuffer(this.Passwd, "passwd");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                ConnectRequest.log.Error((object)ex);
            }
            return "ERROR";
        }

        public void Write(EndianBinaryWriter writer)
        {
            this.Serialize((IOutputArchive)new BinaryOutputArchive(writer), string.Empty);
        }

        public void ReadFields(EndianBinaryReader reader)
        {
            this.Deserialize((IInputArchive)new BinaryInputArchive(reader), string.Empty);
        }

        public int CompareTo(object obj)
        {
            ConnectRequest connectRequest = (ConnectRequest)obj;
            if (connectRequest == null)
                throw new InvalidOperationException("Comparing different types of records.");
            int num1 = this.ProtocolVersion == connectRequest.ProtocolVersion ? 0 : (this.ProtocolVersion < connectRequest.ProtocolVersion ? -1 : 1);
            if (num1 != 0)
                return num1;
            int num2 = this.LastZxidSeen == connectRequest.LastZxidSeen ? 0 : (this.LastZxidSeen < connectRequest.LastZxidSeen ? -1 : 1);
            if (num2 != 0)
                return num2;
            int num3 = this.TimeOut == connectRequest.TimeOut ? 0 : (this.TimeOut < connectRequest.TimeOut ? -1 : 1);
            if (num3 != 0)
                return num3;
            int num4 = this.SessionId == connectRequest.SessionId ? 0 : (this.SessionId < connectRequest.SessionId ? -1 : 1);
            if (num4 != 0)
                return num4;
            int num5 = this.Passwd.CompareTo(connectRequest.Passwd);
            if (num5 != 0)
                return num5;
            return num5;
        }

        public override bool Equals(object obj)
        {
            ConnectRequest connectRequest = (ConnectRequest)obj;
            if (connectRequest == null)
                return false;
            if (object.ReferenceEquals((object)connectRequest, (object)this))
                return true;
            bool flag1 = this.ProtocolVersion == connectRequest.ProtocolVersion;
            if (!flag1)
                return flag1;
            bool flag2 = this.LastZxidSeen == connectRequest.LastZxidSeen;
            if (!flag2)
                return flag2;
            bool flag3 = this.TimeOut == connectRequest.TimeOut;
            if (!flag3)
                return flag3;
            bool flag4 = this.SessionId == connectRequest.SessionId;
            if (!flag4)
                return flag4;
            bool flag5 = this.Passwd.Equals((object)connectRequest.Passwd);
            if (!flag5)
                return flag5;
            return flag5;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * (37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + this.ProtocolVersion) + (int)this.LastZxidSeen) + this.TimeOut) + (int)this.SessionId) + this.Passwd.GetHashCode();
        }

        public static string Signature()
        {
            return "LConnectRequest(ililB)";
        }
    }
}