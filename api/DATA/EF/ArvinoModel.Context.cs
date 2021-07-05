﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DATA.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ArvinoDbContext : DbContext
    {
        public ArvinoDbContext()
            : base("name=ArvinoDbContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<RV_Answers> RV_Answers { get; set; }
        public virtual DbSet<RV_AreaCategory> RV_AreaCategory { get; set; }
        public virtual DbSet<RV_ChatRoom> RV_ChatRoom { get; set; }
        public virtual DbSet<RV_Competition> RV_Competition { get; set; }
        public virtual DbSet<RV_Event> RV_Event { get; set; }
        public virtual DbSet<RV_EventCategory> RV_EventCategory { get; set; }
        public virtual DbSet<RV_Group> RV_Group { get; set; }
        public virtual DbSet<RV_GroupMessages> RV_GroupMessages { get; set; }
        public virtual DbSet<RV_GroupMessages1> RV_GroupMessages1 { get; set; }
        public virtual DbSet<RV_KeyWords> RV_KeyWords { get; set; }
        public virtual DbSet<RV_MessageInGroup> RV_MessageInGroup { get; set; }
        public virtual DbSet<RV_NoteFromSystemManager> RV_NoteFromSystemManager { get; set; }
        public virtual DbSet<RV_PrefrencesType> RV_PrefrencesType { get; set; }
        public virtual DbSet<RV_PurchasedEventsByUsers> RV_PurchasedEventsByUsers { get; set; }
        public virtual DbSet<RV_Question> RV_Question { get; set; }
        public virtual DbSet<RV_Service> RV_Service { get; set; }
        public virtual DbSet<RV_ServiceImage> RV_ServiceImage { get; set; }
        public virtual DbSet<RV_User> RV_User { get; set; }
        public virtual DbSet<RV_UserJoinChatRoom> RV_UserJoinChatRoom { get; set; }
        public virtual DbSet<RV_UserJoinGroup> RV_UserJoinGroup { get; set; }
        public virtual DbSet<RV_UserPrefrences> RV_UserPrefrences { get; set; }
        public virtual DbSet<RV_UserScore> RV_UserScore { get; set; }
        public virtual DbSet<RV_UserType> RV_UserType { get; set; }
        public virtual DbSet<RV_Wine> RV_Wine { get; set; }
        public virtual DbSet<RV_WineCategory> RV_WineCategory { get; set; }
        public virtual DbSet<RV_Winery> RV_Winery { get; set; }
        public virtual DbSet<RV_WineryImage> RV_WineryImage { get; set; }
        public virtual DbSet<RV_WineryManager> RV_WineryManager { get; set; }
        public virtual DbSet<RV_Rate> RV_Rate { get; set; }
        public virtual DbSet<RV_KNNCategory> RV_KNNCategory { get; set; }
        public virtual DbSet<RV_EntityTypes> RV_EntityTypes { get; set; }
        public virtual DbSet<RV_LikesUsers> RV_LikesUsers { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<@event> events { get; set; }
        public virtual DbSet<Event1> Events1 { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Folder> Folders { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Questionere1> Questionere1 { get; set; }
        public virtual DbSet<ShareCours> ShareCourses { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<ToDo> ToDoes { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<Winery> Wineries { get; set; }
        public virtual DbSet<Wine> Wines { get; set; }
        public virtual DbSet<File_in_Email> File_in_Email { get; set; }
        public virtual DbSet<RV_WineryCommand> RV_WineryCommand { get; set; }
        public virtual DbSet<RV_WineComment> RV_WineComment { get; set; }
    }
}
