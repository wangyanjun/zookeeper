// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Data.ZKId
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
    public class ZKId : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(ZKId));

        public string Scheme { get; set; }

        public string Id { get; set; }

        public ZKId()
        {
        }

        public ZKId(string scheme, string id)
        {
            this.Scheme = scheme;
            this.Id = id;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteString(this.Scheme, "scheme");
            a_.WriteString(this.Id, "id");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.Scheme = a_.ReadString("scheme");
            this.Id = a_.ReadString("id");
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
                    binaryOutputArchive.WriteString(this.Scheme, "scheme");
                    binaryOutputArchive.WriteString(this.Id, "id");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                ZKId.log.Error((object)ex);
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
            ZKId zkId = (ZKId)obj;
            if (zkId == null)
                throw new InvalidOperationException("Comparing different types of records.");
            int num1 = this.Scheme.CompareTo(zkId.Scheme);
            if (num1 != 0)
                return num1;
            int num2 = this.Id.CompareTo(zkId.Id);
            if (num2 != 0)
                return num2;
            return num2;
        }

        public override bool Equals(object obj)
        {
            ZKId zkId = (ZKId)obj;
            if (zkId == null)
                return false;
            if (object.ReferenceEquals((object)zkId, (object)this))
                return true;
            bool flag1 = this.Scheme.Equals(zkId.Scheme);
            if (!flag1)
                return flag1;
            bool flag2 = this.Id.Equals(zkId.Id);
            if (!flag2)
                return flag2;
            return flag2;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + this.Scheme.GetHashCode()) + this.Id.GetHashCode();
        }

        public static string Signature()
        {
            return "LId(ss)";
        }
    }
}
