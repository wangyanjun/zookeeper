// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Data.Stat
// Assembly: ZooKeeperNet, Version=3.4.6.1, Culture=neutral, PublicKeyToken=fefd2c046da35b56
// MVID: 50E921CD-B200-42A1-B32B-D0116EE2CA01
// Assembly location: C:\Users\wangy\Documents\Visual Studio 2015\Projects\zkDemo\zkDemo\bin\Debug\ZooKeeperNet.dll

using log4net;
using Org.Apache.Jute;
using System;
using System.IO;
using System.Text;
using ZooKeeperNet.IO;

namespace Org.Apache.Zookeeper.Data
{
    public class Stat : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(Stat));

        public long Czxid { get; set; }

        public long Mzxid { get; set; }

        public long Ctime { get; set; }

        public long Mtime { get; set; }

        public int Version { get; set; }

        public int Cversion { get; set; }

        public int Aversion { get; set; }

        public long EphemeralOwner { get; set; }

        public int DataLength { get; set; }

        public int NumChildren { get; set; }

        public long Pzxid { get; set; }

        public Stat()
        {
        }

        public Stat(long czxid, long mzxid, long ctime, long mtime, int version, int cversion, int aversion, long ephemeralOwner, int dataLength, int numChildren, long pzxid)
        {
            this.Czxid = czxid;
            this.Mzxid = mzxid;
            this.Ctime = ctime;
            this.Mtime = mtime;
            this.Version = version;
            this.Cversion = cversion;
            this.Aversion = aversion;
            this.EphemeralOwner = ephemeralOwner;
            this.DataLength = dataLength;
            this.NumChildren = numChildren;
            this.Pzxid = pzxid;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteLong(this.Czxid, "czxid");
            a_.WriteLong(this.Mzxid, "mzxid");
            a_.WriteLong(this.Ctime, "ctime");
            a_.WriteLong(this.Mtime, "mtime");
            a_.WriteInt(this.Version, "version");
            a_.WriteInt(this.Cversion, "cversion");
            a_.WriteInt(this.Aversion, "aversion");
            a_.WriteLong(this.EphemeralOwner, "ephemeralOwner");
            a_.WriteInt(this.DataLength, "dataLength");
            a_.WriteInt(this.NumChildren, "numChildren");
            a_.WriteLong(this.Pzxid, "pzxid");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.Czxid = a_.ReadLong("czxid");
            this.Mzxid = a_.ReadLong("mzxid");
            this.Ctime = a_.ReadLong("ctime");
            this.Mtime = a_.ReadLong("mtime");
            this.Version = a_.ReadInt("version");
            this.Cversion = a_.ReadInt("cversion");
            this.Aversion = a_.ReadInt("aversion");
            this.EphemeralOwner = a_.ReadLong("ephemeralOwner");
            this.DataLength = a_.ReadInt("dataLength");
            this.NumChildren = a_.ReadInt("numChildren");
            this.Pzxid = a_.ReadLong("pzxid");
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
                    binaryOutputArchive.WriteLong(this.Czxid, "czxid");
                    binaryOutputArchive.WriteLong(this.Mzxid, "mzxid");
                    binaryOutputArchive.WriteLong(this.Ctime, "ctime");
                    binaryOutputArchive.WriteLong(this.Mtime, "mtime");
                    binaryOutputArchive.WriteInt(this.Version, "version");
                    binaryOutputArchive.WriteInt(this.Cversion, "cversion");
                    binaryOutputArchive.WriteInt(this.Aversion, "aversion");
                    binaryOutputArchive.WriteLong(this.EphemeralOwner, "ephemeralOwner");
                    binaryOutputArchive.WriteInt(this.DataLength, "dataLength");
                    binaryOutputArchive.WriteInt(this.NumChildren, "numChildren");
                    binaryOutputArchive.WriteLong(this.Pzxid, "pzxid");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                Stat.log.Error((object)ex);
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
            Stat stat = (Stat)obj;
            if (stat == null)
                throw new InvalidOperationException("Comparing different types of records.");
            int num1 = this.Czxid == stat.Czxid ? 0 : (this.Czxid < stat.Czxid ? -1 : 1);
            if (num1 != 0)
                return num1;
            int num2 = this.Mzxid == stat.Mzxid ? 0 : (this.Mzxid < stat.Mzxid ? -1 : 1);
            if (num2 != 0)
                return num2;
            int num3 = this.Ctime == stat.Ctime ? 0 : (this.Ctime < stat.Ctime ? -1 : 1);
            if (num3 != 0)
                return num3;
            int num4 = this.Mtime == stat.Mtime ? 0 : (this.Mtime < stat.Mtime ? -1 : 1);
            if (num4 != 0)
                return num4;
            int num5 = this.Version == stat.Version ? 0 : (this.Version < stat.Version ? -1 : 1);
            if (num5 != 0)
                return num5;
            int num6 = this.Cversion == stat.Cversion ? 0 : (this.Cversion < stat.Cversion ? -1 : 1);
            if (num6 != 0)
                return num6;
            int num7 = this.Aversion == stat.Aversion ? 0 : (this.Aversion < stat.Aversion ? -1 : 1);
            if (num7 != 0)
                return num7;
            int num8 = this.EphemeralOwner == stat.EphemeralOwner ? 0 : (this.EphemeralOwner < stat.EphemeralOwner ? -1 : 1);
            if (num8 != 0)
                return num8;
            int num9 = this.DataLength == stat.DataLength ? 0 : (this.DataLength < stat.DataLength ? -1 : 1);
            if (num9 != 0)
                return num9;
            int num10 = this.NumChildren == stat.NumChildren ? 0 : (this.NumChildren < stat.NumChildren ? -1 : 1);
            if (num10 != 0)
                return num10;
            int num11 = this.Pzxid == stat.Pzxid ? 0 : (this.Pzxid < stat.Pzxid ? -1 : 1);
            if (num11 != 0)
                return num11;
            return num11;
        }

        public override bool Equals(object obj)
        {
            Stat stat = (Stat)obj;
            if (stat == null)
                return false;
            if (object.ReferenceEquals((object)stat, (object)this))
                return true;
            bool flag1 = this.Czxid == stat.Czxid;
            if (!flag1)
                return flag1;
            bool flag2 = this.Mzxid == stat.Mzxid;
            if (!flag2)
                return flag2;
            bool flag3 = this.Ctime == stat.Ctime;
            if (!flag3)
                return flag3;
            bool flag4 = this.Mtime == stat.Mtime;
            if (!flag4)
                return flag4;
            bool flag5 = this.Version == stat.Version;
            if (!flag5)
                return flag5;
            bool flag6 = this.Cversion == stat.Cversion;
            if (!flag6)
                return flag6;
            bool flag7 = this.Aversion == stat.Aversion;
            if (!flag7)
                return flag7;
            bool flag8 = this.EphemeralOwner == stat.EphemeralOwner;
            if (!flag8)
                return flag8;
            bool flag9 = this.DataLength == stat.DataLength;
            if (!flag9)
                return flag9;
            bool flag10 = this.NumChildren == stat.NumChildren;
            if (!flag10)
                return flag10;
            bool flag11 = this.Pzxid == stat.Pzxid;
            if (!flag11)
                return flag11;
            return flag11;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * (37 * (37 * (37 * (37 * (37 * (37 * (37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + (int)this.Czxid) + (int)this.Mzxid) + (int)this.Ctime) + (int)this.Mtime) + this.Version) + this.Cversion) + this.Aversion) + (int)this.EphemeralOwner) + this.DataLength) + this.NumChildren) + (int)this.Pzxid;
        }

        public static string Signature()
        {
            return "LStat(lllliiiliil)";
        }
    }
}
