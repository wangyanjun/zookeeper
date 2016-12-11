/*
 *  Licensed to the Apache Software Foundation (ASF) under one or more
 *  contributor license agreements.  See the NOTICE file distributed with
 *  this work for additional information regarding copyright ownership.
 *  The ASF licenses this file to You under the Apache License, Version 2.0
 *  (the "License"); you may not use this file except in compliance with
 *  the License.  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 *
 */
﻿using System.Text;
namespace ZooKeeperNet
{
    public sealed class Quotas
    {
        private Quotas() { }
        /** the zookeeper nodes that acts as the management and status node **/
        public const string procZookeeper = "/zookeeper";

        /** the zookeeper quota node that acts as the quota
         * management node for zookeeper */
        public const string quotaZookeeper = "/zookeeper/quota";

        /**
         * the limit node that has the limit of
         * a subtree
         */
        public const string limitNode = "zookeeper_limits";

        /**
         * the stat node that monitors the limit of
         * a subtree.
         */
        public const string statNode = "zookeeper_stats";

        /**
         * return the quota path associated with this
         * prefix
         * @param path the actual path in zookeeper.
         * @return the limit quota path
         */
        public static string QuotaPath(string path)
        {
            StringBuilder builder = new StringBuilder(quotaZookeeper)
            .Append(path)
            .Append(PathUtils.PathSeparator)
            .Append(limitNode);
            return builder.ToString();
        }

        /**
         * return the stat quota path associated with this
         * prefix.
         * @param path the actual path in zookeeper
         * @return the stat quota path
         */
        public static string StatPath(string path)
        {
            StringBuilder builder = new StringBuilder(quotaZookeeper)
            .Append(path)
            .Append(PathUtils.PathSeparator)
            .Append(statNode);
            return builder.ToString();
        }
    }
}
