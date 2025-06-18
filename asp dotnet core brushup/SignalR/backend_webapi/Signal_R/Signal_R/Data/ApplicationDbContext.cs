using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Signal_R.Models;
using Signal_R.Models.AttachmentModel;
using Signal_R.Models.Contact;
using Signal_R.Models.Converstaion;
using Signal_R.Models.MessageModel;
using Signal_R.Models.NotificationModel;
using System.Net.Mail;

namespace Signal_R.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
            
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Models.AttachmentModel.Attachment> Attachments { get; set; }
        public DbSet<AttachmentType> AttachmentTypes { get; set; }
        public DbSet<ContactBlock> ContactBlocks { get; set; }
        public DbSet<ContactRelationship> ContactRelationships { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }
        public DbSet<Conversation> Conversations  { get; set; }
        public DbSet<ConversationMember> ConversationMembers { get; set; }
        public DbSet<MessageAttachment> MessageAttachments { get; set; }
        public DbSet<MessageEditHistory> MessageEditHistories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationLog> NotificationLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ContactBlock>()
                .HasOne(cb => cb.Blocker)
                .WithMany(u => u.BlocksInitiated)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<ContactBlock>()
               .HasOne(cb => cb.Blocked)
               .WithMany(u => u.BlocksReceived)
               .HasForeignKey(cb => cb.BlockedId)
               .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<ContactRelationship>()
                .HasOne(cr => cr.User)
                .WithMany(u => u.Relationships)
                .HasForeignKey(cr => cr.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ContactRequest>()
                .HasOne(cr => cr.Requester)
                .WithMany(u => u.RequestsSent)
                .HasForeignKey(cr => cr.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ContactRequest>()
                .HasOne(cr => cr.Requestee)
                .WithMany(u => u.RequestsReceived)
                .HasForeignKey(cr => cr.RequesteeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Conversation>()
                .HasOne(c => c.Creator)
                .WithMany(u => u.CreatedConversations)
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ConversationMember>()
                .HasOne(cm => cm.User)
                .WithMany(u => u.ConversationMemberships)
                .HasForeignKey(cm => cm.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MessageEditHistory>()
                .HasOne(meh => meh.EditedByUser)
                .WithMany(u => u.MessageEdits)
                .HasForeignKey(meh => meh.EditedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
