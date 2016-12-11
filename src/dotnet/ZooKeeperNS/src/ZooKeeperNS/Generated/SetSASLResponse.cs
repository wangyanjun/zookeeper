// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Proto.SetSASLResponse
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
    public class SetSASLResponse : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(SetSASLResponse));

        public byte[] Token { get; set; }

        public SetSASLResponse()
        {
        }

        public SetSASLResponse(byte[] token)
        {
            this.Token = token;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteBuffer(this.Token, "token");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.Token = a_.ReadBuffer("token");
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
                    binaryOutputArchive.WriteBuffer(this.Token, "token");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                SetSASLResponse.log.Error((object)ex);
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
            SetSASLResponse setSaslResponse = (SetSASLResponse)obj;
            if (setSaslResponse == null)
                throw new InvalidOperationException("Comparing different types of records.");
            int num = this.Token.CompareTo(setSaslResponse.Token);
            if (num != 0)
                return num;
            return num;
        }

        public override bool Equals(object obj)
        {
            SetSASLResponse setSaslResponse = (SetSASLResponse)obj;
            if (setSaslResponse == null)
                return false;
            if (object.ReferenceEquals((object)setSaslResponse, (object)this))
                return true;
            bool flag = this.Token.Equals((object)setSaslResponse.Token);
            if (!flag)
                return flag;
            return flag;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * 17 + this.GetType().GetHashCode()) + this.Token.GetHashCode();
        }

        public static string Signature()
        {
            return "LSetSASLResponse(B)";
        }
    }
}
