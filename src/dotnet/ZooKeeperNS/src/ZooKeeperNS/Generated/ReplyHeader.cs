// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Proto.ReplyHeader
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
    public class ReplyHeader : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(ReplyHeader));

        public int Xid { get; set; }

        public long Zxid { get; set; }

        public int Err { get; set; }

        public ReplyHeader()
        {
        }

        public ReplyHeader(int xid, long zxid, int err)
        {
            this.Xid = xid;
            this.Zxid = zxid;
            this.Err = err;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteInt(this.Xid, "xid");
            a_.WriteLong(this.Zxid, "zxid");
            a_.WriteInt(this.Err, "err");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.Xid = a_.ReadInt("xid");
            this.Zxid = a_.ReadLong("zxid");
            this.Err = a_.ReadInt("err");
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
                    binaryOutputArchive.WriteLong(this.Zxid, "zxid");
                    binaryOutputArchive.WriteInt(this.Err, "err");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                ReplyHeader.log.Error((object)ex);
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
            ReplyHeader replyHeader = (ReplyHeader)obj;
            if (replyHeader == null)
                throw new InvalidOperationException("Comparing different types of records.");
            int num1 = this.Xid == replyHeader.Xid ? 0 : (this.Xid < replyHeader.Xid ? -1 : 1);
            if (num1 != 0)
                return num1;
            int num2 = this.Zxid == replyHeader.Zxid ? 0 : (this.Zxid < replyHeader.Zxid ? -1 : 1);
            if (num2 != 0)
                return num2;
            int num3 = this.Err == replyHeader.Err ? 0 : (this.Err < replyHeader.Err ? -1 : 1);
            if (num3 != 0)
                return num3;
            return num3;
        }

        public override bool Equals(object obj)
        {
            ReplyHeader replyHeader = (ReplyHeader)obj;
            if (replyHeader == null)
                return false;
            if (object.ReferenceEquals((object)replyHeader, (object)this))
                return true;
            bool flag1 = this.Xid == replyHeader.Xid;
            if (!flag1)
                return flag1;
            bool flag2 = this.Zxid == replyHeader.Zxid;
            if (!flag2)
                return flag2;
            bool flag3 = this.Err == replyHeader.Err;
            if (!flag3)
                return flag3;
            return flag3;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + this.Xid) + (int)this.Zxid) + this.Err;
        }

        public static string Signature()
        {
            return "LReplyHeader(ili)";
        }
    }
}
