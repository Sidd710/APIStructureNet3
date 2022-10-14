using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DotNetArchitecture3.Models
{
    public partial class ElectionCampaignContext : DbContext
    {
        public ElectionCampaignContext()
        {
        }

        public ElectionCampaignContext(DbContextOptions<ElectionCampaignContext> options)
            : base(options)
        {
        }

        public DbSet<UserMaster> UserDetails { get; set; }
    }
}
