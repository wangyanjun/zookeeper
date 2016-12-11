// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Proto.CheckVersionRequest
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
    public class CheckVersionRequest : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(CheckVersionRequest));

        public string Path { get; set; }

        public int Version { get; set; }

        public CheckVersionRequest()
        {
        }

        public CheckVersionRequest(string path, int version)
        {
            this.Path = path;
            this.Version = version;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteString(this.Path, "path");
            a_.WriteInt(this.Version, "version");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.Path = a_.ReadString("path");
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
                    binaryOutputArchive.WriteInt(this.Version, "version");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                CheckVersionRequest.log.Error((object)ex);
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
            CheckVersionRequest checkVersionRequest = (CheckVersionRequest)obj;
            if (checkVersionRequest == null)
                throw new InvalidOperationException("Comparing different types of records.");
            int num1 = this.Path.CompareTo(checkVersionRequest.Path);
            if (num1 != 0)
                return num1;
            int num2 = this.Version == checkVersionRequest.Version ? 0 : (this.Version < checkVersionRequest.Version ? -1 : 1);
            if (num2 != 0)
                return num2;
            return num2;
        }

        public override bool Equals(object obj)
        {
            CheckVersionRequest checkVersionRequest = (CheckVersionRequest)obj;
            if (checkVersionRequest == null)
                return false;
            if (object.ReferenceEquals((object)checkVersionRequest, (object)this))
                return true;
            bool flag1 = this.Path.Equals(checkVersionRequest.Path);
            if (!flag1)
                return flag1;
            bool flag2 = this.Version == checkVersionRequest.Version;
            if (!flag2)
                return flag2;
            return flag2;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + this.Path.GetHashCode()) + this.Version;
        }

        public static string Signature()
        {
            return "LCheckVersionRequest(si)";
        }
    }
}
