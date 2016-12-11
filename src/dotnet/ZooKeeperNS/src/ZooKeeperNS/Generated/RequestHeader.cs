// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Proto.RequestHeader
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
    public class RequestHeader : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(RequestHeader));

        public int Xid { get; set; }

        public int Type { get; set; }

        public RequestHeader()
        {
        }

        public RequestHeader(int xid, int type)
        {
            this.Xid = xid;
            this.Type = type;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteInt(this.Xid, "xid");
            a_.WriteInt(this.Type, "type");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.Xid = a_.ReadInt("xid");
            this.Type = a_.ReadInt("type");
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
                    binaryOutputArchive.WriteInt(this.Xid, "xid");
                    binaryOutputArchive.WriteInt(this.Type, "type");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                RequestHeader.log.Error((object)ex);
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
            RequestHeader requestHeader = (RequestHeader)obj;
            if (requestHeader == null)
                throw new InvalidOperationException("Comparing different types of records.");
            int num1 = this.Xid == requestHeader.Xid ? 0 : (this.Xid < requestHeader.Xid ? -1 : 1);
            if (num1 != 0)
                return num1;
            int num2 = this.Type == requestHeader.Type ? 0 : (this.Type < requestHeader.Type ? -1 : 1);
            if (num2 != 0)
                return num2;
            return num2;
        }

        public override bool Equals(object obj)
        {
            RequestHeader requestHeader = (RequestHeader)obj;
            if (requestHeader == null)
                return false;
            if (object.ReferenceEquals((object)requestHeader, (object)this))
                return true;
            bool flag1 = this.Xid == requestHeader.Xid;
            if (!flag1)
                return flag1;
            bool flag2 = this.Type == requestHeader.Type;
            if (!flag2)
                return flag2;
            return flag2;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + this.Xid) + this.Type;
        }

        public static string Signature()
        {
            return "LRequestHeader(ii)";
        }
    }
}
