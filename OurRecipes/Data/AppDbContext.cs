using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OurRecipes.Models;

namespace OurRecipes.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<About> Abouts { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Chief> Chiefs { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<ContactInfo> ContactInfos { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Home> Homes { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<IngredientName> IngredientNames { get; set; }

    public virtual DbSet<IngredientUnit> IngredientUnits { get; set; }

    public virtual DbSet<IngredientsName> IngredientsNames { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeCategory> RecipeCategories { get; set; }

    public virtual DbSet<RecipeCategoryType> RecipeCategoryTypes { get; set; }

    public virtual DbSet<RecipeNote> RecipeNotes { get; set; }

    public virtual DbSet<RecipePreparationStep> RecipePreparationSteps { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Testimonial> Testimonials { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("USER ID=C##deaa;PASSWORD=123456;DATA SOURCE=localhost:1521/xe");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##DEAA")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<About>(entity =>
        {
            entity.HasKey(e => e.AboutId).HasName("ABOUT_PK");

            entity.ToTable("ABOUT");

            entity.Property(e => e.AboutId)
                .HasColumnType("NUMBER")
                .HasColumnName("ABOUT_ID");
            entity.Property(e => e.AboutBody)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("ABOUT_BODY");
            entity.Property(e => e.AboutImage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ABOUT_IMAGE");
            entity.Property(e => e.AboutTitle)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ABOUT_TITLE");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("CARDS_PK");

            entity.ToTable("CARDS");

            entity.Property(e => e.CardId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("CARD_ID");
            entity.Property(e => e.CardCvv)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("CARD_CVV");
            entity.Property(e => e.CardExpireDate)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CARD_EXPIRE_DATE");
            entity.Property(e => e.CardHolderName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CARD_HOLDER_NAME");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CARD_NUMBER");
            entity.Property(e => e.CardValue)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("CARD_VALUE");
        });

        modelBuilder.Entity<Chief>(entity =>
        {
            entity.HasKey(e => e.ChiefId).HasName("CHIEF_PK");

            entity.ToTable("CHIEFS");

            entity.Property(e => e.ChiefId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CHIEF_ID");
            entity.Property(e => e.ChiefExperiencePathFile)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CHIEF_EXPERIENCE_PATH_FILE");
            entity.Property(e => e.ChiefStatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CHIEF_STATUS");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP ")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasColumnName("DELETED_AT");
            entity.Property(e => e.ModifiedAt)
                .HasPrecision(6)
                .HasColumnName("MODIFIED_AT");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Chiefs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CHIEFS_FK1");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("CONTACT_US_PK");

            entity.ToTable("CONTACTS");

            entity.Property(e => e.ContactId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("CONTACT_ID");
            entity.Property(e => e.ContactMessage)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("CONTACT_MESSAGE");
            entity.Property(e => e.ContactSenderEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CONTACT_SENDER_EMAIL");
            entity.Property(e => e.ContactSenderName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CONTACT_SENDER_NAME");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasColumnName("DELETED_AT");
            entity.Property(e => e.ModifiedAt)
                .HasPrecision(6)
                .HasColumnName("MODIFIED_AT");
        });

        modelBuilder.Entity<ContactInfo>(entity =>
        {
            entity.HasKey(e => e.ContactInfoId).HasName("CONTACT_INFO_PK");

            entity.ToTable("CONTACT_INFO");

            entity.Property(e => e.ContactInfoId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("CONTACT_INFO_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.LocationOnMap)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("LOCATION_ON_MAP");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONE");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("COUNTRIES_PK");

            entity.ToTable("COUNTRIES");

            entity.Property(e => e.CountryId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COUNTRY_ID");
            entity.Property(e => e.CountryName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("COUNTRY_NAME");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasColumnName("DELETED_AT");
            entity.Property(e => e.ModifiedAt)
                .HasPrecision(6)
                .HasColumnName("MODIFIED_AT");
        });

        modelBuilder.Entity<Home>(entity =>
        {
            entity.HasKey(e => e.HomeId).HasName("HOME_PK");

            entity.ToTable("HOME");

            entity.Property(e => e.HomeId)
                .HasColumnType("NUMBER")
                .HasColumnName("HOME_ID");
            entity.Property(e => e.FacbookLink)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("FACBOOK_LINK");
            entity.Property(e => e.HomeDesc)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("HOME_DESC");
            entity.Property(e => e.HomeImage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("HOME_IMAGE");
            entity.Property(e => e.HomeLogo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("HOME_LOGO");
            entity.Property(e => e.HomeTitle)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("HOME_TITLE");
            entity.Property(e => e.HomeWebsiteName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HOME_WEBSITE_NAME");
            entity.Property(e => e.InsLink)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("INS_LINK");
            entity.Property(e => e.WorkingDays)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("WORKING_DAYS");
            entity.Property(e => e.YoutubeLink)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("YOUTUBE_LINK");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("INGREDIENTS_PK");

            entity.ToTable("INGREDIENTS");

            entity.Property(e => e.IngredientId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("INGREDIENT_ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasColumnName("DELETED_AT");
            entity.Property(e => e.IngredientCustomName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("INGREDIENT_CUSTOM_NAME");
            entity.Property(e => e.IngredientQuantity)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("INGREDIENT_QUANTITY");
            entity.Property(e => e.IngredientUnitId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("INGREDIENT_UNIT_ID");
            entity.Property(e => e.ModifiedAt)
                .HasPrecision(6)
                .HasColumnName("MODIFIED_AT");
            entity.Property(e => e.RecipeId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RECIPE_ID");

            entity.HasOne(d => d.IngredientUnit).WithMany(p => p.Ingredients)
                .HasForeignKey(d => d.IngredientUnitId)
                .HasConstraintName("INGREDIENTS_FK2");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Ingredients)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("INGREDIENTS_FK1");
        });

        modelBuilder.Entity<IngredientName>(entity =>
        {
            entity.HasKey(e => e.IngredientNameId).HasName("INGREDIENT_NAMES_PK");

            entity.ToTable("INGREDIENT_NAMES");

            entity.Property(e => e.IngredientNameId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("INGREDIENT_NAME_ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasColumnName("DELETED_AT");
            entity.Property(e => e.IngredientName1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("INGREDIENT_NAME");
            entity.Property(e => e.IngredientType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("INGREDIENT_TYPE");
            entity.Property(e => e.ModifiedAt)
                .HasPrecision(6)
                .HasColumnName("MODIFIED_AT");
        });

        modelBuilder.Entity<IngredientUnit>(entity =>
        {
            entity.HasKey(e => e.IngredientUnitId).HasName("INGREDIENT_UNITS_PK");

            entity.ToTable("INGREDIENT_UNITS");

            entity.Property(e => e.IngredientUnitId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("INGREDIENT_UNIT_ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasColumnName("DELETED_AT");
            entity.Property(e => e.IngredientUnitName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("INGREDIENT_UNIT_NAME");
            entity.Property(e => e.IngredientUnitType)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("INGREDIENT_UNIT_TYPE");
            entity.Property(e => e.ModifiedAt)
                .HasPrecision(6)
                .HasColumnName("MODIFIED_AT");
        });

        modelBuilder.Entity<IngredientsName>(entity =>
        {
            entity.HasKey(e => e.IngredientsNamesId).HasName("INGREDIENTS_NAMES_PK");

            entity.ToTable("INGREDIENTS_NAMES");

            entity.Property(e => e.IngredientsNamesId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("INGREDIENTS_NAMES_ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasColumnName("DELETED_AT");
            entity.Property(e => e.IngredientId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("INGREDIENT_ID");
            entity.Property(e => e.IngredientNameId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("INGREDIENT_NAME_ID");
            entity.Property(e => e.ModifiedAt)
                .HasPrecision(6)
                .HasColumnName("MODIFIED_AT");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("INVOICE_PK");

            entity.ToTable("INVOICES");

            entity.Property(e => e.InvoiceId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("INVOICE_ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasColumnName("DELETED_AT");
            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("INVOICE_NUMBER");
            entity.Property(e => e.InvoicePaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("INVOICE_PAYMENT_STATUS");
            entity.Property(e => e.InvoiceTotalAmount)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("INVOICE_TOTAL_AMOUNT");
            entity.Property(e => e.ModifiedAt)
                .HasPrecision(6)
                .HasColumnName("MODIFIED_AT");
            entity.Property(e => e.OrderId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ORDER_ID");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("ORDERS_PK");

            entity.ToTable("ORDERS");

            entity.Property(e => e.OrderId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ORDER_ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasColumnName("DELETED_AT");
            entity.Property(e => e.ModifiedAt)
                .HasPrecision(6)
                .HasColumnName("MODIFIED_AT");
            entity.Property(e => e.OrderPrice)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("ORDER_PRICE");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ORDER_STATUS");
            entity.Property(e => e.RecipeId)
                .HasColumnType("NUMBER")
                .HasColumnName("RECIPE_ID");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Orders)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ORDERS_FK2");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("ORDERS_FK1");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("RECIPE_PK");

            entity.ToTable("RECIPES");

            entity.Property(e => e.RecipeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RECIPE_ID");
            entity.Property(e => e.ChiefId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CHIEF_ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasColumnName("DELETED_AT");
            entity.Property(e => e.ModifiedAt)
                .HasPrecision(6)
                .HasColumnName("MODIFIED_AT");
            entity.Property(e => e.RecipeCardImgPath)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RECIPE_CARD_IMG_PATH");
            entity.Property(e => e.RecipeCategoryId)
                .HasColumnType("NUMBER")
                .HasColumnName("RECIPE_CATEGORY_ID");
            entity.Property(e => e.RecipeCookingTimeMinutes)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RECIPE_COOKING_TIME_MINUTES");
            entity.Property(e => e.RecipeDescription)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("RECIPE_DESCRIPTION");
            entity.Property(e => e.RecipeExplanation)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("RECIPE_EXPLANATION");
            entity.Property(e => e.RecipeMainImgPath)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RECIPE_MAIN_IMG_PATH");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RECIPE_NAME");
            entity.Property(e => e.RecipePreparingTimeMinutes)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RECIPE_PREPARING_TIME_MINUTES");
            entity.Property(e => e.RecipePrice)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("RECIPE_PRICE");
            entity.Property(e => e.RecipeServings)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RECIPE_SERVINGS");
            entity.Property(e => e.RecipeStatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("RECIPE_STATUS");
            entity.Property(e => e.RecipeVideoPath)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RECIPE_VIDEO_PATH");

            entity.HasOne(d => d.Chief).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.ChiefId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("RECIPES_FK2");

            entity.HasOne(d => d.RecipeCategory).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.RecipeCategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("RECIPES_FK1");
        });

        modelBuilder.Entity<RecipeCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("RECIPE_CATEGORIES_PK");

            entity.ToTable("RECIPE_CATEGORIES");

            entity.Property(e => e.CategoryId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CATEGORY_ID");
            entity.Property(e => e.CategoryImage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CATEGORY_IMAGE");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("CATEGORY_NAME");
            entity.Property(e => e.CategoryTypeId)
                .HasColumnType("NUMBER")
                .HasColumnName("CATEGORY_TYPE_ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasColumnName("DELETED_AT");
            entity.Property(e => e.ModifiedAt)
                .HasPrecision(6)
                .HasColumnName("MODIFIED_AT");

            entity.HasOne(d => d.CategoryType).WithMany(p => p.RecipeCategories)
                .HasForeignKey(d => d.CategoryTypeId)
                .HasConstraintName("RECIPE_CATEGORIES_FK1");
        });

        modelBuilder.Entity<RecipeCategoryType>(entity =>
        {
            entity.HasKey(e => e.RecipeCategoryTypeId).HasName("RECIPE_CATEGORY_TYPES_PK");

            entity.ToTable("RECIPE_CATEGORY_TYPES");

            entity.Property(e => e.RecipeCategoryTypeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("RECIPE_CATEGORY_TYPE_ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP ")
                .HasColumnType("DATE")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.RecipeCategoryTypeName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RECIPE_CATEGORY_TYPE_NAME");
        });

        modelBuilder.Entity<RecipeNote>(entity =>
        {
            entity.HasKey(e => e.RecipeNoteId).HasName("RECIPE_NOTES_PK");

            entity.ToTable("RECIPE_NOTES");

            entity.Property(e => e.RecipeNoteId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RECIPE_NOTE_ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasColumnName("DELETED_AT");
            entity.Property(e => e.ModifiedAt)
                .HasPrecision(6)
                .HasColumnName("MODIFIED_AT");
            entity.Property(e => e.RecipeId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RECIPE_ID");
            entity.Property(e => e.RecipeNoteDescription)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("RECIPE_NOTE_DESCRIPTION");
            entity.Property(e => e.RecipeNoteTitle)
                .HasMaxLength(160)
                .IsUnicode(false)
                .HasColumnName("RECIPE_NOTE_TITLE");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeNotes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("RECIPE_NOTES_FK1");
        });

        modelBuilder.Entity<RecipePreparationStep>(entity =>
        {
            entity.HasKey(e => e.RecipePreparationStepId).HasName("RECIPE_PREPARATION_STEPS_PK");

            entity.ToTable("RECIPE_PREPARATION_STEPS");

            entity.Property(e => e.RecipePreparationStepId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RECIPE_PREPARATION_STEP_ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasColumnName("DELETED_AT");
            entity.Property(e => e.ModifiedAt)
                .HasPrecision(6)
                .HasColumnName("MODIFIED_AT");
            entity.Property(e => e.RecipeId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RECIPE_ID");
            entity.Property(e => e.RecipePreparationStepDescription)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("RECIPE_PREPARATION_STEP_DESCRIPTION");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipePreparationSteps)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("RECIPE_PREPARATION_STEPS_FK1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("ROLE_PK");

            entity.ToTable("ROLES");

            entity.Property(e => e.RoleId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasColumnName("DELETED_AT");
            entity.Property(e => e.ModifiedAt)
                .HasPrecision(6)
                .HasColumnName("MODIFIED_AT");
            entity.Property(e => e.RoleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ROLE_NAME");
        });

        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.HasKey(e => e.TestimonialId).HasName("TESTIMONIAL_PK");

            entity.ToTable("TESTIMONIALS");

            entity.Property(e => e.TestimonialId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("TESTIMONIAL_ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasColumnName("DELETED_AT");
            entity.Property(e => e.ModifiedAt)
                .HasPrecision(6)
                .HasColumnName("MODIFIED_AT");
            entity.Property(e => e.TestimonialStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TESTIMONIAL_STATUS");
            entity.Property(e => e.TestimonialText)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("TESTIMONIAL_TEXT");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Testimonials)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TESTIMONIALS_FK1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("USERS_PK");

            entity.ToTable("USERS");

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.DeletedAt)
                .HasPrecision(6)
                .HasColumnName("DELETED_AT");
            entity.Property(e => e.EmailVerificationToken)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL_VERIFICATION_TOKEN");
            entity.Property(e => e.EmailVerificationTokenExpireDate)
                .HasColumnType("DATE")
                .HasColumnName("EMAIL_VERIFICATION_TOKEN_EXPIRE_DATE");
            entity.Property(e => e.IsEmailVerification)
                .HasPrecision(1)
                .HasColumnName("IS_EMAIL_VERIFICATION");
            entity.Property(e => e.ModifiedAt)
                .HasPrecision(6)
                .HasColumnName("MODIFIED_AT");
            entity.Property(e => e.PasswordVerificationToken)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PASSWORD_VERIFICATION_TOKEN");
            entity.Property(e => e.PasswordVerificationTokenExpireDate)
                .HasColumnType("DATE")
                .HasColumnName("PASSWORD_VERIFICATION_TOKEN_EXPIRE_DATE");
            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.UserCountryId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_COUNTRY_ID");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USER_EMAIL");
            entity.Property(e => e.UserGender)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("USER_GENDER");
            entity.Property(e => e.UserImage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USER_IMAGE");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USER_NAME");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USER_PASSWORD");
            entity.Property(e => e.UserPhone)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USER_PHONE");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("USERS_FK1");

            entity.HasOne(d => d.UserCountry).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserCountryId)
                .HasConstraintName("USERS_FK2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
