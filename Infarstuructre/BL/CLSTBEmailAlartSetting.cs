

using Microsoft.EntityFrameworkCore;

namespace Infarstuructre.BL
{
	public interface IIEmailAlartSetting
	{
		List<TBEmailAlartSetting> GetAll();
		TBEmailAlartSetting GetById(int IdEmailAlartSetting);
		bool saveData(TBEmailAlartSetting savee);
		bool UpdateData(TBEmailAlartSetting updatss);
		bool deleteData(int IdEmailAlartSetting);
		List<TBEmailAlartSetting> GetAllv(int IdEmailAlartSetting);
        //////////////////////////APIs/////////////////////////////////////////////////////////////////
        Task<List<TBEmailAlartSetting>> GetAllAsync();
        Task<List<TBEmailAlartSetting>> GetAllvAsync(int IdCustomerMessages);
        Task<List<TBEmailAlartSetting>> GetAllDataentryAsync(string dataEntry);
        Task<TBEmailAlartSetting> GetByIdAsync(int IdCustomerMessages);
        Task<bool> AddDataAsync(TBEmailAlartSetting savee);
        Task<bool> DeleteDataAsync(int TBEmailAlartSetting);
        Task<bool> UpdateDataAsync(TBEmailAlartSetting update);
    }
	public class CLSTBEmailAlartSetting: IIEmailAlartSetting
	{
		MasterDbcontext dbcontext;
		public CLSTBEmailAlartSetting(MasterDbcontext dbcontext1)
        {
			dbcontext=dbcontext1;

		}
		public List<TBEmailAlartSetting> GetAll()
		{
			List<TBEmailAlartSetting> MySlider = dbcontext.TBEmailAlartSettings.OrderByDescending(n => n.IdEmailAlartSetting).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
		public TBEmailAlartSetting GetById(int IdEmailAlartSetting)
		{
			TBEmailAlartSetting sslid = dbcontext.TBEmailAlartSettings.FirstOrDefault(a => a.IdEmailAlartSetting == IdEmailAlartSetting);
			return sslid;
		}
		public bool saveData(TBEmailAlartSetting savee)
		{
			try
			{
				dbcontext.Add<TBEmailAlartSetting>(savee);
				dbcontext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool UpdateData(TBEmailAlartSetting updatss)
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
		public bool deleteData(int IdEmailAlartSetting)
		{
			try
			{
				var catr = GetById(IdEmailAlartSetting);
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
		public List<TBEmailAlartSetting> GetAllv(int IdEmailAlartSetting)
		{
			List<TBEmailAlartSetting> MySlider = dbcontext.TBEmailAlartSettings.OrderByDescending(n => n.IdEmailAlartSetting == IdEmailAlartSetting).Where(a => a.IdEmailAlartSetting == IdEmailAlartSetting).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}

        // //////////////////////////APIs/////////////////////////////////////////////////////////////////

        public async Task<List<TBEmailAlartSetting>> GetAllAsync()
        {
            var myDatd = await dbcontext.TBEmailAlartSettings.OrderByDescending(n => n.IdEmailAlartSetting).Where(a => a.CurrentState == true).ToListAsync();
            return myDatd;
        }

        public async Task<List<TBEmailAlartSetting>> GetAllvAsync(int IdEmailAlartSetting)
        {
            var myDatd = await dbcontext.TBEmailAlartSettings.OrderByDescending(n => n.IdEmailAlartSetting).Where(a => a.IdEmailAlartSetting == IdEmailAlartSetting).Where(a => a.CurrentState == true).ToListAsync();
            return myDatd;
        }

        public async Task<List<TBEmailAlartSetting>> GetAllDataentryAsync(string dataEntry)
        {
            var MySlider = await dbcontext.TBEmailAlartSettings.Where(a => a.DataEntry == dataEntry && a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBEmailAlartSetting> GetByIdAsync(int IdEmailAlartSetting)
        {
            var sslid = await dbcontext.TBEmailAlartSettings.FirstOrDefaultAsync(a => a.IdEmailAlartSetting == IdEmailAlartSetting);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBEmailAlartSetting savee)
        {
            try
            {
                await dbcontext.AddAsync<TBEmailAlartSetting>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> DeleteDataAsync(int IdEmailAlartSetting)
        {
            try
            {
                var email = await GetByIdAsync(IdEmailAlartSetting);
                email.CurrentState = false;
                dbcontext.Entry(email).State = EntityState.Modified;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> UpdateDataAsync(TBEmailAlartSetting update)
        {
            try
            {
                dbcontext.Entry(update).State = EntityState.Modified;
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
