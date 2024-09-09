

namespace Infarstuructre.BL
{
    public interface IITask
    {
        List<TBViewTask> GetAll();
        TBTask GetById(int IdTask);
        bool saveData(TBTask savee);
        bool UpdateData(TBTask updatss);
        bool deleteData(int IdTask);
        List<TBViewTask> GetAllv(int IdTask);
    }
    public class CLSTBTask: IITask
    {
        MasterDbcontext dbcontext;
        public CLSTBTask(MasterDbcontext dbcontext1)
        {
            dbcontext= dbcontext1;
        }

        public List<TBViewTask> GetAll()
        {
            List<TBViewTask> MySlider = dbcontext.ViewTask.OrderByDescending(n => n.IdTask).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBTask GetById(int IdTask)
        {
            TBTask sslid = dbcontext.TBTasks.FirstOrDefault(a => a.IdTask == IdTask);
            return sslid;
        }
        public bool saveData(TBTask savee)
        {
            try
            {
                dbcontext.Add<TBTask>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBTask updatss)
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
        public bool deleteData(int IdTask)
        {
            try
            {
                var catr = GetById(IdTask);
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
        public List<TBViewTask> GetAllv(int IdTask)
        {
            List<TBViewTask> MySlider = dbcontext.ViewTask.OrderByDescending(n => n.IdTask == IdTask).Where(a => a.IdTask == IdTask).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
    }
}
