// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Txn.TxnHeader
// Assembly: ZooKeeperNet, Version=3.4.6.1, Culture=neutral, PublicKeyToken=fefd2c046da35b56
// MVID: 50E921CD-B200-42A1-B32B-D0116EE2CA01
// Assembly location: C:\Users\wangy\Documents\Visual Studio 2015\Projects\zkDemo\zkDemo\bin\Debug\ZooKeeperNet.dll

using log4net;
using Org.Apache.Jute;
using System;
using System.IO;
using System.Text;
using ZooKeeperNet.IO;

namespace Org.Apache.Zookeeper.Txn
{
    public class TxnHeader : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(TxnHeader));

        public long ClientId { get; set; }

        public int Cxid { get; set; }

        public long Zxid { get; set; }

        public long Time { get; set; }

        public int Type { get; set; }

        public TxnHeader()
        {
        }

        public TxnHeader(long clientId, int cxid, long zxid, long time, int type)
        {
            this.ClientId = clientId;
            this.Cxid = cxid;
            this.Zxid = zxid;
            this.Time = time;
            this.Type = type;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteLong(this.ClientId, "clientId");
            a_.WriteInt(this.Cxid, "cxid");
            a_.WriteLong(this.Zxid, "zxid");
            a_.WriteLong(this.Time, "time");
            a_.WriteInt(this.Type, "type");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.ClientId = a_.ReadLong("clientId");
            this.Cxid = a_.ReadInt("cxid");
            this.Zxid = a_.ReadLong("zxid");
            this.Time = a_.ReadLong("time");
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
                    binaryOutputArchive.WriteLong(this.ClientId, "clientId");
                    binaryOutputArchive.WriteInt(this.Cxid, "cxid");
                    binaryOutputArchive.WriteLong(this.Zxid, "zxid");
                    binaryOutputArchive.WriteLong(this.Time, "time");
                    binaryOutputArchive.WriteInt(this.Type, "type");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                TxnHeader.log.Error((object)ex);
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
            TxnHeader txnHeader = (TxnHeader)obj;
            if (txnHeader == null)
                throw new InvalidOperationException("Comparing different types of records.");
            int num1 = this.ClientId == txnHeader.ClientId ? 0 : (this.ClientId < txnHeader.ClientId ? -1 : 1);
            if (num1 != 0)
                return num1;
            int num2 = this.Cxid == txnHeader.Cxid ? 0 : (this.Cxid < txnHeader.Cxid ? -1 : 1);
            if (num2 != 0)
                return num2;
            int num3 = this.Zxid == txnHeader.Zxid ? 0 : (this.Zxid < txnHeader.Zxid ? -1 : 1);
            if (num3 != 0)
                return num3;
            int num4 = this.Time == txnHeader.Time ? 0 : (this.Time < txnHeader.Time ? -1 : 1);
            if (num4 != 0)
                return num4;
            int num5 = this.Type == txnHeader.Type ? 0 : (this.Type < txnHeader.Type ? -1 : 1);
            if (num5 != 0)
                return num5;
            return num5;
        }

        public override bool Equals(object obj)
        {
            TxnHeader txnHeader = (TxnHeader)obj;
            if (txnHeader == null)
                return false;
            if (object.ReferenceEquals((object)txnHeader, (object)this))
                return true;
            bool flag1 = this.ClientId == txnHeader.ClientId;
            if (!flag1)
                return flag1;
            bool flag2 = this.Cxid == txnHeader.Cxid;
            if (!flag2)
                return flag2;
            bool flag3 = this.Zxid == txnHeader.Zxid;
            if (!flag3)
                return flag3;
            bool flag4 = this.Time == txnHeader.Time;
            if (!flag4)
                return flag4;
            bool flag5 = this.Type == txnHeader.Type;
            if (!flag5)
                return flag5;
            return flag5;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * (37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + (int)this.ClientId) + this.Cxid) + (int)this.Zxid) + (int)this.Time) + this.Type;
        }

        public static string Signature()
        {
            return "LTxnHeader(lilli)";
        }
    }
}
