// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Proto.GetChildrenResponse
// Assembly: ZooKeeperNet, Version=3.4.6.1, Culture=neutral, PublicKeyToken=fefd2c046da35b56
// MVID: 50E921CD-B200-42A1-B32B-D0116EE2CA01
// Assembly location: C:\Users\wangy\Documents\Visual Studio 2015\Projects\zkDemo\zkDemo\bin\Debug\ZooKeeperNet.dll

using log4net;
using Org.Apache.Jute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZooKeeperNet.IO;

namespace Org.Apache.Zookeeper.Proto
{
    public class GetChildrenResponse : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(GetChildrenResponse));

        public IEnumerable<string> Children { get; set; }

        public GetChildrenResponse()
        {
        }

        public GetChildrenResponse(IEnumerable<string> children)
        {
            this.Children = children;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.StartVector<string>(this.Children, "children");
            if (this.Children != null)
            {
                foreach (string child in this.Children)
                    a_.WriteString(child, child);
            }
            a_.EndVector<string>(this.Children, "children");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            IIndex index = a_.StartVector("children");
            if (index != null)
            {
                List<string> stringList = new List<string>();
                while (!index.Done())
                {
                    string str = a_.ReadString("e1");
                    stringList.Add(str);
                    index.Incr();
                }
                this.Children = (IEnumerable<string>)stringList;
            }
            a_.EndVector("children");
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
                    binaryOutputArchive.StartVector<string>(this.Children, "children");
                    if (this.Children != null)
                    {
                        foreach (string child in this.Children)
                            binaryOutputArchive.WriteString(child, child);
                    }
                    binaryOutputArchive.EndVector<string>(this.Children, "children");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                GetChildrenResponse.log.Error((object)ex);
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
            throw new InvalidOperationException("comparing GetChildrenResponse is unimplemented");
        }

        public override bool Equals(object obj)
        {
            GetChildrenResponse childrenResponse = (GetChildrenResponse)obj;
            if (childrenResponse == null)
                return false;
            if (object.ReferenceEquals((object)childrenResponse, (object)this))
                return true;
            bool flag = this.Children.Equals((object)childrenResponse.Children);
            if (!flag)
                return flag;
            return flag;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * 17 + this.GetType().GetHashCode()) + this.Children.GetHashCode();
        }

        public static string Signature()
        {
            return "LGetChildrenResponse([s])";
        }
    }
}
