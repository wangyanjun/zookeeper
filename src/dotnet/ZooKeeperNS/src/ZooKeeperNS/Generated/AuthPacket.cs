// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Proto.AuthPacket
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
    public class AuthPacket : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(AuthPacket));

        public int Type { get; set; }

        public string Scheme { get; set; }

        public byte[] Auth { get; set; }

        public AuthPacket()
        {
        }

        public AuthPacket(int type, string scheme, byte[] auth)
        {
            this.Type = type;
            this.Scheme = scheme;
            this.Auth = auth;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteInt(this.Type, "type");
            a_.WriteString(this.Scheme, "scheme");
            a_.WriteBuffer(this.Auth, "auth");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.Type = a_.ReadInt("type");
            this.Scheme = a_.ReadString("scheme");
            this.Auth = a_.ReadBuffer("auth");
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
                    binaryOutputArchive.WriteInt(this.Type, "type");
                    binaryOutputArchive.WriteString(this.Scheme, "scheme");
                    binaryOutputArchive.WriteBuffer(this.Auth, "auth");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                AuthPacket.log.Error((object)ex);
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
            AuthPacket authPacket = (AuthPacket)obj;
            if (authPacket == null)
                throw new InvalidOperationException("Comparing different types of records.");
            int num1 = this.Type == authPacket.Type ? 0 : (this.Type < authPacket.Type ? -1 : 1);
            if (num1 != 0)
                return num1;
            int num2 = this.Scheme.CompareTo(authPacket.Scheme);
            if (num2 != 0)
                return num2;
            int num3 = this.Auth.CompareTo(authPacket.Auth);
            if (num3 != 0)
                return num3;
            return num3;
        }

        public override bool Equals(object obj)
        {
            AuthPacket authPacket = (AuthPacket)obj;
            if (authPacket == null)
                return false;
            if (object.ReferenceEquals((object)authPacket, (object)this))
                return true;
            bool flag1 = this.Type == authPacket.Type;
            if (!flag1)
                return flag1;
            bool flag2 = this.Scheme.Equals(authPacket.Scheme);
            if (!flag2)
                return flag2;
            bool flag3 = this.Auth.Equals((object)authPacket.Auth);
            if (!flag3)
                return flag3;
            return flag3;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + this.Type) + this.Scheme.GetHashCode()) + this.Auth.GetHashCode();
        }

        public static string Signature()
        {
            return "LAuthPacket(isB)";
        }
    }
}
