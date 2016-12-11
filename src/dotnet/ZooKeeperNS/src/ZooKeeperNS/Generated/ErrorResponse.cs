// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Proto.ErrorResponse
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
    public class ErrorResponse : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(ErrorResponse));

        public int Err { get; set; }

        public ErrorResponse()
        {
        }

        public ErrorResponse(int err)
        {
            this.Err = err;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteInt(this.Err, "err");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
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
                    binaryOutputArchive.WriteInt(this.Err, "err");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                ErrorResponse.log.Error((object)ex);
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
            ErrorResponse errorResponse = (ErrorResponse)obj;
            if (errorResponse == null)
                throw new InvalidOperationException("Comparing different types of records.");
            int num = this.Err == errorResponse.Err ? 0 : (this.Err < errorResponse.Err ? -1 : 1);
            if (num != 0)
                return num;
            return num;
        }

        public override bool Equals(object obj)
        {
            ErrorResponse errorResponse = (ErrorResponse)obj;
            if (errorResponse == null)
                return false;
            if (object.ReferenceEquals((object)errorResponse, (object)this))
                return true;
            bool flag = this.Err == errorResponse.Err;
            if (!flag)
                return flag;
            return flag;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * 17 + this.GetType().GetHashCode()) + this.Err;
        }

        public static string Signature()
        {
            return "LErrorResponse(i)";
        }
    }
}
