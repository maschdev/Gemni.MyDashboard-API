using MyDashboard.Infra.Context;

namespace MyDashboard.Infra.Transactions
{
    public class Uow : IUow
    {
        private readonly MyDashboardDataContext _context;

        public Uow(MyDashboardDataContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
        }
    }
}
