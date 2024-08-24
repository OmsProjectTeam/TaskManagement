


namespace Infarstuructre.BL
{
    public  interface IITaskStatus
    {
        List<TBTaskStatus> GetAll();
        TBTaskStatus GetById(int IdTaskStatus);
        bool saveData(TBTaskStatus savee);
        bool UpdateData(TBTaskStatus updatss);
        bool deleteData(int IdTaskStatus);
        List<TBTaskStatus> GetAllv(int IdTaskStatus);
    }
    public class CLSTBTaskStatus: IITaskStatus
    {
        MasterDbcontext dbcontext;
        public CLSTBTaskStatus(MasterDbcontext dbcontext1)
        {
            dbcontext= dbcontext1;
        }
        public List<TBTaskStatus> GetAll()
        {
            List<TBTaskStatus> MySlider = dbcontext.TBTaskStatuss.OrderByDescending(n => n.IdTaskStatus).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBTaskStatus GetById(int IdTaskStatus)
        {
            TBTaskStatus sslid = dbcontext.TBTaskStatuss.FirstOrDefault(a => a.IdTaskStatus == IdTaskStatus);
            return sslid;
        }
        public bool saveData(TBTaskStatus savee)
        {
            try
            {
                dbcontext.Add<TBTaskStatus>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBTaskStatus updatss)
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
        public bool deleteData(int IdTaskStatus)
        {
            try
            {
                var catr = GetById(IdTaskStatus);
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
        public List<TBTaskStatus> GetAllv(int IdTaskStatus)
        {
            List<TBTaskStatus> MySlider = dbcontext.TBTaskStatuss.OrderByDescending(n => n.IdTaskStatus == IdTaskStatus).Where(a => a.IdTaskStatus == IdTaskStatus).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
    }
}
