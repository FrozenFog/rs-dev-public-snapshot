﻿using RelertSharp.Common;
using RelertSharp.MapStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.MapStructure
{
    public class MapReadingMonitor
    {
        private List<MonitorLog> logs = new List<MonitorLog>();
        public void EnableMonitor()
        {
            logs.Clear();
            IsMonitoring = true;
        }
        public void LogFatal(string id, string name, MapObjectType objectType, Exception e)
        {
            if (IsMonitoring)
            {
                MonitorLog log = new MonitorLog()
                {
                    Data = new ComboItem(id, name),
                    ObjectType = objectType,
                    Exception = e
                };
                logs.Add(log);
            }
        }
        public void LogFatal(string id, string name, LogicType logic, Exception e)
        {
            if (IsMonitoring)
            {
                MonitorLog log = new MonitorLog()
                {
                    Data = new ComboItem(id, name),
                    LogicType = logic,
                    Exception = e
                };
                logs.Add(log);
            }
        }
        public void LogFatal(I2dLocateable obj, MapObjectType type, Exception e)
        {
            if (IsMonitoring)
            {
                MonitorLog log = new MonitorLog()
                {
                    Data = obj,
                    ObjectType = type,
                    Exception = e
                };
                logs.Add(log);
            }
        }
        public void EndMonitorLog()
        {
            IsMonitoring = false;
        }
        public IEnumerable<IMonitorLog> GetLogs { get { return logs; } }







        public bool IsMonitoring { get; private set; }
        public bool HasLog { get { return logs.Count > 0; } }

        public interface IMonitorLog
        {
            object Data { get; set; }
            string Message { get; set; }
            string Type { get; }
            string Id { get; set; }
            string Name { get; set; }
            Exception Exception { get; }
        }

        private class MonitorLog : IMonitorLog
        {
            public object Data { get; set; }
            public MapObjectType ObjectType { get; set; }
            public LogicType LogicType { get; set; }
            public string Message { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }
            public Exception Exception { get; set; }
            public string Type
            {
                get
                {
                    if (ObjectType == MapObjectType.Undefined) return LogicType.ToString();
                    else return ObjectType.ToString();
                }
            }
        }

    }
}
