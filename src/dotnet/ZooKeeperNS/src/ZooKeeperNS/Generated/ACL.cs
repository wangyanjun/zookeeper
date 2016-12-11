// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Data.ACL
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
    public class ACL : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(ACL));

        public int Perms { get; set; }

        public ZKId Id { get; set; }

        public ACL()
        {
        }

        public ACL(int perms, ZKId id)
        {
            this.Perms = perms;
            this.Id = id;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteInt(this.Perms, "perms");
            a_.WriteRecord((IRecord)this.Id, "id");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.Perms = a_.ReadInt("perms");
            this.Id = new ZKId();
            a_.ReadRecord((IRecord)this.Id, "id");
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
                    binaryOutputArchive.WriteInt(this.Perms, "perms");
                    binaryOutputArchive.WriteRecord((IRecord)this.Id, "id");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                ACL.log.Error((object)ex);
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
            ACL acl = (ACL)obj;
            if (acl == null)
                throw new InvalidOperationException("Comparing different types of records.");
            int num1 = this.Perms == acl.Perms ? 0 : (this.Perms < acl.Perms ? -1 : 1);
            if (num1 != 0)
                return num1;
            int num2 = this.Id.CompareTo((object)acl.Id);
            if (num2 != 0)
                return num2;
            return num2;
        }

        public override bool Equals(object obj)
        {
            ACL acl = (ACL)obj;
            if (acl == null)
                return false;
            if (object.ReferenceEquals((object)acl, (object)this))
                return true;
            bool flag1 = this.Perms == acl.Perms;
            if (!flag1)
                return flag1;
            bool flag2 = this.Id.Equals((object)acl.Id);
            if (!flag2)
                return flag2;
            return flag2;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + this.Perms) + this.Id.GetHashCode();
        }

        public static string Signature()
        {
            return "LACL(iLId(ss))";
        }
    }
}
