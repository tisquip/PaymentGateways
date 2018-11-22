﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SASA.PayPalExpress.SampleClient.RazorP.Models;
using System;

namespace SASA.PayPalExpress.SampleClient.RazorP.Migrations
{
    [DbContext(typeof(SASAPayPalExpressSampleClientRazorPContext))]
    [Migration("20181101225219_AddModel_PayPalBearearsToken")]
    partial class AddModel_PayPalBearearsToken
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SASA.PayPalExpress.PayPalBearersToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTimeObtained");

                    b.Property<string>("Token");

                    b.HasKey("Id");

                    b.ToTable("PayPalBearersToken");
                });
#pragma warning restore 612, 618
        }
    }
}
