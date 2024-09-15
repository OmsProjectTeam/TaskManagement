

using Microsoft.EntityFrameworkCore;

namespace Infarstuructre.BL
{
    public interface IITypesOfTask
    {
        List<TBTypesOfTask> GetAll();
        TBTypesOfTask GetById(int IdTypesOfTask);
        bool saveData(TBTypesOfTask savee);
        bool UpdateData(TBTypesOfTask updatss);
        bool deleteData(int IdTypesOfTask);
        List<TBTypesOfTask> GetAllv(int IdTypesOfTask);
        // //////////////////////////////////API///////////////////////////////
        Task<List<TBTypesOfTask>> GetAllAsync();
        Task<TBTypesOfTask> GetByIdAsync(int id);
        Task<bool> AddDataAsync(TBTypesOfTask sslid);
        Task<bool> UpdateDataAsync(TBTypesOfTask sslid);
        Task<bool> DeleteDataAsync(int id);
    }
    public class CLSTBTypesOfTask: IITypesOfTask
    {
        MasterDbcontext dbcontext;
        public CLSTBTypesOfTask(MasterDbcontext dbcontext1)
        {
            dbcontext= dbcontext1;
        }
        public List<TBTypesOfTask> GetAll()
        {
            List<TBTypesOfTask> MySlider = dbcontext.TBTypesOfTasks.OrderByDescending(n => n.IdTypesOfTask).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBTypesOfTask GetById(int IdTypesOfTask)
        {
            TBTypesOfTask sslid = dbcontext.TBTypesOfTasks.FirstOrDefault(a => a.IdTypesOfTask == IdTypesOfTask);
            return sslid;
        }
        public bool saveData(TBTypesOfTask savee)
        {
            try
            {
                dbcontext.Add<TBTypesOfTask>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBTypesOfTask updatss)
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
        public bool deleteData(int IdTypesOfTask)
        {
            try
            {
                var catr = GetById(IdTypesOfTask);
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
        public List<TBTypesOfTask> GetAllv(int IdTypesOfTask)
        {
            List<TBTypesOfTask> MySlider = dbcontext.TBTypesOfTasks.OrderByDescending(n => n.IdTypesOfTask == IdTypesOfTask).Where(a => a.IdTypesOfTask == IdTypesOfTask).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        // ///////////////////////////////////////////////////APIs///////////////////////////////////////////////////////////


        public async Task<List<TBTypesOfTask>> GetAllAsync()
        {
            List<TBTypesOfTask> MySlider = await dbcontext.TBTypesOfTasks.OrderByDescending(n => n.IdTypesOfTask).Where(a => a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBTypesOfTask> GetByIdAsync(int id)
        {
            TBTypesOfTask sslid = await dbcontext.TBTypesOfTasks.FirstOrDefaultAsync(a => a.IdTypesOfTask == id);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBTypesOfTask sslid)
        {
            try
            {
                await dbcontext.AddAsync<TBTypesOfTask>(sslid);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateDataAsync(TBTypesOfTask sslid)
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
