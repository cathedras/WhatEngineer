using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using WhatEngineerBll.StartDB;

namespace CosmoPlat.DAL.Command
{
    /// <summary>
     /// Entity FrameWork帮助类（提供各种对数据的操作方法）
     /// </summary>
    /// <typeparam name="CosmoEntities"></typeparam>
    /// 
    public class EFTools<T> where T : DbContext
    {

        public EFTools(string connetionString)
        {
            ConnetionString = connetionString;
        }
        private static string ConnetionString
        {
            get;
            set;
        }

        private DbContext GetDbContext()
        {
            return (T)Activator.CreateInstance(typeof(T), ConnetionString); ;
        }

        public static EFTools<T> GetInstance(string connetionString = null)
        {       
            EFTools<T> _instance = null;
            if (!string.IsNullOrEmpty(connetionString))
            {
                ConnetionString = connetionString;
            }
            if (_instance == null)
            {
                _instance = new EFTools<T>(ConnetionString);
            }
            return _instance;
        }


        public bool InitDbContext()
        {
            try
            {
                if (!string.IsNullOrEmpty(ConnetionString))
                {
                    using (DbContext db = GetDbContext())
                    {
                        db.Database.Initialize(true);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception err)
            {

                return false;
            }
        }
        public bool InitSqlDbContext()
        {
            try
            {
                if (!string.IsNullOrEmpty(ConnetionString))
                {
                    using T context = (T)Activator.CreateInstance(typeof(T), ConnetionString);
                    context.Database.Initialize(true);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        /// <summary>
        /// 获取所有的实体  zhaolin
        /// </summary>
        /// <typeparam name="T"> 泛型实体类型  在调用前必须制定 且只能为引用类型</typeparam>
        /// <returns>结果集</returns>
        public List<T> GetAll<T>() where T : class
        {
            try
            {
                using (DbContext db = GetDbContext())
                {
                    var tmpLst = db.Set<T>().AsNoTracking().ToList();
                    return tmpLst;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<T>();
            }
        }

        /// <summary>
        /// 获取单个实体(条件的最后一条)
        /// </summary>
        /// <typeparam name="T">泛型实体类型  在调用前必须制定 且只能为引用类型</typeparam>
        /// <param name="whereProc">过滤的表达式</param>
        /// <returns>实体</returns>
        public T GetSingleLast<T, Tkey>(Expression<Func<T, bool>> whereProc, Expression<Func<T, Tkey>> orderProc, string desc = "desc") where T : class
        {
            try
            {
                using (DbContext db = GetDbContext())
                {
                    if (desc == "desc")
                    {
                        return db.Set<T>().AsNoTracking().Where(whereProc).OrderByDescending(orderProc).FirstOrDefault();
                    }
                    return db.Set<T>().AsNoTracking().Where(whereProc).OrderBy(orderProc).FirstOrDefault();
                    // return ef.Set<T>().FirstOrDefault(whereProc);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        /// <summary>
        /// 获取所有的实体可排序
        /// </summary>
        /// <typeparam name="T">泛型实体类型  在调用前必须制定 且只能为引用类型</typeparam>
        /// <param name="whereProc"> 过滤的表达式</param>
        /// <returns>结果集</returns>
        public List<T> GetAllByWhere<T>(Expression<Func<T, bool>> whereProc) where T : class
        {
            try
            {
                using (DbContext db = GetDbContext())
                {

                    return db.Set<T>().AsNoTracking().Where(whereProc).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<T>();
            }

        }





        /// <summary>
        /// 获取某一个实体类型的所有数据并排序
         /// </summary>
        /// <typeparam name="TEntity">待查询的实体的类型</typeparam>
        /// <typeparam name="TKey">排序的条件的属性的数值类型</typeparam>
        /// <param name="OrderByProc">排序的lambda表达式</param>
        /// <param name="IsDesc">false 顺序 true 逆序</param>
        /// <returns>失败或者没有数据 返回 空集合 否则返回  查询的到结果集</returns>
        public List<TEntity> GetAllAndOrderBy<TEntity, TKey>(Expression<Func<TEntity, TKey>> OrderByProc, bool IsDesc) where TEntity : class
        {
            try
            {
                using (DbContext db = GetDbContext())
                {

                    if (IsDesc)
                    {
                        return db.Set<TEntity>().AsNoTracking().OrderByDescending(OrderByProc).ToList();
                    }
                    return db.Set<TEntity>().AsNoTracking().OrderBy(OrderByProc).ToList();
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return new List<TEntity>();
            }
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T">泛型实体类型  在调用前必须制定 且只能为引用类型</typeparam>
        /// <param name="whereProc">过滤的表达式</param>
        /// <returns>实体</returns>
        public T GetSingle<T>(Expression<Func<T, bool>> whereProc) where T : class
        {
            try
            {

                using (DbContext db = GetDbContext())
                {

                    return db.Set<T>().AsNoTracking().Where(whereProc).FirstOrDefault();

                    // return ef.Set<T>().FirstOrDefault(whereProc);

                }


            }
            catch (Exception)
            {

                return null;
            }


        }



        /// <summary>
        /// 获取分页的实体
        /// </summary>
        /// <typeparam name="T">泛型实体类型  在调用前必须制定 且只能为引用类型</typeparam>
        /// <typeparam name="TKey">成员属性的类型</typeparam>
        /// <param name="pageIndex">当前的页码</param>
        /// <param name="pageCount">每页显示的行数</param>
        /// <param name="orderProc">排序的表达式</param>
        /// <param name="orderByType">排序的类型</param>
        /// <returns></returns>
        public List<T> GetPageByOrder<T, TKey>(int pageIndex, int pageCount, Expression<Func<T, TKey>> orderProc, string orderByType) where T : class
        {
            try
            {
                using (DbContext db = GetDbContext())
                {

                    if (orderByType == "desc")
                    {
                        return db.Set<T>().AsNoTracking().OrderByDescending(orderProc).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
                    }

                    return db.Set<T>().AsNoTracking().OrderBy(orderProc).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();


                }


            }
            catch (Exception)
            {

                return new List<T>();
            }

        }


        /// <summary>
        /// 获取分页的实体
        /// </summary>
        /// <typeparam name="T">泛型实体类型  在调用前必须制定 且只能为引用类型</typeparam>
        /// <typeparam name="TKey">成员属性的类型</typeparam>
        /// <param name="pageIndex">当前的页码</param>
        /// <param name="pageCount">每页显示的行数</param>
        /// <param name="whereProc">过滤的表达式</param>
        /// <param name="orderProc">排序的表达式</param>
        /// <param name="orderByType">排序的类型</param>
        /// <returns></returns>
        public List<T> GetPageByOrderAndWhere<T, TKey>(int pageIndex, int pageCount, Expression<Func<T, bool>> whereProc, Expression<Func<T, TKey>> orderProc, string orderByType = null) where T : class
        {
            try
            {
                using (DbContext db = GetDbContext())
                {
                    if (orderByType == "desc")
                    {
                        return db.Set<T>().AsNoTracking().OrderByDescending(orderProc).Where(whereProc).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
                    }
                    return db.Set<T>().AsNoTracking().OrderBy(orderProc).Where(whereProc).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<T>();
            }

        }





        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex">当前是第几页</param>
        /// <param name="pageCount">每一页的行数</param>
        /// <returns></returns>
        public List<T> GetPageByWhere<T>(int pageIndex, int pageCount, Expression<Func<T, bool>> whereExp) where T : class
        {

            try
            {
                using (DbContext db = GetDbContext())
                {
                    if (whereExp == null)
                    {
                        return db.Set<T>().AsNoTracking().Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
                    }
                    else
                    {
                        return db.Set<T>().AsNoTracking().Where(whereExp).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
                    }
                }



            }
            catch (Exception)
            {

                return null;
            }
        }




        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereExp"></param>
        /// <returns></returns>
        public int GetAllCount<T>(Expression<Func<T, bool>> whereExp) where T : class
        {
            using (DbContext db = GetDbContext())
            {
                if (whereExp == null)
                {
                    return db.Set<T>().AsNoTracking().Count();
                }
                else
                {
                    return db.Set<T>().AsNoTracking().Where(whereExp).Count();
                }
            }



        }




        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="T">泛型实体类型  在调用前必须制定 且只能为引用类型</typeparam>
        /// <param name="whereProc">删除的条件</param>
        /// <returns>结果</returns>
        public bool DeleteByWhere<T>(Expression<Func<T, bool>> whereProc) where T : class
        {
            try
            {
                using (DbContext db = GetDbContext())
                {
                    List<T> list = db.Set<T>().Where(whereProc).ToList();

                    if (list == null || list.Count == 0)
                    {
                        return true;
                    }
                    list.ForEach(R =>
                    {
                        db.Set<T>().Remove(R);
                    });
                    return db.SaveChanges() > 0;
                }





            }
            catch (Exception)
            {

                return false;
            }


        }



        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <typeparam name="T">泛型实体类型  在调用前必须制定 且只能为引用类型</typeparam>
        /// <param name="t">待删除的实体</param>
        /// <returns>结果</returns>
        public bool DeleteEntity<T>(T t) where T : class
        {
            try
            {

                using (DbContext db = GetDbContext())
                {
                    db.Set<T>().Attach(t);
                    db.Set<T>().Remove(t);

                    //var  entity =    ef.Entry<T>(t);
                    //entity.State = System.Data.EntityState.Deleted;

                    return db.SaveChanges() > 0;

                }

            }
            catch (Exception)
            {

                return false;
            }


        }


        /// <summary>
                /// 删除多个
                /// </summary>
                /// <typeparam name="T">泛型实体类型  在调用前必须制定 且只能为引用类型</typeparam>
                /// <param name="tlist">待删除的集合</param>
                /// <returns>结果</returns>

        public bool DelEntity<T>(List<T> tlist) where T : class
        {
            try
            {
                using (DbContext db = GetDbContext())
                {
                    if (tlist.Count == 0)
                    {
                        return true;
                    }

                    foreach (var item in tlist)
                    {
                        db.Set<T>().Attach(item);

                        db.Set<T>().Remove(item);
                    }

                    return db.SaveChanges() > 0;

                }

            }


            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// 根据条件删除权限
        /// </summary>
        /// <typeparam name="T">泛型实体类型  在调用前必须制定 且只能为引用类型</typeparam>
        /// <param name="whereProc">删除的条件</param>
        /// <returns>结果</returns>
        public bool DeleteClass<T>(Expression<Func<T, bool>> whereProc) where T : class
        {
            try
            {
                using (DbContext db = GetDbContext())
                {
                    List<T> list = db.Set<T>().Where(whereProc).ToList();

                    if (list == null || list.Count == 0)
                    {
                        return true;
                    }

                    list.ForEach(R =>
                    {

                        db.Set<T>().Remove(R);

                    });
                    return db.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 修改
                /// </summary>
                /// <typeparam name="T"></typeparam>
                /// <param name="t"></param>
                /// <returns></returns>
        public bool UpdateEntity<T>(T t) where T : class
        {
            try
            {
                using (DbContext db = GetDbContext())
                {

                    if (db.Entry<T>(t).State == System.Data.Entity.EntityState.Detached)
                    {
                        db.Set<T>().Attach(t);
                        db.Entry<T>(t).State = System.Data.Entity.EntityState.Modified;
                    }
                    return db.SaveChanges() > 0;
                }

            }
            catch (Exception ex)
            {

                string error = ex.Message;
                return false;
            }

        }

        /// <summary>
        /// 添加实体单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Add<T>(T t) where T : class
        {

            try
            {
                using (DbContext db = GetDbContext())
                {


                    db.Set<T>().Add(t);

                    return db.SaveChanges() > 0;

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }


        }
        /// <summary>
                /// 添加一组数据
                /// </summary>
                /// <typeparam name="T"></typeparam>
                /// <param name="tlist"></param>
                /// <returns></returns>
        public bool AddRange<T>(List<T> tlist) where T : class
        {
            try
            {

                using (DbContext db = GetDbContext())
                {
                    if (tlist.Count == 0)
                    {
                        return true;
                    }

                    //foreach (var item in tlist)
                    //{
                    //    db.Set<T>().Add(item);
                    //}
                    db.Set<T>().AddRange(tlist);

                    return db.SaveChanges() > 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 根据sql查询数据 （可以使自定义实体）
        /// </summary>
        /// <typeparam name="TModel">实体类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <returns>失败或者没有数据 返回 空集合 否则返回  查询的到结果集</returns>
        public List<TModel> GetDataBySql<TModel>(string sql) where TModel : class
        {
            try
            {

                using (DbContext db = GetDbContext())
                {

                    return db.Database.SqlQuery<TModel>(sql).ToList();
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return new List<TModel>();
            }
        }





        /// <summary>
        /// 执行sql  增加   删除   修改
        /// </summary>
        /// <param name="sql">增加   删除   修改  sql</param>
        /// <returns>true  成功  false  失败</returns>
        public bool ExcuteSql(string sql)
        {
            try
            {
                using (DbContext db = GetDbContext())
                {
                    return db.Database.ExecuteSqlCommand(sql) > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        /// 获取分页数据（不需要条件）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TN">实体的成员属性的类型</typeparam>
        /// <param name="pageIndex">当前请求的页面</param>
        /// <param name="pageCount">每页的行数</param>
        /// <param name="orderByProc">排序的表达式</param>
        /// <returns></returns>

        //public List<T> GetPageEntity<T, TN>(int pageIndex, int pageCount, Expression<Func<T, TN>> orderByProc, Expression<Func<T, bool>> whereExp) where T : class
        //{
        //    try
        //    {

        //        using (CosmoEntities ef = new CosmoEntities())
        //        {
        //            if (whereExp == null)
        //            {
        //                return ef.Set<T>().OrderBy(orderByProc).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
        //            }

        //            return ef.Set<T>().OrderBy(orderByProc).Skip((pageIndex - 1) * pageCount).Take(pageCount).Where(whereExp).ToList();
        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return null;
        //    }
        //}
    }

}

