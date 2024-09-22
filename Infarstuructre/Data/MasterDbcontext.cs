using Domin.Entity;

using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace Infarstuructre.Data
{
	public class MasterDbcontext : IdentityDbContext<ApplicationUser>
	{
		public MasterDbcontext(DbContextOptions<MasterDbcontext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

            //***********************************************************


			builder.Entity<VwUser>(entity =>
			{
				entity.HasNoKey();
				entity.ToView("VwUsers");
			});


            //*********************************************************  
            //***********************************************************


			builder.Entity<TBViewProjectInformation>(entity =>
			{
				entity.HasNoKey();
				entity.ToView("ViewProjectInformation");
			});	
            builder.Entity<TBViewTask>(entity =>
			{
				entity.HasNoKey();
				entity.ToView("ViewTask");
			});


            //*********************************************************
            builder.Entity<TBViewRequestsTask>(entity =>
			{
				entity.HasNoKey();
				entity.ToView("ViewRequestsTask");
			});


            //*********************************************************
            //---------------------------------
            builder.Entity<TBProjectType>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBProjectType>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBProjectType>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");
            //--------------------------------- 
            //---------------------------------
            builder.Entity<TBTypesOfTask>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBTypesOfTask>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBTypesOfTask>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");
            //--------------------------------- 
            //---------------------------------
            builder.Entity<TBTaskStatus>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBTaskStatus>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBTaskStatus>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");
            //--------------------------------- 
            //---------------------------------
            builder.Entity<TBProjectInformation>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBProjectInformation>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
    
            //---------------------------------  
            //---------------------------------
            builder.Entity<TBTask>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBTask>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
    
            //---------------------------------   
            //---------------------------------
            builder.Entity<TBEmailAlartSetting>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBEmailAlartSetting>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");  
            builder.Entity<TBEmailAlartSetting>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");
    
            //---------------------------------   
            //---------------------------------
            builder.Entity<TBRequestsTask>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBRequestsTask>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");  
  
    
            //---------------------------------
        }
        //***********************************
        public DbSet<VwUser> VwUsers { get; set; }
        public DbSet<TBProjectType> TBProjectTypes { get; set; }
        public DbSet<TBTypesOfTask> TBTypesOfTasks { get; set; } 
        public DbSet<TBTaskStatus> TBTaskStatuss { get; set; } 
        public DbSet<TBProjectInformation> TBProjectInformations { get; set; } 
        public DbSet<TBViewProjectInformation> ViewProjectInformation { get; set; } 
        public DbSet<TBTask> TBTasks { get; set; } 
        public DbSet<TBViewTask> ViewTask { get; set; } 
        public DbSet<TBEmailAlartSetting> TBEmailAlartSettings { get; set; } 
        public DbSet<TBRequestsTask> TBRequestsTasks { get; set; } 
        public DbSet<TBViewRequestsTask> ViewRequestsTask { get; set; } 
    }
}
