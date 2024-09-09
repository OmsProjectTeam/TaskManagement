

using Microsoft.EntityFrameworkCore;

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
        // ///////////////////////////API//////////////////////////////////////
        Task<List<TBViewTask>> GetAllAsync();
        Task<TBTask> GetByIdAsync(int id);
        Task<bool> AddDataAsync(TBTask sslid);
        Task<bool> UpdateDataAsync(TBTask sslid);
        Task<bool> DeleteDataAsync(int id);

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


        // ///////////////////////////////////////////////////APIs///////////////////////////////////////////////////////////


        public async Task<List<TBViewTask>> GetAllAsync()
        {
            List<TBViewTask> MySlider = await dbcontext.ViewTask.OrderByDescending(n => n.IdTask).Where(a => a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBTask> GetByIdAsync(int id)
        {
            TBTask sslid = await dbcontext.TBTasks.FirstOrDefaultAsync(a => a.IdTask == id);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBTask sslid)
        {
            try
            {
                await dbcontext.AddAsync<TBTask>(sslid);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateDataAsync(TBTask sslid)
        {
            try
            {
                dbcontext.Entry(sslid).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteDataAsync(int id)
        {
            try
            {
                var catr = await GetByIdAsync(id);
                catr.CurrentState = false;
                //TbSubCateegoory dele = dbcontex.TbSubCateegoorys.Where(a => a.IdBrand == IdBrand).FirstOrDefault();
                //dbcontex.TbSubCateegoorys.Remove(dele);
                dbcontext.Entry(catr).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
