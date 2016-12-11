// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Proto.WatcherEvent
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
    public class WatcherEvent : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(WatcherEvent));

        public int Type { get; set; }

        public int State { get; set; }

        public string Path { get; set; }

        public WatcherEvent()
        {
        }

        public WatcherEvent(int type, int state, string path)
        {
            this.Type = type;
            this.State = state;
            this.Path = path;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.WriteInt(this.Type, "type");
            a_.WriteInt(this.State, "state");
            a_.WriteString(this.Path, "path");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.Type = a_.ReadInt("type");
            this.State = a_.ReadInt("state");
            this.Path = a_.ReadString("path");
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
                    binaryOutputArchive.WriteInt(this.State, "state");
                    binaryOutputArchive.WriteString(this.Path, "path");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                WatcherEvent.log.Error((object)ex);
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
            WatcherEvent watcherEvent = (WatcherEvent)obj;
            if (watcherEvent == null)
                throw new InvalidOperationException("Comparing different types of records.");
            int num1 = this.Type == watcherEvent.Type ? 0 : (this.Type < watcherEvent.Type ? -1 : 1);
            if (num1 != 0)
                return num1;
            int num2 = this.State == watcherEvent.State ? 0 : (this.State < watcherEvent.State ? -1 : 1);
            if (num2 != 0)
                return num2;
            int num3 = this.Path.CompareTo(watcherEvent.Path);
            if (num3 != 0)
                return num3;
            return num3;
        }

        public override bool Equals(object obj)
        {
            WatcherEvent watcherEvent = (WatcherEvent)obj;
            if (watcherEvent == null)
                return false;
            if (object.ReferenceEquals((object)watcherEvent, (object)this))
                return true;
            bool flag1 = this.Type == watcherEvent.Type;
            if (!flag1)
                return flag1;
            bool flag2 = this.State == watcherEvent.State;
            if (!flag2)
                return flag2;
            bool flag3 = this.Path.Equals(watcherEvent.Path);
            if (!flag3)
                return flag3;
            return flag3;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * (37 * (37 * 17 + this.GetType().GetHashCode()) + this.Type) + this.State) + this.Path.GetHashCode();
        }

        public static string Signature()
        {
            return "LWatcherEvent(iis)";
        }
    }
}
