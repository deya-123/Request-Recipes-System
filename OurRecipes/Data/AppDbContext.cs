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

    public virtual DbSet<Chief> Chiefs { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<IngredientName> IngredientNames { get; set; }

    public virtual DbSet<IngredientUnit> IngredientUnits { get; set; }

    public virtual DbSet<IngredientsName> IngredientsNames { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeCategory> RecipeCategories { get; set; }

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

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("COUNTRIES_PK");

            entity.ToTable("COUNTRIES");

            entity.Property(e => e.CountryId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COUNTRY_ID");
            entity.Property(e => e.CountryName)
                .HasMaxLength(20)
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
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("RECIPE_CARD_IMG_PATH");
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
        });

        modelBuilder.Entity<RecipeCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("RECIPE_CATEGORIES_PK");

            entity.ToTable("RECIPE_CATEGORIES");

            entity.Property(e => e.CategoryId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CATEGORY_ID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("CATEGORY_NAME");
            entity.Property(e => e.CategoryType)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("CATEGORY_TYPE");
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
            entity.Property(e => e.TestimonialText)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("TESTIMONIAL_TEXT");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");
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
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
