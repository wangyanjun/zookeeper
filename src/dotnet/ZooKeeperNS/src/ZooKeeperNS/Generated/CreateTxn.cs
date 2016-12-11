// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Txn.CreateTxn
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

namespace Org.Apache.Zookeeper.Txn
{
    public class CreateTxn : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(CreateTxn));

        public string Path { get; set; }

        public byte[] Data { get; set; }

        public IEnumerable<ACL> Acl { get; set; }

        public bool Ephemeral { get; set; }

        public int ParentCVersion { get; set; }

        public CreateTxn()
        {
        }

        public CreateTxn(string path, byte[] data, IEnumerable<ACL> acl, bool ephemeral, int parentCVersion)
        {
            this.Path = path;
            this.Data = data;
            this.Acl = acl;
            this.Ephemeral = ephemeral;
            this.ParentCVersion = parentCVersion;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteString(this.Path, "path");
            a_.WriteBuffer(this.Data, "data");
            a_.StartVector<ACL>(this.Acl, "acl");
            if (this.Acl != null)
            {
                foreach (ACL acl in this.Acl)
                    a_.WriteRecord((IRecord)acl, "e1");
            }
            a_.EndVector<ACL>(this.Acl, "acl");
            a_.WriteBool(this.Ephemeral, "ephemeral");
            a_.WriteInt(this.ParentCVersion, "parentCVersion");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.Path = a_.ReadString("path");
            this.Data = a_.ReadBuffer("data");
            IIndex index = a_.StartVector("acl");
            if (index != null)
            {
                List<ACL> aclList = new List<ACL>();
                while (!index.Done())
                {
                    ACL acl = new ACL();
                    a_.ReadRecord((IRecord)acl, "e1");
                    aclList.Add(acl);
                    index.Incr();
                }
                this.Acl = (IEnumerable<ACL>)aclList;
            }
            a_.EndVector("acl");
            this.Ephemeral = a_.ReadBool("ephemeral");
            this.ParentCVersion = a_.ReadInt("parentCVersion");
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
                    binaryOutputArchive.WriteString(this.Path, "path");
                    binaryOutputArchive.WriteBuffer(this.Data, "data");
                    binaryOutputArchive.StartVector<ACL>(this.Acl, "acl");
                    if (this.Acl != null)
                    {
                        foreach (ACL acl in this.Acl)
                            binaryOutputArchive.WriteRecord((IRecord)acl, "e1");
                    }
                    binaryOutputArchive.EndVector<ACL>(this.Acl, "acl");
                    binaryOutputArchive.WriteBool(this.Ephemeral, "ephemeral");
                    binaryOutputArchive.WriteInt(this.ParentCVersion, "parentCVersion");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                CreateTxn.log.Error((object)ex);
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
            throw new InvalidOperationException("comparing CreateTxn is unimplemented");
        }

        public override bool Equals(object obj)
        {
            CreateTxn createTxn = (CreateTxn)obj;
            if (createTxn == null)
                return false;
            if (object.ReferenceEquals((object)createTxn, (object)this))
                return true;
            bool flag1 = this.Path.Equals(createTxn.Path);
            if (!flag1)
                return flag1;
            bool flag2 = this.Data.Equals((object)createTxn.Data);
            if (!flag2)
                return flag2;
            bool flag3 = this.Acl.Equals((object)createTxn.Acl);
            if (!flag3)
                return flag3;
            bool flag4 = this.Ephemeral == createTxn.Ephemeral;
            if (!flag4)
                return flag4;
            bool flag5 = this.ParentCVersion == createTxn.ParentCVersion;
            if (!flag5)
                return flag5;
            return flag5;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * (37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + this.Path.GetHashCode()) + this.Data.GetHashCode()) + this.Acl.GetHashCode()) + (this.Ephemeral ? 0 : 1)) + this.ParentCVersion;
        }

        public static string Signature()
        {
            return "LCreateTxn(sB[LACL(iLId(ss))]zi)";
        }
    }
}
