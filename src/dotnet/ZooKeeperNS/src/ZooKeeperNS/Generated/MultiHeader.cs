// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Proto.MultiHeader
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
    public class MultiHeader : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(MultiHeader));

        public int Type { get; set; }

        public bool Done { get; set; }

        public int Err { get; set; }

        public MultiHeader()
        {
        }

        public MultiHeader(int type, bool done, int err)
        {
            this.Type = type;
            this.Done = done;
            this.Err = err;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteInt(this.Type, "type");
            a_.WriteBool(this.Done, "done");
            a_.WriteInt(this.Err, "err");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.Type = a_.ReadInt("type");
            this.Done = a_.ReadBool("done");
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
                    binaryOutputArchive.WriteInt(this.Type, "type");
                    binaryOutputArchive.WriteBool(this.Done, "done");
                    binaryOutputArchive.WriteInt(this.Err, "err");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                MultiHeader.log.Error((object)ex);
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
            MultiHeader multiHeader = (MultiHeader)obj;
            if (multiHeader == null)
                throw new InvalidOperationException("Comparing different types of records.");
            int num1 = this.Type == multiHeader.Type ? 0 : (this.Type < multiHeader.Type ? -1 : 1);
            if (num1 != 0)
                return num1;
            int num2 = this.Done == multiHeader.Done ? 0 : (this.Done ? 1 : -1);
            if (num2 != 0)
                return num2;
            int num3 = this.Err == multiHeader.Err ? 0 : (this.Err < multiHeader.Err ? -1 : 1);
            if (num3 != 0)
                return num3;
            return num3;
        }

        public override bool Equals(object obj)
        {
            MultiHeader multiHeader = (MultiHeader)obj;
            if (multiHeader == null)
                return false;
            if (object.ReferenceEquals((object)multiHeader, (object)this))
                return true;
            bool flag1 = this.Type == multiHeader.Type;
            if (!flag1)
                return flag1;
            bool flag2 = this.Done == multiHeader.Done;
            if (!flag2)
                return flag2;
            bool flag3 = this.Err == multiHeader.Err;
            if (!flag3)
                return flag3;
            return flag3;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + this.Type) + (this.Done ? 0 : 1)) + this.Err;
        }

        public static string Signature()
        {
            return "LMultiHeader(izi)";
        }
    }
}
