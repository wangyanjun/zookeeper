// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Proto.ConnectResponse
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
    public class ConnectResponse : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(ConnectResponse));

        public int ProtocolVersion { get; set; }

        public int TimeOut { get; set; }

        public long SessionId { get; set; }

        public byte[] Passwd { get; set; }

        public ConnectResponse()
        {
        }

        public ConnectResponse(int protocolVersion, int timeOut, long sessionId, byte[] passwd)
        {
            this.ProtocolVersion = protocolVersion;
            this.TimeOut = timeOut;
            this.SessionId = sessionId;
            this.Passwd = passwd;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteInt(this.ProtocolVersion, "protocolVersion");
            a_.WriteInt(this.TimeOut, "timeOut");
            a_.WriteLong(this.SessionId, "sessionId");
            a_.WriteBuffer(this.Passwd, "passwd");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.ProtocolVersion = a_.ReadInt("protocolVersion");
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
                ConnectResponse.log.Error((object)ex);
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
            ConnectResponse connectResponse = (ConnectResponse)obj;
            if (connectResponse == null)
                throw new InvalidOperationException("Comparing different types of records.");
            int num1 = this.ProtocolVersion == connectResponse.ProtocolVersion ? 0 : (this.ProtocolVersion < connectResponse.ProtocolVersion ? -1 : 1);
            if (num1 != 0)
                return num1;
            int num2 = this.TimeOut == connectResponse.TimeOut ? 0 : (this.TimeOut < connectResponse.TimeOut ? -1 : 1);
            if (num2 != 0)
                return num2;
            int num3 = this.SessionId == connectResponse.SessionId ? 0 : (this.SessionId < connectResponse.SessionId ? -1 : 1);
            if (num3 != 0)
                return num3;
            int num4 = this.Passwd.CompareTo(connectResponse.Passwd);
            if (num4 != 0)
                return num4;
            return num4;
        }

        public override bool Equals(object obj)
        {
            ConnectResponse connectResponse = (ConnectResponse)obj;
            if (connectResponse == null)
                return false;
            if (object.ReferenceEquals((object)connectResponse, (object)this))
                return true;
            bool flag1 = this.ProtocolVersion == connectResponse.ProtocolVersion;
            if (!flag1)
                return flag1;
            bool flag2 = this.TimeOut == connectResponse.TimeOut;
            if (!flag2)
                return flag2;
            bool flag3 = this.SessionId == connectResponse.SessionId;
            if (!flag3)
                return flag3;
            bool flag4 = this.Passwd.Equals((object)connectResponse.Passwd);
            if (!flag4)
                return flag4;
            return flag4;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + this.ProtocolVersion) + this.TimeOut) + (int)this.SessionId) + this.Passwd.GetHashCode();
        }

        public static string Signature()
        {
            return "LConnectResponse(iilB)";
        }
    }
}
