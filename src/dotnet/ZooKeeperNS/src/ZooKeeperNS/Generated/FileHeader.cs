// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Server.Persistence.FileHeader
// Assembly: ZooKeeperNet, Version=3.4.6.1, Culture=neutral, PublicKeyToken=fefd2c046da35b56
// MVID: 50E921CD-B200-42A1-B32B-D0116EE2CA01
// Assembly location: C:\Users\wangy\Documents\Visual Studio 2015\Projects\zkDemo\zkDemo\bin\Debug\ZooKeeperNet.dll

using log4net;
using Org.Apache.Jute;
using System;
using System.IO;
using System.Text;
using ZooKeeperNet.IO;

namespace Org.Apache.Zookeeper.Server.Persistence
{
    public class FileHeader : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(FileHeader));

        public int Magic { get; set; }

        public int Version { get; set; }

        public long Dbid { get; set; }

        public FileHeader()
        {
        }

        public FileHeader(int magic, int version, long dbid)
        {
            this.Magic = magic;
            this.Version = version;
            this.Dbid = dbid;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteInt(this.Magic, "magic");
            a_.WriteInt(this.Version, "version");
            a_.WriteLong(this.Dbid, "dbid");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.Magic = a_.ReadInt("magic");
            this.Version = a_.ReadInt("version");
            this.Dbid = a_.ReadLong("dbid");
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
                    binaryOutputArchive.WriteInt(this.Magic, "magic");
                    binaryOutputArchive.WriteInt(this.Version, "version");
                    binaryOutputArchive.WriteLong(this.Dbid, "dbid");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                FileHeader.log.Error((object)ex);
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
            FileHeader fileHeader = (FileHeader)obj;
            if (fileHeader == null)
                throw new InvalidOperationException("Comparing different types of records.");
            int num1 = this.Magic == fileHeader.Magic ? 0 : (this.Magic < fileHeader.Magic ? -1 : 1);
            if (num1 != 0)
                return num1;
            int num2 = this.Version == fileHeader.Version ? 0 : (this.Version < fileHeader.Version ? -1 : 1);
            if (num2 != 0)
                return num2;
            int num3 = this.Dbid == fileHeader.Dbid ? 0 : (this.Dbid < fileHeader.Dbid ? -1 : 1);
            if (num3 != 0)
                return num3;
            return num3;
        }

        public override bool Equals(object obj)
        {
            FileHeader fileHeader = (FileHeader)obj;
            if (fileHeader == null)
                return false;
            if (object.ReferenceEquals((object)fileHeader, (object)this))
                return true;
            bool flag1 = this.Magic == fileHeader.Magic;
            if (!flag1)
                return flag1;
            bool flag2 = this.Version == fileHeader.Version;
            if (!flag2)
                return flag2;
            bool flag3 = this.Dbid == fileHeader.Dbid;
            if (!flag3)
                return flag3;
            return flag3;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + this.Magic) + this.Version) + (int)this.Dbid;
        }

        public static string Signature()
        {
            return "LFileHeader(iil)";
        }
    }
}
