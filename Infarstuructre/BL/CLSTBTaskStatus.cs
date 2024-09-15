


using Microsoft.EntityFrameworkCore;

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
        // ///////////////////////////API//////////////////////////////////////
        Task<List<TBTaskStatus>> GetAllAsync();
        Task<TBTaskStatus> GetByIdAsync(int id);
        Task<bool> AddDataAsync(TBTaskStatus sslid);
        Task<bool> UpdateDataAsync(TBTaskStatus sslid);
        Task<bool> DeleteDataAsync(int id);
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

        // ///////////////////////////////////////////////////APIs///////////////////////////////////////////////////////////


        public async Task<List<TBTaskStatus>> GetAllAsync()
        {
            List<TBTaskStatus> MySlider = await dbcontext.TBTaskStatuss.OrderByDescending(n => n.IdTaskStatus).Where(a => a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBTaskStatus> GetByIdAsync(int id)
        {
            TBTaskStatus sslid = await dbcontext.TBTaskStatuss.FirstOrDefaultAsync(a => a.IdTaskStatus == id);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBTaskStatus sslid)
        {
            try
            {
                await dbcontext.AddAsync<TBTaskStatus>(sslid);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateDataAsync(TBTaskStatus sslid)
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
