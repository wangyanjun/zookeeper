// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Txn.SetACLTxn
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
    public class SetACLTxn : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(SetACLTxn));

        public string Path { get; set; }

        public IEnumerable<ACL> Acl { get; set; }

        public int Version { get; set; }

        public SetACLTxn()
        {
        }

        public SetACLTxn(string path, IEnumerable<ACL> acl, int version)
        {
            this.Path = path;
            this.Acl = acl;
            this.Version = version;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteString(this.Path, "path");
            a_.StartVector<ACL>(this.Acl, "acl");
            if (this.Acl != null)
            {
                foreach (ACL acl in this.Acl)
                    a_.WriteRecord((IRecord)acl, "e1");
            }
            a_.EndVector<ACL>(this.Acl, "acl");
            a_.WriteInt(this.Version, "version");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.Path = a_.ReadString("path");
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
            this.Version = a_.ReadInt("version");
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
                    binaryOutputArchive.StartVector<ACL>(this.Acl, "acl");
                    if (this.Acl != null)
                    {
                        foreach (ACL acl in this.Acl)
                            binaryOutputArchive.WriteRecord((IRecord)acl, "e1");
                    }
                    binaryOutputArchive.EndVector<ACL>(this.Acl, "acl");
                    binaryOutputArchive.WriteInt(this.Version, "version");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                SetACLTxn.log.Error((object)ex);
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
            throw new InvalidOperationException("comparing SetACLTxn is unimplemented");
        }

        public override bool Equals(object obj)
        {
            SetACLTxn setAclTxn = (SetACLTxn)obj;
            if (setAclTxn == null)
                return false;
            if (object.ReferenceEquals((object)setAclTxn, (object)this))
                return true;
            bool flag1 = this.Path.Equals(setAclTxn.Path);
            if (!flag1)
                return flag1;
            bool flag2 = this.Acl.Equals((object)setAclTxn.Acl);
            if (!flag2)
                return flag2;
            bool flag3 = this.Version == setAclTxn.Version;
            if (!flag3)
                return flag3;
            return flag3;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + this.Path.GetHashCode()) + this.Acl.GetHashCode()) + this.Version;
        }

        public static string Signature()
        {
            return "LSetACLTxn(s[LACL(iLId(ss))]i)";
        }
    }
}
