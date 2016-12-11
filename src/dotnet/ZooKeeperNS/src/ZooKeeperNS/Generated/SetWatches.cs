// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Proto.SetWatches
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
    public class SetWatches : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(SetWatches));

        public long RelativeZxid { get; set; }

        public IEnumerable<string> DataWatches { get; set; }

        public IEnumerable<string> ExistWatches { get; set; }

        public IEnumerable<string> ChildWatches { get; set; }

        public SetWatches()
        {
        }

        public SetWatches(long relativeZxid, IEnumerable<string> dataWatches, IEnumerable<string> existWatches, IEnumerable<string> childWatches)
        {
            this.RelativeZxid = relativeZxid;
            this.DataWatches = dataWatches;
            this.ExistWatches = existWatches;
            this.ChildWatches = childWatches;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteLong(this.RelativeZxid, "relativeZxid");
            a_.StartVector<string>(this.DataWatches, "dataWatches");
            if (this.DataWatches != null)
            {
                foreach (string dataWatch in this.DataWatches)
                    a_.WriteString(dataWatch, dataWatch);
            }
            a_.EndVector<string>(this.DataWatches, "dataWatches");
            a_.StartVector<string>(this.ExistWatches, "existWatches");
            if (this.ExistWatches != null)
            {
                foreach (string existWatch in this.ExistWatches)
                    a_.WriteString(existWatch, existWatch);
            }
            a_.EndVector<string>(this.ExistWatches, "existWatches");
            a_.StartVector<string>(this.ChildWatches, "childWatches");
            if (this.ChildWatches != null)
            {
                foreach (string childWatch in this.ChildWatches)
                    a_.WriteString(childWatch, childWatch);
            }
            a_.EndVector<string>(this.ChildWatches, "childWatches");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.RelativeZxid = a_.ReadLong("relativeZxid");
            IIndex index1 = a_.StartVector("dataWatches");
            if (index1 != null)
            {
                List<string> stringList = new List<string>();
                while (!index1.Done())
                {
                    string str = a_.ReadString("e1");
                    stringList.Add(str);
                    index1.Incr();
                }
                this.DataWatches = (IEnumerable<string>)stringList;
            }
            a_.EndVector("dataWatches");
            IIndex index2 = a_.StartVector("existWatches");
            if (index2 != null)
            {
                List<string> stringList = new List<string>();
                while (!index2.Done())
                {
                    string str = a_.ReadString("e1");
                    stringList.Add(str);
                    index2.Incr();
                }
                this.ExistWatches = (IEnumerable<string>)stringList;
            }
            a_.EndVector("existWatches");
            IIndex index3 = a_.StartVector("childWatches");
            if (index3 != null)
            {
                List<string> stringList = new List<string>();
                while (!index3.Done())
                {
                    string str = a_.ReadString("e1");
                    stringList.Add(str);
                    index3.Incr();
                }
                this.ChildWatches = (IEnumerable<string>)stringList;
            }
            a_.EndVector("childWatches");
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
                    binaryOutputArchive.WriteLong(this.RelativeZxid, "relativeZxid");
                    binaryOutputArchive.StartVector<string>(this.DataWatches, "dataWatches");
                    if (this.DataWatches != null)
                    {
                        foreach (string dataWatch in this.DataWatches)
                            binaryOutputArchive.WriteString(dataWatch, dataWatch);
                    }
                    binaryOutputArchive.EndVector<string>(this.DataWatches, "dataWatches");
                    binaryOutputArchive.StartVector<string>(this.ExistWatches, "existWatches");
                    if (this.ExistWatches != null)
                    {
                        foreach (string existWatch in this.ExistWatches)
                            binaryOutputArchive.WriteString(existWatch, existWatch);
                    }
                    binaryOutputArchive.EndVector<string>(this.ExistWatches, "existWatches");
                    binaryOutputArchive.StartVector<string>(this.ChildWatches, "childWatches");
                    if (this.ChildWatches != null)
                    {
                        foreach (string childWatch in this.ChildWatches)
                            binaryOutputArchive.WriteString(childWatch, childWatch);
                    }
                    binaryOutputArchive.EndVector<string>(this.ChildWatches, "childWatches");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                SetWatches.log.Error((object)ex);
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
            throw new InvalidOperationException("comparing SetWatches is unimplemented");
        }

        public override bool Equals(object obj)
        {
            SetWatches setWatches = (SetWatches)obj;
            if (setWatches == null)
                return false;
            if (object.ReferenceEquals((object)setWatches, (object)this))
                return true;
            bool flag1 = this.RelativeZxid == setWatches.RelativeZxid;
            if (!flag1)
                return flag1;
            bool flag2 = this.DataWatches.Equals((object)setWatches.DataWatches);
            if (!flag2)
                return flag2;
            bool flag3 = this.ExistWatches.Equals((object)setWatches.ExistWatches);
            if (!flag3)
                return flag3;
            bool flag4 = this.ChildWatches.Equals((object)setWatches.ChildWatches);
            if (!flag4)
                return flag4;
            return flag4;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + (int)this.RelativeZxid) + this.DataWatches.GetHashCode()) + this.ExistWatches.GetHashCode()) + this.ChildWatches.GetHashCode();
        }

        public static string Signature()
        {
            return "LSetWatches(l[s][s][s])";
        }
    }
}
