// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Proto.GetACLResponse
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

namespace Org.Apache.Zookeeper.Proto
{
    public class GetACLResponse : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(GetACLResponse));

        public IEnumerable<ACL> Acl { get; set; }

        public Stat Stat { get; set; }

        public GetACLResponse()
        {
        }

        public GetACLResponse(IEnumerable<ACL> acl, Stat stat)
        {
            this.Acl = acl;
            this.Stat = stat;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.StartVector<ACL>(this.Acl, "acl");
            if (this.Acl != null)
            {
                foreach (ACL acl in this.Acl)
                    a_.WriteRecord((IRecord)acl, "e1");
            }
            a_.EndVector<ACL>(this.Acl, "acl");
            a_.WriteRecord((IRecord)this.Stat, "stat");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
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
            this.Stat = new Stat();
            a_.ReadRecord((IRecord)this.Stat, "stat");
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
                    binaryOutputArchive.StartVector<ACL>(this.Acl, "acl");
                    if (this.Acl != null)
                    {
                        foreach (ACL acl in this.Acl)
                            binaryOutputArchive.WriteRecord((IRecord)acl, "e1");
                    }
                    binaryOutputArchive.EndVector<ACL>(this.Acl, "acl");
                    binaryOutputArchive.WriteRecord((IRecord)this.Stat, "stat");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                GetACLResponse.log.Error((object)ex);
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
            throw new InvalidOperationException("comparing GetACLResponse is unimplemented");
        }

        public override bool Equals(object obj)
        {
            GetACLResponse getAclResponse = (GetACLResponse)obj;
            if (getAclResponse == null)
                return false;
            if (object.ReferenceEquals((object)getAclResponse, (object)this))
                return true;
            bool flag1 = this.Acl.Equals((object)getAclResponse.Acl);
            if (!flag1)
                return flag1;
            bool flag2 = this.Stat.Equals((object)getAclResponse.Stat);
            if (!flag2)
                return flag2;
            return flag2;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + this.Acl.GetHashCode()) + this.Stat.GetHashCode();
        }

        public static string Signature()
        {
            return "LGetACLResponse([LACL(iLId(ss))]LStat(lllliiiliil))";
        }
    }
}
