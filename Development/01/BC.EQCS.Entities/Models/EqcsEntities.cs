using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using BC.EQCS.Entities.Models.Mapping;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BC.EQCS.Entities.Models
{
    public class EqcsEntities 
        :  IdentityDbContext<ApplicationUser, EqcsEntities.CustomIdentityRole, int, EqcsEntities.CustomIdentityUserLogin, EqcsEntities.CustomIdentityUserRole, EqcsEntities.CustomIdentityUserClaim>
    {
        static EqcsEntities()
        {
            Database.SetInitializer<EqcsEntities>(null);
        }

        public EqcsEntities() : base("Name=EqcsEntities")
        {
        }

        public DbSet<TestCentre> TestCentres { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<TestLocation> TestLocations { get; set; }
        public DbSet<ApplicationAsset> ApplicationAssets { get; set; }
        public DbSet<AdminUnit> AdminUnits { get; set; }
        public DbSet<AdminUnitType> AdminUnitTypes { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
      //  public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserToRoleToAdminUnit> UserToRoleToAdminUnits { get; set; }
        public DbSet<IncidentAction> IncidentActions { get; set; }
        public DbSet<IncidentActionListing> IncidentActionListings { get; set; }
        public DbSet<IncidentActivityLog> IncidentActivityLogs { get; set; }
        public DbSet<IncidentActivityListingView> IncidentActivityListingView { get; set; }
        public DbSet<IncidentsListingView> IncidentsListingView { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrganisationType> OrganisationTypes { get; set; }
        public DbSet<RiskRating> RiskRatings { get; set; }
        public DbSet<ResidualRiskRating> ResidualRiskRatings { get; set; }
        public DbSet<IncidentClass> IncidentClasses { get; set; }
        public DbSet<IncidentClassType> IncidentClassTypes { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<IncidentView> IncidentViews { get; set; }
        public DbSet<IncidentCandidate> IncidentCandidates { get; set; }
        public DbSet<IncidentCandidateView> IncidentCandidateViews { get; set; }
        public DbSet<NotificationMapping> NotificationMappings { get; set; }
        public DbSet<NotificationEvent> NotificationEvents { get; set; }
        public DbSet<NotificationMessageTemplate> NotificationMessageTemplates { get; set; }
        public DbSet<NotificationMessage> NotificationMessages { get; set; }
        public DbSet<DocumentStorage> Documents { get; set; }
        public DbSet<UkviImmediateReportType> UkviImmediateReportTypes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new IncidentClassTypeMap());
            modelBuilder.Configurations.Add(new IncidentClassMap());
            modelBuilder.Configurations.Add(new TestCentreMap());
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new OrganisationMap());
            modelBuilder.Configurations.Add(new TestLocationMap());
            modelBuilder.Configurations.Add(new ApplicationAssetMap());
            modelBuilder.Configurations.Add(new AdminUnitMap());
            modelBuilder.Configurations.Add(new AdminUnitTypeMap());
            modelBuilder.Configurations.Add(new ApplicationRoleMap());
            modelBuilder.Configurations.Add(new ApplicationUserMap());
            modelBuilder.Configurations.Add(new UserToRoleToAdminUnitMap());
            modelBuilder.Configurations.Add(new IncidentActionMap());
            modelBuilder.Configurations.Add(new IncidentActionListingMap());
            modelBuilder.Configurations.Add(new IncidentActivityLogMap());
            modelBuilder.Configurations.Add(new IncidentMap());
            modelBuilder.Configurations.Add(new IncidentsListingViewMap());
            modelBuilder.Configurations.Add(new IncidentActivityListingViewMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new OrganisationTypeMap());
            modelBuilder.Configurations.Add(new RiskRatingMap());
            modelBuilder.Configurations.Add(new ResidualRiskRatingMap());
            modelBuilder.Configurations.Add(new IncidentViewMap());
            modelBuilder.Configurations.Add(new UserRoleToClassToSchemaKeyMap());
            modelBuilder.Configurations.Add(new IncidentCandidateMap());
            modelBuilder.Configurations.Add(new IncidentCandidateViewMap());
            modelBuilder.Configurations.Add(new NotificationMappingMap());
            modelBuilder.Configurations.Add(new NotificationEventMap());
            modelBuilder.Configurations.Add(new NotificationMessageTemplateMap());
            modelBuilder.Configurations.Add(new NotificationMessageMap());
            modelBuilder.Configurations.Add(new DocumentStorageMap());
            modelBuilder.Configurations.Add(new UkviImmediateReportTypeMap());

            modelBuilder.Entity<Incident>().Property(t => t.RowVersion).IsRowVersion();

            modelBuilder.Entity<CustomIdentityUserRole>()
              .HasKey(r => new { r.UserId, r.RoleId })
              .ToTable("ExternalUserRoles");

            modelBuilder.Entity<CustomIdentityUserLogin>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
                .ToTable("ExternalUserLogins");

            modelBuilder.Entity<CustomIdentityUserClaim>()
                .ToTable("ExternalUserClaims");
            

            var role = modelBuilder.Entity<CustomIdentityRole>()
                .ToTable("ExternalRoles");
            role.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("RoleNameIndex") { IsUnique = true }));
            role.HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);

        }


        public class CustomIdentityRole : IdentityRole<int, CustomIdentityUserRole>
        {
        }

        public class CustomIdentityUserRole : IdentityUserRole<int>
        {
            
        }

        public class CustomIdentityUserLogin : IdentityUserLogin<int>
        {
        }

        public class CustomIdentityUserClaim : IdentityUserClaim<int>
        {

        }

        public class CustomUserStore : UserStore<ApplicationUser, CustomIdentityRole, int, CustomIdentityUserLogin, CustomIdentityUserRole, CustomIdentityUserClaim>
        {
            public CustomUserStore(EqcsEntities context)
                : base(context)
            {
                
            }
        } 
    }
}