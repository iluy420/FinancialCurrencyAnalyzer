using Consts;
using DataBase.Core.Models;
using System.Data.Entity;

namespace DataBase.Contexts
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
            : base(ConstsDataBase.CONNECTION_STRING)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Сurrency> Сurrencys { get; set; }

        public DbSet<Metal> Metals { get; set; }

        //public DbSet<SubscriptionForecastPriceCurrency> SubscriptionForecastPriceCurrencys { get; set; }

        public DbSet<UserCurrency> UserCurrencys { get; set; }

        public DbSet<UserMetals> UserMetals { get; set; }
    }
}