// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Proto.CreateRequest
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
    public class CreateRequest : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(CreateRequest));

        public string Path { get; set; }

        public byte[] Data { get; set; }

        public IEnumerable<ACL> Acl { get; set; }

        public int Flags { get; set; }

        public CreateRequest()
        {
        }

        public CreateRequest(string path, byte[] data, IEnumerable<ACL> acl, int flags)
        {
            this.Path = path;
            this.Data = data;
            this.Acl = acl;
            this.Flags = flags;
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
            a_.WriteInt(this.Flags, "flags");
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
            this.Flags = a_.ReadInt("flags");
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
                    binaryOutputArchive.WriteInt(this.Flags, "flags");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                CreateRequest.log.Error((object)ex);
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
            throw new InvalidOperationException("comparing CreateRequest is unimplemented");
        }

        public override bool Equals(object obj)
        {
            CreateRequest createRequest = (CreateRequest)obj;
            if (createRequest == null)
                return false;
            if (object.ReferenceEquals((object)createRequest, (object)this))
                return true;
            bool flag1 = this.Path.Equals(createRequest.Path);
            if (!flag1)
                return flag1;
            bool flag2 = this.Data.Equals((object)createRequest.Data);
            if (!flag2)
                return flag2;
            bool flag3 = this.Acl.Equals((object)createRequest.Acl);
            if (!flag3)
                return flag3;
            bool flag4 = this.Flags == createRequest.Flags;
            if (!flag4)
                return flag4;
            return flag4;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + this.Path.GetHashCode()) + this.Data.GetHashCode()) + this.Acl.GetHashCode()) + this.Flags;
        }

        public static string Signature()
        {
            return "LCreateRequest(sB[LACL(iLId(ss))]i)";
        }
    }
}
