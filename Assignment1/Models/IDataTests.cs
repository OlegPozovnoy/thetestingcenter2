using System.Linq;

namespace Assignment1.Models
{
    public class IDataTests : IMockTests
    {
        // db connection
        private DbModel db = new DbModel();

        public IQueryable<Test> Categories { get { return db.Tests; } }

        public void Delete(Test test)
        {
            db.Tests.Remove(test);
            db.SaveChanges();
        }

        public Test Save(Test test)
        {
            if (test.Id == 0)
            {
                db.Tests.Add(test);
            }
            else
            {
                db.Entry(test).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();
            return test;
        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}