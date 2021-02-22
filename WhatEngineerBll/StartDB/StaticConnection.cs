using System;
using System.Collections.Generic;
using System.Text;

namespace WhatEngineerBll.StartDB
{
    public class StaticConnection
    {
        public static string LoginDBConn { get; set; } = "Data source=./db/LoginDB.db;Pooling=true;FailIfMissing=false;foreign keys=true";
    }
}
