using System;
using System.Collections.Generic;
using System.Text;

namespace WhatEgineerSqlite.Entity
{
    public interface IHistory
    {
        int Id { get; set; }
        string Hash { get; set; }
        string Context { get; set; }
        DateTime CreateDate { get; set; }
    }
}
