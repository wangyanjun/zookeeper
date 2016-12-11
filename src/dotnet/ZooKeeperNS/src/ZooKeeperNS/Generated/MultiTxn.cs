// Decompiled with JetBrains decompiler
// Type: Org.Apache.Zookeeper.Txn.MultiTxn
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

namespace Org.Apache.Zookeeper.Txn
{
    public class MultiTxn : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(MultiTxn));

        public IEnumerable<Org.Apache.Zookeeper.Txn.Txn> Txns { get; set; }

        public MultiTxn()
        {
        }

        public MultiTxn(IEnumerable<Org.Apache.Zookeeper.Txn.Txn> txns)
        {
            this.Txns = txns;
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord((IRecord)this, tag);
            a_.StartVector<Org.Apache.Zookeeper.Txn.Txn>(this.Txns, "txns");
            if (this.Txns != null)
            {
                foreach (Org.Apache.Zookeeper.Txn.Txn txn in this.Txns)
                    a_.WriteRecord((IRecord)txn, "e1");
            }
            a_.EndVector<Org.Apache.Zookeeper.Txn.Txn>(this.Txns, "txns");
            a_.EndRecord((IRecord)this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            IIndex index = a_.StartVector("txns");
            if (index != null)
            {
                List<Org.Apache.Zookeeper.Txn.Txn> txnList = new List<Org.Apache.Zookeeper.Txn.Txn>();
                while (!index.Done())
                {
                    Org.Apache.Zookeeper.Txn.Txn txn = new Org.Apache.Zookeeper.Txn.Txn();
                    a_.ReadRecord((IRecord)txn, "e1");
                    txnList.Add(txn);
                    index.Incr();
                }
                this.Txns = (IEnumerable<Org.Apache.Zookeeper.Txn.Txn>)txnList;
            }
            a_.EndVector("txns");
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
                    binaryOutputArchive.StartVector<Org.Apache.Zookeeper.Txn.Txn>(this.Txns, "txns");
                    if (this.Txns != null)
                    {
                        foreach (Org.Apache.Zookeeper.Txn.Txn txn in this.Txns)
                            binaryOutputArchive.WriteRecord((IRecord)txn, "e1");
                    }
                    binaryOutputArchive.EndVector<Org.Apache.Zookeeper.Txn.Txn>(this.Txns, "txns");
                    binaryOutputArchive.EndRecord((IRecord)this, string.Empty);
                    memoryStream.Position = 0L;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                MultiTxn.log.Error((object)ex);
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
            throw new InvalidOperationException("comparing MultiTxn is unimplemented");
        }

        public override bool Equals(object obj)
        {
            MultiTxn multiTxn = (MultiTxn)obj;
            if (multiTxn == null)
                return false;
            if (object.ReferenceEquals((object)multiTxn, (object)this))
                return true;
            bool flag = this.Txns.Equals((object)multiTxn.Txns);
            if (!flag)
                return flag;
            return flag;
        }

        public override int GetHashCode()
        {
            return 37 * (37 * 17 + this.GetType().GetHashCode()) + this.Txns.GetHashCode();
        }

        public static string Signature()
        {
            return "LMultiTxn([LTxn(iB)])";
        }
    }
}
