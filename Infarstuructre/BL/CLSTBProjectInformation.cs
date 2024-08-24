

namespace Infarstuructre.BL
{
	public interface IIProjectInformation
	{
		List<TBProjectInformation> GetAll();
		TBProjectInformation GetById(int IdProjectInformation);
		bool saveData(TBProjectInformation savee);
		bool UpdateData(TBProjectInformation updatss);
		bool deleteData(int IdProjectInformation);
		List<TBProjectInformation> GetAllv(int IdProjectInformation);
	}

	public class CLSTBProjectInformation: IIProjectInformation
	{
		MasterDbcontext dbcontext;
		public CLSTBProjectInformation(MasterDbcontext dbcontext1)
        {
			dbcontext= dbcontext1;

		}
		public List<TBProjectInformation> GetAll()
		{
			List<TBProjectInformation> MySlider = dbcontext.TBProjectInformations.OrderByDescending(n => n.IdProjectInformation).Where(a => a.CurrentState == true).ToList();
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
		public List<TBProjectInformation> GetAllv(int IdProjectInformation)
		{
			List<TBProjectInformation> MySlider = dbcontext.TBProjectInformations.OrderByDescending(n => n.IdProjectInformation == IdProjectInformation).Where(a => a.IdProjectInformation == IdProjectInformation).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
	
	}
}
