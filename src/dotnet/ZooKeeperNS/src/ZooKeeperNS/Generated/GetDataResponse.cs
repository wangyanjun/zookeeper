// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Proto.GetDataResponse
// Assembly: ZooKeeperNet, Version=3.4.6.1, Culture=neutral, PublicKeyToken=fefd2c046da35b56
// MVID: 50E921CD-B200-42A1-B32B-D0116EE2CA01
// Assembly location: C:\Users\wangy\Documents\Visual Studio 2015\Projects\zkDemo\zkDemo\bin\Debug\ZooKeeperNet.dll

using log4net;
using Org.Apache.Jute;
using Org.Apache.Zookeeper.Data;
using System;
using System.IO;
using System.Text;
using ZooKeeperNet.IO;

namespace Org.Apache.Zookeeper.Proto
{
    public class GetDataResponse : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(GetDataResponse));

        public byte[] Data { get; set; }

        public Stat Stat { get; set; }

        public GetDataResponse()
        {
        }

        public GetDataResponse(byte[] data, Stat stat)
        {
            this.Data = data;
            this.Stat = stat;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteBuffer(this.Data, "data");
            a_.WriteRecord((IRecord)this.Stat, "stat");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.Data = a_.ReadBuffer("data");
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
                    binaryOutputArchive.WriteBuffer(this.Data, "data");
                    binaryOutputArchive.WriteRecord((IRecord)this.Stat, "stat");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                GetDataResponse.log.Error((object)ex);
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
            GetDataResponse getDataResponse = (GetDataResponse)obj;
            if (getDataResponse == null)
                throw new InvalidOperationException("Comparing different types of records.");
            int num1 = this.Data.CompareTo(getDataResponse.Data);
            if (num1 != 0)
                return num1;
            int num2 = this.Stat.CompareTo((object)getDataResponse.Stat);
            if (num2 != 0)
                return num2;
            return num2;
        }

        public override bool Equals(object obj)
        {
            GetDataResponse getDataResponse = (GetDataResponse)obj;
            if (getDataResponse == null)
                return false;
            if (object.ReferenceEquals((object)getDataResponse, (object)this))
                return true;
            bool flag1 = this.Data.Equals((object)getDataResponse.Data);
            if (!flag1)
                return flag1;
            bool flag2 = this.Stat.Equals((object)getDataResponse.Stat);
            if (!flag2)
                return flag2;
            return flag2;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + this.Data.GetHashCode()) + this.Stat.GetHashCode();
        }

        public static string Signature()
        {
            return "LGetDataResponse(BLStat(lllliiiliil))";
        }
    }
}
