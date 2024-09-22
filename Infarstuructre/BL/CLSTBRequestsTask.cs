﻿

namespace Infarstuructre.BL
{
    public interface IIRequestsTask
    {

        List<TBViewRequestsTask> GetAll();
        TBRequestsTask GetById(int IdRequestsTask);
        bool saveData(TBRequestsTask savee);
        bool UpdateData(TBRequestsTask updatss);
        bool deleteData(int IdRequestsTask);
        List<TBViewRequestsTask> GetAllv(int IdRequestsTask);
        bool DELETPHOTO(int IdRequestsTask);
        bool DELETPHOTOWethError(string PhotoNAme);
        public TBViewRequestsTask GetByIdview(int IdRequestsTask);

    }
    public class CLSTBRequestsTask : IIRequestsTask
    {

        MasterDbcontext dbcontext;
        public CLSTBRequestsTask(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public List<TBViewRequestsTask> GetAll()
        {
            List<TBViewRequestsTask> MySlider = dbcontext.ViewRequestsTask.OrderByDescending(n => n.IdRequestsTask).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBRequestsTask GetById(int IdRequestsTask)
        {
            TBRequestsTask sslid = dbcontext.TBRequestsTasks.FirstOrDefault(a => a.IdRequestsTask == IdRequestsTask);
            return sslid;
        }
        public bool saveData(TBRequestsTask savee)
        {
            try
            {
                dbcontext.Add<TBRequestsTask>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBRequestsTask updatss)
        {
            try
            {
                dbcontext.Entry(updatss).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool deleteData(int IdRequestsTask)
        {
            try
            {
                var catr = GetById(IdRequestsTask);
                catr.CurrentState = false;
                //TbSubCateegoory dele = dbcontex.TbSubCateegoorys.Where(a => a.IdBrand == IdBrand).FirstOrDefault();
                //dbcontex.TbSubCateegoorys.Remove(dele);
                dbcontext.Entry(catr).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<TBViewRequestsTask> GetAllv(int IdRequestsTask)
        {
            List<TBViewRequestsTask> MySlider = dbcontext.ViewRequestsTask.OrderByDescending(n => n.IdRequestsTask == IdRequestsTask).Where(a => a.IdRequestsTask == IdRequestsTask).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public bool DELETPHOTO(int IdRequestsTask)
        {
            try
            {
                var catr = GetById(IdRequestsTask);
                //using (FileStream fs = new FileStream(catr.Photo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                //{
                if (!string.IsNullOrEmpty(catr.Photo))
                {
                    // إذا كان هناك صورة قديمة، قم بمسحها من الملف
                    var oldFilePath = Path.Combine(@"wwwroot/Images/Home", catr.Photo);
                    if (System.IO.File.Exists(oldFilePath))
                    {


                        // استخدم FileShare.None للسماح بحذف الملف أثناء استخدامه
                        using (FileStream fs = new FileStream(oldFilePath, FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            System.Threading.Thread.Sleep(200);
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                        }

                        System.IO.File.Delete(oldFilePath);
                    }
                }
                //}
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool DELETPHOTOWethError(string PhotoNAme)
        {
            try
            {
                if (!string.IsNullOrEmpty(PhotoNAme))
                {
                    // إذا كان هناك صورة قديمة، قم بمسحها من الملف
                    var oldFilePath = Path.Combine(@"wwwroot/Images/Home", PhotoNAme);
                    if (System.IO.File.Exists(oldFilePath))
                    {


                        // استخدم FileShare.None للسماح بحذف الملف أثناء استخدامه
                        using (FileStream fs = new FileStream(oldFilePath, FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            System.Threading.Thread.Sleep(200);
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                        }

                        System.IO.File.Delete(oldFilePath);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                // يفضل ألا تترك البرنامج يتجاوز الأخطاء بصمت، يفضل تسجيل الخطأ أو إعادة رميه
                return false;
            }
        }

        public TBViewRequestsTask GetByIdview(int IdRequestsTask)
        {
            TBViewRequestsTask sslid = dbcontext.ViewRequestsTask.FirstOrDefault(a => a.IdRequestsTask == IdRequestsTask);
            return sslid;
        }


    }
}
