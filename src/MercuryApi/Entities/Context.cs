using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercuryApi.Entities
{
    public class MyContext : DbContext {

        public MyContext() : base() {
        }

        public MyContext(DbContextOptions<MyContext> options) : base(options) {
        }
    }
}
