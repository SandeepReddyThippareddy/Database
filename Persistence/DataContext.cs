using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventAttendee> EventAttendees { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        //public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<UserFollowing> UserFollowings { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<EventAttendee>(x => x.HasKey(aa => new { aa.AppUserId, aa.EventId }));

            builder.Entity<EventAttendee>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Events)
                .HasForeignKey(aa => aa.AppUserId);

            builder.Entity<EventAttendee>()
                .HasOne(u => u.Event)
                .WithMany(u => u.Attendees)
                .HasForeignKey(aa => aa.EventId);

            builder.Entity<Comment>()
                .HasOne(a => a.Event)
                .WithMany(c => c.Comments)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<Feedback>()
            //    .HasOne(a => a.Event)
            //    .WithMany(c => c.Feedbacks)
            //    .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserFollowing>(b =>
            {
                b.HasKey(k => new { k.ObserverId, k.TargetId });

                b.HasOne(o => o.Observer)
                    .WithMany(f => f.Followings)
                    .HasForeignKey(o => o.ObserverId)
                    .OnDelete(DeleteBehavior.Cascade);
                b.HasOne(t => t.Target)
                    .WithMany(f => f.Followers)
                    .HasForeignKey(t => t.TargetId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}