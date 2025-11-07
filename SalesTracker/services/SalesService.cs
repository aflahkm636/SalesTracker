using SalesTracker.data;

namespace SalesTracker.services
{
    public class SalesService
    {
        private readonly AppdbContext _context;

        public SalesService(AppdbContext context)
        {
            _context = context;
        }

    }
}
