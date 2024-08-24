﻿

namespace Infarstuructre.BL
{
    public interface IIProjectType
    {
        List<TBProjectType> GetAll();
        TBProjectType GetById(int IdProjectType);
        bool saveData(TBProjectType savee);
        bool UpdateData(TBProjectType updatss);
        bool deleteData(int IdProjectType);
        List<TBProjectType> GetAllv(int IdProjectType);
    }
    public class CLSTBProjectType: IIProjectType
    {
        MasterDbcontext dbcontext;
        public CLSTBProjectType(MasterDbcontext dbcontext1)
        {
            dbcontext= dbcontext1;
        }
        public List<TBProjectType> GetAll()
        {
            List<TBProjectType> MySlider = dbcontext.TBProjectTypes.OrderByDescending(n => n.IdProjectType).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBProjectType GetById(int IdProjectType)
        {
            TBProjectType sslid = dbcontext.TBProjectTypes.FirstOrDefault(a => a.IdProjectType == IdProjectType);
            return sslid;
        }
        public bool saveData(TBProjectType savee)
        {
            try
            {
                dbcontext.Add<TBProjectType>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBProjectType updatss)
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
        public bool deleteData(int IdProjectType)
        {
            try
            {
                var catr = GetById(IdProjectType);
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
        public List<TBProjectType> GetAllv(int IdProjectType)
        {
            List<TBProjectType> MySlider = dbcontext.TBProjectTypes.OrderByDescending(n => n.IdProjectType == IdProjectType).Where(a => a.IdProjectType == IdProjectType).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
    }
}