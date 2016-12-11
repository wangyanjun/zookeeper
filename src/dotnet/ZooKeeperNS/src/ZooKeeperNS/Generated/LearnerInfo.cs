// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Server.Quorum.LearnerInfo
// Assembly: ZooKeeperNet, Version=3.4.6.1, Culture=neutral, PublicKeyToken=fefd2c046da35b56
// MVID: 50E921CD-B200-42A1-B32B-D0116EE2CA01
// Assembly location: C:\Users\wangy\Documents\Visual Studio 2015\Projects\zkDemo\zkDemo\bin\Debug\ZooKeeperNet.dll

using log4net;
using Org.Apache.Jute;
using System;
using System.IO;
using System.Text;
using ZooKeeperNet.IO;

namespace Org.Apache.Zookeeper.Server.Quorum
{
    public class LearnerInfo : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(LearnerInfo));

        public long Serverid { get; set; }

        public int ProtocolVersion { get; set; }

        public LearnerInfo()
        {
        }

        public LearnerInfo(long serverid, int protocolVersion)
        {
            this.Serverid = serverid;
            this.ProtocolVersion = protocolVersion;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteLong(this.Serverid, "serverid");
            a_.WriteInt(this.ProtocolVersion, "protocolVersion");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.Serverid = a_.ReadLong("serverid");
            this.ProtocolVersion = a_.ReadInt("protocolVersion");
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
                    binaryOutputArchive.WriteLong(this.Serverid, "serverid");
                    binaryOutputArchive.WriteInt(this.ProtocolVersion, "protocolVersion");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                LearnerInfo.log.Error((object)ex);
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
            LearnerInfo learnerInfo = (LearnerInfo)obj;
            if (learnerInfo == null)
                throw new InvalidOperationException("Comparing different types of records.");
            int num1 = this.Serverid == learnerInfo.Serverid ? 0 : (this.Serverid < learnerInfo.Serverid ? -1 : 1);
            if (num1 != 0)
                return num1;
            int num2 = this.ProtocolVersion == learnerInfo.ProtocolVersion ? 0 : (this.ProtocolVersion < learnerInfo.ProtocolVersion ? -1 : 1);
            if (num2 != 0)
                return num2;
            return num2;
        }

        public override bool Equals(object obj)
        {
            LearnerInfo learnerInfo = (LearnerInfo)obj;
            if (learnerInfo == null)
                return false;
            if (object.ReferenceEquals((object)learnerInfo, (object)this))
                return true;
            bool flag1 = this.Serverid == learnerInfo.Serverid;
            if (!flag1)
                return flag1;
            bool flag2 = this.ProtocolVersion == learnerInfo.ProtocolVersion;
            if (!flag2)
                return flag2;
            return flag2;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + (int)this.Serverid) + this.ProtocolVersion;
        }

        public static string Signature()
        {
            return "LLearnerInfo(li)";
        }
    }
}
