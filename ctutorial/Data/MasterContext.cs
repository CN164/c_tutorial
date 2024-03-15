using ctutorial.Data;
using Microsoft.EntityFrameworkCore;

namespace ctutorial.Data
{
    public class MasterContext : CtutorialContext<MasterContext>    
    {
        public MasterContext(DbContextOptions<MasterContext> options) : base(options) { }
    }
}
