using CosmoPlat.DAL.Command;
using SQLite.CodeFirst.NetCore.Console;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Text;
using WhatEgineerSqlite.Entity.ShopInfo;
using WhatEngineerBll.StartDB;
using System.Linq;

namespace WhatEngineerBll.Manager
{
    public interface IManagerBll
    {
        public void AddPictureInfo(List<PictureResourceInfo> picObj);
    }
    public class ManagerBll : IManagerBll
    {
        private EFTools<LoginDbContext> _efDbtool;
        public ManagerBll()
        {
            _efDbtool = EFTools<LoginDbContext>.GetInstance(StaticConnection.LoginDBConn);
        }

        public void AddPictureInfo(List<PictureResourceInfo> picList)
        {
            if (picList.Count == 1)
            {
                _efDbtool.Add(picList.FirstOrDefault());

            }
            else
            {
                _efDbtool.AddRange<PictureResourceInfo>(picList);

            }
        }

        public void DeletePictureInfo()
        {
            
        }
        
        public List<PictureResourceInfo> GetAllPictureInfo()
        {
            return _efDbtool.GetAll<PictureResourceInfo>();
        }

    }
}
