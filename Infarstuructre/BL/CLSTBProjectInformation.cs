

using Domin.Entity;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infarstuructre.BL
{
	public interface IIProjectInformation
	{
		List<TBViewProjectInformation> GetAll();
		TBProjectInformation GetById(int IdProjectInformation);
		bool saveData(TBProjectInformation savee);
		bool UpdateData(TBProjectInformation updatss);
		bool deleteData(int IdProjectInformation);
		List<TBViewProjectInformation> GetAllv(int IdProjectInformation);

		// //////////////////////////////////API///////////////////////////////
		Task<List<TBViewProjectInformation>> GetAllAsync();
		Task<TBProjectInformation> GetByIdAsync(int id);
		Task<bool> AddDataAsync(TBProjectInformation sslid);
		Task<bool> UpdateDataAsync(TBProjectInformation sslid);
		Task<bool> DeleteDataAsync(int id);

    }

	public class CLSTBProjectInformation: IIProjectInformation
	{
		MasterDbcontext dbcontext;
		public CLSTBProjectInformation(MasterDbcontext dbcontext1)
        {
			dbcontext= dbcontext1;

		}
		public List<TBViewProjectInformation> GetAll()
		{
			List<TBViewProjectInformation> MySlider = dbcontext.ViewProjectInformation.OrderByDescending(n => n.IdProjectInformation).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
		public TBProjectInformation GetById(int IdProjectInformation)
		{
			TBProjectInformation sslid = dbcontext.TBProjectInformations.FirstOrDefault(a => a.IdProjectInformation == IdProjectInformation);
			return sslid;
		}
		public bool saveData(TBProjectInformation savee)
		{
			try
			{
				dbcontext.Add<TBProjectInformation>(savee);
				dbcontext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool UpdateData(TBProjectInformation updatss)
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
		public bool deleteData(int IdProjectInformation)
		{
			try
			{
				var catr = GetById(IdProjectInformation);
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
		public List<TBViewProjectInformation> GetAllv(int IdProjectInformation)
		{
			List<TBViewProjectInformation> MySlider = dbcontext.ViewProjectInformation.OrderByDescending(n => n.IdProjectInformation == IdProjectInformation).Where(a => a.IdProjectInformation == IdProjectInformation).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
	// ///////////////////////////////////////////////////APIs///////////////////////////////////////////////////////////
		public async Task<List<TBViewProjectInformation>> GetAllAsync()
		{
            List<TBViewProjectInformation> MySlider = await dbcontext.ViewProjectInformation.OrderByDescending(n => n.IdProjectInformation).Where(a => a.CurrentState == true).ToListAsync();
            return MySlider;
        }
        public async Task<TBProjectInformation> GetByIdAsync(int id)
        {
            TBProjectInformation sslid = await dbcontext.TBProjectInformations.FirstOrDefaultAsync(a => a.IdProjectInformation == id);
            return sslid;
        }
		public async Task<bool> AddDataAsync(TBProjectInformation sslid)
		{
            try
            {
                await dbcontext.AddAsync<TBProjectInformation>(sslid);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
		public async Task<bool> UpdateDataAsync(TBProjectInformation sslid)
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
