using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AluminiRepository.Factories
{

    public interface IDbConnectionFactory
    {
        db_Alumni_TestEntities CreateConnection();
    }
    public class AluminiDatabaseContextFactory : IDbConnectionFactory
    {

        private readonly string connectionString;

        public AluminiDatabaseContextFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public db_Alumni_TestEntities CreateConnection()
        {
            return new db_Alumni_TestEntities();
        }
    }
}
