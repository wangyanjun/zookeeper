// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Server.Quorum.QuorumPacket
// Assembly: ZooKeeperNet, Version=3.4.6.1, Culture=neutral, PublicKeyToken=fefd2c046da35b56
// MVID: 50E921CD-B200-42A1-B32B-D0116EE2CA01
// Assembly location: C:\Users\wangy\Documents\Visual Studio 2015\Projects\zkDemo\zkDemo\bin\Debug\ZooKeeperNet.dll

using log4net;
using Org.Apache.Jute;
using Org.Apache.Zookeeper.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZooKeeperNet.IO;

namespace Org.Apache.Zookeeper.Server.Quorum
{
    public class QuorumPacket : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(QuorumPacket));

        public int Type { get; set; }

        public long Zxid { get; set; }

        public byte[] Data { get; set; }

        public IEnumerable<ZKId> Authinfo { get; set; }

        public QuorumPacket()
        {
        }

        public QuorumPacket(int type, long zxid, byte[] data, IEnumerable<ZKId> authinfo)
        {
            this.Type = type;
            this.Zxid = zxid;
            this.Data = data;
            this.Authinfo = authinfo;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteInt(this.Type, "type");
            a_.WriteLong(this.Zxid, "zxid");
            a_.WriteBuffer(this.Data, "data");
            a_.StartVector<ZKId>(this.Authinfo, "authinfo");
            if (this.Authinfo != null)
            {
                foreach (ZKId zkId in this.Authinfo)
                    a_.WriteRecord((IRecord)zkId, "e1");
            }
            a_.EndVector<ZKId>(this.Authinfo, "authinfo");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.Type = a_.ReadInt("type");
            this.Zxid = a_.ReadLong("zxid");
            this.Data = a_.ReadBuffer("data");
            IIndex index = a_.StartVector("authinfo");
            if (index != null)
            {
                List<ZKId> zkIdList = new List<ZKId>();
                while (!index.Done())
                {
                    ZKId zkId = new ZKId();
                    a_.ReadRecord((IRecord)zkId, "e1");
                    zkIdList.Add(zkId);
                    index.Incr();
                }
                this.Authinfo = (IEnumerable<ZKId>)zkIdList;
            }
            a_.EndVector("authinfo");
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
                    binaryOutputArchive.WriteInt(this.Type, "type");
                    binaryOutputArchive.WriteLong(this.Zxid, "zxid");
                    binaryOutputArchive.WriteBuffer(this.Data, "data");
                    binaryOutputArchive.StartVector<ZKId>(this.Authinfo, "authinfo");
                    if (this.Authinfo != null)
                    {
                        foreach (ZKId zkId in this.Authinfo)
                            binaryOutputArchive.WriteRecord((IRecord)zkId, "e1");
                    }
                    binaryOutputArchive.EndVector<ZKId>(this.Authinfo, "authinfo");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                QuorumPacket.log.Error((object)ex);
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
            throw new InvalidOperationException("comparing QuorumPacket is unimplemented");
        }

        public override bool Equals(object obj)
        {
            QuorumPacket quorumPacket = (QuorumPacket)obj;
            if (quorumPacket == null)
                return false;
            if (object.ReferenceEquals((object)quorumPacket, (object)this))
                return true;
            bool flag1 = this.Type == quorumPacket.Type;
            if (!flag1)
                return flag1;
            bool flag2 = this.Zxid == quorumPacket.Zxid;
            if (!flag2)
                return flag2;
            bool flag3 = this.Data.Equals((object)quorumPacket.Data);
            if (!flag3)
                return flag3;
            bool flag4 = this.Authinfo.Equals((object)quorumPacket.Authinfo);
            if (!flag4)
                return flag4;
            return flag4;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + this.Type) + (int)this.Zxid) + this.Data.GetHashCode()) + this.Authinfo.GetHashCode();
        }

        public static string Signature()
        {
            return "LQuorumPacket(ilB[LId(ss)])";
        }
    }
}
